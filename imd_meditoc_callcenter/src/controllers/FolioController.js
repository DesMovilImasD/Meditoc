import { MeditocHeaders, MeditocHeadersCT } from "../configurations/headersConfig";

const { serverMain } = require("../configurations/serverConfig");

class FolioController {
    constructor() {
        this.apiCrearFolioEmpresa = "Api/Folio/Create/FolioEmpresa";
        this.apiObtenerFolios = "Api/Folios/Get/Report";
        this.apiUpdFechaVencimiento = "Api/Folio/Update/FechaVencimiento";
        this.apiDelFoliosEmpresa = "Api/Folio/Delete/FoliosEmpresa";
        this.apiSaveVentaCalle = "Api/Folio/Save/Folio/VentaCalle";
        this.apiVerificarVentaCalle = "Api/Folio/Verificar/Folio/VentaCalle";
        this.apiDescargarPlantilla = "Api/Folio/Get/Folio/VentaCalle/Plantilla";
        this.apiVerificarArchivo = "Api/Folio/Verificar/Archivo";
        this.apiGenerarFolioArchivo = "Api/Folio/Generar/Folio/Archivo";
    }

    async funcCrearFoliosEmpresa(entFolioEmpresa) {
        let response = { Code: 0, Message: "", Result: false };
        try {
            const apiResponse = await fetch(`${serverMain}${this.apiCrearFolioEmpresa}`, {
                method: "POST",
                body: JSON.stringify(entFolioEmpresa),
                headers: MeditocHeadersCT,
            });

            response = await apiResponse.json();
        } catch (error) {
            response.Code = -1;
            response.Message = "Ocurrió un error al intentar crear los folios de la empresa";
        }
        return response;
    }

    async funcGetFolios(
        piIdFolio = null,
        piIdEmpresa = null,
        piIdProducto = null,
        piIdOrigen = null,
        psFolio = "",
        psOrdenConekta = "",
        pbTerminosYCondiciones = null,
        pbActivo = true,
        pbBaja = false
    ) {
        let response = { Code: 0, Message: "", Result: [] };
        try {
            const apiResponse = await fetch(
                `${serverMain}${this.apiObtenerFolios}?piIdFolio=${piIdFolio}&piIdEmpresa=${piIdEmpresa}&piIdProducto=${piIdProducto}&piIdOrigen=${piIdOrigen}&psFolio=${psFolio}&psOrdenConekta=${psOrdenConekta}&pbTerminosYCondiciones=${pbTerminosYCondiciones}&pbActivo=${pbActivo}&pbBaja=${pbBaja}`,
                {
                    headers: MeditocHeaders,
                }
            );

            response = await apiResponse.json();
        } catch (error) {
            response.Code = -1;
            response.Message = "Ocurrió un error al intentar obtener los folios";
        }
        return response;
    }

    async funcUpdFechaVencimiento(entFolioFV) {
        let response = { Code: 0, Message: "", Result: false };
        try {
            const apiResponse = await fetch(`${serverMain}${this.apiUpdFechaVencimiento}`, {
                method: "POST",
                body: JSON.stringify(entFolioFV),
                headers: MeditocHeadersCT,
            });

            response = await apiResponse.json();
        } catch (error) {
            response.Code = -1;
            response.Message = "Ocurrió un error al intentar modificar la fecha de vencimiento de los folios";
        }
        return response;
    }

    async funcEliminarFoliosEmpresa(entFolioFV) {
        let response = { Code: 0, Message: "", Result: false };
        try {
            const apiResponse = await fetch(`${serverMain}${this.apiDelFoliosEmpresa}`, {
                method: "POST",
                body: JSON.stringify(entFolioFV),
                headers: MeditocHeadersCT,
            });

            response = await apiResponse.json();
        } catch (error) {
            response.Code = -1;
            response.Message = "Ocurrió un error al intentar eliminar los folios";
        }
        return response;
    }

    async funcVerificarVentaCalle(archivoFolios) {
        let response = { Code: 0, Message: "", Result: [] };
        try {
            const apiResponse = await fetch(`${serverMain}${this.apiVerificarVentaCalle}`, {
                method: "POST",
                body: archivoFolios,
                headers: MeditocHeaders,
            });

            response = await apiResponse.json();
        } catch (error) {
            response.Code = -1;
            response.Message = "Ocurrió un error al intentar verificar los folios";
        }
        return response;
    }

    async funcVerificarArchivo(piIdEmpresa = 0, piIdProducto = 0, archivoFolios = new File()) {
        let response = { Code: 0, Message: "", Result: {} };
        try {
            const apiResponse = await fetch(
                `${serverMain}${this.apiVerificarArchivo}?piIdEmpresa=${piIdEmpresa}&piIdProducto=${piIdProducto}`,
                {
                    method: "POST",
                    body: archivoFolios,
                    headers: MeditocHeaders,
                }
            );
            response = await apiResponse.json();
        } catch (error) {
            response.Code = -1;
            response.Message = "Ocurrió un error al intentar verificar el archivo";
        }
        return response;
    }

    async funcGenerarFolioArchivo(piIdEmpresa = 0, piIdProducto = 0, piIdUsuarioMod = 0, archivoFolios = new File()) {
        let response = { Code: 0, Message: "", Result: false };
        try {
            const apiResponse = await fetch(
                `${serverMain}${this.apiGenerarFolioArchivo}?piIdEmpresa=${piIdEmpresa}&piIdProducto=${piIdProducto}&piIdUsuarioMod=${piIdUsuarioMod}`,
                {
                    method: "POST",
                    body: archivoFolios,
                    headers: MeditocHeaders,
                }
            );
            response = await apiResponse.json();
        } catch (error) {
            response.Code = -1;
            response.Message = "Ocurrió un error al generar los folios desde el archivo";
        }
        return response;
    }

    async funcSaveVentaCalle(piIdUsuarioMod, sFolioEmpresa, archivoFolios) {
        let response = { Code: 0, Message: "", Result: false };
        try {
            const apiResponse = await fetch(
                `${serverMain}${this.apiSaveVentaCalle}?piIdUsuarioMod=${piIdUsuarioMod}&sFolioEmpresa=${sFolioEmpresa}`,
                {
                    method: "POST",
                    body: archivoFolios,
                    headers: MeditocHeaders,
                }
            );

            response = await apiResponse.json();
        } catch (error) {
            response.Code = -1;
            response.Message = "Ocurrió un error al intentar guardar los folios";
        }
        return response;
    }

    async funcDescargarPlantilla() {
        let response = { Code: 0, Message: "", Result: false };
        const errorMessage = "Ocurrió un error al intentar descargar la plantilla";
        try {
            const apiResponse = await fetch(`${serverMain}${this.apiDescargarPlantilla}`, {
                method: "GET",
                headers: MeditocHeaders,
            });

            if (apiResponse.ok) {
                let file = await apiResponse.blob();
                let link = document.createElement("a");
                link.href = window.URL.createObjectURL(file);
                link.download = "plantilla-folios-venta-calle.xlsx";
                link.click();
                link.remove();
                response.Message = "La plantilla se ha descargado correctamente";
            } else {
                response.Code = -1;
                response.Message = errorMessage;
            }
        } catch (error) {
            response.Code = -1;
            response.Message = errorMessage;
        }
        return response;
    }
}

export default FolioController;
