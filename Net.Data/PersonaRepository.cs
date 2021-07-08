using Microsoft.Data.SqlClient;
using Net.Business.Entities;
using Net.Connection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Transactions;

namespace Net.Data
{
    public class PersonaRepository : RepositoryBase<BE_Persona>, IPersonaRepository
    {
        private string _aplicacionName;
        private string _metodoName;
        private readonly Regex regex = new Regex(@"<(\w+)>.*");

        const string DB_ESQUEMA = "";
        const string SP_GET = DB_ESQUEMA + "SEG_GetPersonaAll";
        const string SP_GET_ID = DB_ESQUEMA + "SEG_GetPersonaPorId";
        const string SP_GET_ID_USUARIO = DB_ESQUEMA + "SEG_GetUsuarioPorPersona";
        const string SP_INSERT = DB_ESQUEMA + "SEG_SetPersonaInsert";
        const string SP_INSERT_USUARIO = DB_ESQUEMA + "SEG_SetUsuarioInsert";
        const string SP_DELETE = DB_ESQUEMA + "SEG_SetPersonaDelete";
        const string SP_UPDATE = DB_ESQUEMA + "SEG_SetPersonaUpdate";
        const string SP_UPDATE_USUARIO = DB_ESQUEMA + "SEG_SetUsuarioUpdate";

        public PersonaRepository(IConnectionSQL context)
            : base(context)
        {
            _aplicacionName = this.GetType().Name;
        }

        public Task<IEnumerable<BE_Persona>> GetAll(BE_Persona entidad)
        {
            return Task.Run(() => FindAll(entidad, SP_GET));
        }
        public Task<BE_Persona> GetById(BE_Persona entidad)
        {
            var objListPrincipal = Task.Run(() =>
            {
                BE_Persona p = context.ExecuteSqlViewId<BE_Persona>(SP_GET_ID, entidad);
                BE_Usuario usu = context.ExecuteSqlViewId<BE_Usuario>(SP_GET_ID_USUARIO, new BE_Usuario { IdPersona = entidad.IdPersona });
                usu.ClaveOrigen = usu.Clave;
                usu.Clave = usu.Clave;
                p.EntidadUsuario = usu;
                return p;
            });
            return objListPrincipal;
        }
        public async Task<BE_ResultadoTransaccion<BE_Persona>> Create(BE_Persona value)
        {
            BE_ResultadoTransaccion<BE_Persona> vResultadoTransaccion = new BE_ResultadoTransaccion<BE_Persona>();
            _metodoName = regex.Match(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name).Groups[1].Value.ToString();

            vResultadoTransaccion.ResultadoMetodo = _metodoName;
            vResultadoTransaccion.ResultadoAplicacion = _aplicacionName;

            try
            {
                using (SqlConnection conn = new SqlConnection(context.DevuelveConnectionSQL()))
                {
                    using (CommittableTransaction transaction = new CommittableTransaction())
                    {
                        await conn.OpenAsync();
                        conn.EnlistTransaction(transaction);

                        try
                        {
                            using (SqlCommand cmd = new SqlCommand(SP_INSERT, conn))
                            {
                                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                                SqlParameter oParam = new SqlParameter("@IdPersona", value.IdPersona);
                                oParam.SqlDbType = SqlDbType.Int;
                                oParam.Direction = ParameterDirection.Output;
                                cmd.Parameters.Add(oParam);

                                cmd.Parameters.Add(new SqlParameter("@Nombre", value.Nombre));
                                cmd.Parameters.Add(new SqlParameter("@ApellidoPaterno", value.ApellidoPaterno));
                                cmd.Parameters.Add(new SqlParameter("@ApellidoMaterno", value.ApellidoMaterno));
                                cmd.Parameters.Add(new SqlParameter("@NroDocumento", value.NroDocumento));
                                cmd.Parameters.Add(new SqlParameter("@NroTelefono", value.NroTelefono));
                                cmd.Parameters.Add(new SqlParameter("@FlgActivo", value.FlgActivo));
                                //cmd.Parameters.Add(new SqlParameter("@CodCentroCosto", value.CodCentroCosto));
                                cmd.Parameters.Add(new SqlParameter("@RegUsuario", value.RegUsuario));
                                cmd.Parameters.Add(new SqlParameter("@RegEstacion", value.RegEstacion));

                                await cmd.ExecuteNonQueryAsync();

                                value.IdPersona = (int)cmd.Parameters["@IdPersona"].Value;
                                value.EntidadUsuario.IdPersona = value.IdPersona;
                            }

                            UsuarioRepository usuarioRepository = new UsuarioRepository(context);
                            BE_ResultadoTransaccion<bool> resultadoTransaccionEXisteUsuarioAD = await usuarioRepository.ValidaExisteUsuarioDirectorioActivo(value.EntidadUsuario.Usuario);

                            if (resultadoTransaccionEXisteUsuarioAD.ResultadoCodigo == -1)
                            {
                                transaction.Rollback();
                                vResultadoTransaccion.IdRegistro = -1;
                                vResultadoTransaccion.ResultadoCodigo = -1;
                                vResultadoTransaccion.ResultadoDescripcion = resultadoTransaccionEXisteUsuarioAD.ResultadoDescripcion;
                                return vResultadoTransaccion;
                            }

                            using (SqlCommand cmd = new SqlCommand(SP_INSERT_USUARIO, conn))
                            {
                                cmd.Parameters.Clear();
                                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                                
                                SqlParameter oParam = new SqlParameter("@IdUsuario", value.EntidadUsuario.IdUsuario);
                                oParam.SqlDbType = SqlDbType.Int;
                                oParam.Direction = ParameterDirection.Output;
                                cmd.Parameters.Add(oParam);

                                cmd.Parameters.Add(new SqlParameter("@IdPersona", value.EntidadUsuario.IdPersona));
                                cmd.Parameters.Add(new SqlParameter("@IdPerfil", value.EntidadUsuario.IdPerfil));
                                cmd.Parameters.Add(new SqlParameter("@Usuario", value.EntidadUsuario.Usuario));
                                cmd.Parameters.Add(new SqlParameter("@Clave", value.EntidadUsuario.ClaveOrigen));
                                cmd.Parameters.Add(new SqlParameter("@Email", value.EntidadUsuario.Email));
                                cmd.Parameters.Add(new SqlParameter("@Imagen", value.EntidadUsuario.Imagen));
                                cmd.Parameters.Add(new SqlParameter("@ThemeDark", value.EntidadUsuario.ThemeDark));
                                cmd.Parameters.Add(new SqlParameter("@ThemeColor", value.EntidadUsuario.ThemeColor));
                                cmd.Parameters.Add(new SqlParameter("@TypeMenu", value.EntidadUsuario.TypeMenu));
                                cmd.Parameters.Add(new SqlParameter("@RegUsuario", value.RegUsuario));
                                cmd.Parameters.Add(new SqlParameter("@RegEstacion", value.RegEstacion));

                                await cmd.ExecuteNonQueryAsync();

                                value.EntidadUsuario.IdUsuario = (int)cmd.Parameters["@IdUsuario"].Value;
                            }

                            vResultadoTransaccion.IdRegistro = (int)value.EntidadUsuario.IdUsuario;
                            vResultadoTransaccion.ResultadoCodigo = 0;
                            vResultadoTransaccion.ResultadoDescripcion = "Se realizo con Exito...!!!";

                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            vResultadoTransaccion.IdRegistro = -1;
                            vResultadoTransaccion.ResultadoCodigo = -1;
                            vResultadoTransaccion.ResultadoDescripcion = ex.Message.ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                vResultadoTransaccion.IdRegistro = -1;
                vResultadoTransaccion.ResultadoCodigo = -1;
                vResultadoTransaccion.ResultadoDescripcion = ex.Message.ToString();
            }

            return vResultadoTransaccion;
        }
        public async Task<BE_ResultadoTransaccion<BE_Persona>> Update(BE_Persona value)
        {
            BE_ResultadoTransaccion<BE_Persona> vResultadoTransaccion = new BE_ResultadoTransaccion<BE_Persona>();
            _metodoName = regex.Match(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name).Groups[1].Value.ToString();

            vResultadoTransaccion.ResultadoMetodo = _metodoName;
            vResultadoTransaccion.ResultadoAplicacion = _aplicacionName;

            using (SqlConnection conn = new SqlConnection(context.DevuelveConnectionSQL()))
            {
                using (CommittableTransaction transaction = new CommittableTransaction())
                {
                    await conn.OpenAsync();
                    conn.EnlistTransaction(transaction);

                    try
                    {
                        using (SqlCommand cmd = new SqlCommand(SP_UPDATE, conn))
                        {
                            cmd.CommandType = System.Data.CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@IdPersona", value.IdPersona));
                            cmd.Parameters.Add(new SqlParameter("@Nombre", value.Nombre));
                            cmd.Parameters.Add(new SqlParameter("@ApellidoPaterno", value.ApellidoPaterno));
                            cmd.Parameters.Add(new SqlParameter("@ApellidoMaterno", value.ApellidoMaterno));
                            cmd.Parameters.Add(new SqlParameter("@NroDocumento", value.NroDocumento));
                            cmd.Parameters.Add(new SqlParameter("@NroTelefono", value.NroTelefono));
                            cmd.Parameters.Add(new SqlParameter("@FlgActivo", value.FlgActivo));
                            cmd.Parameters.Add(new SqlParameter("@RegUsuario", value.RegUsuario));
                            cmd.Parameters.Add(new SqlParameter("@RegEstacion", value.RegEstacion));

                            await cmd.ExecuteNonQueryAsync();
                        }

                        using (SqlCommand cmd = new SqlCommand(SP_UPDATE_USUARIO, conn))
                        {
                            cmd.CommandType = System.Data.CommandType.StoredProcedure;

                            cmd.Parameters.Add(new SqlParameter("@IdUsuario", value.EntidadUsuario.IdUsuario));
                            cmd.Parameters.Add(new SqlParameter("@IdPerfil", value.EntidadUsuario.IdPerfil));

                            cmd.Parameters.Add(new SqlParameter("@Clave", value.EntidadUsuario.ClaveOrigen));
                            cmd.Parameters.Add(new SqlParameter("@Email", value.EntidadUsuario.Email));
                            cmd.Parameters.Add(new SqlParameter("@Imagen", value.EntidadUsuario.Imagen));
                            cmd.Parameters.Add(new SqlParameter("@ThemeDark", value.EntidadUsuario.ThemeDark));
                            cmd.Parameters.Add(new SqlParameter("@ThemeColor", value.EntidadUsuario.ThemeColor));
                            cmd.Parameters.Add(new SqlParameter("@TypeMenu", value.EntidadUsuario.TypeMenu));
                            cmd.Parameters.Add(new SqlParameter("@RegUsuario", value.RegUsuario));
                            cmd.Parameters.Add(new SqlParameter("@RegEstacion", value.RegEstacion));

                            await cmd.ExecuteNonQueryAsync();
                        }

                        transaction.Commit();

                        vResultadoTransaccion.IdRegistro = 0;
                        vResultadoTransaccion.ResultadoCodigo = 0;
                        vResultadoTransaccion.ResultadoDescripcion = "Se realizo con Exito...!!!";
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        vResultadoTransaccion.IdRegistro = -1;
                        vResultadoTransaccion.ResultadoCodigo = -1;
                        vResultadoTransaccion.ResultadoDescripcion = ex.Message.ToString();
                        return vResultadoTransaccion;
                    }
                }
            }

            return vResultadoTransaccion;
        }
        public Task<BE_ResultadoTransaccion<BE_Persona>> Delete(BE_Persona entidad)
        {
            BE_ResultadoTransaccion<BE_Persona> vResultadoTransaccion = new BE_ResultadoTransaccion<BE_Persona>();
            _metodoName = regex.Match(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name).Groups[1].Value.ToString();

            vResultadoTransaccion.ResultadoMetodo = _metodoName;
            vResultadoTransaccion.ResultadoAplicacion = _aplicacionName;

            return Task.Run(() => {
                try
                {
                    Delete(entidad, SP_DELETE);

                    vResultadoTransaccion.IdRegistro = 0;
                    vResultadoTransaccion.ResultadoCodigo = 0;
                    vResultadoTransaccion.ResultadoDescripcion = "Se realizo con Exito...!!!";
                    return vResultadoTransaccion;
                }
                catch (Exception ex)
                {
                    vResultadoTransaccion.IdRegistro = -1;
                    vResultadoTransaccion.ResultadoCodigo = -1;
                    vResultadoTransaccion.ResultadoDescripcion = ex.Message.ToString();
                    return vResultadoTransaccion;
                }
            });
        }
    }
}