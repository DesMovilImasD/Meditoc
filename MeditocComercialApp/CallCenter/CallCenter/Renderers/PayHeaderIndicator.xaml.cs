using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace CallCenter.Renderers
{
    public partial class PayHeaderIndicator : ContentView
    {
        public PayHeaderIndicator()
        {
            InitializeComponent();

            TabIndicator1.IsVisible = true;
            HeaderTabLabel1.TextColor = Color.White;

            TabIndicator2.IsVisible = false;
            HeaderTabLabel2.TextColor = Color.LightGray;
        }

        public static readonly BindableProperty TabLabel1Property =
            BindableProperty.Create("TabLabel1", typeof(string), typeof(PayHeaderIndicator), "");
        public string TabLabel1
        {
            get
            {
                return (string)GetValue(TabLabel1Property);
            }
            set
            {
                SetValue(TabLabel1Property, value);
            }
        }

        public static readonly BindableProperty TabEnable1Property =
            BindableProperty.Create("TabEnable1", typeof(bool), typeof(PayHeaderIndicator), false);
        public bool TabEnable1
        {
            get
            {
                return (bool)GetValue(TabEnable1Property);
            }
            set
            {
                SetValue(TabEnable1Property, value);
            }
        }

        public static readonly BindableProperty TabLabel2Property =
            BindableProperty.Create("TabLabel2", typeof(string), typeof(PayHeaderIndicator), "");
        public string TabLabel2
        {
            get
            {
                return (string)GetValue(TabLabel2Property);
            }
            set
            {
                SetValue(TabLabel2Property, value);
            }
        }

        public static readonly BindableProperty TabEnable2Property =
            BindableProperty.Create("TabEnable2", typeof(bool), typeof(PayHeaderIndicator), false);
        public bool TabEnable2
        {
            get
            {
                return (bool)GetValue(TabEnable2Property);
            }
            set
            {
                SetValue(TabEnable2Property, value);
            }
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            switch (propertyName)
            {
                case "TabLabel1":
                    {
                        HeaderTabLabel1.Text = TabLabel1;
                        break;
                    }
                case "TabLabel2":
                    {
                        HeaderTabLabel2.Text = TabLabel2;
                        break;
                    }
                case "TabEnable1":
                    {
                        TabIndicator1.IsVisible = TabEnable1;
                        HeaderTabLabel1.TextColor = TabEnable1 ? Color.White : Color.LightGray;
                        break;
                    }
                case "TabEnable2":
                    {
                        TabIndicator2.IsVisible = TabEnable2;
                        HeaderTabLabel2.TextColor = TabEnable2 ? Color.White : Color.LightGray;
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }

    }
}
