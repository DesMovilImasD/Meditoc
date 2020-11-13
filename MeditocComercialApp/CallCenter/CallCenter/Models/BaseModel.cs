namespace CallCenter.Models
{
    /// <summary>Modelo Base de herencia para los modelos.
    /// </summary>
    public class BaseModel
    {
        /// <summary>Campo el cual contiene el Token unico para intercambio
        /// </summary>
        public string sToken { get; set; }

        /// <summary>Campo el cual contiene el UID del cliente "Paciente"
        /// </summary>
        public string sUIDCliente { get; set; }

        /// <summary>Campo el cual contiene el UID del receptor "DR"
        /// </summary>
        public string sUIDDR { get; set; }

        /// <summary>Campo el cual contiene la bandera del resultado de la solicitiud
        /// </summary>
        public bool bResult { get; set; }

        /// <summary>Campo el cual contiene el mensaje de respuesta de la solicitud este puede,
        /// ser un error o afirmación todo dependera de la bansdera de respuesta.
        /// </summary>
        public string sMensajeRespuesta { get; set; }

        /// <summary>Campo el cual contiene los errores presentes en la operación solicitada.
        /// </summary>
        public string sErrorGeneral { get; set; }

    }
}