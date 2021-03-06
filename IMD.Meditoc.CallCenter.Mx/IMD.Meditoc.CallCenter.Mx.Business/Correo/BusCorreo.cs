﻿using IMD.Admin.Utilities.Business;
using IMD.Admin.Utilities.Entities;
using IMD.Meditoc.CallCenter.Mx.Data.Correo;
using IMD.Meditoc.CallCenter.Mx.Entities;
using IMD.Meditoc.CallCenter.Mx.Entities.Correo;
using log4net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Net;
using System.Net.Mail;

namespace IMD.Meditoc.CallCenter.Mx.Business.Correo
{
    public class BusCorreo
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(BusCorreo));
        DatCorreo datCorreo;
        public BusCorreo()
        {
            datCorreo = new DatCorreo();
        }

        public EntCorreo entCorreo = new EntCorreo();

        /// <summary>
        /// Método para el envio de correos
        /// </summary>
        /// <param name="sTipo">Tipo de correo a enviar</param>
        /// <param name="sMailTo">Correo destino</param>
        /// <param name="formatos">Lista de archivos a adjuntar para enviar</param>

        private void BDatosEnvioMailINC(string sTipo, string sUsuario, string sClave, string sAsunto, string sMensaje)
        {
            try
            {

                entCorreo.sServerMail = ConfigurationManager.AppSettings["ServerMail"].ToString();

                entCorreo.bSSLMail = Convert.ToBoolean(ConfigurationManager.AppSettings["SSLMail"]);

                entCorreo.iPortMail = Convert.ToInt16(ConfigurationManager.AppSettings["PortMail"]);


                sUsuario = ConfigurationManager.AppSettings["UserMail_GENERAL"];

                sClave = ConfigurationManager.AppSettings["PassMail_GENERAL"];


                entCorreo.sUserMail = sUsuario;
                entCorreo.sPassMail = sClave;
                entCorreo.sAsuntoMail = sAsunto;
                entCorreo.sMensajeMail = sMensaje;
                entCorreo.bAdjuntarFile = true;
            }
            catch (Exception a)
            {
                throw new ArgumentException("No se pudo Enviar el Correo. " + a.Message);
            }
        }

        public IMDResponse<bool> BEnviarEmail(string sTipo, string sUsuario, string sClave, string sAsunto, string sMensaje, string sMailTo, string sFile, string sContentType)
        {
            entCorreo.sFile = sFile;
            IMDResponse<bool> response = new IMDResponse<bool>();

            try
            {

                this.BDatosEnvioMailINC(sTipo, sUsuario, sClave, sAsunto, sMensaje);
                MailMessage correo = new MailMessage();
                correo.From = new MailAddress(entCorreo.sUserMail);
                correo.To.Add(sMailTo);

                correo.Subject = sAsunto;
                correo.Body = sMensaje;
                correo.IsBodyHtml = true;
                correo.Priority = MailPriority.Normal;

                correo.Headers.Add("Disposition-Notification-To", entCorreo.sUserMail);

                SmtpClient smtp = new SmtpClient();
                smtp.Host = entCorreo.sServerMail;
                smtp.Port = entCorreo.iPortMail;
                smtp.Credentials = new NetworkCredential(entCorreo.sUserMail, entCorreo.sPassMail);
                smtp.EnableSsl = entCorreo.bSSLMail;

                logger.Info(IMDSerialize.Serialize(66823458253448, $"Inicia Envio de coreos", entCorreo));
                //logger.Info(IMDSerialize.Serialize(66823458253447, $"Inicia Envio de coreos", smtp));
                logger.Info(IMDSerialize.Serialize(66823458253445, $"Inicia Envio de coreos", correo));

                smtp.Send(correo);


                response.Code = 0;
                response.Result = true;
            }
            catch (Exception Ex)
            {
                response.Code = 66823458253449;
                response.Message = "Ocurrió un error inesperado al enviar el correo electrónico.";

                logger.Info(IMDSerialize.Serialize(66823458253449, $"Error Envio de coreos", Ex, response));
            }

            return response;
        }


        public void BEnviarMailArchivo(string sTipo, string sUsuario, string sClave, string sAsunto, string sMensaje, string sMailTo, List<string> sFile, string sContentType, bool bfile)
        {
            try
            {
                this.BDatosEnvioMailINC(sTipo, sUsuario, sClave, sAsunto, sMensaje);
                MailMessage correo = new MailMessage();
                correo.From = new MailAddress(entCorreo.sUserMail);
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
                correo.Subject = entCorreo.sAsuntoMail;
                correo.Body = entCorreo.sMensajeMail;
                correo.IsBodyHtml = true;
                correo.Priority = MailPriority.Normal;

                correo.Headers.Add("Disposition-Notification-To", entCorreo.sUserMail);

                SmtpClient smtp = new SmtpClient();
                smtp.Host = entCorreo.sServerMail;
                smtp.Port = entCorreo.iPortMail;
                smtp.Credentials = new NetworkCredential(entCorreo.sUserMail, entCorreo.sPassMail);
                smtp.EnableSsl = entCorreo.bSSLMail;



                smtp.Send(correo);

            }
            catch (Exception Ex)
            {
                throw new ArgumentException("No se pudo Enviar el Correo. " + Ex.Message);
            }
        }

        public string BCargaTxt(string path, ref string serror)
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

        /// <summary>
        /// Guardar el correo de una orden comprada
        /// </summary>
        /// <param name="psOrderId"></param>
        /// <param name="psBody"></param>
        /// <param name="psTo"></param>
        /// <param name="psSubject"></param>
        /// <returns></returns>
        public IMDResponse<bool> BSaveCorreo(string psOrderId, string psBody, string psTo, string psSubject)
        {
            IMDResponse<bool> response = new IMDResponse<bool>();

            string metodo = nameof(this.BSaveCorreo);
            logger.Info(IMDSerialize.Serialize(67823458627183, $"Inicia {metodo}(string psOrderId, string psBody, string psTo, string psSubject)", psOrderId, psTo, psSubject));

            try
            {
                if (string.IsNullOrWhiteSpace(psOrderId))
                {
                    response.Code = -3468763467;
                    response.Message = "No se ingresó la orden para guardar el correo.";
                    return response;
                }
                IMDResponse<bool> resSaveCorreo = datCorreo.DSaveCorreo(psOrderId, psBody, psTo, psSubject);
                if (resSaveCorreo.Code != 0)
                {
                    return resSaveCorreo;
                }

                response.Code = 0;
                response.Message = "El correo de la orden ha sido guardada correctamente.";
                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Code = 67823458627960;
                response.Message = "Ocurrió un error inesperado al guardar el correo de la orden.";

                logger.Error(IMDSerialize.Serialize(67823458627960, $"Error en {metodo}(string psOrderId, string psBody, string psTo, string psSubject): {ex.Message}", psOrderId, psTo, psSubject, ex, response));
            }
            return response;
        }

        /// <summary>
        /// Reenviar el correo de una orden de compra
        /// </summary>
        /// <param name="psOrderId"></param>
        /// <returns></returns>
        public IMDResponse<bool> BReenviarCorreo(string psOrderId)
        {
            IMDResponse<bool> response = new IMDResponse<bool>();

            string metodo = nameof(this.BReenviarCorreo);
            logger.Info(IMDSerialize.Serialize(67823458628737, $"Inicia {metodo}(string psOrderId)", psOrderId));

            try
            {
                if (string.IsNullOrWhiteSpace(psOrderId))
                {
                    response.Code = -345673;
                    response.Message = "No se especificó la orden para guardar el correo.";
                    return response;
                }

                IMDResponse<DataTable> resGetData = datCorreo.DGetCorreo(psOrderId);
                if (resGetData.Code != 0)
                {
                    return resGetData.GetResponse<bool>();
                }

                if (resGetData.Result.Rows.Count != 1)
                {
                    response.Code = -345673;
                    response.Message = "La orden no existe en el registro de correos del sistema.";
                    return response;
                }

                IMDDataRow dr = new IMDDataRow(resGetData.Result.Rows[0]);

                EntOrderEmail entOrderEmail = new EntOrderEmail
                {
                    sBody = dr.ConvertTo<string>("sBody"),
                    sOrderId = dr.ConvertTo<string>("sOrderId"),
                    sSubject = dr.ConvertTo<string>("sSubject"),
                    sTo = dr.ConvertTo<string>("sTo"),
                };

                IMDResponse<bool> resEnviar = this.BEnviarEmail("", "", "", entOrderEmail.sSubject + " (Reenviado)", entOrderEmail.sBody, entOrderEmail.sTo, "", "");
                if (resEnviar.Code != 0)
                {
                    return resEnviar;
                }

                response.Code = 0;
                response.Message = $"Se ha reenviado el detalle de la orden a {entOrderEmail.sTo}.";
                response.Result = true;

            }
            catch (Exception ex)
            {
                response.Code = 67823458629514;
                response.Message = "Ocurrió un error inesperado al reenviar el correo de la orden al cliente";

                logger.Error(IMDSerialize.Serialize(67823458629514, $"Error en {metodo}(string psOrderId): {ex.Message}", psOrderId, ex, response));
            }
            return response;
        }
    }
}
