import apiKeyToken from "./tokenConfig";
import { defaultTaxValue } from "./taxConfig";

const defaultMonthly = [
    {
        compra_minima: 300,
        descripcion: "3 meses sin intereses",
        meses: 3,
    },
    {
        compra_minima: 600,
        descripcion: "6 meses sin intereses",
        meses: 6,
    },
    {
        compra_minima: 900,
        descripcion: "9 meses sin intereses",
        meses: 9,
    },
    {
        compra_minima: 1200,
        descripcion: "12 meses sin intereses",
        meses: 12,
    },
];

const defaultAppInfo = {
    nMaximoDescuento: 0.9,
    sAvisoDePrivacidad: "#",
    sConektaPublicKey: apiKeyToken,
    sCorreoContacto: "contacto@meditoc.com",
    sCorreoSoporte: "contacto@meditoc.com",
    sDireccionEmpresa: "Calle 17 #113, Col. Itzimná, 97100, Mérida, Yuc.",
    sTelefonoEmpresa: "5551-003021",
    sTerminosYCondiciones: "#",
    nIVA: defaultTaxValue,
    bTieneMesesSinIntereses: defaultMonthly.length > 0,
    lstMensualidades: defaultMonthly,
};

export default defaultAppInfo;
export { defaultMonthly };
/**
 * SIMULACIÓN
 */
// const lstGetProducts = [
//     {
//         lstProducto: [],
//         iIdProducto: 294,
//         sNombre: "Orientación Médica",
//         sNombreCorto: "Médica",
//         sDescripcion: "Podrá realizar una orientación médica con nuestros especialistas.",
//         sResumen:
//             "Contará con un servicio de orientación médica válido por 24 horas, el cual le dará acceso a tener una llamada telefónica, chat y videollamada.",
//         fPrecio: 55.0,
//         iTiempoVigencia: 0,
//         sIcon: "f469",
//     },
//     {
//         lstProducto: [],
//         iIdProducto: 295,
//         sNombre: "Orientación Psicológica ",
//         sNombreCorto: "Psicológica",
//         sDescripcion: "Podrá realizar una orientación priscologica con nuestros especialistas.",
//         sResumen:
//             "Contará con un servicio de orientación psicológica válido por 24 horas, el cual le dará acceso a tener una llamada telefónica, chat y videollamada.",
//         fPrecio: 55.0,
//         iTiempoVigencia: 0,
//         sIcon: "f7f5",
//     },
//     {
//         lstProducto: [],
//         iIdProducto: 297,
//         sNombre: "Orientación Nutricional",
//         sNombreCorto: "Nutricional",
//         sDescripcion: "Podrá realizar una orientación nutricional con nuestros especialistas",
//         sResumen:
//             "Contará con un servicio de orientación nutricional válido por 24 horas, el cual le dará acceso a tener una llamada telefónica, chat y videollamada.",
//         fPrecio: 55.0,
//         iTiempoVigencia: 0,
//         sIcon: "f481",
//     },
// ];
// const lstGetProductMapped = lstGetProducts.map((product) => ({
//     name: product.sNombre,
//     shortName: product.sNombreCorto,
//     price: product.fPrecio,
//     id: product.iIdProducto,
//     productType: 2,
//     qty: 1,
//     icon: `&#x${product.sIcon};`,
//     monthsExpiration: 0,
//     info: product.sResumen,
//     selected: false,
// }));
// setLstOrientationProducts(lstGetProductMapped);

/**
 * SIMULACIÓN
 */
// const lstGetProducts = [
//     {
//         lstProducto: [],
//         iIdProducto: 292,
//         sNombre: "Membresía 6 meses",
//         sNombreCorto: "6 meses",
//         sDescripcion: "Membresía por 6  meses",
//         sResumen:
//             "Desde el primer día contará con servicio de orientación médica, nutricional y psicológica durante seis meses para todos los miembros de su familia, empleados de empresas u hogar y sus familias, el cual es simple de usar, tendrá respuesta inmediata y llamadas ilimitadas de orientación médica.",
//         fPrecio: 800.0,
//         iTiempoVigencia: 6,
//         sIcon: "f7e6",
//     },
//     {
//         lstProducto: [],
//         iIdProducto: 296,
//         sNombre: "Membresía 12 meses",
//         sNombreCorto: "1 año",
//         sDescripcion: "Membresía por 1 año",
//         sResumen:
//             "Desde el primer día contará con servicio de orientación médica, nutricional y psicológica los 365 días del año para todos los miembros de su familia, empleados de empresas u hogar y sus familias, el cual es simple de usar, tendrá respuesta inmediata y llamadas ilimitadas de orientación médica.",
//         fPrecio: 1450.0,
//         iTiempoVigencia: 12,
//         sIcon: "f7e6",
//     },
// ];
// const lstGetProductMapped = lstGetProducts.map((product) => ({
//     name: product.sNombre,
//     shortName: product.sNombreCorto,
//     price: product.fPrecio,
//     id: product.iIdProducto,
//     productType: 1,
//     qty: 1,
//     icon: `&#x${product.sIcon};`,
//     monthsExpiration: product.iTiempoVigencia,
//     info: product.sResumen,
//     selected: false,
// }));
// setLstMembershipProducts(lstGetProductMapped);
