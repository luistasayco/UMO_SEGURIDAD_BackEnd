using Net.Business.Entities;
using Net.Connection;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Net.Data
{
    public interface IParametroConexionRepository : IRepositoryBase<BE_ParametroConexion>
    {
        Task<BE_ParametroConexion> GetById(BE_ParametroConexion entidad);
        Task<int> Create(BE_ParametroConexion entidad);
        Task Update(BE_ParametroConexion entidad);
        Task Delete(BE_ParametroConexion entidad);
    }
}
