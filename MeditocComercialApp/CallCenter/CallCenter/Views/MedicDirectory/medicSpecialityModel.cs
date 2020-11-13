using System;
using System.Collections.Generic;
using System.Threading.Tasks;
#if __ANDROID__
using Android.Content;
#endif
using CallCenter.Helpers;
using CallCenter.Services;
using CallCenter.ViewModels;
using Xamarin.Forms;

namespace CallCenter.Views.MedicDirectory
{
    public class medicSpecialityModel : BaseViewModel
    {
        readonly ICPFeeds cpFeeds;
        private InternetService oInternetService;
        private MedicSpecialityView ViewContext { get; set; }


#region -------- [constructor] -------
#if __ANDROID__
        private Intent intent;
        public medicSpecialityModel(MedicSpecialityView page, Intent _intent) : base(page)
        {
            intent = _intent;
#else
        public medicSpecialityModel(MedicSpecialityView page) : base(page)
        {
#endif
            ViewContext = page;
            oInternetService = new InternetService(page);

            this.cpFeeds = DependencyService.Get<ICPFeeds>();
            oInternetService = new InternetService(page);
        }
#endregion
        

        public async Task<List<EntDirectorio>> CargarDatosByEspecialidad(int? iIdEspecialidad = null, string sBuscador = null)
        {
            List<EntDirectorio> medicSpecialties = new List<EntDirectorio>();
            try
            {
                medicSpecialties = await cpFeeds.getMedicSpeciality(iIdEspecialidad,sBuscador);
            }
            catch (Exception ex)
            {

            }

            return medicSpecialties;
        }
    }
}
