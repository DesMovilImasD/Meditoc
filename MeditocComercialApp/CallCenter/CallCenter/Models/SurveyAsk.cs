using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace CallCenter.Models
{

    public enum TYPE_FIELD
    {
        CHECKBOX = 1,
        TEXTFIELD
    }

    public class SurveyAsk
    {
        [JsonProperty("sParam")]
        public string Code { get; set; }

        [JsonProperty("sNombre")]
        public string Ask { get; set; }

        [JsonProperty("iOrden")]
        public int? SortKey { get; set; }

        [JsonIgnore]
        public TYPE_FIELD TypeField { get; set; }

        [JsonIgnore]
        public String ValueField { get; set; }

        [JsonIgnore]
        public Boolean Required { get; set; } = false;

        [JsonIgnore]
        public bool Selected { get; set; } = false;


        public static SurveyAsk BuildTextField(String Code, String Ask, String Value, Boolean Required)
        {
            return new SurveyAsk
            {
                Selected = false,
                SortKey = null,
                TypeField = TYPE_FIELD.TEXTFIELD,
                ValueField = Value,
                Ask = Ask,
                Code = Code,
                Required = Required
            };
        }
    }

    public class SurveyAnswer
    {

        public static SurveyAnswer Build(SurveyAsk model)
        {
            return new SurveyAnswer
            {
                Selected = model.Selected,
                Code = model.Code,
                Name = model.TypeField == TYPE_FIELD.TEXTFIELD ? model.Ask : null,
            };
        }

        [JsonProperty("sParam")]
        public string Code { get; set; }

        [JsonProperty("bPrecionado")]
        public bool Selected { get; set; } = false;

        [JsonProperty("sNombre")]
        public string Name { get; set; }
    }

    public class SurveyRequest
    {
        public static SurveyRequest Create(List<SurveyAsk> Items, double Latitude, double Longitude, string error = null)
        {
            var model = new SurveyRequest();
            model.Latitude = Latitude;
            model.Longitude = Longitude;
            model.ErrorLocation = error;

            foreach(var item in Items)
            {
                model.Data.Add(SurveyAnswer.Build(item));
            }
            return model;
        }

        public SurveyRequest()
        {
            Data = new List<SurveyAnswer>();
        }

        [JsonProperty("sFolio")]
        public string Folio { get; set; }

        [JsonProperty("sLatitud")]
        public double Latitude { get; set; }

        [JsonProperty("sLongitud")]
        public double Longitude { get; set; }

        [JsonProperty("sError")]
        public string ErrorLocation { get; set; }

        [JsonProperty("lstPreguntas")]
        public List<SurveyAnswer> Data { get; set; }
    }

    public class SurveyResponse
    {
        [JsonProperty("bRespuesta")]
        public bool Status { get; set; }

        [JsonProperty("bTipoFolio")]
        public bool IsType2 { get; set; }

        [JsonProperty("sFolio")]
        public string Folio { get; set; }

        [JsonProperty("sMensaje")]
        public string Msg { get; set; }

        [JsonProperty("sParameter1")]
        public string Data { get; set; }

        public List<string> Items { get; set; }

        public void Build()
        {
            try
            {
                if (!string.IsNullOrEmpty(Data))
                {
                    Items = JsonConvert.DeserializeObject<List<string>>(Data);
                }
                
            }
            catch(Exception e)
            {

            }
            
        }
    }
}
