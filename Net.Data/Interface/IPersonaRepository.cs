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
        Task<BE_ResultadoTransaccion<BE_Persona>> Create(BE_Persona entidad);
        Task<BE_ResultadoTransaccion<BE_Persona>> Update(BE_Persona entidad);
        Task<BE_ResultadoTransaccion<BE_Persona>> Delete(BE_Persona entidad);
    }
}
