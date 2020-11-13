using BC.CallCenter.Clases;
using BC.CallCenter.NuevaImplementacion.Data;
using BC.CallCenter.NuevaImplementacion.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BC.CallCenter.NuevaImplementacion.Business
{
    public class BitacoraBusiness
    {
        BitacoraData oBitacoraD = new BitacoraData();
        BitacoraDTO oBitacoraDTO;

        public void save(int iIdAcceso = 0, int iIdEncuesta = 0, int iIdLlamada = 0, string sEstatus = "", string sMensaje = "", string sCoordenadas = "")
        {
            try
            {
                oBitacoraDTO = new BitacoraDTO
                {
                    iIdAcceso = iIdAcceso,
                    iIdEncuesta = iIdEncuesta,
                    iIdLlamada = iIdLlamada,
                    sEstatus = sEstatus,
                    sMensaje = sMensaje,
                    sCoordenadas = sCoordenadas,
                    dtFechaCreacion = DateTime.Now
                };

                oBitacoraD.save(oBitacoraDTO);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public int SaveTrazado(int iIdAcceso = 0, string sMensaje = "", string sCoordenadas = "")
        {
            try
            {
                oBitacoraDTO = new BitacoraDTO
                {
                    iIdAcceso = iIdAcceso,
                    iIdEncuesta = 0,
                    iIdLlamada = 0,
                    sEstatus = clsEnums.sDescripcionEnum(clsEnums.enumEstatusBitacora.TRAZADO),
                    sMensaje = sMensaje,
                    sCoordenadas = sCoordenadas,
                    dtFechaCreacion = DateTime.Now
                };

                oBitacoraDTO.iIdBitacora = oBitacoraD.saveTrazado(oBitacoraDTO);

            }
            catch (Exception)
            {

                throw;
            }
            return oBitacoraDTO.iIdBitacora;
        }
    }
}
