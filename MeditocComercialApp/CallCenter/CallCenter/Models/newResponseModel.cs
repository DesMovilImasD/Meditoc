using System;
namespace CallCenter.Models
{
    public class newResponseModel<T>
    {

        public Int64 Code { get; set; }
        public string Message { get; set; }
        public T Result { get; set; }

    }
}
