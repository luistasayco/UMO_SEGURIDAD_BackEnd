using Net.Business.Entities;
using Net.Connection;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Net.Data
{
    public interface IOpcionxPerfilRepository : IRepositoryBase<BE_OpcionxPerfil>
    {
        Task<IEnumerable<BE_OpcionxPerfil>> GetAllSeleccionado(BE_OpcionxPerfil entidad);
        Task<IEnumerable<BE_OpcionxPerfil>> GetAllPorSeleccionar(BE_OpcionxPerfil entidad);
        Task<int> Create(BE_OpcionxPerfil entidad);
        Task Delete(BE_OpcionxPerfil entidad);
    }
}