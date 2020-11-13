using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
    public class MedicDirectoryModel : BaseViewModel
    {

        private readonly ObservableCollection<specialtyDTO> source = new ObservableCollection<specialtyDTO>();
        public ObservableCollection<specialtyDTO> DataSource { get { return source; } }

        readonly ICPFeeds cpFeeds;
        private InternetService oInternetService;
        private MeditocDirectoryView ViewContext { get; set; }


#region -------- [constructor] -------
#if __ANDROID__
        private Intent intent;
        public MedicDirectoryModel(MeditocDirectoryView page, Intent _intent) : base(page)
        {
            intent = _intent;
#else
        public MedicDirectoryModel(MeditocDirectoryView page): base(page)
        {
#endif
            ViewContext = page;
            oInternetService = new InternetService(page);

            this.cpFeeds = DependencyService.Get<ICPFeeds>();
            oInternetService = new InternetService(page);
        }
#endregion


        public async Task<List<specialtyDTO>> CargarDatos()
        {
            List<specialtyDTO> specialties = new List<specialtyDTO>();
            try
            {
                 specialties = await cpFeeds.getSpeciality();


                specialties = (from i in specialties
                               select new specialtyDTO
                               {
                                   iIdEspecialidad = i.iIdEspecialidad,
                                   sNombre = i.sNombre.ToUpper()
                               }).ToList();
            }
            catch (Exception ex)
            {

            }

            return specialties
                .OrderBy(x => x.sNombre)
                .ToList();
        }

    }
}
