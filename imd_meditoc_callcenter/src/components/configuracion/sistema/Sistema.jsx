import React, { useState, Fragment, useEffect } from "react";
import { IconButton, Tooltip } from "@material-ui/core";
import AddIcon from "@material-ui/icons/Add";
import InsertDriveFileIcon from "@material-ui/icons/InsertDriveFile";
import SistemaModulo from "./SistemaModulo";
import FormModulo from "./FormModulo";
import Simbologia from "./Simbologia";
import SubmoduloBarra from "../../SubmoduloBarra";
import SubmoduloContenido from "../../SubmoduloContenido";
import CGUController from "../../../controllers/CGUController";

/*************************************************************
 * Descripcion: Submódulo para la vista principal "SISTEMA" del portal Meditoc
 * Creado: Cristopher Noh
 * Fecha: 26/08/2020
 * Invocado desde: ContentMain
 *************************************************************/
const Sistema = (props) => {
    const { usuarioSesion, funcLoader, funcAlert } = props;

    const cguController = new CGUController();

    const [listaSistema, setListaSistema] = useState([]);
    const [modalAgregarModuloOpen, setModalAgregarModuloOpen] = useState(false);

    const funcGetPermisosXPerfil = async () => {
        funcLoader(true, "Consultado elementos del sistema...");
        const response = await cguController.funcGetPermisosXPeril();
        funcLoader();

        if (response.Code !== 0) {
            funcAlert(response.Message);
            return;
        }

        setListaSistema(response.Result);
    };

    const handleAgregarModuloOpen = () => {
        setModalAgregarModuloOpen(true);
    };

    const entModuloNuevo = {
        iIdModulo: 0,
        sNombre: "",
    };

    useEffect(() => {
        // eslint-disable-next-line
        funcGetPermisosXPerfil();
    }, []);

    return (
        <Fragment>
            <SubmoduloBarra title="SISTEMA">
                <Tooltip title="Nuevo módulo" arrow>
                    <IconButton onClick={handleAgregarModuloOpen}>
                        <InsertDriveFileIcon className="color-0" />
                    </IconButton>
                </Tooltip>
            </SubmoduloBarra>

            <SubmoduloContenido>
                {listaSistema.length > 0
                    ? listaSistema.map((modulo) => (
                          <SistemaModulo
                              key={modulo.iIdModulo}
                              modulo={modulo}
                              usuarioSesion={usuarioSesion}
                              funcGetPermisosXPerfil={funcGetPermisosXPerfil}
                              funcLoader={funcLoader}
                              funcAlert={funcAlert}
                          />
                      ))
                    : "(No hay módulos configurados)"}
                {}
            </SubmoduloContenido>

            <Simbologia />

            <FormModulo
                entModulo={entModuloNuevo}
                open={modalAgregarModuloOpen}
                setOpen={setModalAgregarModuloOpen}
                usuarioSesion={usuarioSesion}
                funcGetPermisosXPerfil={funcGetPermisosXPerfil}
                funcLoader={funcLoader}
                funcAlert={funcAlert}
            />
        </Fragment>
    );
};

export default Sistema;
