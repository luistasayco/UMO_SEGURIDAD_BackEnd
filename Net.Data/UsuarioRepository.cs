using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Net.Business.Entities;
using Net.Connection;
using Net.CrossCotting;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Text.RegularExpressions;

namespace Net.Data
{
    public class UsuarioRepository : RepositoryBase<BE_Usuario>, IUsuarioRepository
    {
        private readonly string _cnx;
        private string _aplicacionName;
        private string _metodoName;
        private readonly Regex regex = new Regex(@"<(\w+)>.*");

        const string DB_ESQUEMA = "";
        const string SP_GET = DB_ESQUEMA + "SEG_GetUsuarioAll";
        const string SP_GET_ID = DB_ESQUEMA + "SEG_GetUsuarioPorId";
        const string SP_GET_USUARIO = DB_ESQUEMA + "SEG_GetUsuarioPorUsuario";
        const string SP_UPDATE_AUTOGENERADA = DB_ESQUEMA + "SEG_SetUsuarioUpdatePassword";
        const string SP_UPDATE_PASSWORD = DB_ESQUEMA + "SEG_SetUsuarioUpdatePassword";
        private readonly ParametrosTokenConfig tokenConfig;

        public UsuarioRepository(IConnectionSQL context, IOptions<ParametrosTokenConfig> tokenConfig)
            : base(context)
        {
            this.tokenConfig = tokenConfig.Value;
            _aplicacionName = this.GetType().Name;
        }

        public Task<IEnumerable<BE_Usuario>> GetAll(BE_Usuario entidad)
        {
            return Task.Run(() => FindAll(entidad, SP_GET));
        }
        public Task<BE_Usuario> GetById(BE_Usuario entidad)
        {
            return Task.Run(() => FindById(entidad, SP_GET_ID));
        }
        public BE_Usuario VerificarLogin(BE_Usuario entidad)
        {
            return FindById(entidad, SP_GET_USUARIO);
        }

        public async Task<BE_ResultadoTransaccion<BE_UsuarioAutenticar>> Autenticar(BE_UsuarioAutenticar entidad)
        {
            var claveDesEncriptada = EncriptaHelper.DecryptStringAES(entidad.Clave);

            //BE_ResultadoTransaccion<BE_UsuarioAutenticar> resultadoTransaccion = await AutenticarUsuarioDirectorioActivo(entidad.Usuario.ToUpper(), claveDesEncriptada);
            BE_ResultadoTransaccion<BE_UsuarioAutenticar> resultadoTransaccion = new BE_ResultadoTransaccion<BE_UsuarioAutenticar>();

            if (resultadoTransaccion.ResultadoCodigo == -1)
            {
                return resultadoTransaccion;
            }

            BE_Usuario user = VerificarLogin(new BE_Usuario { Usuario = entidad.Usuario.ToUpper() });

            if (user == null)
            {
                resultadoTransaccion.ResultadoCodigo = -1;
                resultadoTransaccion.ResultadoDescripcion = "Usuario existe en DA. pero no se encuentra registrado en el Portal (Coordinar con el Area de TI)";
                return resultadoTransaccion;
            }

            //SEMILLA
            string semilla = tokenConfig.Semilla;

            var claims = new[]
            {
                new Claim("IdUsuario",user.IdUsuario.ToString()),
                new Claim("Usuario",user.Usuario.ToUpper())
            };

            //firma - header
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(semilla));
            var signCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            //generador de JWT
            var token = new JwtSecurityToken(
                issuer: tokenConfig.Emisor,
                audience: tokenConfig.Destinatario,
                claims: claims,
                expires: DateTime.Now.AddHours(10),
                signingCredentials: signCredentials
            );

            string tokenGenerado = new JwtSecurityTokenHandler().WriteToken(token);

            BE_UsuarioAutenticar UsuarioAutenticar = new BE_UsuarioAutenticar { Usuario = user.Usuario.ToUpper(), Token = tokenGenerado };

            resultadoTransaccion.ResultadoCodigo = 0;
            resultadoTransaccion.ResultadoDescripcion = "Se autentico correctamente";
            resultadoTransaccion.data = UsuarioAutenticar;

            return resultadoTransaccion;
        }

        public async Task<BE_ResultadoTransaccion<bool>> AutenticarUsuario(BE_UsuarioAutenticar entidad)
        {
            var claveDesEncriptada = EncriptaHelper.DecryptStringAES(entidad.Clave);

            BE_ResultadoTransaccion<bool> resultadoTransaccion = new BE_ResultadoTransaccion<bool>();

            //BE_ResultadoTransaccion<BE_UsuarioAutenticar> resultadoTransaccionDirectorio = await AutenticarUsuarioDirectorioActivo(entidad.Usuario.ToUpper(), claveDesEncriptada);
            BE_ResultadoTransaccion<bool> resultadoTransaccionDirectorio = new BE_ResultadoTransaccion<bool>();

            if (resultadoTransaccionDirectorio.ResultadoCodigo == -1)
            {
                resultadoTransaccion.ResultadoCodigo = -1;
                resultadoTransaccion.ResultadoDescripcion = resultadoTransaccionDirectorio.ResultadoDescripcion;
                resultadoTransaccion.data = false;
                return resultadoTransaccion;
            }

            BE_Usuario user = VerificarLogin(new BE_Usuario { Usuario = entidad.Usuario.ToUpper() });

            if (user == null)
            {
                resultadoTransaccion.ResultadoCodigo = -1;
                resultadoTransaccion.ResultadoDescripcion = "Usuario existe en DA. pero no se encuentra registrado en el Portal (Coordinar con el Area de TI)";
                resultadoTransaccion.data = false;
                return resultadoTransaccion;
            }

            resultadoTransaccion.ResultadoCodigo = 0;
            resultadoTransaccion.ResultadoDescripcion = "Se autentico correctamente";
            resultadoTransaccion.data = true;

            return resultadoTransaccion;
        }

        public Task<BE_UsuarioDatos> ObtienePermisosPorUsuario(BE_UsuarioDatos entidad)
        {
            return Task.Run(() =>
            {
                BE_Usuario user = VerificarLogin(new BE_Usuario { Usuario = entidad.Usuario });

                if (user == null)
                {
                    return null;
                }

                MenuRepository menuRepository = new MenuRepository(context);

                var listaAccesoMenu = menuRepository.GetAllPorIdUsuario(user.IdUsuario).Result.ToList();

                BE_UsuarioDatos UsuarioAutenticar = new BE_UsuarioDatos
                {
                    Usuario = user.Usuario,
                    IdUsuario = user.IdUsuario,
                    Imagen = user.Imagen,
                    Nombre = user.Nombre,
                    Email = user.Email,
                    IdPersona = user.IdPersona,
                    IdPerfil = user.IdPerfil,
                    CodCentroCosto = user.CodCentroCosto,
                    DesCentroCosto = user.DesCentroCosto,
                    ListaAccesoMenu = listaAccesoMenu
                };

                return UsuarioAutenticar;
            });
        }

        public async Task RecuperarPassword(BE_Usuario entidad)
        {
            var data = FindById(entidad, SP_GET_USUARIO);
            var nuevaClaveAutogenerado = GenerarCodigo(6);

            var nuevaClaveEncriptada = EncriptaHelper.EncryptStringAES(nuevaClaveAutogenerado);

            Update(new BE_Usuario { IdUsuario = data.IdUsuario, Clave = nuevaClaveEncriptada,RegUsuario = data.IdUsuario, RegEstacion = "Unknown" }, SP_UPDATE_AUTOGENERADA);
            EmailSenderRepository emailSenderRepository = new EmailSenderRepository(context);
            var mensaje = string.Format("Buen Día, Se envia nueva constraseña - {0}", nuevaClaveAutogenerado);
            await emailSenderRepository.SendEmailAsync(data.Email, "Correo Automatico - Recuperar Contraseña", mensaje);
        }

        private string GenerarCodigo(int length)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public Task UpdatePassword(BE_Usuario entidad)
        {
            return Task.Run(() => {
                Update(new BE_Usuario { IdUsuario = entidad.IdUsuario, Clave = entidad.ClaveOrigen, RegUsuario = entidad.RegUsuario, RegEstacion = entidad.RegEstacion }, SP_UPDATE_PASSWORD);
            });
        }

        public Task<BE_ResultadoTransaccion<BE_UsuarioAutenticar>> AutenticarUsuarioDirectorioActivo(string usuario, string password)
        {

            BE_ResultadoTransaccion<BE_UsuarioAutenticar> vResultadoTransaccion = new BE_ResultadoTransaccion<BE_UsuarioAutenticar>();
            _metodoName = regex.Match(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name).Groups[1].Value.ToString();

            vResultadoTransaccion.ResultadoMetodo = _metodoName;
            vResultadoTransaccion.ResultadoAplicacion = _aplicacionName;

            string domainName = "SANFELIPE";

            SearchResultCollection results;

            return Task.Run(() =>
            {

                try
                {
                    DirectoryEntry de = new DirectoryEntry("LDAP://" + domainName, usuario, password);
                    DirectorySearcher dsearch = new DirectorySearcher(de);

                    PrincipalContext AD = new PrincipalContext(ContextType.Domain, domainName);
                    UserPrincipal u = new UserPrincipal(AD);
                    UserPrincipal user = UserPrincipal.FindByIdentity(AD, usuario);

                    u.SamAccountName = usuario;

                    if (user != null)
                    {

                        if (user.LastPasswordSet.HasValue == false)
                        {
                            if (user.PasswordNeverExpires == false)
                            {
                                vResultadoTransaccion.ResultadoCodigo = -1;
                                vResultadoTransaccion.ResultadoDescripcion = "Favor de cambiar su password en una maquina con dominio";

                                return vResultadoTransaccion;
                            }
                        }

                        long daysLeft = 0;
                        if (!u.PasswordNeverExpires)
                        {
                            using (DirectoryEntry entry2 = new DirectoryEntry("WinNT://" + domainName + '/' + usuario + ",user"))
                            {
                                var PasswordExpirationDate = entry2.InvokeGet("PasswordExpirationDate");

                                if (PasswordExpirationDate != null)
                                {
                                    daysLeft = long.Parse(Math.Round((DateTime.Now - (DateTime)PasswordExpirationDate).TotalDays).ToString());
                                    if (daysLeft > 0)
                                    {
                                        vResultadoTransaccion.ResultadoCodigo = -1;
                                        vResultadoTransaccion.ResultadoDescripcion = "La contraseña del usuario ha caducado";

                                        return vResultadoTransaccion;

                                    }
                                }
                            }
                        }

                    }

                    dsearch.PropertiesToLoad.Add("name");
                    dsearch.PropertiesToLoad.Add("mail");
                    dsearch.PropertiesToLoad.Add("givenname");
                    dsearch.PropertiesToLoad.Add("sn");
                    dsearch.PropertiesToLoad.Add("userPrincipalName");
                    dsearch.PropertiesToLoad.Add("distinguishedName");
                    dsearch.PropertiesToLoad.Add("samaccountname");
                    dsearch.PropertiesToLoad.Add("displayname");
                    dsearch.PropertiesToLoad.Add("title");
                    dsearch.PropertiesToLoad.Add("st");
                    dsearch.PropertiesToLoad.Add("department");

                    dsearch.Filter = "samaccountname=" + usuario;

                    results = dsearch.FindAll();
                    List<string> nombres = new List<string>();
                    object objUser = new { nombre = string.Empty, usuario = string.Empty };
                    string nombre, nombreCompleto, mail, givenname, sn, title, department = string.Empty;
                    string samaccountnamename = string.Empty;
                    int userCount = 0;

                    nombre = string.Empty;
                    nombreCompleto = string.Empty;

                    foreach (SearchResult sr in results)
                    {
                        userCount += 1;
                        if (sr.Properties["mail"].Count > 0) mail = sr.Properties["mail"][0].ToString();
                        if (sr.Properties["givenname"].Count > 0) givenname = sr.Properties["givenname"][0].ToString();
                        if (sr.Properties["sn"].Count > 0) sn = sr.Properties["sn"][0].ToString();
                        if (sr.Properties["title"].Count > 0) title = sr.Properties["title"][0].ToString();
                        if (sr.Properties["department"].Count > 0) department = sr.Properties["department"][0].ToString();
                        if (sr.Properties["displayname"].Count > 0) nombreCompleto = sr.Properties["displayname"][0].ToString();
                    }

                    if (userCount > 1)
                    {
                        vResultadoTransaccion.ResultadoCodigo = -1;
                        vResultadoTransaccion.ResultadoDescripcion = "error se encontraron 2 cuentas iguales: " + usuario;

                        return vResultadoTransaccion;
                    }

                    vResultadoTransaccion.ResultadoCodigo = 0;
                    vResultadoTransaccion.ResultadoDescripcion = "Acceso Correcto" + usuario;

                    return vResultadoTransaccion;

                }
                catch (Exception ex)
                {
                    vResultadoTransaccion.ResultadoCodigo = -1;
                    vResultadoTransaccion.ResultadoDescripcion = ex.Message.ToString();

                    return vResultadoTransaccion;
                }
            });

        }
    }
}
