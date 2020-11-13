using CallCenter.Models;
using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CallCenter.Helpers;

namespace CallCenter.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuPage : ContentPage
    {
        MainPage RootPage { get => Application.Current.MainPage as MainPage; }
        List<HomeMenuItem> menuItems;
        public MenuPage()
        {
            InitializeComponent();
            //lblUserLogin.Text = Settings.sUserName.ToString();
            menuItems = new List<HomeMenuItem>
            {
               
                    new HomeMenuItem { Id = 3, Title = "Inicio" },
                    new HomeMenuItem { Id = 0, Title = "Mi Cuenta" },
                    new HomeMenuItem { Id = 1, Title = "Términos y condiciones" },
                    new HomeMenuItem { Id = 2, Title = "Aviso de privacidad" },
                    new HomeMenuItem { Id = -1, Title = "Salir" },
            };

            ListViewMenu.ItemsSource = menuItems;

            ListViewMenu.SelectedItem = menuItems[0];
            ListViewMenu.ItemSelected += async (sender, e) =>
            {
                if (e.SelectedItem == null)
                    return;

                var id = (int)((HomeMenuItem)e.SelectedItem).Id;
                await RootPage.NavigateFromMenu(id);
                ListViewMenu.SelectedItem = null;
            };
            ListViewMenu.SelectedItem = null;
        }
    }
}