using CallCenter.Models;
using CallCenter.Views.MedicDirectory;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CallCenter.Helpers
{
    public interface ICPFeeds
    {
        Task<bool> m_Login(string psUsername, string psPassword, string psTipoCheck);
        Task<ServiceResponseModel> GetService();
        Task<MembershipResponseModel> GetMembership();
        Task<bool> m_Acepta_Temino_y_condiciones(string sFolio);
        Task<bool> GetPolicies();
        Task<CouponResponseModel> VerifyCoupon(string Coupon);
        Task<bool> m_Cambio_Contasena(string sFolio, string sPassword);
        Task<BuyProductResponseModel> RegisterSubscription(BuyProductRequestModel request);
        Task<List<specialtyDTO>> getSpeciality();
        Task<List<EntDirectorio>> getMedicSpeciality(int? iIdEspecialidad = null, string sBuscador = null);
        Task<List<EntDirectorio>> ObtenerImagenes(List<EntDirectorio> medicSpecialityDTOs);


        Task<newResponseModel<medicSpecialityDTO>> m_SolicitaMedico(bool? bEsAgendada, int? iIdUsuario, DateTime? dtFechaVencimiento);

    }
}
