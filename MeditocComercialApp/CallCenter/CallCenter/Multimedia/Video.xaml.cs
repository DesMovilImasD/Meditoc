using CallCenter.Renderers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CallCenter.Multimedia
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Video : ContentPage
    {
		public Video ()
		{
			InitializeComponent ();

            //if (EnableBackButtonOverride)
            //{
            //    //this.CustomBackButtonAction = async () =>
            //    //{
            //    //    var result = await this.DisplayAlert(null,
            //    //        "Hey wait now! are you sure " +
            //    //        "you want to go back?",
            //    //        "Yes go back", "Nope");

            //    //    if (result)
            //    //    {
            //    //        await Navigation.PopAsync(true);
            //    //    }
            //    //};
            //}

#if __IOS__

#else
#endif
        }
        public async void OnClicked(object sender, EventArgs e)
        {
            var btn = sender as Button;
            await DisplayAlert("Test", String.Format("Clicked !! {0}", btn.Text), "OK");
        }
        private void ImageButton_Clicked(object sender, EventArgs e)
        {
            if (Context.Instance.Videollamada_init)
            {
                Context.Instance.LocalCameraMedia.VideoMuted = true;
                Context.Instance.LocalCameraMedia.AudioMuted = true;
                Context.Instance.Videollamada_init = false;
                Context.Instance.WriteLine("FINALIZARLLAMADA");
            }
        }

        //private void ToggleRecordVideo(object sender, EventArgs args)
        //{
        //    MenuItem item = (MenuItem)sender;
        //    Context.Instance.ToggleRecordVideo((string)item.Tag);
        //}

        //private void ToggleRecordAudio(object sender, EventArgs args)
        //{
        //    MenuItem item = (MenuItem)sender;
        //    Context.Instance.ToggleRecordAudio((string)item.Tag);
        //}

        //private void MuteAudioMedia(object sender, EventArgs args)
        //{
        //    MenuItem item = (MenuItem)sender;
        //    Context.Instance.ToggleAudioMute((string)item.Tag);
        //}

        //private void MuteVideoMedia(object sender, EventArgs args)
        //{
        //    MenuItem item = (MenuItem)sender;
        //    Context.Instance.ToggleVideoMute((string)item.Tag);
        //}

        //public ContextMenu CreateRemoteContextMenu(string id)
        //{
        //    ContextMenu menu = new ContextMenu();
        //    menu.Items.Add(new MenuItem() { Header = id == null ? "Local" : "Remote", IsEnabled = false, Tag = id });
        //    menu.Items.Add(new Separator());

        //    var item = new MenuItem() { Header = "Mute Audio", IsCheckable = true, IsChecked = false, Tag = id };
        //    item.Click += MuteAudioMedia;
        //    menu.Items.Add(item);

        //    item = new MenuItem() { Header = "Mute Video", IsCheckable = true, IsChecked = false, Tag = id };
        //    item.Click += MuteVideoMedia;
        //    menu.Items.Add(item);

        //    menu.Items.Add(new Separator());

        //    item = new MenuItem() { Header = "Record Audio", IsCheckable = true, IsChecked = false, Tag = id };
        //    item.Click += ToggleRecordAudio;
        //    menu.Items.Add(item);

        //    item = new MenuItem() { Header = "Record Video", IsCheckable = true, IsChecked = false, Tag = id };
        //    item.Click += ToggleRecordVideo;
        //    menu.Items.Add(item);

        //    return menu;
        //}
    }
}
