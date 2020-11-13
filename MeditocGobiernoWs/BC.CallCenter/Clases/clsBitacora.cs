using BC.CallCenter.Models.BE;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;

namespace BC.CallCenter.Clases
{
    public class clsBitacora
    {
        clsBitacoraBE oclsBitacoraBE;

        Database db = clsBDPersonalizada.CreateDatabase("cnxCallCenter");

        public void m_Save(string psUID, string psIDDR, string sClienteUID, string psMensaje, bool pbError = false, string sCoordenadas = "", string sFolio = "", string sNumero = "", string sTipoFolio = "", string sCP = "")
        {
            oclsBitacoraBE = new clsBitacoraBE();

            try
            {
                oclsBitacoraBE.iIdMedico = psIDDR;
                oclsBitacoraBE.sUserID = sClienteUID;

                oclsBitacoraBE.sUID = psUID;
                oclsBitacoraBE.sMensaje = psMensaje;
                oclsBitacoraBE.bError = pbError;
                oclsBitacoraBE.sCoordenadas = sCoordenadas;
                oclsBitacoraBE.sFolio = sFolio;
                oclsBitacoraBE.sNumero = sNumero;
                oclsBitacoraBE.sTipoFolio = sTipoFolio;
                oclsBitacoraBE.sCP = sCP;
                oclsBitacoraBE.m_Save(db);
            }
            catch (Exception ex)
            {

            }
        }
    }
}
