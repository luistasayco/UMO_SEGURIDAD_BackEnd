using Microsoft.Extensions.Options;
using Net.Connection;
using Net.CrossCotting;

namespace Net.Data
{
    public class RepositoryWrapper: IRepositoryWrapper
    {
        private IConnectionSQL _repoContext;
        private readonly IOptions<ParametrosTokenConfig> tokenConfig;
        private IUsuarioRepository _Usuario;
        private IMenuRepository _Menu;
        private IOpcionRepository _Opcion;
        private IOpcionxPerfilRepository _OpcionxPerfil;
        private IPerfilRepository _Perfil;
        private IPersonaRepository _Persona;
        private IParametroConexionRepository _ParametroConexion;
        private IParametroSistemaRepository _ParametroSistema;

        public RepositoryWrapper(IConnectionSQL repoContext, IOptions<ParametrosTokenConfig> tokenConfig)
        {
            _repoContext = repoContext;
            this.tokenConfig = tokenConfig;
        }

        public IUsuarioRepository Usuario
        {
            get
            {
                if (_Usuario == null)
                {
                    _Usuario = new UsuarioRepository(_repoContext, this.tokenConfig);
                }
                return _Usuario;
            }
        }
        public IMenuRepository Menu
        {
            get
            {
                if (_Menu == null)
                {
                    _Menu = new MenuRepository(_repoContext);
                }
                return _Menu;
            }
        }
        public IOpcionRepository Opcion
        {
            get
            {
                if (_Opcion == null)
                {
                    _Opcion = new OpcionRepository(_repoContext);
                }
                return _Opcion;
            }
        }
        public IOpcionxPerfilRepository OpcionxPerfil
        {
            get
            {
                if (_OpcionxPerfil == null)
                {
                    _OpcionxPerfil = new OpcionxPerfilRepository(_repoContext);
                }
                return _OpcionxPerfil;
            }
        }
        public IPerfilRepository Perfil
        {
            get
            {
                if (_Perfil == null)
                {
                    _Perfil = new PerfilRepository(_repoContext);
                }
                return _Perfil;
            }
        }
        public IPersonaRepository Persona
        {
            get
            {
                if (_Persona == null)
                {
                    _Persona = new PersonaRepository(_repoContext);
                }
                return _Persona;
            }
        }

        public IParametroConexionRepository ParametroConexion
        {
            get
            {
                if (_ParametroConexion == null)
                {
                    _ParametroConexion = new ParametroConexionRepository(_repoContext);
                }
                return _ParametroConexion;
            }
        }

        public IParametroSistemaRepository ParametroSistema
        {
            get
            {
                if (_ParametroSistema == null)
                {
                    _ParametroSistema = new ParametroSistemaRepository(_repoContext);
                }
                return _ParametroSistema;
            }
        }
    }
}
