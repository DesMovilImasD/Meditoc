const { serverMain } = require("../configurations/serverConfig");
const { MeditocHeadersCT, MeditocHeaders } = require("../configurations/headersConfig");

class ColaboradorController {
    constructor() {
        this.apiSaveColaborador = "Api/Colaborador/Save/CallCenter/Especialista";
        this.apiGetColaborador = "Api/Get/Colaboradores/CallCenter/Especialistas";
        this.apiSaveColaboradorFoto = "Api/Colaborador/Save/Foto";
        this.apiGetColaboradorFoto = "Api/Colaborador/Get/Foto";
        this.apiDeleteColaboradorFoto = "Api/Colaborador/Eliminar/Foto";
        this.apiGetDirectorio = "Api/Colaborador/Get/Directorio/Especialistas";
    }

    async funcSaveColaborador(entColaborador) {
        let response = { Code: 0, Message: "", Result: false };
        try {
            const apiResponse = await fetch(`${serverMain}${this.apiSaveColaborador}`, {
                method: "POST",
                body: JSON.stringify(entColaborador),
                headers: MeditocHeadersCT,
            });

            response = await apiResponse.json();
        } catch (error) {
            response.Code = -1;
            response.Message = "Ocurrió un error al intentar guardar el colaborador";
        }
        return response;
    }

    async funcGetColaboradores(
        piIdColaborador = null,
        piIdTipoDoctor = null,
        piIdEspecialidad = null,
        piIdUsuarioCGU = null
    ) {
        let response = { Code: 0, Message: "", Result: [] };
        try {
            const apiResponse = await fetch(
                `${serverMain}${this.apiGetColaborador}?piIdColaborador=${piIdColaborador}&piIdTipoDoctor=${piIdTipoDoctor}&piIdEspecialidad=${piIdEspecialidad}&piIdUsuarioCGU=${piIdUsuarioCGU}`,
                {
                    headers: MeditocHeaders,
                }
            );

            response = await apiResponse.json();
        } catch (error) {
            response.Code = -1;
            response.Message = "Ocurrió un error al intentar obtener los colaboradores";
        }
        return response;
    }

    async funcSaveColaboradorFoto(piIdColaborador, piIdUsuarioMod, foto) {
        let response = { Code: 0, Message: "", Result: false };
        try {
            const apiResponse = await fetch(
                `${serverMain}${this.apiSaveColaboradorFoto}?piIdColaborador=${piIdColaborador}&piIdUsuarioMod=${piIdUsuarioMod}`,
                {
                    method: "POST",
                    body: foto,
                    headers: MeditocHeaders,
                }
            );

            response = await apiResponse.json();
        } catch (error) {
            response.Code = -1;
            response.Message = "Ocurrió un error al intentar guardar la foto del colaborador";
        }
        return response;
    }

    async funcGetColaboradorFoto(piIdColaborador) {
        let response = { Code: 0, Message: "", Result: "" };
        try {
            const apiResponse = await fetch(
                `${serverMain}${this.apiGetColaboradorFoto}?piIdColaborador=${piIdColaborador}`,
                {
                    headers: MeditocHeaders,
                }
            );

            response = await apiResponse.json();
        } catch (error) {
            response.Code = -1;
            response.Message = "Ocurrió un error al intentar obtener la foto del colaborador";
        }
        return response;
    }

    async funcDeleteColaboradorFoto(piIdColaborador, piIdUsuarioMod) {
        let response = { Code: 0, Message: "", Result: false };
        try {
            const apiResponse = await fetch(
                `${serverMain}${this.apiDeleteColaboradorFoto}?piIdColaborador=${piIdColaborador}&piIdUsuarioMod=${piIdUsuarioMod}`,
                {
                    method: "POST",
                    headers: MeditocHeaders,
                }
            );

            response = await apiResponse.json();
        } catch (error) {
            response.Code = -1;
            response.Message = "Ocurrió un error al intentar eliminar la foto del colaborador";
        }
        return response;
    }

    async funcGetDirectorio(piIdEspecialidad = null, psBuscador = "", piPage = null, piPageSize = 0) {
        let response = { Code: 0, Message: "", Result: [] };
        try {
            const apiResponse = await fetch(
                `${serverMain}${this.apiGetDirectorio}?piIdEspecialidad=${piIdEspecialidad}&psBuscador=${psBuscador}&piPage=${piPage}&piPageSize=${piPageSize}`,
                {
                    headers: MeditocHeaders,
                }
            );

            response = await apiResponse.json();
        } catch (error) {
            response.Code = -1;
            response.Message = "Ocurrió un error al intentar obtener el directorio médico";
        }
        return response;
    }
}

export default ColaboradorController;
