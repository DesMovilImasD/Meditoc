import { MeditocHeaders, MeditocHeadersCT } from "../configurations/headersConfig";

const { serverMain } = require("../configurations/serverConfig");

class EmpresaController {
    constructor() {
        this.apiSaveEmpresa = "Api/Empresa/Create/Empresa";
        this.apiGetEmpresas = "Api/Empresa/Get/GetEmpresas";
    }

    async funcSaveEmpresa(entEmpresa) {
        let response = { Code: 0, Message: "", Result: {} };
        try {
            const apiResponse = await fetch(`${serverMain}${this.apiSaveEmpresa}`, {
                method: "POST",
                body: JSON.stringify(entEmpresa),
                headers: MeditocHeadersCT,
            });

            response = await apiResponse.json();
        } catch (error) {
            response.Code = -1;
            response.Message = "Ocurrió un error al intentar guardar la empresa";
        }
        return response;
    }

    async funcGetEmpresas(iIdEmpresa = null) {
        let response = { Code: 0, Message: "", Result: [] };
        try {
            const apiResponse = await fetch(`${serverMain}${this.apiGetEmpresas}?iIdEmpresa=${iIdEmpresa}`, {
                headers: MeditocHeaders,
            });

            response = await apiResponse.json();
        } catch (error) {
            response.Code = -1;
            response.Message = "Ocurrió un error al intentar obtener las empresas";
        }
        return response;
    }
}

export default EmpresaController;
