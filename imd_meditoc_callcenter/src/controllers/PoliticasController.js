const { MeditocHeaders } = require("../configurations/headersConfig");
const { serverMain } = require("../configurations/serverConfig");

class PoliticasController {
    constructor() {
        this.apiGetCatalogos = "Api/Politicas/Get/Sistema/Catalogos";
    }

    async funcGetCatalogos() {
        let response = { Code: 0, Message: "", Result: {} };
        try {
            const apiResponse = await fetch(`${serverMain}${this.apiGetCatalogos}`, {
                headers: MeditocHeaders,
            });

            response = await apiResponse.json();
        } catch (error) {
            response.Code = -1;
            response.Message = "Ocurrió un error al intentar obtener los catálogos del sistema";
        }
        return response;
    }
}

export default PoliticasController;
