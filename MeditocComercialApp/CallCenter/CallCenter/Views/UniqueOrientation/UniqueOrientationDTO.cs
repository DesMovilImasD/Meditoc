using System;

using Xamarin.Forms;
using CallCenter.Helpers.FontAwesome;
namespace CallCenter.Views.UniqueOrientation
{
    /// <summary>
    /// modelo solamente utilizado para los eventos y manejo d einformacion
    /// relacionada a la vista 
    /// </summary>
    public class UniqueOrientationDTO
    {
        public string SelectedIcon { get; set; } = "";
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
        public string Cost { get; set; }
        public bool Selected { get; set; } = false;
        public string BgColor { get; set; } = "Transparent";
        public double RealCost { get; set; }

       public void render()
        {
            SelectedIcon = Selected ? FontAwesomeIcons.Check : "";
            BgColor = Selected ? "#575E6654" : "Transparent";
        }
    }
}

