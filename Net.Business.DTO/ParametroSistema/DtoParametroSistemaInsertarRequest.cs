using Net.Business.Entities;
using System;

namespace Net.Business.DTO
{
    public class DtoParametroSistemaInsertarRequest: EntityBase
    {
        public int IdParametrosSistema { get; set; }
        public string SendEmail { get; set; }
        public string SendEmailPasswordOrigen { get; set; }
        public int SendEmailPort { get; set; }
        public Boolean SendEmailEnabledSSL { get; set; }
        public string SendEmailHost { get; set; }
        public string EmailGoogleDrive { get; set; }
        public string EmailPassword { get; set; }

        public BE_ParametroSistema RetornaParametroSistema()
        {
            return new BE_ParametroSistema
            {
                IdParametrosSistema = this.IdParametrosSistema,
                SendEmail = this.SendEmail,
                SendEmailPasswordOrigen = this.SendEmailPasswordOrigen,
                SendEmailPort = this.SendEmailPort,
                SendEmailEnabledSSL = this.SendEmailEnabledSSL,
                SendEmailHost = this.SendEmailHost,
                EmailGoogleDrive = this.EmailGoogleDrive,
                EmailPassword = this.EmailPassword,
                RegUsuario = this.RegUsuario,
                RegEstacion = this.RegEstacion
            };
        }
    }
}
