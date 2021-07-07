using Net.Business.Entities;
using Net.Connection;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Net.Data
{
    public class OpcionRepository : RepositoryBase<BE_Opcion>, IOpcionRepository
    {
        const string DB_ESQUEMA = "";
        const string SP_GET = DB_ESQUEMA + "SEG_GetOpcionAll";

        const string SP_GET_ID = DB_ESQUEMA + "SEG_GetOpcionPorId";
        const string SP_INSERT = DB_ESQUEMA + "SEG_SetOpcionInsert";
        const string SP_DELETE = DB_ESQUEMA + "SEG_SetOpcionDelete";
        const string SP_UPDATE = DB_ESQUEMA + "SEG_SetOpcionUpdate";

        public OpcionRepository(IConnectionSQL context)
            : base(context)
        {
        }
        public Task<IEnumerable<BE_Opcion>> GetAll(BE_Opcion entidad)
        {
            return Task.Run(() => FindAll(entidad, SP_GET));
        }
        public Task<BE_Opcion> GetById(BE_Opcion entidad)
        {
            return Task.Run(() => FindById(entidad, SP_GET_ID));
        }
        public async Task<int> Create(BE_Opcion entidad)
        {
            return await Task.Run(() => Create(entidad, SP_INSERT));
        }
        public Task Update(BE_Opcion entidad)
        {
            return Task.Run(() => Update(entidad, SP_UPDATE));
        }
        public Task Delete(BE_Opcion entidad)
        {
            return Task.Run(() => Delete(entidad, SP_DELETE));
        }
    }
}