using CallCenter.Helpers;
using CallCenter.Helpers.FontAwesome;
using CallCenter.Models;
using CallCenter.Services;
using CallCenter.Views.MedicDirectory;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(cpFeedService))]
namespace CallCenter.Services
{
    public class cpFeedService : ICPFeeds
    {

        public cpFeedService()
        {

        }

        public async Task<bool> m_Login(string psUserLogin, string psPassword, string psTipoCheck)
        {
            wsConnexion ws = new wsConnexion();
            Settings.sError = "";

            try
            {
                string sMethod = String.Format(@"LoginApp?sUsuario={0}&sPassword={1}", psUserLogin, psPassword);
                newResponseModel<EntFolio> sResponseUser = new newResponseModel<EntFolio>();
                string sResponse = await ws.GetDataRestAsync(new { }, "Api/Folio/Get", sMethod);

                sResponseUser = JsonConvert.DeserializeObject<newResponseModel<EntFolio>>(sResponse);

                if (sResponseUser.Code == 0 && psTipoCheck == "ENTRADA" && sResponseUser.Result != null)
                {
                    Settings.iIdUsuario = sResponseUser.Result.iIdPaciente;
                    Settings.dtFechaVencimiento = sResponseUser.Result.dtFechaVencimiento;
                    Settings.sUserNameLogin = psUserLogin;
                    Settings.sFolio = psUserLogin;
                    Settings.sPassLogin = psPassword;
                    Settings.bTerminoYcondiciones = sResponseUser.Result.bTerminosYCondiciones;
                    Settings.ProductType = sResponseUser.Result.iIdProducto;
                    Settings.bEsAgendada = sResponseUser.Result.bEsAgendada;
                    return true;
                }
                else
                {
                    Settings.sError = sResponseUser.Message;
                    return false;
                }
            }
            catch (Exception e)
            {
                Settings.sError = e.Message;
                return false;
            }
        }

        public async Task<bool> m_Cambio_Contasena(string sFolio, string sPassword)
        {
            try
            {
                wsConnexion ws = new wsConnexion();
                Settings.sError = "";
                var uri = $"{Settings.sUrl}/Api/Folio/Update/FolioPassword?sFolio={sFolio}&sPassword={sPassword}";
                var response = await ws.Get<newResponseModel<bool>>(uri);

                try
                {

                    if (response.Code != 0)
                        throw new Exception("Hubo un error al internar conectarse con el servicio, comuníquese con su proveedor.");

                }
                catch { throw new Exception("Hubo un error al internar solicitar el cambio de contraseña, favor de reintentar."); }

                if (!response.Result)
                {
                    Settings.sError = response.Message;
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                Settings.sError = ex.Message;
                return false;
            }
        }


        public async Task<newResponseModel<medicSpecialityDTO>> m_SolicitaMedico(bool? bEsAgendada, int? iIdUsuario, DateTime? dtFechaVencimiento)
        {
            newResponseModel<medicSpecialityDTO> response = new newResponseModel<medicSpecialityDTO>();
            try
            {
                wsConnexion ws = new wsConnexion();
                Settings.sError = "";
                var uri = $"{Settings.sUrl}/Api/Colaborador/Get/Colaborador/ObtenerSala?bEsAgendada={bEsAgendada}&iIdUsuario={iIdUsuario}";
                response = await ws.Get<newResponseModel<medicSpecialityDTO>>(uri);

                try
                {
                    if (response.Code != 0)
                        throw new Exception("Hubo un error al internar conectarse con el servicio, comuníquese con su proveedor.");
                }
                catch { throw new Exception("Hubo un error al internar solicitar el chat, favor de reintentar."); }


                Settings.sUIDDR = response.Result.iNumSala;
            }
            catch (Exception e)
            {
                Settings.bClicButton = false;
                Settings.sError = e.Message;
            }

            return response;
        }

        public async Task<bool> m_Acepta_Temino_y_condiciones(string sFolio)
        {
            try
            {
                wsConnexion ws = new wsConnexion();
                Settings.sError = "";
                var uri = $"{Settings.sUrl}/Api/Folio/Update/TerminosYCondiciones?sFolio=" + sFolio;
                var response = await ws.Get<newResponseModel<bool>>(uri);

                try
                {
                    if (response == null)
                        throw new Exception("Hubo un error al internar conectarse con el servicio, comuníquese con su proveedor.");
                }
                catch { throw new Exception("Hubo un error al internar solicitar el cambio de contraseña, favor de reintentar."); }

                if (response.Result)
                {
                    Settings.bTerminoYcondiciones = true;
                    return true;
                }
                else
                {
                    Settings.sError = response.Message;
                    return false;
                }
            }
            catch (Exception ex)
            {
                Settings.sError = ex.Message;
                return false;
            }
        }

        /// <summary>
        /// get all services
        /// </summary>
        /// <returns></returns>
        public async Task<ServiceResponseModel> GetService()
        {
            try
            {
                var uri = $"{Settings.sUrl}/Api/Producto/Get/ObtenerServicio";
                wsConnexion ws = new wsConnexion();
                Settings.sError = "";

                var response = await ws.Get<newResponseModel<List<ServiceItemResponse>>>(uri);

                if (response is null)
                    throw new Exception("Hubo un error al internar conectarse con el servicio, comuníquese con su proveedor.");

                if (response.Code != 0)
                {
                    return ServiceResponseModel.Fail(response.Message);
                }


                return ServiceResponseModel.Success(response.Result);
            }
            catch (Exception e)
            {
                return ServiceResponseModel.Fail(e.Message);
            }
        }

        /// <summary>
        /// get all memberships
        /// </summary>
        /// <returns></returns>
        public async Task<MembershipResponseModel> GetMembership()
        {
            try
            {
                var uri = $"{Settings.sUrl}/Api/Producto/Get/ObtenerMembresia";
                wsConnexion ws = new wsConnexion();
                Settings.sError = "";

                var response = await ws.Get<newResponseModel<List<MembershipItemResponse>>>(uri);


                if (response is null)
                    throw new Exception("Hubo un error al internar conectarse con el servicio, comuníquese con su proveedor.");

                if (response.Code != 0)
                {
                    return MembershipResponseModel.Fail(response.Message);
                }

                return MembershipResponseModel.Success(response.Result);
            }
            catch (Exception e)
            {
                return MembershipResponseModel.Fail(e.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Coupon"></param>
        /// <returns></returns>
        public async Task<CouponResponseModel> VerifyCoupon(string Coupon)
        {
            wsConnexion ws = new wsConnexion();
            Settings.sError = "";
            var uri = $"{Settings.CouponUrlEndpoint}?psCodigo={Coupon}";
            return await ws.Get<CouponResponseModel>(uri);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<BuyProductResponseModel> RegisterSubscription(BuyProductRequestModel request)
        {
            try
            {
                wsConnexion ws = new wsConnexion();
                Settings.sError = "";

                //string str = "";
                string str = await ws.GetDataRestAsync(request, "Api/Folio/Create", "Folio");

                var response = JsonConvert.DeserializeObject<newResponseModel<BuyProductInfoItemResponseModel>>(str);

                if (response is null)
                    throw new Exception("Hubo un error al internar conectarse con el servicio, comuníquese con su proveedor.");

                if (response.Code != 0)
                {
                    return BuyProductResponseModel.Fail(response.Message);
                }

                return BuyProductResponseModel.Success(response.Result);
            }
            catch (Exception e)
            {
                var error = "Ha ocurrido un error al procesar la respuesta del servidor";
                return BuyProductResponseModel.Fail(error);
            }
        }

        public async Task<bool> GetPolicies()
        {
            try
            {
                wsConnexion ws = new wsConnexion();
                Settings.sError = "";
                var uri = $"{Settings.sUrl}/Api/Politicas/Get/Politicas";
                var response = await ws.Get<newResponseModel<PoliciesResponseModel>>(uri);

                if (response.Code != 0)
                {
                    new Exception(response.Message);
                }

                Settings.LinkTermsAndConditions = response is null ?
                    string.Empty : response.Result.TermsAndConditionsLink;

                Settings.LinkPrivacity = response is null ?
                    string.Empty : response.Result.PrivacityLink;

                Settings.ContactEmail = response.Result.Contact;
                Settings.SupportEmail = response.Result.Support;
                Settings.CompanyAddress = response.Result.CompanyAddess;
                Settings.CompanyPhone = response.Result.CompanyPhone;
                Settings.IVA = response.Result.IVA;
                Settings.ThresholdCouponDiscount = response.Result.MaxDiscount;
                Settings.MonthlyPayments = JsonConvert.SerializeObject(response.Result.MonthlyPayments);
                Settings.HasMonthsWithoutInterest = response.Result.HasMonthsWithoutInterest;
                Settings.ConektaPublicKey = response.Result.ConektaPublicKey;
                Settings.IceLinkKey = response.Result.keyIceLink;
                Settings.IceLinkDomainKey = response.Result.keyDomainIceLink;
                Settings.IceLinkServers = JsonConvert.SerializeObject(response.Result.rutasIceServer);

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<List<specialtyDTO>> getSpeciality()
        {
            List<specialtyDTO> lstSpeciality = new List<specialtyDTO>();
            try
            {
                wsConnexion ws = new wsConnexion();
                Settings.sError = "";
                var uri = $"{Settings.sUrl}/Api/Especialidad/Get/RegistrosFiltrados";
                var response = await ws.Get<newResponseModel<List<specialtyDTO>>>(uri);

                if (response.Code != 0)
                {
                    new Exception(response.Message);
                }

                lstSpeciality = response.Result;
            }
            catch (Exception ex)
            {

            }

            return lstSpeciality
                .Where(c => c.iIdEspecialidad != 1)
                .ToList();
        }

        public async Task<List<EntDirectorio>> getMedicSpeciality(int? iIdEspecialidad = null, string sBuscador = null)
        {
            List<EntDirectorio> lstSpeciality = new List<EntDirectorio>();
            try
            {
                wsConnexion ws = new wsConnexion();
                Settings.sError = "";
                var uri = $"{Settings.sUrl}/Api/Colaborador/Get/Directorio/Especialistas/Publico?piIdEspecialidad={iIdEspecialidad}&psBuscador={sBuscador}";
                var response = await ws.Get<newResponseModel<EspecialistasDTO>>(uri);

                if (response.Code != 0)
                {
                    new Exception(response.Message);
                }

                lstSpeciality = response.Result.lstColaboradores;

                lstSpeciality = await ObtenerImagenes(lstSpeciality);
            }
            catch (Exception ex)
            {

            }

            return lstSpeciality
                .OrderBy(x => x.sNombre)
                .ToList();
        }

        public async Task<List<EntDirectorio>> ObtenerImagenes(List<EntDirectorio> medicSpecialityDTOs)
        {
            try
            {
                wsConnexion ws = new wsConnexion();
                Settings.sError = "";

                foreach (var item in medicSpecialityDTOs)
                {                    
                    item.sIconWhatsApp = FontAwesomeIcons.Whatsapp;
                    item.sIconMaps = FontAwesomeIcons.LocationArrow;
                    item.sIconCellPhone = FontAwesomeIcons.Phone;
                }
            }
            catch (Exception ex)
            {

            }
            return medicSpecialityDTOs;
        }
    }


}
