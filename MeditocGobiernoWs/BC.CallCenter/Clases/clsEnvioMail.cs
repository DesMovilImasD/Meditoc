using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Configuration;
using System.Data;
using System.IO;
using BC.CallCenter.Models.Info;

namespace BC.CallCenter.Clases
{
    public class clsEnvioMail
        {
        
        clsEnvioMailInfo oEnvioMail = new clsEnvioMailInfo();

        /// <summary>
        /// Método para el envio de correos
        /// </summary>
        /// <param name="sTipo">Tipo de correo a enviar</param>
        /// <param name="sMailTo">Correo destino</param>
        /// <param name="formatos">Lista de archivos a adjuntar para enviar</param>

        private void m_DatosEnvioMailINC(string sTipo, string sUsuario, string sClave, string sAsunto, string sMensaje)
        {
            try
            {

                oEnvioMail.sServerMail = ConfigurationManager.AppSettings["ServerMail"].ToString();

                oEnvioMail.bSSLMail = Convert.ToBoolean(ConfigurationManager.AppSettings["SSLMail"]);

                oEnvioMail.iPortMail = Convert.ToInt16(ConfigurationManager.AppSettings["PortMail"]);


                sUsuario = ConfigurationManager.AppSettings["UserMail_GENERAL"];

                sClave = ConfigurationManager.AppSettings["PassMail_GENERAL"];


                oEnvioMail.sUserMail = sUsuario;
                oEnvioMail.sPassMail = sClave;
                oEnvioMail.sAsuntoMail = sAsunto;
                oEnvioMail.sMensajeMail = sMensaje;
                oEnvioMail.bAdjuntarFile = true;
            }
            catch (Exception a)
            {
                throw new ArgumentException("No se pudo Enviar el Correo. " + a.Message);
            }
        }

        public string m_EnviarEmail(string sTipo, string sUsuario, string sClave, string sAsunto, string sMensaje, string sMailTo, string sFile, string sContentType)
        {
            oEnvioMail.sFile = sFile;
            string sResult = "";

            try
            {

                this.m_DatosEnvioMailINC(sTipo, sUsuario, sClave, sAsunto, sMensaje);
                MailMessage correo = new MailMessage();
                correo.From = new MailAddress(oEnvioMail.sUserMail);
                correo.To.Add(sMailTo);

                correo.Subject = sAsunto;
                correo.Body = sMensaje;
                correo.IsBodyHtml = true;
                correo.Priority = MailPriority.Normal;
               
                correo.Headers.Add("Disposition-Notification-To", oEnvioMail.sUserMail);

                SmtpClient smtp = new SmtpClient();
                smtp.Host = oEnvioMail.sServerMail;
                smtp.Port = oEnvioMail.iPortMail;
                smtp.Credentials = new NetworkCredential(oEnvioMail.sUserMail, oEnvioMail.sPassMail);
                smtp.EnableSsl = oEnvioMail.bSSLMail;

                smtp.Send(correo);
          
            }
            catch (Exception Ex)
            {
                sResult = "No se pudo Enviar el Correo. " + Ex.Message;
            }

            return sResult;
        }


        public void m_EnviarMailArchivo(string sTipo, string sUsuario, string sClave, string sAsunto, string sMensaje, string sMailTo, List<string> sFile, string sContentType, bool bfile)
        {
            try
            {
                this.m_DatosEnvioMailINC(sTipo, sUsuario, sClave, sAsunto, sMensaje);
                MailMessage correo = new MailMessage();
                correo.From = new MailAddress(oEnvioMail.sUserMail);
                correo.To.Add(sMailTo);

                if (bfile)
                {

                    if (sFile.Count > 0)
                    {
                        for (int a = 0; a < sFile.Count; a++)
                        {
                            System.Net.Mail.Attachment attachment;
                            attachment = new System.Net.Mail.Attachment(sFile[a]); //Attaching File to Mail  
                            correo.Attachments.Add(attachment);
                        }

                    }
                  
                }
                correo.Subject = oEnvioMail.sAsuntoMail;
                correo.Body = oEnvioMail.sMensajeMail;
                correo.IsBodyHtml = true;
                correo.Priority = MailPriority.Normal;

                correo.Headers.Add("Disposition-Notification-To", oEnvioMail.sUserMail);

                SmtpClient smtp = new SmtpClient();
                smtp.Host = oEnvioMail.sServerMail;
                smtp.Port = oEnvioMail.iPortMail;
                smtp.Credentials = new NetworkCredential(oEnvioMail.sUserMail, oEnvioMail.sPassMail);
                smtp.EnableSsl = oEnvioMail.bSSLMail;

                smtp.Send(correo);

            }
            catch (Exception Ex)
            {
                throw new ArgumentException("No se pudo Enviar el Correo. " + Ex.Message);
            }
        }

        public string cargatxt(string path, ref string serror)
        {
            string texto = null;
            try
            {
                System.IO.StreamReader sr = new System.IO.StreamReader(path);
                texto = sr.ReadToEnd();
                sr.Close();
            }
            catch (Exception ex)
            {
                serror = ex.Message;
            }
            return texto;
        }

    }
}
