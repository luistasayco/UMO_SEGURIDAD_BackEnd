using Net.Business.Entities;
using Net.Connection;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Net.Data
{
    public class PerfilRepository : RepositoryBase<BE_Peril>, IPerfilRepository
    {
        const string DB_ESQUEMA = "";
        const string SP_GET = DB_ESQUEMA + "SEG_GetPerfilAll";
        const string SP_GET_ID = DB_ESQUEMA + "SEG_GetPerfilPorId";
        const string SP_INSERT = DB_ESQUEMA + "SEG_SetPerfilInsert";
        const string SP_DELETE = DB_ESQUEMA + "SEG_SetPerfilDelete";
        const string SP_UPDATE = DB_ESQUEMA + "SEG_SetPerfilUpdate";

        public PerfilRepository(IConnectionSQL context)
            : base(context)
        {
        }
        public Task<IEnumerable<BE_Peril>> GetAll(BE_Peril entidad)
        {
            return Task.Run(() => FindAll(entidad, SP_GET));
        }
        public Task<BE_Peril> GetById(BE_Peril entidad)
        {
            return Task.Run(() => FindById(entidad, SP_GET_ID));
        }
        public async Task<int> Create(BE_Peril entidad)
        {

            return await Task.Run(() => Create(entidad, SP_INSERT));
        }
        public Task Update(BE_Peril entidad)
        {
            return Task.Run(() => Update(entidad, SP_UPDATE));
        }
        public Task Delete(BE_Peril entidad)
        {
            return Task.Run(() => Delete(entidad, SP_DELETE));
        }
    }
}