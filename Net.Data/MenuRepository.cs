using Net.Business.Entities;
using Net.Connection;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Net.Data
{
    public class MenuRepository : RepositoryBase<BE_Menu>, IMenuRepository
    {
        const string DB_ESQUEMA = "";
        const string SP_GET = DB_ESQUEMA + "SEG_GetMenuAll";
        const string SP_GET_ID = DB_ESQUEMA + "SEG_GetMenuPorId";
        const string SP_GET_MENU_POR_USUARIO = DB_ESQUEMA + "SEG_GetAccesoMenuxPerfil";
        const string SP_GET_OPCION_POR_USUARIO = DB_ESQUEMA + "SEG_GetAccesoOpcionesxPerfil";
        const string SP_INSERT = DB_ESQUEMA + "SEG_SetMenuInsert";
        const string SP_DELETE = DB_ESQUEMA + "SEG_SetMenuDelete";
        const string SP_UPDATE = DB_ESQUEMA + "SEG_SetMenuUpdate";

        public MenuRepository(IConnectionSQL context)
            : base(context)
        {
        }
        public Task<IEnumerable<BE_Menu>> GetAll(BE_Menu entidad)
        {
            return Task.Run(() => FindAll(entidad, SP_GET));
        }
        public Task<BE_Menu> GetById(BE_Menu entidad)
        {
            return Task.Run(() => FindById(entidad, SP_GET_ID));
        }
        public Task<IEnumerable<BE_Menu>> GetAllPorIdUsuario(int? idUsuario)
        {
            return Task.Run(() => {
                IEnumerable<BE_Menu> listMenu = context.ExecuteSqlViewFindByCondition<BE_Menu>(SP_GET_MENU_POR_USUARIO, new BE_Usuario { IdUsuario = idUsuario });

                IEnumerable<BE_Opcion> listOpcion;

                foreach (var item in listMenu)
                {
                    listOpcion = context.ExecuteSqlViewFindByCondition<BE_Opcion>(SP_GET_OPCION_POR_USUARIO, new EF_Opcion { IdUsuario = idUsuario, IdMenu = item.IdMenu });
                    listMenu.FirstOrDefault(x => x.IdMenu == item.IdMenu).ListaOpciones = listOpcion;
                }

                return listMenu;
            });
        }
        public async Task<int> Create(BE_Menu entidad)
        {
            return await Task.Run(() => Create(entidad, SP_INSERT));
        }
        public Task Update(BE_Menu entidad)
        {
            return Task.Run(() => Update(entidad, SP_UPDATE));
        }
        public Task Delete(BE_Menu entidad)
        {
            return Task.Run(() => Delete(entidad, SP_DELETE));
        }
    }
}