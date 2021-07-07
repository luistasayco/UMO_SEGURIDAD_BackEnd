using Net.Business.Entities;
using Net.Connection;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Net.Data
{
    public interface IPersonaRepository : IRepositoryBase<BE_Persona>
    {
        Task<IEnumerable<BE_Persona>> GetAll(BE_Persona entidad);
        Task<BE_Persona> GetById(BE_Persona entidad);
        Task<int> Create(BE_Persona entidad);
        Task Update(BE_Persona entidad);
        Task Delete(BE_Persona entidad);
    }
}
