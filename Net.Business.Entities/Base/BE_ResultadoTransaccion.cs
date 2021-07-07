namespace Net.Business.Entities
{
    public class BE_ResultadoTransaccion<T>
    {
        /// <summary>
        /// ID de un nuevo registro o Id de registro actualizar
        /// </summary>
        public int IdRegistro { get; set; }
        /// <summary>
        /// Resultado de Codigo, Obtenido de la Base de Datos
        /// 0 => Correcto
        /// -1 => Error
        /// </summary>
        public int ResultadoCodigo { get; set; }
        /// <summary>
        /// Descripcion de la acción al finalizar el metodo invocado
        /// </summary>
        public string ResultadoDescripcion { get; set; }
        public string ResultadoAplicacion { get; set; }
        public string ResultadoMetodo { get; set; }
        public T data { get; set; }
        public string NombreEstacion { get => System.Environment.MachineName; set => NombreEstacion = System.Environment.MachineName; }
    }
}
