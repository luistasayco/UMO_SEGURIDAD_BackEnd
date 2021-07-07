using Net.Business.Entities;
using Net.Connection;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Net.Data
{
    public interface IMenuRepository : IRepositoryBase<BE_Menu>
    {
        Task<IEnumerable<BE_Menu>> GetAll(BE_Menu entidad);
        Task<IEnumerable<BE_Menu>> GetAllPorIdUsuario(int? idUsuario);
        Task<BE_Menu> GetById(BE_Menu entidad);
        Task<int> Create(BE_Menu entidad);
        Task Update(BE_Menu entidad);
        Task Delete(BE_Menu entidad);
    }
}
