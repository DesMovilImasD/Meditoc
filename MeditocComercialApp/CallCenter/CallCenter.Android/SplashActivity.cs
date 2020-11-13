
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace CallCenter.Droid
{
    /// <summary>
    /// no utilizar como splash, el splash se carga desde la condiguracion
    /// del main activity, para modificarlo favor de buscar el layout en la carpeta resources
    /// o directamente en el archivo styles de android.
    /// </summary>
    [Activity(Label = "Meditoc 360", Theme = "@style/MainTheme.Splash", MainLauncher = false, NoHistory = true)]
    public class SplashActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
        }

        protected override void OnResume()
        {
            base.OnResume();
            Task startupWork = new Task(() => {
                StartActivity(new Intent(Application.Context, typeof(MainActivity)));
            });
            startupWork.Start();
            
        }
    }
}
