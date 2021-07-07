using Net.Business.Entities;
using Net.Connection;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Net.Data
{
    public class OpcionxPerfilRepository : RepositoryBase<BE_OpcionxPerfil>, IOpcionxPerfilRepository
    {
        const string DB_ESQUEMA = "";
        const string SP_GET_SELECCIONADO = DB_ESQUEMA + "SEG_GetOpcionxPerfilAllSeleccionado";
        const string SP_GET_POR_SELECCIONAR = DB_ESQUEMA + "SEG_GetOpcionxPerfilAllPorSeleccionar";
        const string SP_INSERT = DB_ESQUEMA + "SEG_SetOpcionxPerfilInsert";
        const string SP_DELETE = DB_ESQUEMA + "SEG_SetOpcionxPerfilDelete";

        public OpcionxPerfilRepository(IConnectionSQL context)
            : base(context)
        {
        }
        public Task<IEnumerable<BE_OpcionxPerfil>> GetAllSeleccionado(BE_OpcionxPerfil entidad)
        {
            return Task.Run(() => FindAll(entidad, SP_GET_SELECCIONADO));
        }
        public Task<IEnumerable<BE_OpcionxPerfil>> GetAllPorSeleccionar(BE_OpcionxPerfil entidad)
        {
            return Task.Run(() => FindAll(entidad, SP_GET_POR_SELECCIONAR));
        }
        public async Task<int> Create(BE_OpcionxPerfil entidad)
        {
            return await Task.Run(() => Create(entidad, SP_INSERT));
        }
        public Task Delete(BE_OpcionxPerfil entidad)
        {
            return Task.Run(() => Delete(entidad, SP_DELETE));
        }
    }
}