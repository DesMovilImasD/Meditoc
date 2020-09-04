using IMD.Admin.Conekta.Entities;
using IMD.Admin.Conekta.Data;
using IMD.Admin.Utilities.Business;
using IMD.Admin.Utilities.Entities;
using log4net;
using Newtonsoft.Json.Schema;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace IMD.Admin.Conekta.Business
{
    public class BusPromociones
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(BusPromociones));

        DatPromociones datPromociones;

        public BusPromociones()
        {
            datPromociones = new DatPromociones();
        }

        public IMDResponse<bool> BActivarCupon(EntCreateCupon entCreateCupon, int? piIdUsuario = null)
        {
            IMDResponse<bool> response = new IMDResponse<bool>();

            string metodo = nameof(this.BActivarCupon);
            logger.Info(IMDSerialize.Serialize(67823458189732, $"Inicia {metodo}(EntCreateCupon entCreateCupon, int? piIdUsuario = null)", entCreateCupon, piIdUsuario));

            try
            {
                if (entCreateCupon == null)
                {
                    response.Code = 8768767234634;
                    response.Message = "No se ingresó información de cupón";
                    return response;
                }
                switch (entCreateCupon.fiIdCuponCategoria)
                {
                    case (int)EnumCategoriaCupon.DescuentoMonto:
                        response = this.BDescuentoMonto(entCreateCupon, piIdUsuario);
                        break;
                    default:
                        response.Code = 7687687634563;
                        response.Message = "No se ingresó el tipo de promoción de cupón";
                        break;
                }
            }
            catch (Exception ex)
            {
                response.Code = 67823458190509;
                response.Message = "Ocurrió un error al intentar activar el cupón";

                logger.Error(IMDSerialize.Serialize(67823458190509, $"Error en {metodo}(EntCreateCupon entCreateCupon, int? piIdUsuario = null): {ex.Message}", entCreateCupon, piIdUsuario, ex, response));
            }
            return response;
        }

        public IMDResponse<bool> BAplicarCupon(int piIdCupon, int? piIdUsuario = null)
        {
            IMDResponse<bool> response = new IMDResponse<bool>();

            string metodo = nameof(this.BAplicarCupon);
            logger.Info(IMDSerialize.Serialize(67823458195948, $"Inicia {metodo}(int piIdCupon, int? piIdUsuario = null)", piIdCupon, piIdUsuario));

            try
            {
                IMDResponse<EntCupon> respuestaValidarCupon = BValidarCupon(piIdCupon: piIdCupon);
                if (respuestaValidarCupon.Code != 0)
                {
                    return respuestaValidarCupon.GetResponse<bool>();
                }

                EntCupon entCupon = new EntCupon();
                entCupon.fiIdCupon = piIdCupon;

                IMDResponse<bool> respuestaAplicarCupon = datPromociones.DSaveCupon(entCupon, piIdUsuario);
                if (respuestaAplicarCupon.Code != 0)
                {
                    return respuestaAplicarCupon;
                }

                response.Code = 0;
                response.Message = "Cupón aplicado";
                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Code = 67823458196725;
                response.Message = "Ocurrió un error al intentar aplicar el cupón";

                logger.Error(IMDSerialize.Serialize(67823458196725, $"Error en {metodo}(int piIdCupon, int? piIdUsuario = null): {ex.Message}", piIdCupon, piIdUsuario, ex, response));
            }
            return response;
        }

        public IMDResponse<EntCupon> BValidarCupon(string psCodigo = null, int? piIdCupon = null)
        {
            IMDResponse<EntCupon> response = new IMDResponse<EntCupon>();

            string metodo = nameof(this.BValidarCupon);
            logger.Info(IMDSerialize.Serialize(67823458197502, $"Inicia {metodo}(string psCodigo = null, int? piIdCupon = null)", psCodigo, piIdCupon));

            try
            {
                IMDResponse<List<EntCupon>> respuestaCupon;

                if (!string.IsNullOrWhiteSpace(psCodigo) && psCodigo?.Length >= 6)
                {
                    respuestaCupon = BObtenerCupones(psCodigo: psCodigo);
                }
                else
                {
                    if (piIdCupon != null && piIdCupon >= 10000)
                    {
                        respuestaCupon = BObtenerCupones(piIdCupon: piIdCupon);
                    }
                    else
                    {
                        response.Code = 7682736456;
                        response.Message = "No se ingresó un cupón válido";
                        return response;
                    }
                }

                if (respuestaCupon.Code != 0)
                {
                    return respuestaCupon.GetResponse<EntCupon>();
                }

                if (respuestaCupon.Result.Count == 0)
                {
                    response.Code = 7682736456;
                    response.Message = "Cupón no válido";
                    return response;
                }

                EntCupon cupon = respuestaCupon.Result.First();

                if (!cupon.fbActivo || cupon.fbBaja)
                {
                    response.Code = 736458763456;
                    response.Message = "El cupón ha expirado";
                    return response;
                }


                if (cupon.fdtFechaVencimiento != null)
                {
                    if (DateTime.Now > cupon.fdtFechaVencimiento)
                    {
                        response.Code = 87928346572345;
                        response.Message = "El cupón ha expirado";
                        return response;
                    }
                }

                if (cupon.fiTotalCanjeado >= cupon.fiTotalLanzamiento)
                {
                    response.Code = 8583745697324;
                    response.Message = "El cupón ha expirado";
                    return response;
                }

                response.Code = 0;
                response.Message = "Cupón validado";
                response.Result = cupon;
            }
            catch (Exception ex)
            {
                response.Code = 67823458198279;
                response.Message = "Ocurrió un error al intentar validar el cupón";

                logger.Error(IMDSerialize.Serialize(67823458198279, $"Error en {metodo}(string psCodigo = null, int? piIdCupon = null): {ex.Message}", psCodigo, piIdCupon, ex, response));
            }
            return response;
        }

        public IMDResponse<bool> BDeshabilitarCupon(int piIdCupon, int? piIdUsuario = null)
        {
            IMDResponse<bool> response = new IMDResponse<bool>();

            string metodo = nameof(this.BDeshabilitarCupon);
            logger.Info(IMDSerialize.Serialize(67823458199056, $"Inicia {metodo}(int piIdCupon, int? piIdUsuario = null)", piIdCupon, piIdUsuario));

            try
            {
                if (piIdCupon <= 0)
                {
                    response.Code = 7682736456;
                    response.Message = "Cupón no válido";
                    return response;
                }

                IMDResponse<bool> respuestaDeshabilitarCupon = datPromociones.DUnsuscribeCupon(piIdCupon, piIdUsuario);
                if (respuestaDeshabilitarCupon.Code != 0)
                {
                    return respuestaDeshabilitarCupon;
                }

                response.Code = 0;
                response.Message = "El cupón ha sido desactivado";
                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Code = 67823458199833;
                response.Message = "Ocurrió un error al intentar desactivar el cupón";

                logger.Error(IMDSerialize.Serialize(67823458199833, $"Error en {metodo}(int piIdCupon, int? piIdUsuario = null): {ex.Message}", piIdCupon, piIdUsuario, ex, response));
            }
            return response;
        }

        public IMDResponse<List<EntCupon>> BObtenerCupones(int? piIdCupon = null, int? piIdCuponCategoria = null, string psDescripcion = null, string psCodigo = null, DateTime? pdtFechaVencimientoInicio = null, DateTime? pdtFechaVencimientoFin = null, DateTime? pdtFechaCreacionInicio = null, DateTime? pdtFechaCreacionFin = null, bool pbActivo = true, bool pbBaja = false)
        {
            IMDResponse<List<EntCupon>> response = new IMDResponse<List<EntCupon>>();

            string metodo = nameof(this.BObtenerCupones);
            logger.Info(IMDSerialize.Serialize(67823458191286, $"Inicia {metodo}(int? piIdCupon = null, int? piIdCuponCategoria = null, string psDescripcion = null, string psCodigo = null, DateTime? pdtFechaVencimientoInicio = null, DateTime? pdtFechaVencimientoFin = null, DateTime? pdtFechaCreacionInicio = null, DateTime? pdtFechaCreacionFin = null, bool pbActivo = true, bool pbBaja = false)", piIdCupon, piIdCuponCategoria, psDescripcion, psCodigo, pdtFechaVencimientoInicio, pdtFechaVencimientoFin, pdtFechaCreacionInicio, pdtFechaCreacionFin, pbActivo, pbBaja));

            try
            {
                IMDResponse<DataTable> respuestaObtenerCupones = datPromociones.DGetCupones(piIdCupon, piIdCuponCategoria, psDescripcion, psCodigo, pdtFechaVencimientoInicio, pdtFechaVencimientoFin, pdtFechaCreacionInicio, pdtFechaCreacionFin, pbActivo, pbBaja);
                if (respuestaObtenerCupones.Code != 0)
                {
                    return respuestaObtenerCupones.GetResponse<List<EntCupon>>();
                }

                List<EntCupon> listaCupones = new List<EntCupon>();
                foreach (DataRow filaCupon in respuestaObtenerCupones.Result.Rows)
                {
                    IMDDataRow dr = new IMDDataRow(filaCupon);
                    EntCupon cupon = new EntCupon();

                    cupon.fbActivo = Convert.ToBoolean(dr.ConvertTo<int>("bActivo"));
                    cupon.fbBaja = Convert.ToBoolean(dr.ConvertTo<int>("bBaja"));
                    cupon.fdtFechaVencimiento = dr.ConvertTo<DateTime?>("dtFechaVencimiento");
                    cupon.fiIdCupon = dr.ConvertTo<int>("iIdCupon");
                    cupon.fiIdCuponCategoria = dr.ConvertTo<int>("iIdCuponCategoria");
                    cupon.fiMesBono = dr.ConvertTo<int?>("iMesBono");
                    cupon.fiTotalLanzamiento = dr.ConvertTo<int>("iTotalLanzamiento");
                    cupon.fiTotalCanjeado = dr.ConvertTo<int>("iTotalCanjeado");
                    cupon.fnMontoDescuento = dr.ConvertTo<double?>("nMontoDescuento");
                    cupon.fnPorcentajeDescuento = dr.ConvertTo<double?>("nPorcentajeDescuento");
                    cupon.fsCodigo = dr.ConvertTo<string>("sCodigo");
                    cupon.fsDescripcion = dr.ConvertTo<string>("sDescripcion");
                    cupon.fsDescripcionCategoria = dr.ConvertTo<string>("sDescripcionCategoria");

                    listaCupones.Add(cupon);
                }

                response.Code = 0;
                response.Message = "Lista de cupones obtenida";
                response.Result = listaCupones;
            }
            catch (Exception ex)
            {
                response.Code = 67823458192063;
                response.Message = "Ocurrió un error al intentar consultar los cupones";

                logger.Error(IMDSerialize.Serialize(67823458192063, $"Error en {metodo}(int? piIdCupon = null, int? piIdCuponCategoria = null, string psDescripcion = null, string psCodigo = null, DateTime? pdtFechaVencimientoInicio = null, DateTime? pdtFechaVencimientoFin = null, DateTime? pdtFechaCreacionInicio = null, DateTime? pdtFechaCreacionFin = null, bool pbActivo = true, bool pbBaja = false): {ex.Message}", piIdCupon, piIdCuponCategoria, psDescripcion, psCodigo, pdtFechaVencimientoInicio, pdtFechaVencimientoFin, pdtFechaCreacionInicio, pdtFechaCreacionFin, pbActivo, pbBaja, ex, response));
            }
            return response;
        }

        private IMDResponse<bool> BDescuentoMonto(EntCreateCupon entCreateCupon, int? piIdUsuario = null)
        {
            IMDResponse<bool> response = new IMDResponse<bool>();

            string metodo = nameof(this.BDescuentoMonto);
            logger.Info(IMDSerialize.Serialize(67823458192840, $"Inicia {metodo}(EntCreateCupon entCreateCupon, int? piIdUsuario = null)", entCreateCupon, piIdUsuario));

            try
            {
                if (entCreateCupon.fnMontoDescuento <= 0d)
                {
                    response.Code = 7687687634563;
                    response.Message = "El descuento no puede ser de monto igual o menor a $0.00 pesos";
                    return response;
                }

                //entCreateCupon.fnMontoDescuento = (int)(entCreateCupon.fnMontoDescuento * 100);

                if (string.IsNullOrWhiteSpace(entCreateCupon.fsCodigo))
                {
                    if (entCreateCupon.fiLongitudCodigo <= 0)
                    {
                        response.Code = 7687687634563;
                        response.Message = "No se especificó la longitud del código de cupón nuevo";
                        return response;
                    }

                    IMDResponse<string> respuestaGenerarCodigoCupon = this.BGenerarCodigoCupon((int)entCreateCupon.fiLongitudCodigo);
                    if (respuestaGenerarCodigoCupon.Code != 0)
                    {
                        return respuestaGenerarCodigoCupon.GetResponse<bool>();
                    }

                    entCreateCupon.fsCodigo = respuestaGenerarCodigoCupon.Result;
                }

                EntCupon entCupon = new EntCupon();

                if (entCreateCupon.fiDiasActivo > 0)
                {
                    entCupon.fdtFechaVencimiento = DateTime.Now.AddDays(entCreateCupon.fiDiasActivo);
                }

                entCupon.fiIdCuponCategoria = (int)EnumCategoriaCupon.DescuentoMonto;

                if (entCreateCupon.fiTotalLanzamiento < 1)
                {
                    response.Code = 7687687634563;
                    response.Message = "No se especificó el total de cupones a activar";
                    return response;
                }

                entCupon.fiTotalLanzamiento = entCreateCupon.fiTotalLanzamiento;
                entCupon.fnMontoDescuento = entCreateCupon.fnMontoDescuento;
                entCupon.fsCodigo = entCreateCupon.fsCodigo;
                entCupon.fsDescripcion = entCreateCupon.fsDescripcion;

                IMDResponse<bool> respuestaGuardarCupon = datPromociones.DSaveCupon(entCupon, piIdUsuario);
                if (respuestaGuardarCupon.Code != 0)
                {
                    return respuestaGuardarCupon;
                }

                response.Code = 0;
                response.Message = "El cupón se guardó correctamente";
                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Code = 67823458193617;
                response.Message = "Ocurrió un error al guardar el cupón";

                logger.Error(IMDSerialize.Serialize(67823458193617, $"Error en {metodo}(EntCreateCupon entCreateCupon, int? piIdUsuario = null): {ex.Message}", entCreateCupon, piIdUsuario, ex, response));
            }
            return response;
        }

        private IMDResponse<string> BGenerarCodigoCupon(int piLongitud)
        {
            IMDResponse<string> response = new IMDResponse<string>();

            string metodo = nameof(this.BGenerarCodigoCupon);
            logger.Info(IMDSerialize.Serialize(67823458194394, $"Inicia {metodo}(int piLongitud)", piLongitud));

            try
            {
                if (piLongitud < 6)
                {
                    response.Code = 88473423456872;
                    response.Message = "La longitud mínima para un cupón es de 6 caractéres";
                    return response;
                }

                IMDResponse<int> respuestaNuevoID = this.BNuevoIdCupon();
                if (respuestaNuevoID.Code != 0)
                {
                    return respuestaNuevoID.GetResponse<string>();
                } 

                IMDEndec iMDEndec = new IMDEndec();

                string sCodigoEncr = iMDEndec.BEncrypt(respuestaNuevoID.Result.ToString(), "abcdefghijklmnop", "abcdefgh")?.Result;

                sCodigoEncr = sCodigoEncr?.Replace("+", "")?.Replace("=", "")?.Replace("/", "")?.Substring(0, piLongitud)?.ToUpper();

                if (string.IsNullOrWhiteSpace(sCodigoEncr))
                {
                    response.Code = 88473423456872;
                    response.Message = "No se pudo generar el código del cupón";
                    return response;
                }
                response.Code = 0;
                response.Result = sCodigoEncr;
            }
            catch (Exception ex)
            {
                response.Code = 67823458195171;
                response.Message = "Ocurrió un error al generar el cupón";

                logger.Error(IMDSerialize.Serialize(67823458195171, $"Error en {metodo}(int piLongitud): {ex.Message}", piLongitud, ex, response));
            }
            return response;
        }

        public IMDResponse<int> BNuevoIdCupon()
        {
            IMDResponse<int> response = new IMDResponse<int>();

            string metodo = nameof(this.BNuevoIdCupon);
            logger.Info(IMDSerialize.Serialize(67823458211488, $"Inicia {metodo}"));

            try
            {
                IMDResponse<DataTable> respuestaObtenerId = datPromociones.CGetNewCouponID();
                if (respuestaObtenerId.Code != 0)
                {
                    return respuestaObtenerId.GetResponse<int>();
                }

                IMDDataRow dr = new IMDDataRow(respuestaObtenerId.Result.Rows[0]);

                int nuevoCuponID = dr.ConvertTo<int>("iIdCupon");

                if(nuevoCuponID == 0)
                {
                    response.Code = 87674367568658;
                    response.Message = "No se pudo generar el nuevo cupón";
                    return response;
                }

                response.Code = 0;
                response.Result = nuevoCuponID;
            }
            catch (Exception ex)
            {
                response.Code = 67823458212265;
                response.Message = "Ocurrió un error inesperado";

                logger.Error(IMDSerialize.Serialize(67823458212265, $"Error en {metodo}: {ex.Message}", ex, response));
            }
            return response;
        }
    }
}