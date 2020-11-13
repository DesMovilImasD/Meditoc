using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BC.CallCenterPortable.Models
{
    public class SerializeModel
    {
        private struct LogObject
        {
            public long FcCode { get; set; }
            public string FcMessage { get; set; }
            public List<object> FlstArgs { get; set; }
        }
        public static string Serialize(long pcCode, string pcMessage, params object[] args)
        {
            try
            {
                LogObject logObject = new LogObject();
                logObject.FcCode = pcCode;
                logObject.FcMessage = pcMessage;
                logObject.FlstArgs = new List<object>();

                foreach (object item in args)
                {
                    logObject.FlstArgs.Add(item);
                }

                string mcSerialize = JsonConvert.SerializeObject(logObject);

                return mcSerialize;
            }
            catch (Exception)
            {
                return "Los parámetros no se pudieron serializar";
            }
        }
    }
}
