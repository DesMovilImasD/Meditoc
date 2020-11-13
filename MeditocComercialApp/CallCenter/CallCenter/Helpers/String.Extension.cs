using System;
namespace CallCenter.Helpers
{
	public static class StringExtension
	{
        /**
         * elimina la primera ocurrencia (token) encontrada en un
         * cadena de caracteres y convierte el resultado a double
         */
		public static Double RemoveFirstOcurrenceToDouble(this String data, String token)
        {
            int index = data.IndexOf(token   );
            string _str_cleaned = (index < 0)
                ? data
                : data.Remove(index, token.Length);

            return Double.TryParse(_str_cleaned, out Double value) ?
                value :
                throw new Exception("Invalid string conversion to double ");
        }

        /**
         * elimina todas las ocurrencias encontradas en un cadena de
         * caracteres y el resultado se convierte a double.
         */
        public static Double RemoveAllOcurrenceToDouble(this String data, String token)
        {
            var _str_cleaned = data.Replace(token, "");

            return Double.TryParse(_str_cleaned, out Double value) ?
                value :
                throw new Exception("Invalid string conversion to double");
        }


        public static string ToUnicode(this string code, string default_value)
        {
            try
            {
                int HexCode = int.Parse(code, System.Globalization.NumberStyles.HexNumber);
                string unicodeString = char.ConvertFromUtf32(HexCode);
                return unicodeString;
            }
            catch(Exception e)
            {
                return default_value;
            }
            
        }
	}
}
