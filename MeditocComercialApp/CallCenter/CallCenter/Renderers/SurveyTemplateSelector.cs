using System;
using CallCenter.Models;
using Xamarin.Forms;

namespace CallCenter.Renderers
{
    public class SurveyTemplateSelector : DataTemplateSelector
    {

        public SurveyTemplateSelector()
        {
            this.TextFieldTemplate = new DataTemplate(typeof(InputFieldCell));
            this.AskTemplate = new DataTemplate(typeof(AskSurveyCell));
        }

        private readonly DataTemplate TextFieldTemplate;
        private readonly DataTemplate AskTemplate;

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            var model = (SurveyAsk)item;
            switch( model.TypeField )
            {
                case TYPE_FIELD.TEXTFIELD: return TextFieldTemplate;
                default: return AskTemplate;
            };
        }
    }
}
