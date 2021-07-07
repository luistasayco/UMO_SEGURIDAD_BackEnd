using Net.Business.Entities;
using Net.Connection;
using System.Threading.Tasks;

namespace Net.Data
{
    public interface IParametroSistemaRepository : IRepositoryBase<BE_ParametroSistema>
    {
        Task<BE_ParametroSistema> GetById(BE_ParametroSistema entidad);
        Task<int> Create(BE_ParametroSistema entidad);
        Task Update(BE_ParametroSistema entidad);
        Task Delete(BE_ParametroSistema entidad);
    }
}
