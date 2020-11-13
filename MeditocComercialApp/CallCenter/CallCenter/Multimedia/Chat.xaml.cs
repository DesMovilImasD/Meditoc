using FM.IceLink;
using System;
using Xamarin.Forms;
using System.Reflection;
using Plugin.SimpleAudioPlayer;
using CallCenter.Helpers;
using Rg.Plugins.Popup.Services;
using CallCenter.Renderers;
using Xamarin.Forms.Xaml;
using System.Threading.Tasks;


#if __IOS__
using CoreGraphics;
using UIKit;
using Xamarin.Forms.Platform.iOS;
#else
using Android.Views;
using Xamarin.Forms.Platform.Android;
#endif


namespace CallCenter.Multimedia
{
    
    public partial class Chat : CoolTabbedPage
    {
        bool bVideo = false;
        //bool Videollamada_init = false;
        //Page videoTab = null;
        ISimpleAudioPlayer player;

        public Chat(bool pbVideo)
        {
            Settings.bEnProceso = false;
            bVideo = pbVideo;
            InitializeComponent();
            //NavigationPage.SetHasNavigationBar(this, true);
            Context.Instance.oNavigation = this.Navigation;

            if (EnableBackButtonOverride)
            {
                this.CustomBackButtonAction =  () =>
                {
                    
                    Device.BeginInvokeOnMainThread(async() => {
                        if (await DisplayAlert("¿Salir?", "¿Está seguro(a) que desea finalizar su consulta?", "Si", "No"))
                        {
                            unSuscribe();
                            Context.Instance.LeaveAsync();
                            Settings.bCancelaDoctor = true;
                            await RemoveLoading();
                            await Navigation.PopToRootAsync();
                        }

                    });
                };
            }

            try
            {
                var player = CrossSimpleAudioPlayer.Current;

                player.Load("call.mp3");

                this.CurrentPageChanged += async (object sender, EventArgs e) =>
                {
                    var i = this.Children.IndexOf(this.CurrentPage);
                    if (Context.Instance.IsMedicConnected)
                    {
                        System.Diagnostics.Debug.WriteLine("Page No:" + i);
                        if (i == 1)
                        {
                            if (!Context.Instance.Videollamada_init)
                            {
                                try
                                {
                                    Context.Instance.LocalCameraMedia.VideoMuted = true;
                                    Context.Instance.LocalCameraMedia.AudioMuted = true;
                                    bool a = await DisplayAlert("Videollamada", "¿Quieres iniciar una videollamada con el médico?", "Si", "No");
                                    if (a)
                                    {

                                        Context.Instance.LocalCameraMedia.VideoMuted = false;
                                        Context.Instance.LocalCameraMedia.AudioMuted = false;

                                        Plugin.SimpleAudioPlayer.CrossSimpleAudioPlayer.Current.Play();

                                        Plugin.SimpleAudioPlayer.CrossSimpleAudioPlayer.Current.Loop = true;

                                        Device.StartTimer(TimeSpan.FromSeconds(5), () =>
                                        {
                                            // Do something
                                            Plugin.SimpleAudioPlayer.CrossSimpleAudioPlayer.Current.Loop = false;
                                            return false; // True = Repeat again, False = Stop the timer
                                        });
                                        Context.Instance.WriteLine("LLAMADAENTRANTE");
                                        Context.Instance.Videollamada_init = true;
                                    }
                                    else
                                    {
                                        // Videollamada_init = false;
                                        CurrentPage = Children[0];
                                    }
                                }
                                catch{

                                }
                            }

                        }
                        else
                        {
                            if (Context.Instance.Videollamada_init)
                            {
                                bool a = await DisplayAlert("Alerta", "¿Quieres salir de la videollamada con el médico?", "Si", "No");

                                if (a)
                                {
                                    try
                                    {
                                        Context.Instance.LocalCameraMedia.VideoMuted = true;
                                        Context.Instance.LocalCameraMedia.AudioMuted = true;


                                        Context.Instance.Videollamada_init = false;
                                        Context.Instance.WriteLine("FINALIZARLLAMADA");
                                    }
                                    catch{

                                    }
                                }
                                else
                                {
                                    CurrentPage = Children[1];

                                }
                            }

                        }
                    }
                    else
                    {
                        CurrentPage = Children[0];
                        await DisplayAlert("Conectando", "Favor de esperar a un médico en linea ", "Ok");

                    }
                };
            }
            catch {

            }
        }
        private void TabbedPage_Appearing(object sender, EventArgs e)
        {
            try
            {


#if __IOS__
               
                Context.Instance.StartLocalMedia(videoTab.Content as AbsoluteLayout).Then<object>((o) =>
#else
             
            Context.Instance.StartLocalMedia(Android.App.Application.Context, videoTab.Content as AbsoluteLayout).Then<object>((o) =>
#endif
                {
                    Future<object> oContext = Context.Instance.JoinAsync();



                    //Context.Instance.ToggleRecordVideo("False");


                    return oContext;
                }, (ex) =>
                {
                    Log.Error("Could not start local media.", ex);
                    Alert(ex.Message);
                }).Fail(ex =>
                {
                    Log.Error("Could not join conference.", ex);
                    Alert(ex.Message);
                });

                //Context.Instance.EnableVideoSend = false;
                // Context.Instance.ToggleVideoMute("");
                //Context.Instance.ToggleAudioMute("");
                if (!Context.Instance.Videollamada_init)
                {
                    Context.Instance.LocalCameraMedia.VideoMuted = true;
                    Context.Instance.LocalCameraMedia.AudioMuted = true;
                }
            }
            catch {

            }
        }

        public void Alert(string format, params object[] args)
        {
            DisplayAlert("Alert", string.Format(format, args), "Ok");
        }


        void TabbedPage_Disappearing(object sender, System.EventArgs e)
        {
            Context.Instance.LeaveAsync()
            .Fail((ex) =>
            {
                Log.Error("Failed to leave conference.", ex);
                Alert(ex.Message);
            });

            Context.Instance.StopLocalMedia()
            .Fail((ex) =>
            {
                Log.Error("Failed to stop local media.", ex);
                Alert(ex.Message);
            });
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            suscribe();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            unSuscribe();
        }

        protected override bool OnBackButtonPressed()
        {
            // Begin an asyncronous task on the UI thread because we intend to ask the users permission.
            Device.BeginInvokeOnMainThread(async () =>
            {
                if (await DisplayAlert("¿Salir?", "¿Está seguro(a) que desea finalizar su consulta?", "Si", "No"))
                {

                    unSuscribe();
                    Context.Instance.LeaveAsync();
                    Settings.bCancelaDoctor = true;
                    await RemoveLoading();
                    await Navigation.PopToRootAsync();
                }
            });

            // Always return true because this method is not asynchronous.
            // We must handle the action ourselves: see above.
            return true;
        }

        System.IO.Stream GetStreamFromFile(string filename)
        {
            var assembly = typeof(App).GetTypeInfo().Assembly;

            var stream = assembly.GetManifestResourceStream("CallCenter." + filename);

            return stream;
        }

        async Task RemoveLoading()
        {
            var count = PopupNavigation.Instance.PopupStack.Count;
            if (count > 0)
            {
                await PopupNavigation.Instance.PopAsync();
            }
        }

        void suscribe()
        {
            MessagingCenter.Subscribe<Text, object>(this,
                GlobalEventSender.VIDEO_CLOSE_BY_MEDIC,
                async (sender, arg) => {
                    unSuscribe();
                    Settings.bCancelaDoctor = true;
                    Context.Instance.Videollamada_init = false;
                    //WriteMessage(e.Name, "Gracias por usar el servicio rapidoctor, su consulta ha finalizado.", "I");
                    
                    await DisplayAlert("Información", "Gracias por usar el servicio Meditoc, su orientación ha finalizado.", "ok");
                    Context.Instance.LeaveAsync();
                    await RemoveLoading();
                    await Navigation.PopToRootAsync();
                    //TODO: goto home switch.

                });
        }

        void unSuscribe()
        {
            MessagingCenter.Unsubscribe<Text, object>(this, GlobalEventSender.VIDEO_CLOSE_BY_MEDIC);
        }

    }

}