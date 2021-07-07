using Net.Business.Entities;
using Net.Connection;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Net.Data
{
    public interface IOpcionRepository : IRepositoryBase<BE_Opcion>
    {
        Task<IEnumerable<BE_Opcion>> GetAll(BE_Opcion entidad);
        Task<BE_Opcion> GetById(BE_Opcion entidad);
        Task<int> Create(BE_Opcion entidad);
        Task Update(BE_Opcion entidad);
        Task Delete(BE_Opcion entidad);
    }
}
