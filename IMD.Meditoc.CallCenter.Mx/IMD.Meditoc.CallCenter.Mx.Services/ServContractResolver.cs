using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IMD.Meditoc.CallCenter.Mx.Services
{
    public class ServContractResolver : DefaultContractResolver
    {
        private readonly string[] propiedadesOcultar;

        public ServContractResolver(string[] pPropiedadesOcultar)
        {
            propiedadesOcultar = pPropiedadesOcultar;
        }

        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {
            IList<JsonProperty> properties = base.CreateProperties(type, memberSerialization);

            properties =
                properties.Where(p => !propiedadesOcultar.Contains(p.PropertyName)).ToList();

            return properties;
        }
    }
}
