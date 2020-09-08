﻿using IMD.Admin.Utilities.Business;
using IMD.Admin.Utilities.Entities;
using IMD.Meditoc.CallCenter.Mx.Entities;
using log4net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace IMD.Meditoc.CallCenter.Mx.Business.Correo
{
    public class BusCorreo
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(BusCorreo));

        public EntCorreo oEnvioMail = new EntCorreo();

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

        public IMDResponse<bool> m_EnviarEmail(string sTipo, string sUsuario, string sClave, string sAsunto, string sMensaje, string sMailTo, string sFile, string sContentType)
        {
            oEnvioMail.sFile = sFile;
            IMDResponse<bool> response = new IMDResponse<bool>();

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

                logger.Info(IMDSerialize.Serialize(66823458253448, $"Inicia Envio de coreos", oEnvioMail));
                logger.Info(IMDSerialize.Serialize(66823458253447, $"Inicia Envio de coreos", smtp));
                logger.Info(IMDSerialize.Serialize(66823458253445, $"Inicia Envio de coreos", correo));

                smtp.Send(correo);


                response.Code = 0;
                response.Result = true;
            }
            catch (Exception Ex)
            {
                logger.Info(IMDSerialize.Serialize(66823458253449, $"Error Envio de coreos", Ex));
                response.Code = 66823458253449;
                response.Message = "Ocurrió un error inesperado";
            }

            return response;
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