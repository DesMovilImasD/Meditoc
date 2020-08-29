import PropTypes from "prop-types";
import React, { useState, Fragment, useEffect } from "react";
import { IconButton, Tooltip } from "@material-ui/core";
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

    //Servicios API
    const cguController = new CGUController();

    //Objeto vacío de un módulo
    const moduloEntidadVacia = {
        iIdModulo: 0,
        sNombre: "",
    };

    //Guardar lista de los elementos del sistema (módulos, submódulos y botones)
    const [listaSistema, setListaSistema] = useState([]);

    //Guardar state (Mostrar/Ocultar) del formulario para crear módulos
    const [modalAgregarModuloOpen, setModalAgregarModuloOpen] = useState(false);

    //Consumir servicio para consultar los elementos del sistema (módulos, submódulos y botones)
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

    //Funcion para mostrar el formulario para crear módulos
    const handleAgregarModuloOpen = () => {
        setModalAgregarModuloOpen(true);
    };

    //Consultar elementos del sistema en la primera carga de este componente
    useEffect(() => {
        funcGetPermisosXPerfil();

        // eslint-disable-next-line
    }, []);

    return (
        <Fragment>
            <SubmoduloBarra title="SISTEMA">
                <Tooltip title="Agregar un nuevo módulo" arrow>
                    <IconButton onClick={handleAgregarModuloOpen}>
                        <InsertDriveFileIcon className="color-0" />
                    </IconButton>
                </Tooltip>
            </SubmoduloBarra>

            <SubmoduloContenido>
                {listaSistema.length > 0 ? (
                    listaSistema.map((modulo) => (
                        <SistemaModulo
                            key={modulo.iIdModulo}
                            entModulo={modulo}
                            usuarioSesion={usuarioSesion}
                            funcGetPermisosXPerfil={funcGetPermisosXPerfil}
                            funcLoader={funcLoader}
                            funcAlert={funcAlert}
                        />
                    ))
                ) : (
                    <div className="center">(No hay módulos configurados)</div>
                )}
            </SubmoduloContenido>

            <Simbologia />

            <FormModulo
                entModulo={moduloEntidadVacia}
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

Sistema.propTypes = {
    funcAlert: PropTypes.func,
    funcLoader: PropTypes.func,
    usuarioSesion: PropTypes.object,
};

export default Sistema;
