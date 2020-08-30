import PropTypes from "prop-types";
import React, { useState, useEffect } from "react";
import { Dialog, IconButton, Tooltip, Slide } from "@material-ui/core";
import SubmoduloBarra from "../../../utilidades/SubmoduloBarra";
import AddIcon from "@material-ui/icons/Add";
import CancelPresentationIcon from "@material-ui/icons/CancelPresentation";
import CGUController from "../../../../controllers/CGUController";
import SubmoduloContenido from "../../../utilidades/SubmoduloContenido";
import SeleccionarModulos from "./SeleccionarModulo";
import PermisoModulo from "./PermisoModulo";
import Simbologia from "../sistema/Simbologia";

const Transition = React.forwardRef(function Transition(props, ref) {
    return <Slide direction="up" ref={ref} {...props} />;
});

/*************************************************************
 * Descripcion: Representa la ventana de administración de los permisos para el perfil previamente seleccionado
 * Creado: Cristopher Noh
 * Fecha: 28/08/2020
 * Invocado desde: Perfiles
 *************************************************************/
const Permisos = (props) => {
    const { entPerfil, listaSistema, open, setOpen, usuarioSesion, funcLoader, funcAlert } = props;

    //Lista de modulos que el perfil tiene permisos para acceder
    const [listaPermisosModulo, setListaPermisosModulo] = useState([]);

    //State para mostrar/ocultar el modal para seleccionar los módulos disponibles para que el usuario les de permisos
    const [modalSeleccionarModulosOpen, setModalSeleccionarModulosOpen] = useState(false);

    //Funcion para esta ventana de administrarció de permisos
    const handleClose = () => {
        setOpen(false);
    };

    //Función para abrir el modal para seleccionar los módulos disponibles para que el usuario les de permisos
    const handleClickSeleccionarModulos = () => {
        setModalSeleccionarModulosOpen(true);
    };

    //Consumir servicio para obtener todos los permisos actuales del perfil seleccionado
    const funcGetPermisosXPerfil = async () => {
        funcLoader(true, "Consultado permisos del perfil...");

        const cguController = new CGUController();
        const response = await cguController.funcGetPermisosXPeril(entPerfil.iIdPerfil);

        if (response.Code !== 0) {
            funcAlert(response.Message);
            return;
        }
        funcLoader();

        setListaPermisosModulo(response.Result);
    };

    //Actualizar la lista de permisos actuales del perfil seleccionado
    useEffect(() => {
        if (entPerfil.iIdPerfil !== 0 && open === true) {
            funcGetPermisosXPerfil();
        }

        // eslint-disable-next-line
    }, [entPerfil]);

    return (
        <Dialog fullScreen open={open} onClose={handleClose} TransitionComponent={Transition}>
            <SubmoduloBarra title={"Administrar permisos para " + entPerfil.sNombre}>
                <Tooltip title="Cerrar ventana" arrow>
                    <IconButton onClick={handleClose}>
                        <CancelPresentationIcon className="color-0" />
                    </IconButton>
                </Tooltip>
                <Tooltip title="Agregar permisos a módulos de Meditoc CallCenter" arrow>
                    <IconButton onClick={handleClickSeleccionarModulos}>
                        <AddIcon className="color-0" />
                    </IconButton>
                </Tooltip>
            </SubmoduloBarra>
            <div>
                <SubmoduloContenido>
                    {listaPermisosModulo.length > 0 ? (
                        listaPermisosModulo.map((modulo) => (
                            <PermisoModulo
                                key={modulo.iIdModulo}
                                entPerfil={entPerfil}
                                entModulo={modulo}
                                listaSistema={listaSistema}
                                listaPermisosPerfil={listaPermisosModulo}
                                funcGetPermisosXPerfil={funcGetPermisosXPerfil}
                                usuarioSesion={usuarioSesion}
                                funcLoader={funcLoader}
                                funcAlert={funcAlert}
                            />
                        ))
                    ) : (
                        <div className="color-3 center">
                            (Este perfil no tiene ningún permiso para acceder a los módulos de Meditoc CallCenter)
                        </div>
                    )}
                </SubmoduloContenido>
            </div>

            <SeleccionarModulos
                entPerfil={entPerfil}
                listaPermisosPerfil={listaPermisosModulo}
                listaSistema={listaSistema}
                open={modalSeleccionarModulosOpen}
                setOpen={setModalSeleccionarModulosOpen}
                funcGetPermisosXPerfil={funcGetPermisosXPerfil}
                usuarioSesion={usuarioSesion}
                funcLoader={funcLoader}
                funcAlert={funcAlert}
            />
            <Simbologia />
        </Dialog>
    );
};

Permisos.propTypes = {
    entPerfil: PropTypes.shape({
        iIdPerfil: PropTypes.number,
        sNombre: PropTypes.string,
    }),
    funcAlert: PropTypes.func,
    funcLoader: PropTypes.func,
    listaSistema: PropTypes.array,
    open: PropTypes.bool,
    setOpen: PropTypes.func,
    usuarioSesion: PropTypes.object,
};

export default Permisos;
