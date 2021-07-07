namespace Net.Data
{
    public interface IRepositoryWrapper
    {
        IUsuarioRepository Usuario { get; }
        IMenuRepository Menu { get; }
        IOpcionRepository Opcion { get; }
        IOpcionxPerfilRepository OpcionxPerfil { get; }
        IPerfilRepository Perfil { get; }
        IPersonaRepository Persona { get; }

        // Propio del negocio
        IParametroConexionRepository ParametroConexion { get; }
        IParametroSistemaRepository ParametroSistema { get; }
    }
}
