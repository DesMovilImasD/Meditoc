using CallCenter.Helpers;
using CallCenter.Renderers;
using CallCenter.ViewModels;
using CallCenter.Views.HomeSwitch;
using Rg.Plugins.Popup.Services;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CallCenter.Multimedia
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Text : ContentPage
    {
        // private ObservableCollection<Message> myList;// = new ObservableCollection<Message>();

        ChatViewModel oChatViewModel;
        bool bchatiniciado = false;
        public Text()
		{
            var mainDisplayInfo = DeviceDisplay.MainDisplayInfo;

            Settings.bCancelaDoctor = false;

            var height = mainDisplayInfo.Height;
          
          
            BindingContext = oChatViewModel = new ChatViewModel();
          
            InitializeComponent ();
           

            Context.Instance.MessageReceived += Instance_MessageReceived;
            Context.Instance.PeerJoined += Instance_PeerJoined;
            Context.Instance.PeerLeft += Instance_PeerLeft;
            string folio = Settings.sFolio; // string.IsNullOrEmpty(Settings.COVIDFolio) ? Settings.sFolio : Settings.COVIDFolio;
            oChatViewModel.Messages.Add(new MessageViewModel { Text = "Gracias por contactar a tú orientación médica en línea, su folio de usuario es:" + folio , IsIncoming = true, MessagDateTime = DateTime.Now });

            Device.StartTimer(TimeSpan.FromSeconds(8), () =>
            {
                // Do something
                bchatiniciado = true;
                return false; // True = Repeat again, False = Stop the timer
            });
      

        }
        private void Instance_PeerLeft(string p)
        {
            Device.BeginInvokeOnMainThread(new Action(() =>
            {
                string folio = Settings.sFolio; // string.IsNullOrEmpty(Settings.COVIDFolio) ? Settings.sFolio : Settings.COVIDFolio;
                oChatViewModel.Messages.Add(new MessageViewModel { Text = "Gracias por contactar a tú orientación médica en línea, su folio de usuario es: " + folio, IsIncoming = true, MessagDateTime = DateTime.Now });
                //stkActivity.IsVisible = false;
                //iaIndicator.IsVisible = false;
                // MessagesListView.ScrollTo(oChatViewModel.Messages[a], ScrollToPosition.End, true);
            }));
        }

        private void Instance_PeerJoined(string p)
        {
            Device.BeginInvokeOnMainThread(new Action(async () =>
            {
                try
                {
                    oChatViewModel.Messages.Add(new MessageViewModel { Text = "Tu médico está en línea", IsIncoming = true, MessagDateTime = DateTime.Now });
                    //MessagesListView.ScrollTo(oChatViewModel.Messages[a], ScrollToPosition.End, true);
                    //stkActivity.IsVisible = false;
                    //iaIndicator.IsVisible = false;
                    //iaIndicator.IsRunning = false;
                    var count = PopupNavigation.Instance.PopupStack.Count;
                    if (count > 0)
                    {
                        await PopupNavigation.Instance.PopAsync();
                    }
                    Context.Instance.IsMedicConnected = true;
                }
                catch {

                }
            }));
        }

        private void Instance_MessageReceived(object sender, MessageReceivedArgs e)
        {
            Device.BeginInvokeOnMainThread(new Action(async () =>
            {
                try
                {
                    if (e.Message == "FINALIZARLLAMADA")
                    //if (false)
                    {
                        Context.Instance.LocalCameraMedia.VideoMuted = true;
                        Context.Instance.LocalCameraMedia.AudioMuted = true;
                        WriteMessage(e.Name, "La videollamada ha sido finalizada.", "I");
                        
                        Context.Instance.Videollamada_init = false;
                        await DisplayAlert("Información", "La videollamada ha sido finalizada por el médico, favor de continuar la conversación por el chat.", "ok");

                       
                    }
                    else
                    {
                        //   if (true)
                        if (e.Message == "FINALIZARCONSULTA")
                        {
                            if (bchatiniciado)
                            {
                                try
                                {
                                  
                                    MessagingCenter.Send<Text, object>(this, GlobalEventSender.VIDEO_CLOSE_BY_MEDIC, null);
                                }
                                catch
                                {

                                }
                            }

                        }
                        else
                        {
                            WriteMessage(e.Name, e.Message, "I");
                        }
                    }
                }
                catch {

                }
              

            }));
        }

        private void WriteMessage(string name, string message, string pos)
        {
            int a = oChatViewModel.Messages.Count;
            string pscolor = "";
            if (pos == "I") {
                //pscolor = "Gray";
                oChatViewModel.Messages.Add(new MessageViewModel { Text = message, IsIncoming = true, MessagDateTime = DateTime.Now });
                MessagesListView.ScrollTo(oChatViewModel.Messages[a], ScrollToPosition.End, true);
               
            }
            else {
                //pscolor = "Blue";
                oChatViewModel.Messages.Add(new MessageViewModel { Text = message, IsIncoming = false, MessagDateTime = DateTime.Now });
                MessagesListView.ScrollTo(oChatViewModel.Messages[a], ScrollToPosition.End, true);
            }
           
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            string text = textMessage.Text;
            textMessage.Text = string.Empty;

            if (!string.IsNullOrWhiteSpace(text))
            {
                Context.Instance.WriteLine(text);

                WriteMessage("Me", text,"D");
            }
        }

        void OnTapGestureRecognizerTapped(object sender, EventArgs args)
        {
            textMessage.Unfocus();

            //DisplayAlert("text", "TEXT", "ok");
            //tapCount++;
            //var imageSender = (Image)sender;
            //// watch the monkey go from color to black&white!
            //if (tapCount % 2 == 0)
            //{
            //    imageSender.Source = "tapped.jpg";
            //}
            //else
            //{
            //    imageSender.Source = "tapped_bw.jpg";
            //}
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            textMessage.Unfocus();
            ((ListView)sender).SelectedItem = null;
        }
    }

    public class ChatText
    {
        public string DisplayName { get; set; }
        public LayoutOptions sAling { get; set; }
    }

    
}