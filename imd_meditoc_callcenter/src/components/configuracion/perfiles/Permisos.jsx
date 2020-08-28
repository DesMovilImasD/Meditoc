import React, { useState, useEffect } from "react";
import { Dialog, IconButton, Tooltip, Slide } from "@material-ui/core";
import SubmoduloBarra from "../../SubmoduloBarra";
import AddIcon from "@material-ui/icons/Add";
import CancelPresentationIcon from "@material-ui/icons/CancelPresentation";
import CGUController from "../../../controllers/CGUController";
import SubmoduloContenido from "../../SubmoduloContenido";
import SeleccionarModulos from "./SeleccionarModulo";
import PermisoModulo from "./PermisoModulo";
import Simbologia from "../sistema/Simbologia";

const Transition = React.forwardRef(function Transition(props, ref) {
    return <Slide direction="up" ref={ref} {...props} />;
});

const Permisos = (props) => {
    const { entPerfil, listaSistema, open, setOpen, usuarioSesion, funcLoader, funcAlert } = props;

    const cguController = new CGUController();

    const [listaPermisosPerfil, setListaPermisosPerfil] = useState([]);
    const [modalSeleccionarModulosOpen, setModalSeleccionarModulosOpen] = useState(false);

    const handleClose = () => {
        setOpen(false);
    };

    const handleClickSeleccionarModulos = () => {
        setModalSeleccionarModulosOpen(true);
    };

    //Obtener todos los permisos actuales del perfil a administrar
    const funcGetPermisosXPerfil = async () => {
        funcLoader(true, "Consultado permisos del perfil...");
        const response = await cguController.funcGetPermisosXPeril(entPerfil.iIdPerfil);
        funcLoader();

        if (response.Code !== 0) {
            funcAlert(response.Message);
            return;
        }

        setListaPermisosPerfil(response.Result);
    };

    useEffect(() => {
        if (entPerfil.iIdPerfil !== 0 && open === true) {
            funcGetPermisosXPerfil();
        }
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
                    {listaPermisosPerfil.length > 0 ? (
                        listaPermisosPerfil.map((modulo) => (
                            <PermisoModulo
                                key={modulo.iIdModulo}
                                entPerfil={entPerfil}
                                entModulo={modulo}
                                listaSistema={listaSistema}
                                listaPermisosPerfil={listaPermisosPerfil}
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
                listaPermisosPerfil={listaPermisosPerfil}
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

export default Permisos;
