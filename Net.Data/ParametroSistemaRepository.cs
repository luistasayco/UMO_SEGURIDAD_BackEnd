using Net.Business.Entities;
using Net.Connection;
using Net.CrossCotting;
using System.Threading.Tasks;

namespace Net.Data
{
    public class ParametroSistemaRepository: RepositoryBase<BE_ParametroSistema>, IParametroSistemaRepository
    {
        const string DB_ESQUEMA = "";
        const string SP_GET_ID = DB_ESQUEMA + "SEG_GetParametroSistemaPorId";
        const string SP_INSERT = DB_ESQUEMA + "SEG_SetParametroSistemaInsert";
        const string SP_DELETE = DB_ESQUEMA + "SEG_SetParametroSistemaDelete";
        const string SP_UPDATE = DB_ESQUEMA + "SEG_SetParametroSistemaUpdate";

        public ParametroSistemaRepository(IConnectionSQL context)
            : base(context)
        {
        }
        public Task<BE_ParametroSistema> GetById(BE_ParametroSistema entidad)
        {
            return Task.Run(() => {
                var data = FindById(entidad, SP_GET_ID);
                data.SendEmailPasswordOrigen = data.SendEmailPassword;
                return data;
            });
        }
        public async Task<int> Create(BE_ParametroSistema entidad)
        {
            return await Task.Run(() =>
            {
                entidad.SendEmailPassword = entidad.SendEmailPasswordOrigen;
                entidad.SendEmailPasswordOrigen = null;
                var id = Create(entidad, SP_INSERT);
                return id;
            });
        }
        public Task Update(BE_ParametroSistema entidad)
        {
            return Task.Run(() => {
                entidad.SendEmailPassword = entidad.SendEmailPasswordOrigen;
                entidad.SendEmailPasswordOrigen = null;
                Update(entidad, SP_UPDATE);
            });
        }
        public Task Delete(BE_ParametroSistema entidad)
        {
            return Task.Run(() => Delete(entidad, SP_DELETE));
        }
    }
}
