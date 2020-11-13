using System;

using Xamarin.Forms;

namespace CallCenter.Renderers
{
    public interface  ICoolContentPage 
    {
        bool EnableBackButtonOverride { get; set; }
        Action CustomBackButtonAction { get; set; }

        void UnSubscribe();
    }
}

