using Net.Business.Entities;
using Net.Connection;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Net.Data
{
    public interface IPerfilRepository : IRepositoryBase<BE_Peril>
    {
        Task<IEnumerable<BE_Peril>> GetAll(BE_Peril entidad);
        Task<BE_Peril> GetById(BE_Peril entidad);
        Task<int> Create(BE_Peril entidad);
        Task Update(BE_Peril entidad);
        Task Delete(BE_Peril entidad);
    }
}