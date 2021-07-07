using Net.Business.Entities;
using Net.Connection;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Net.Data
{
    public interface IUsuarioRepository :  IRepositoryBase<BE_Usuario>
    {
        Task<IEnumerable<BE_Usuario>> GetAll(BE_Usuario entidad);
        Task<BE_Usuario> GetById(BE_Usuario entidad);
        Task<BE_ResultadoTransaccion<BE_UsuarioAutenticar>> Autenticar(BE_UsuarioAutenticar entidad);
        Task<BE_ResultadoTransaccion<bool>> AutenticarUsuario(BE_UsuarioAutenticar entidad);
        Task<BE_UsuarioDatos> ObtienePermisosPorUsuario(BE_UsuarioDatos entidad);
        BE_Usuario VerificarLogin(BE_Usuario entidad);
        Task RecuperarPassword(BE_Usuario entidad);
        Task UpdatePassword(BE_Usuario entidad);
    }
}
