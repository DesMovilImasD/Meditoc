import { Accordion, AccordionDetails, AccordionSummary, IconButton, Tooltip } from "@material-ui/core";
import React, { Fragment, useEffect, useState } from "react";

import AccountTreeIcon from "@material-ui/icons/AccountTree";
import BlockIcon from "@material-ui/icons/Block";
import CGUController from "../../../../controllers/CGUController";
import ExpandMore from "@material-ui/icons/ExpandMore";
import MeditocConfirmacion from "../../../utilidades/MeditocConfirmacion";
import PermisoSubmodulo from "./PermisoSubmodulo";
import PropTypes from "prop-types";
import SeleccionarSubmodulo from "./SeleccionarSubmodulo";
import SettingsIcon from "@material-ui/icons/Settings";
import { makeStyles } from "@material-ui/core/styles";
import theme from "../../../../configurations/themeConfig";

const useStyles = makeStyles({
    backColor: {
        backgroundColor: theme.palette.primary.main,
    },
    marginAcc: {
        margin: "0px !important",
        borderBottom: `1px solid ${theme.palette.primary.main}`,
    },
    hideLineAcc: {
        position: "initial",
    },
});

/*************************************************************
 * Descripcion: Representa una fila de un módulo con las opciones de agregar permisos a submódulo y quitar el permiso de este módulo
 * Creado: Cristopher Noh
 * Fecha: 28/08/2020
 * Invocado desde: Permisos
 *************************************************************/
const PermisoModulo = (props) => {
    const {
        entPerfil,
        entModulo,
        listaSistema,
        listaPermisosPerfil,
        funcGetPermisosXPerfil,
        usuarioSesion,
        permisos,
        funcLoader,
        funcAlert,
    } = props;

    //Variables de estilos
    const classes = useStyles();
    const colorClass = "color-1";

    //State para mostrar/ocultar la alerta de confirmación para quitar permisos a este módulo
    const [modalEliminarPermisoModuloOpen, setModalEliminarPermisoModuloOpen] = useState(false);

    //State para mostrar/ocultar el modal para seleccionar los submódulos disponibles de este módulo para darles permisos
    const [modalSeleccionarSubmodulosOpen, setModalSeleccionarSubmodulosOpen] = useState(false);

    //Lista de todos los submódulos disponibles para dar permisos de este módulo
    const [lstSubmodulosSistema, setLstSubmodulosSistema] = useState([]);

    //lista de los submódulos de este módulo a los cuales el perfil tiene permisos
    const [lstSubmodulosPerfil, setLstSubmodulosPerfil] = useState([]);

    //Filtar los submódulos del sistema para mostrar únicamente los que pertenecen a este módulo
    useEffect(() => {
        let lstSubmodulosSistemaTemp = [];
        try {
            lstSubmodulosSistemaTemp = listaSistema.find((x) => x.iIdModulo === entModulo.iIdModulo).lstSubModulo;
        } catch (error) {}
        setLstSubmodulosSistema(lstSubmodulosSistemaTemp);

        let lstSubmodulosPerfilTemp = [];
        try {
            lstSubmodulosPerfilTemp = listaPermisosPerfil.find((x) => x.iIdModulo === entModulo.iIdModulo).lstSubModulo;
        } catch (error) {}
        setLstSubmodulosPerfil(lstSubmodulosPerfilTemp);

        // eslint-disable-next-line
    }, [listaPermisosPerfil]);

    //Funcion para abrir la alerta de confirmación para quitar permisos a este módulo
    const handleClickEliminarPermisoModulo = (e) => {
        e.stopPropagation();
        setModalEliminarPermisoModuloOpen(true);
    };

    //Función para abrir el modal para seleccionar los submódulos disponibles de este módulo para darles permisos
    const handleClickAgregarSubmodulo = (e) => {
        e.stopPropagation();
        setModalSeleccionarSubmodulosOpen(true);
    };

    //Consumir servicio para quitar el permiso al perfil de acceder a este módulo
    const funcEliminarPermisoModulo = async () => {
        const listaPermisosParaGuardar = [
            {
                iIdPerfil: entPerfil.iIdPerfil,
                iIdModulo: entModulo.iIdModulo,
                iIdSubModulo: 0,
                iIdBoton: 0,
                iIdUsuarioMod: usuarioSesion.iIdUsuario,
                bActivo: false,
                bBaja: true,
            },
        ];

        funcLoader(true, "Removiendo permisos de módulo...");

        const cguController = new CGUController();
        const response = await cguController.funcSavePermiso(listaPermisosParaGuardar);

        if (response.Code === 0) {
            setModalEliminarPermisoModuloOpen(false);
            await funcGetPermisosXPerfil();

            funcAlert(response.Message, "success");
        } else {
            funcAlert(response.Message);
        }

        funcLoader();
    };

    return (
        <Fragment>
            <Accordion elevation={0} classes={{ root: classes.hideLineAcc }}>
                <AccordionSummary
                    expandIcon={
                        <Tooltip title={`Mostrar/Ocultar submódulos de ${entModulo.sNombre}`} placement="top-end" arrow>
                            <ExpandMore className={colorClass} />
                        </Tooltip>
                    }
                    classes={{ content: classes.marginAcc }}
                >
                    <div className="align-self-center flx-grw-1">
                        <AccountTreeIcon className={colorClass + " vertical-align-middle"} />
                        <span className={colorClass + " size-15"}>
                            {entModulo.sNombre} ({entModulo.iIdModulo})
                        </span>
                    </div>
                    {permisos.Botones["8"] !== undefined && ( //Agregar permisos a submódulos de
                        <Tooltip title={`${permisos.Botones["8"].Nombre} ${entModulo.sNombre}`} placement="top" arrow>
                            <IconButton onClick={handleClickAgregarSubmodulo}>
                                <SettingsIcon className={colorClass} />
                            </IconButton>
                        </Tooltip>
                    )}
                    {permisos.Botones["7"] !== undefined && ( //Quitar permiso para el módulo
                        <Tooltip title={`${permisos.Botones["7"].Nombre} ${entModulo.sNombre}`} placement="top" arrow>
                            <IconButton onClick={handleClickEliminarPermisoModulo}>
                                <BlockIcon className={colorClass} />
                            </IconButton>
                        </Tooltip>
                    )}
                </AccordionSummary>
                <AccordionDetails>
                    <div className="acc-content">
                        {lstSubmodulosPerfil.length > 0 ? (
                            lstSubmodulosPerfil.map((submodulo) => (
                                <PermisoSubmodulo
                                    key={submodulo.iIdSubModulo}
                                    entPerfil={entPerfil}
                                    entSubmodulo={submodulo}
                                    lstSubmodulosSistema={lstSubmodulosSistema}
                                    lstSubmodulosPerfil={lstSubmodulosPerfil}
                                    funcGetPermisosXPerfil={funcGetPermisosXPerfil}
                                    usuarioSesion={usuarioSesion}
                                    permisos={permisos}
                                    funcLoader={funcLoader}
                                    funcAlert={funcAlert}
                                />
                            ))
                        ) : (
                            <div className="color-3 center">
                                (Este módulo no tiene permisos para acceder a los submódulos de {entModulo.sNombre})
                            </div>
                        )}
                    </div>
                </AccordionDetails>
            </Accordion>
            <MeditocConfirmacion
                title="Quitar permiso a módulo"
                open={modalEliminarPermisoModuloOpen}
                setOpen={setModalEliminarPermisoModuloOpen}
                okFunc={funcEliminarPermisoModulo}
            >
                ¿Desea remover el permiso para el módulo {entModulo.sNombre} y todos sus submódulos y botones?
            </MeditocConfirmacion>
            <SeleccionarSubmodulo
                entPerfil={entPerfil}
                entModulo={entModulo}
                lstSubmodulosSistema={lstSubmodulosSistema}
                lstSubmodulosPerfil={lstSubmodulosPerfil}
                open={modalSeleccionarSubmodulosOpen}
                setOpen={setModalSeleccionarSubmodulosOpen}
                funcGetPermisosXPerfil={funcGetPermisosXPerfil}
                usuarioSesion={usuarioSesion}
                funcLoader={funcLoader}
                funcAlert={funcAlert}
            />
        </Fragment>
    );
};

PermisoModulo.propTypes = {
    entModulo: PropTypes.shape({
        iIdModulo: PropTypes.number,
        sNombre: PropTypes.string,
    }),
    entPerfil: PropTypes.shape({
        iIdPerfil: PropTypes.number,
    }),
    funcAlert: PropTypes.func,
    funcGetPermisosXPerfil: PropTypes.func,
    funcLoader: PropTypes.func,
    listaPermisosPerfil: PropTypes.array,
    listaSistema: PropTypes.array,
    usuarioSesion: PropTypes.shape({
        iIdUsuario: PropTypes.number,
    }),
};

export default PermisoModulo;
