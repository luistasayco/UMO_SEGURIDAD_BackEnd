using Net.Business.Entities;
using Net.Connection;
using Net.CrossCotting;
using System.Threading.Tasks;

namespace Net.Data
{
    public class ParametroConexionRepository : RepositoryBase<BE_ParametroConexion>, IParametroConexionRepository
    {
        const string DB_ESQUEMA = "";
        const string SP_GET_ID = DB_ESQUEMA + "SEG_GetParametroConexionPorId";
        const string SP_INSERT = DB_ESQUEMA + "SEG_SetParametroConexionInsert";
        const string SP_DELETE = DB_ESQUEMA + "SEG_SetParametroConexionDelete";
        const string SP_UPDATE = DB_ESQUEMA + "SEG_SetParametroConexionUpdate";
        public ParametroConexionRepository(IConnectionSQL context)
            : base(context)
        {
        }
        public Task<BE_ParametroConexion> GetById(BE_ParametroConexion entidad)
        {
            return Task.Run(() => {

                var data = FindById(entidad, SP_GET_ID);
                //data.AplicacionPassword = null;
                data.AplicacionPasswordOriginal = data.AplicacionPassword;
                //data.SapPassword = null;
                data.SapPasswordOriginal = data.SapPassword;
                return data;
                });
        }
        public async Task<int> Create(BE_ParametroConexion entidad)
        {
            return await Task.Run(() =>
            {
                entidad.AplicacionPassword = entidad.AplicacionPasswordOriginal;
                entidad.AplicacionPasswordOriginal = null;
                entidad.SapPassword = entidad.SapPasswordOriginal;
                entidad.SapPasswordOriginal = null;
                var id = Create(entidad, SP_INSERT);
                return id;
            });
        }
        public Task Update(BE_ParametroConexion entidad)
        {
            return Task.Run(() => {

                entidad.AplicacionPassword = entidad.AplicacionPasswordOriginal;
                entidad.SapPassword = entidad.SapPasswordOriginal;


                entidad.AplicacionPasswordOriginal = null;
                
                entidad.SapPasswordOriginal = null;

                Update(entidad, SP_UPDATE); 
            });
        }
        public Task Delete(BE_ParametroConexion entidad)
        {
            return Task.Run(() => Delete(entidad, SP_DELETE));
        }
    }
}
