using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CallCenter.Renderers
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InputFieldCell : ViewCell
    {
        public InputFieldCell()
        {
            InitializeComponent();
        }
    }
}
