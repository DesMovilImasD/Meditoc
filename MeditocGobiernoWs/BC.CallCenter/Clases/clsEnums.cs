using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Reflection;

namespace BC.CallCenter.Clases
{
    public class clsEnums
    {
        public static string sDescSemilla = "1NM0B1L14R14";

        public enum enumSemilla
        {
            [Description("1NM0B1L14R14")]
            sSemilla = 1,
        }

        public enum enumSemillaPassRecovery
        {
            [Description("S3rvic105")]
            sSemilla = 1,
        }

        public static string sDescripcionEnum(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes != null && attributes.Length > 0)
                return attributes[0].Description;
            else
                return value.ToString();
        }

        public enum enumBotones
        {
            [Description("ACCESO A VENTANA")]
            DOF = 1,
            [Description("ACCESO A BOTON")]
            DOE = 2
        };

        public enum enumEstatusBitacora
        {
            [Description("LOGIN")]
            LOGIN,
            [Description("ENCUESTA")]
            ENCUESTA,
            [Description("LLAMADA")]
            LLAMADA,
            [Description("TRAZADO")]
            TRAZADO,
            [Description("ERROR")]
            ERROR
        };
    }
}
