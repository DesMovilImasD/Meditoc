using CallCenter.Models;
using Plugin.Settings;
using Plugin.Settings.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace CallCenter.Helpers
{
    public static class Settings
    {
        private static ISettings AppSettings
        {
            get
            {
                return CrossSettings.Current;
            }
        }

        #region Setting Constants

        private const string SettingsKey = "settings_key";
        private static readonly string SettingsDefault = string.Empty;

        #endregion

        public static string GeneralSettings
        {
            get
            {
                return AppSettings.GetValueOrDefault(SettingsKey, SettingsDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(SettingsKey, value);
            }
        }

        #region -------- [TERMS AND CONDITIONS] --------
        /// <summary>
        /// vinculo para abrir la liga de terminos y condiciones
        /// </summary>
        private static string _LinkTermsAndConditions = "";
        public static string LinkTermsAndConditions
        {
            get { return AppSettings.GetValueOrDefault("_sTermsAndConditions", _LinkTermsAndConditions); }
            set { AppSettings.AddOrUpdateValue("_sTermsAndConditions", value); }
        }
        #endregion


        #region -------- [PRIVACITY] --------
        /// <summary>
        /// vinculo para abrir la liga de privacidad.
        /// </summary>
        private static string _LinkPrivacity = "";
        public static string LinkPrivacity
        {
            get { return AppSettings.GetValueOrDefault("_sLinkPrivacity", _LinkPrivacity); }
            set { AppSettings.AddOrUpdateValue("_sLinkPrivacity", value); }
        }
        #endregion

        #region ------- [COVID INFO] --------
        /// <summary>
        /// vinculo para la liga de covid id.
        /// </summary>
        private static string _LinkCovId = "";
        public static string LinkCovID
        {
            get { return AppSettings.GetValueOrDefault("_sLinkCovID", _LinkCovId); }
            set { AppSettings.AddOrUpdateValue("_sLinkCovID", value); }
        }



        #endregion

        #region -------- [ENABLE FOLIO NAVIGATION] -------

        /// <summary>
        /// tipo de folio utilizado para entrar a un videollamda o
        /// a la validacion del folio.
        /// </summary>
        private static bool _isFolio = false;
        public static bool IsFolio
        {
            get { return AppSettings.GetValueOrDefault("_bFolio", _isFolio); }
            set { AppSettings.AddOrUpdateValue("_bFolio", value); }
        }

        #endregion

        #region -------- [SERVER URL] --------
        // pruebas
        //private static string _sBaseUrl = "http://148.240.238.149";
        //produccion nuevo
        //private static string _sBaseUrl = "http://201.46.92.51";
        //private static string _sBaseUrl = "https://148.240.238.150/MeditocWS"; //Ruta temporal para el nuevo servicio de meditoc
        private static string _sBaseUrl = "https://productos.meditoc.com/MeditocWS/";
        public static string BaseUrl
        {
            get { return AppSettings.GetValueOrDefault("sBaseUrl", _sBaseUrl); }
            set { AppSettings.AddOrUpdateValue("sBaseUrl", value); }
        }

        // pruebas
        //private static string _sUrl = Settings.BaseUrl + "/WS";
        // produccion nuevo
        private static string _sUrl = Settings.BaseUrl + "";
        public static string sUrl
        {
            get { return AppSettings.GetValueOrDefault("sUrl", _sUrl); }
            set { AppSettings.AddOrUpdateValue("sUrl", value); }
        }


        public static string _couponEndpoint = Settings.BaseUrl + "/api/promociones/validar/cupon";
        public static string CouponUrlEndpoint
        {
            get { return AppSettings.GetValueOrDefault("_couponEndpoint", _couponEndpoint); }
            set { AppSettings.AddOrUpdateValue("_couponEndpoint", value); }
        }

        #endregion

        #region -------- [ICELINK CONFIGURATION] ---------

        /// <summary>
        /// Llave de conexion de icelink
        /// </summary>
        private static string _iceLinkKey = "";
        public static string IceLinkKey
        {
            get { return AppSettings.GetValueOrDefault("ICELINK_KEY", _iceLinkKey); }
            set { AppSettings.AddOrUpdateValue("ICELINK_KEY", value); }
        }

        /// <summary>
        /// llave de dominio de icelink
        /// </summary>
        private static string _iceLinkDomainKey = "";
        public static string IceLinkDomainKey
        {
            get { return AppSettings.GetValueOrDefault("ICELINK_DOMAIN_KEY", _iceLinkDomainKey); }
            set { AppSettings.AddOrUpdateValue("ICELINK_DOMAIN_KEY", value); }
        }

        /// <summary>
        /// ice link servers
        /// </summary>
        private static string _iceLinkServers = "";
        public static string IceLinkServers
        {
            get { return AppSettings.GetValueOrDefault("ICELINK_SERVERS", _iceLinkServers); }
            set { AppSettings.AddOrUpdateValue("ICELINK_SERVERS", value); }
        }
        #endregion

        #region -------- [CONFIGURACION DEL LOGIN] --------
        /// <summary>
        /// 1 = membresia
        /// 2 = orientacion unica
        /// </summary>
        private static int _productType = 0;
        public static int ProductType
        {
            get { return AppSettings.GetValueOrDefault("_productType", _productType); }
            set { AppSettings.AddOrUpdateValue("_productType", value); }
        }

        /// <summary>
        /// Campo el cual almacena el usuario a loguearse. 
        /// </summary>     
        private static string _sUserNameLogin = string.Empty;
        public static string sUserNameLogin
        {
            get { return AppSettings.GetValueOrDefault("sUserNameLogin", _sUserNameLogin); }
            set { AppSettings.AddOrUpdateValue("sUserNameLogin", value); }
        }

        /// <summary>
        /// Campo el cual almacena el password del usuario a loguearse. 
        /// </summary>   
        private static string _sPassLogin = string.Empty;
        public static string sPassLogin
        {
            get { return AppSettings.GetValueOrDefault("sPassLogin", _sPassLogin); }
            set { AppSettings.AddOrUpdateValue("sPassLogin", value); }
        }

        /// <summary>
        /// Campo el cual almacena el Nombre del usuario logueado. 
        /// </summary> 
        private static string _sUserName = string.Empty;
        public static string sUserName
        {
            get { return AppSettings.GetValueOrDefault("sUserName", _sUserName); }
            set { AppSettings.AddOrUpdateValue("sUserName", value); }
        }

        /// <summary>
        /// Campo el cual almacena el tipo de usuari logueado. 
        /// </summary> 
        private static bool _bDoctor = false;
        public static bool bDoctor
        {
            get { return AppSettings.GetValueOrDefault("bDoctor", _bDoctor); }
            set { AppSettings.AddOrUpdateValue("bDoctor", value); }
        }

        /// <summary>
        /// Campo el cual almacena la bandera para indicar si el usuario sigue en sesion. 
        /// </summary> 
        private static bool _bSession = false;
        public static bool bSession
        {
            get { return AppSettings.GetValueOrDefault("bSession", _bSession); }
            set { AppSettings.AddOrUpdateValue("bSession", value); }
        }

        /// <summary>
        /// Campo el cual almacena el sexo del usuario. 
        /// </summary> 
        private static string _sSexo = "";
        public static string sSexo
        {
            get { return AppSettings.GetValueOrDefault("sSexo", _sSexo); }
            set { AppSettings.AddOrUpdateValue("sSexo", value); }
        }

        /// <summary>
        /// Campo el cual almacena el UID del usuario usado en CometChat 
        /// </summary> 
        private static string _sUsuarioUID = "";
        public static string sUsuarioUID
        {
            get { return AppSettings.GetValueOrDefault("sUsuarioUID", _sUsuarioUID); }
            set { AppSettings.AddOrUpdateValue("sUsuarioUID", value); }
        }

        #endregion

        #region [Configuración Solicitud Chat]
        /// <summary>
        /// Campo el cual indica si esta disponible un chat con el DR. 
        /// </summary> 
        private static bool _bRespuestaChat = false;
        public static bool bRespuestaChat
        {
            get { return AppSettings.GetValueOrDefault("bRespuestaChat", _bRespuestaChat); }
            set { AppSettings.AddOrUpdateValue("bRespuestaChat", value); }
        }

        /// <summary>
        /// Campo el cual almacena el ID del DR disponible para el chat. 
        /// </summary> 
        private static string _sUIDDR = "";
        public static string sUIDDR
        {
            get { return AppSettings.GetValueOrDefault("sUIDDR", _sUIDDR); }
            set { AppSettings.AddOrUpdateValue("sUIDDR", value); }
        }

        //public static string _COVIDFolio = "";
        //public static string COVIDFolio
        //{
        //    get { return AppSettings.GetValueOrDefault("covid", _COVIDFolio); }
        //    set { AppSettings.AddOrUpdateValue("covid", value); }
        //}

        #endregion

        /// <summary>
        /// Campo el cual almacena el estatus del servicio de cometchat 
        /// </summary> 
        private static bool _bChatInicializado = false;
        public static bool bChatInicializado
        {
            get { return AppSettings.GetValueOrDefault("bChatInicializado", _bChatInicializado); }
            set { AppSettings.AddOrUpdateValue("bChatInicializado", value); }
        }

        /// <summary>
        /// Campo el cual almacena una bandera de logueo
        /// </summary> 
        private static bool _bLogueado = false;
        public static bool bLogueado
        {
            get { return AppSettings.GetValueOrDefault("bLogueado", _bLogueado); }
            set { AppSettings.AddOrUpdateValue("bLogueado", value); }
        }

        /// <summary>
        /// Campo el cual almacena una bandera que indica si el poopup esta activo
        /// </summary> 
        private static bool _bPoPupActivo = false;
        public static bool bPoPupActivo
        {
            get { return AppSettings.GetValueOrDefault("bPoPupActivo", _bPoPupActivo); }
            set { AppSettings.AddOrUpdateValue("bPoPupActivo", value); }
        }

        /// <summary>
        /// Campo el cual almacena una bandera de logueo
        /// </summary> 
        private static bool _bClosePopPup = true;
        public static bool bClosePopPup
        {
            get { return AppSettings.GetValueOrDefault("bClosePopPup", _bClosePopPup); }
            set { AppSettings.AddOrUpdateValue("bClosePopPup", value); }
        }
        /// <summary>
        /// Campo el cual almacena una bandera de logueo
        /// </summary> 
        private static bool _bClicButton = false;
        public static bool bClicButton
        {
            get { return AppSettings.GetValueOrDefault("bClicButton", _bClicButton); }
            set { AppSettings.AddOrUpdateValue("bClicButton", value); }
        }

        /// <summary>
        /// Campo el cual almacena un error persistente. 
        /// </summary> 
        private static string _sError = "";
        public static string sError
        {
            get { return AppSettings.GetValueOrDefault("sError", _sError); }
            set { AppSettings.AddOrUpdateValue("sError", value); }
        }

        /// <summary>
        /// Campo el cual almacena el Paso actual de la recuperación de la contraseña. 
        /// </summary> 
        private static int _iPaso = 1;
        public static int iPaso
        {
            get { return AppSettings.GetValueOrDefault("iPaso", _iPaso); }
            set { AppSettings.AddOrUpdateValue("iPaso", value); }
        }

        /// <summary>
        /// Campo el cual almacena el numero de telefono de comuicador con los DRs. 
        /// </summary> 
        private static string _sTelefonoDRs = "9994017301";
        public static string sTelefonoDRs
        {
            get { return AppSettings.GetValueOrDefault("sTelefonoDRs", _sTelefonoDRs); }
            set { AppSettings.AddOrUpdateValue("sTelefonoDRs", value); }
        }

        private static int iidUsuario = 0;
        public static int iIdUsuario
        {
            get { return AppSettings.GetValueOrDefault("iIdUsuario", iidUsuario); }
            set { AppSettings.AddOrUpdateValue("iIdUsuario", value); }
        }

        private static string _sfolio = "";
        public static string sFolio
        {
            get { return AppSettings.GetValueOrDefault("sFolio", _sfolio); }
            set { AppSettings.AddOrUpdateValue("sFolio", value); }
        }
        private static string _sinstitucion = "";
        public static string sInstitucion
        {
            get { return AppSettings.GetValueOrDefault("sInstitucion", _sinstitucion); }
            set { AppSettings.AddOrUpdateValue("sInstitucion", value); }
        }

        /// <summary>
        /// Campo el cual almacena la bandera para indicar si el usuario asepto terminos y condiciones. 
        /// </summary> 
        private static bool _bTerminoYcondiciones = false;
        public static bool bTerminoYcondiciones
        {
            get { return AppSettings.GetValueOrDefault("bTerminoYcondiciones", _bTerminoYcondiciones); }
            set { AppSettings.AddOrUpdateValue("bTerminoYcondiciones", value); }
        }

        /// <summary>
        /// Campo el cual almacena la bandera para indicar si hay algun proceso en marcha. 
        /// </summary> 
        private static bool _bEnProceso = false;
        public static bool bEnProceso
        {
            get { return AppSettings.GetValueOrDefault("bEnProceso", _bEnProceso); }
            set { AppSettings.AddOrUpdateValue("bEnProceso", value); }
        }

        /// <summary>
        /// 
        /// </summary>
        private static bool _bCancelaDoctor = false;
        public static bool bCancelaDoctor
        {
            get { return AppSettings.GetValueOrDefault("bCancelaDoctor", _bCancelaDoctor); }
            set { AppSettings.AddOrUpdateValue("bCancelaDoctor", value); }
        }

        /// <summary>
        /// llave de conekta
        /// </summary>
        private static string _conektaPublicKey = "key_GyCqFsGWvYaFP3a7C9Lyfjg";
        public static string ConektaPublicKey
        {
            get { return AppSettings.GetValueOrDefault("_conektaPublicKey", _conektaPublicKey); }
            set { AppSettings.AddOrUpdateValue("_conektaPublicKey", value); }
        }

        /// <summary>
        /// email de contacto
        /// </summary>
        private static string _contactEmail = "prueba@gmail.com";
        public static string ContactEmail
        {
            get { return AppSettings.GetValueOrDefault("_contactEmail", _contactEmail); }
            set { AppSettings.AddOrUpdateValue("_contactEmail", value); }
        }

        /// <summary>
        /// email de soporte
        /// </summary>
        private static string _supporEmail = "prueba@gmail.com";
        public static string SupportEmail
        {
            get { return AppSettings.GetValueOrDefault("_supportmail", _supporEmail); }
            set { AppSettings.AddOrUpdateValue("_supportmail", value); }
        }

        /// <summary>
        /// direccion de la empresa
        /// </summary>
        private static string _companyAddress = "";
        public static string CompanyAddress
        {
            get { return AppSettings.GetValueOrDefault("_companyAddress", _companyAddress); }
            set { AppSettings.AddOrUpdateValue("_companyAddress", value); }
        }

        /// <summary>
        /// telefono de la empresa
        /// </summary>
        private static string _companyPhone = "";
        public static string CompanyPhone
        {
            get { return AppSettings.GetValueOrDefault("_companyPhone", _companyPhone); }
            set { AppSettings.AddOrUpdateValue("_companyPhone", value); }
        }

        private static double _thresholdCouponDiscount = 0.9;
        public static double ThresholdCouponDiscount
        {
            get { return AppSettings.GetValueOrDefault("_thresholdCouponDiscount", _thresholdCouponDiscount); }
            set { AppSettings.AddOrUpdateValue("_thresholdCouponDiscount", value); }
        }

        private static double _iva = 0.16;
        public static double IVA
        {
            get { return AppSettings.GetValueOrDefault("_iva", _iva); }
            set { AppSettings.AddOrUpdateValue("_iva", value); }
        }

        private static string _monthlyPayments = "";
        public static string MonthlyPayments
        {
            get { return AppSettings.GetValueOrDefault("_monthlyPayments", _monthlyPayments); }
            set { AppSettings.AddOrUpdateValue("_monthlyPayments", value); }
        }

        private static bool _hasMonthsWithoutInterest = false;
        public static bool HasMonthsWithoutInterest
        {
            get { return AppSettings.GetValueOrDefault("_hasMonthsWithoutInterest", _hasMonthsWithoutInterest); }
            set { AppSettings.AddOrUpdateValue("_hasMonthsWithoutInterest", value); }
        }

        public static string AppKey { get; set; } = "qSVBJIQpOqtp0UfwzwX1ER6fNYR8YiPU/bw5CdEqYqk=";
        public static string AppToken { get; set; } = "Xx3ePv63cUTg77QPATmztJ3J8cdO1riA7g+lVRzOzhfnl9FnaVT1O2YIv8YCTVRZ";

        public static int iIdEspecialidad { get; set; }
        public static string sEspecialidad { get; set; }
        public static DateTime dtFechaVencimiento { get; set; }
        public static bool bEsAgendada { get; set; }
    }
}