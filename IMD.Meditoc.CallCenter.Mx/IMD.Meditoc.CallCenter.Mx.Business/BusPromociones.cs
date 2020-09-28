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
using IMD.Admin.Conekta.Entities.Promotions;

namespace IMD.Admin.Conekta.Business
{
    public class BusPromociones
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(BusPromociones));

        DatPromociones datPromociones;

        public BusPromociones(string appToken, string appKey)
        {
            datPromociones = new DatPromociones(appToken, appKey);
        }

        /// <summary>
        /// Función: Guarda un nuevo cupón en la base
        /// Creado: Cristopher Noh 28/07/2020
        /// Modificado:
        /// </summary>
        /// <param name="entCreateCupon">Entidad de cupón nuevo</param>
        /// <param name="piIdUsuario">Usuario que registra</param>
        /// <returns></returns>
        public IMDResponse<bool> BActivarCupon(EntCreateCupon entCreateCupon, int? piIdUsuario = null)
        {
            IMDResponse<bool> response = new IMDResponse<bool>();

            string metodo = nameof(this.BActivarCupon);
            logger.Info(IMDSerialize.Serialize(67823458189732, $"Inicia {metodo}(EntCreateCupon entCreateCupon, int? piIdUsuario = null)", entCreateCupon, piIdUsuario));

            try
            {
                if (entCreateCupon == null)
                {
                    response.Code = 67823458220812;
                    response.Message = "No se ingresó información de cupón";
                    return response;
                }


                if (string.IsNullOrWhiteSpace(entCreateCupon.fsCodigo))
                {
                    if (entCreateCupon.fiLongitudCodigo <= 0)
                    {
                        response.Code = 67823458221589;
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
                else
                {
                    entCreateCupon.fsCodigo = entCreateCupon.fsCodigo.Replace(" ", "").ToUpper();

                    IMDResponse<List<EntCupon>> respuestaVerificarCodigo = BObtenerCupones(psCodigo: entCreateCupon.fsCodigo);
                    if (respuestaVerificarCodigo.Code != 0)
                    {
                        return respuestaVerificarCodigo.GetResponse<bool>();
                    }
                    if (respuestaVerificarCodigo.Result.Count > 0)
                    {
                        response.Code = 67823458222366;
                        response.Message = "Ya hay un cupón activo con el código proporcionado";
                        return response;
                    }
                }

                EntCupon entCupon = new EntCupon();

                if (entCreateCupon.fiDiasActivo != 0 && entCreateCupon.fiDiasActivo != null)
                {
                    if (entCreateCupon.fiDiasActivo > 0)
                    {
                        entCupon.fdtFechaVencimiento = DateTime.Now.AddDays((int)entCreateCupon.fiDiasActivo);
                    }
                    else
                    {
                        response.Code = 67823458223143;
                        response.Message = "Los días activos del cupón deben ser mayores o iguales a 1 día. Si no requiere días de vencimiento, deje el campo vacío";
                        return response;
                    }
                }


                if (entCreateCupon.fiTotalLanzamiento < 1)
                {
                    response.Code = 67823458223920;
                    response.Message = "No se especificó el total de cupones a activar. El total de lanzamiento debe ser mínimo 1 cupón";
                    return response;
                }


                entCupon.fiTotalLanzamiento = entCreateCupon.fiTotalLanzamiento;
                entCupon.fsCodigo = entCreateCupon.fsCodigo;
                entCupon.fsDescripcion = entCreateCupon.fsDescripcion;

                switch (entCreateCupon.fiIdCuponCategoria)
                {
                    case (int)EnumCategoriaCupon.DescuentoMonto:
                        if (entCreateCupon.fnMontoDescuento <= 0d || entCreateCupon.fnMontoDescuento == null)
                        {
                            response.Code = 67823458224697;
                            response.Message = "El descuento no puede ser de monto igual o menor a $0.00 pesos";
                            return response;
                        }

                        entCupon.fiIdCuponCategoria = (int)EnumCategoriaCupon.DescuentoMonto;
                        entCupon.fnMontoDescuento = entCreateCupon.fnMontoDescuento;

                        break;

                    case (int)EnumCategoriaCupon.DescuentoPorcentaje:
                        if (entCreateCupon.fnPorcentajeDescuento >= 1)
                        {
                            entCreateCupon.fnPorcentajeDescuento /= 100;
                        }
                        if (entCreateCupon.fnPorcentajeDescuento <= 0d || entCreateCupon.fnPorcentajeDescuento > 1 || entCreateCupon.fnPorcentajeDescuento == null)
                        {
                            response.Code = 67823458225474;
                            response.Message = "El porcentaje de descuento no puede ser 0% o más de 100%";
                            return response;
                        }

                        entCupon.fiIdCuponCategoria = (int)EnumCategoriaCupon.DescuentoPorcentaje;
                        entCupon.fnPorcentajeDescuento = entCreateCupon.fnPorcentajeDescuento;

                        break;

                    default:
                        response.Code = 67823458226251;
                        response.Message = "No se ingresó el tipo de promoción de cupón";
                        break;
                }

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
                response.Code = 67823458190509;
                response.Message = "Ocurrió un error al intentar activar el cupón";

                logger.Error(IMDSerialize.Serialize(67823458190509, $"Error en {metodo}(EntCreateCupon entCreateCupon, int? piIdUsuario = null): {ex.Message}", entCreateCupon, piIdUsuario, ex, response));
            }
            return response;
        }

        /// <summary>
        /// Función: Aplica el cupón guardando en el campo de cupones canjeado
        /// Creado: Cristopher Noh 28/07/2020
        /// Modificado:
        /// </summary>
        /// <param name="piIdCupon">Cupón a aplicar</param>
        /// <param name="piIdUsuario">Usuario que aplica</param>
        /// <returns></returns>
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
                response.Message = "Código aplicado";
                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Code = 67823458196725;
                response.Message = "Ocurrió un error al intentar aplicar el código";

                logger.Error(IMDSerialize.Serialize(67823458196725, $"Error en {metodo}(int piIdCupon, int? piIdUsuario = null): {ex.Message}", piIdCupon, piIdUsuario, ex, response));
            }
            return response;
        }

        /// <summary>
        /// Función: Validar el cupón a canjear
        /// Creado: Cristopher Noh 28/07/2020
        /// Modificado:
        /// </summary>
        /// <param name="psCodigo">Código de cupón</param>
        /// <param name="piIdCupon">Id de cupón</param>
        /// <returns>Datos del cupón</returns>
        public IMDResponse<EntCupon> BValidarCupon(string psCodigo = null, int? piIdCupon = null)
        {
            IMDResponse<EntCupon> response = new IMDResponse<EntCupon>();

            string metodo = nameof(this.BValidarCupon);
            logger.Info(IMDSerialize.Serialize(67823458197502, $"Inicia {metodo}(string psCodigo = null, int? piIdCupon = null)", psCodigo, piIdCupon));

            try
            {
                IMDResponse<List<EntCupon>> respuestaCupon;

                if (!string.IsNullOrWhiteSpace(psCodigo) && psCodigo?.Length > 0)
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
                        response.Code = 67823458227028;
                        response.Message = "No se ingresó un código válido";
                        return response;
                    }
                }

                if (respuestaCupon.Code != 0)
                {
                    return respuestaCupon.GetResponse<EntCupon>();
                }

                if (respuestaCupon.Result.Count == 0)
                {
                    response.Code = 67823458227805;
                    response.Message = "Código no válido";
                    return response;
                }

                EntCupon cupon = respuestaCupon.Result.First();
                string mensajeCodigoExpirado = "El código ha expirado";

                if (!cupon.fbActivo || cupon.fbBaja)
                {
                    response.Code = 67823458228582;
                    response.Message = mensajeCodigoExpirado;
                    return response;
                }


                if (cupon.fdtFechaVencimiento != null)
                {
                    if (DateTime.Now > cupon.fdtFechaVencimiento)
                    {
                        response.Code = 67823458229359;
                        response.Message = mensajeCodigoExpirado;
                        return response;
                    }
                }

                if (cupon.fiTotalCanjeado >= cupon.fiTotalLanzamiento)
                {
                    response.Code = 67823458230136;
                    response.Message = mensajeCodigoExpirado;
                    return response;
                }

                response.Code = 0;
                response.Message = "Código validado";
                response.Result = cupon;
            }
            catch (Exception ex)
            {
                response.Code = 67823458198279;
                response.Message = "Ocurrió un error al intentar validar el código";

                logger.Error(IMDSerialize.Serialize(67823458198279, $"Error en {metodo}(string psCodigo = null, int? piIdCupon = null): {ex.Message}", psCodigo, piIdCupon, ex, response));
            }
            return response;
        }

        /// <summary>
        /// Función: Dar de baja el cupón con el ID proporcionado
        /// Creado: Cristopher Noh 28/07/2020
        /// Modificado:
        /// </summary>
        /// <param name="piIdCupon">ID de cupón</param>
        /// <param name="piIdUsuario">Usuario que da de baja</param>
        /// <returns></returns>
        public IMDResponse<bool> BDeshabilitarCupon(int piIdCupon, int? piIdUsuario = null)
        {
            IMDResponse<bool> response = new IMDResponse<bool>();

            string metodo = nameof(this.BDeshabilitarCupon);
            logger.Info(IMDSerialize.Serialize(67823458199056, $"Inicia {metodo}(int piIdCupon, int? piIdUsuario = null)", piIdCupon, piIdUsuario));

            try
            {
                if (piIdCupon <= 0)
                {
                    response.Code = 67823458230913;
                    response.Message = "Código no válido";
                    return response;
                }

                IMDResponse<bool> respuestaDeshabilitarCupon = datPromociones.DUnsuscribeCupon(piIdCupon, piIdUsuario);
                if (respuestaDeshabilitarCupon.Code != 0)
                {
                    return respuestaDeshabilitarCupon;
                }

                response.Code = 0;
                response.Message = "El código ha sido desactivado";
                response.Result = true;
            }
            catch (Exception ex)
            {
                response.Code = 67823458199833;
                response.Message = "Ocurrió un error al intentar desactivar el código";

                logger.Error(IMDSerialize.Serialize(67823458199833, $"Error en {metodo}(int piIdCupon, int? piIdUsuario = null): {ex.Message}", piIdCupon, piIdUsuario, ex, response));
            }
            return response;
        }

        /// <summary>
        /// Función: Obtiene la lista de cupones
        /// Creado: Cristopher Noh 28/07/2020
        /// Modificado:
        /// </summary>
        /// <param name="piIdCupon">Id de cupón</param>
        /// <param name="piIdCuponCategoria">Categoría del cupón</param>
        /// <param name="psDescripcion">Descripción del cupón</param>
        /// <param name="psCodigo">Código de cupón</param>
        /// <param name="pdtFechaVencimientoInicio">Fecha de vencimiento desde...</param>
        /// <param name="pdtFechaVencimientoFin">..a la Fecha de vencimiento</param>
        /// <param name="pdtFechaCreacionInicio">Fecha de creación desde...</param>
        /// <param name="pdtFechaCreacionFin">...a la fecha de creación</param>
        /// <param name="pbActivo">Cupón activo</param>
        /// <param name="pbBaja">Cupón inactivo</param>
        /// <returns></returns>
        public IMDResponse<List<EntCupon>> BObtenerCupones(int? piIdCupon = null, int? piIdCuponCategoria = null, string psDescripcion = null, string psCodigo = null, DateTime? pdtFechaVencimientoInicio = null, DateTime? pdtFechaVencimientoFin = null, DateTime? pdtFechaCreacionInicio = null, DateTime? pdtFechaCreacionFin = null, bool? pbActivo = true, bool? pbBaja = false)
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
                    cupon.sFechaVencimiento = cupon.fdtFechaVencimiento == null ? "Hasta agotar" : ((DateTime)cupon.fdtFechaVencimiento).ToString("dd/MM/yy HH:mm");
                    cupon.dtFechaCreacion = dr.ConvertTo<DateTime?>("dtFechaCreacion");
                    cupon.sFechaCreacion = cupon.dtFechaCreacion == null ? null : ((DateTime)cupon.dtFechaCreacion).ToString("dd/MM/yy HH:mm");
                    cupon.fiIdCupon = dr.ConvertTo<int>("iIdCupon");
                    cupon.fiIdCuponCategoria = dr.ConvertTo<int>("iIdCuponCategoria");
                    cupon.fiMesBono = dr.ConvertTo<int?>("iMesBono");
                    cupon.fiTotalLanzamiento = dr.ConvertTo<int>("iTotalLanzamiento");
                    cupon.fiTotalCanjeado = dr.ConvertTo<int>("iTotalCanjeado");
                    cupon.fnMontoDescuento = dr.ConvertTo<double?>("nMontoDescuento");
                    cupon.sMontoDescuento = cupon.fnMontoDescuento == null ? null : ((double)cupon.fnMontoDescuento / 100d).ToString("C");
                    cupon.fnPorcentajeDescuento = dr.ConvertTo<double?>("nPorcentajeDescuento");
                    cupon.sPorcentajeDescuento = cupon.fnPorcentajeDescuento == null ? null: ((double)cupon.fnPorcentajeDescuento).ToString("#%");
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

        /// <summary>
        /// Función: Genera un código de cupón aleatorio con la longitud proporcionada
        /// Creado: Cristopher Noh 28/07/2020
        /// Modificado:
        /// </summary>
        /// <param name="piLongitud"></param>
        /// <returns></returns>
        private IMDResponse<string> BGenerarCodigoCupon(int piLongitud)
        {
            IMDResponse<string> response = new IMDResponse<string>();

            string metodo = nameof(this.BGenerarCodigoCupon);
            logger.Info(IMDSerialize.Serialize(67823458194394, $"Inicia {metodo}(int piLongitud)", piLongitud));

            try
            {
                if (piLongitud < 6)
                {
                    response.Code = 67823458231690;
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

                sCodigoEncr = sCodigoEncr?.Replace("+", "")?.Replace("=", "")?.Replace("/", "");
                sCodigoEncr = sCodigoEncr?.Substring(0, piLongitud > sCodigoEncr.Length ? sCodigoEncr.Length : piLongitud)?.ToUpper();

                if (string.IsNullOrWhiteSpace(sCodigoEncr))
                {
                    response.Code = 67823458232467;
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

        /// <summary>
        /// Función: Genera un ID de cupón
        /// Creado: Cristopher Noh 28/07/2020
        /// Modificado:
        /// </summary>
        /// <returns></returns>
        public IMDResponse<int> BNuevoIdCupon()
        {
            IMDResponse<int> response = new IMDResponse<int>();

            string metodo = nameof(this.BNuevoIdCupon);
            logger.Info(IMDSerialize.Serialize(67823458211488, $"Inicia {metodo}()"));

            try
            {
                IMDResponse<DataTable> respuestaObtenerId = datPromociones.DGetNewCouponID();
                if (respuestaObtenerId.Code != 0)
                {
                    return respuestaObtenerId.GetResponse<int>();
                }

                IMDDataRow dr = new IMDDataRow(respuestaObtenerId.Result.Rows[0]);

                int nuevoCuponID = dr.ConvertTo<int>("iIdCupon");

                if (nuevoCuponID == 0)
                {
                    response.Code = 67823458233244;
                    response.Message = "No se pudo generar el nuevo cupón";
                    return response;
                }

                response.Code = 0;
                response.Result = nuevoCuponID;
            }
            catch (Exception ex)
            {
                response.Code = 67823458212265;
                response.Message = "Ocurrió un error inesperado al generar el nuevo ID del cupón";

                logger.Error(IMDSerialize.Serialize(67823458212265, $"Error en {metodo}(): {ex.Message}", ex, response));
            }
            return response;
        }

        /// <summary>
        /// Función: Valida si un cupón ya ha sido aplicado por un usuario al momento de realizar la compra
        /// Creado: Cristopher Noh 28/07/2020
        /// Modificado:
        /// </summary>
        /// <param name="piIdCupon">ID del cupón</param>
        /// <param name="psEmail">Correo del cliente que aplica</param>
        /// <returns></returns>
        public IMDResponse<bool> BGetCuponUsed(int piIdCupon, string psEmail)
        {
            IMDResponse<bool> response = new IMDResponse<bool>();

            string metodo = nameof(this.BGetCuponUsed);
            logger.Info(IMDSerialize.Serialize(67823458214596, $"Inicia {metodo}(int piIdCupon, string psEmail)", piIdCupon, psEmail));

            try
            {
                if (piIdCupon <= 0 || string.IsNullOrWhiteSpace(psEmail))
                {
                    response.Code = 67823458234021;
                    response.Message = "No se ingresaron datos completos";
                    return response;
                }

                IMDResponse<DataTable> respuestaValidarCuponEmail = datPromociones.DGetCuponUsed(piIdCupon, psEmail);
                if (respuestaValidarCuponEmail.Code != 0)
                {
                    return respuestaValidarCuponEmail.GetResponse<bool>();
                }

                if (respuestaValidarCuponEmail.Result.Rows.Count > 0)
                {
                    response.Code = 0;
                    response.Result = false;
                    response.Message = "El usuario ya ha aplicado este código";
                }
                else
                {
                    response.Code = 0;
                    response.Result = true;
                    response.Message = "Código aplicable al usuario";
                }
            }
            catch (Exception ex)
            {
                response.Code = 67823458215373;
                response.Message = "Ocurrió un error inesperado al verificar el código aplicado";

                logger.Error(IMDSerialize.Serialize(67823458215373, $"Error en {metodo}(int piIdCupon, string psEmail): {ex.Message}", piIdCupon, psEmail, ex, response));
            }
            return response;
        }

        /// <summary>
        /// Función: Obtiene solo los nombres de cupones para autocompletado
        /// Creado: Cristopher Noh 14/08/2020
        /// Modificado:
        /// </summary>
        /// <returns></returns>
        public IMDResponse<List<string>> BGetCuponAutocomplete()
        {
            IMDResponse<List<string>> response = new IMDResponse<List<string>>();

            string metodo = nameof(this.BGetCuponAutocomplete);
            logger.Info(IMDSerialize.Serialize(67823458236352, $"Inicia {metodo}()"));

            try
            {
                IMDResponse<DataTable> respuestaObtenerCodigos = datPromociones.DGetCuponAutocomplete();
                if (respuestaObtenerCodigos.Code != 0)
                {
                    return respuestaObtenerCodigos.GetResponse<List<string>>();
                }

                List<string> lstCodigos = new List<string>();
                foreach (DataRow fila in respuestaObtenerCodigos.Result.Rows)
                {
                    string codigo = fila["sCodigo"].ToString();
                    lstCodigos.Add(codigo);
                }

                lstCodigos = lstCodigos.GroupBy(x => x).Select(x => x.Key).OrderBy(x => x).ToList();

                response.Code = 0;
                response.Message = "Códigos obtenidos";
                response.Result = lstCodigos;
            }
            catch (Exception ex)
            {
                response.Code = 67823458237129;
                response.Message = "Ocurrió un error inesperado al consultar los códigos";

                logger.Error(IMDSerialize.Serialize(67823458237129, $"Error en {metodo}(): {ex.Message}", ex, response));
            }
            return response;
        }
    }
}