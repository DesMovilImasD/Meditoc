using IMD.Admin.Conekta.Business;
using IMD.Admin.Conekta.Entities;
using IMD.Admin.Utilities.Business;
using IMD.Admin.Utilities.Entities;
using IMD.Meditoc.CallCenter.Mx.Business.CGU;
using IMD.Meditoc.CallCenter.Mx.Business.Producto;
using IMD.Meditoc.CallCenter.Mx.Data.Folio;
using IMD.Meditoc.CallCenter.Mx.Entities;
using IMD.Meditoc.CallCenter.Mx.Entities.Folio;
using IMD.Meditoc.CallCenter.Mx.Entities.Ordenes;
using IMD.Meditoc.CallCenter.Mx.Entities.Paciente;
using IMD.Meditoc.CallCenter.Mx.Entities.Producto;
using log4net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMD.Meditoc.CallCenter.Mx.Business.Folio
{
    public class BusFolio
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(BusFolio));
        DatFolio datFolio;

        public BusFolio()
        {
            datFolio = new DatFolio();
        }

        public IMDResponse<EntDetalleCompra> CSaveNuevoFolio(EntConecktaPago entConecktaPago)
        {
            IMDResponse<EntDetalleCompra> response = new IMDResponse<EntDetalleCompra>();

            string metodo = nameof(this.CSaveNuevoFolio);
            logger.Info(IMDSerialize.Serialize(67823458415062, $"Inicia {metodo}"));

            try
            {
                BusProducto busProducto = new BusProducto();
                IMDResponse<List<EntProducto>> lstProductos = busProducto.BObtenerProductos(null);


                for (int i = 0; i < entConecktaPago.lstLineItems.Count; i++)
                {
                    EntProducto oProducto = lstProductos.Result
                        .Find(x => x.iIdProducto == entConecktaPago.lstLineItems[i].product_id);

                    if (oProducto == null)
                    {
                        response.Code = 67823458415839;
                        response.Message = "Producto no válido.";
                        return response;
                    }

                    entConecktaPago.lstLineItems[i].monthsExpiration = oProducto.iMesVigencia;
                    entConecktaPago.lstLineItems[i].name = oProducto.sNombre;
                    entConecktaPago.lstLineItems[i].unit_price = (int)(oProducto.fCosto * 100);
                }

                //Se manda a llamar la creaciión de orden de conekta
                BusOrder busOrder = new BusOrder();

                EntCreateOrder entCreateOrder = new EntCreateOrder();

                string datosSerealizados = JsonConvert.SerializeObject(entConecktaPago);
                entCreateOrder = JsonConvert.DeserializeObject<EntCreateOrder>(datosSerealizados);

                IMDResponse<EntOrder> entOrder = busOrder.BCreateOrder(entCreateOrder);

                if (entOrder.Code == 0)
                {
                    response = BGuardarCompraUnica(entOrder.Result, entConecktaPago);
                }

                response.Code = 0;
                response.Message = "Operación exitosa.";
            }
            catch (Exception ex)
            {
                response.Code = 67823458415839;
                response.Message = "Ocurrió un error inesperado";

                logger.Error(IMDSerialize.Serialize(67823458415839, $"Error en {metodo}: {ex.Message}", ex, response));
            }
            return response;
        }

        public IMDResponse<EntDetalleCompra> BGuardarCompraUnica(EntOrder entOrder, EntConecktaPago entConecktaPago)
        {
            IMDResponse<EntDetalleCompra> response = new IMDResponse<EntDetalleCompra>();

            string metodo = nameof(this.BGuardarCompraUnica);
            logger.Info(IMDSerialize.Serialize(67823458416616, $"Inicia {metodo}"));

            try
            {
                //Se crea el folio
                EntFolio entFolio = new EntFolio();
                entFolio.iIdEmpresa = 0;
                entFolio.iIdOrigen = 0;
                entFolio.bTerminosYCondiciones = false;
                entFolio.sOrdenConekta = entOrder.id;

                //Se crea el paciente
                EntPaciente entPaciente = new EntPaciente();

                entPaciente.iIdPaciente = 0;
                entPaciente.iIdFolio = entFolio.iIdFolio;
                entPaciente.sTelefono = entConecktaPago.pacienteUnico.sTelefono;
                entPaciente.sCorreo = entConecktaPago.pacienteUnico.sCorreo;
                entPaciente.sNombre = entConecktaPago.pacienteUnico.sNombre;


                entFolio.iConsecutivo = entConecktaPago.lstLineItems.Count;

                foreach (line_items item in entConecktaPago.lstLineItems)
                {
                    for (int i = 0; i < item.quantity; i++)
                    {
                        entFolio.iIdProducto = item.product_id;

                        if (item.monthsExpiration == 0)
                        {
                            entFolio.dtFechaVencimiento = DateTime.Now.AddDays(Convert.ToInt16(ConfigurationManager.AppSettings["iDiasDespuesVencimiento"]));
                        }

                        if (item.monthsExpiration != 0)
                        {
                            entFolio.dtFechaVencimiento = DateTime.Now.AddMonths(item.monthsExpiration);
                        }


                        BusGeneratePassword busGenerate = new BusGeneratePassword();
                        BusUsuario busUsuario = new BusUsuario();

                        entFolio.sPassword = busGenerate.Generate(6);

                        entFolio.sPassword = busUsuario.BEncodePassword(entFolio.sPassword);

                        //Se agrega el folio

                        IMDResponse<DataTable> dtFolio = datFolio.DSaveFolio(entFolio);

                        if (dtFolio.Code != 0)
                        {
                            response = dtFolio.GetResponse<EntDetalleCompra>();
                            return response;
                        }

                        //Se agrega el paciente


                    }
                }

            }
            catch (Exception ex)
            {
                response.Code = 67823458417393;
                response.Message = "Ocurrió un error inesperado";

                logger.Error(IMDSerialize.Serialize(67823458417393, $"Error en {metodo}: {ex.Message}", ex, response));
            }
            return response;
        }
    }
}
