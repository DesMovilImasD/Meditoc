import PropTypes from "prop-types";
import React, { Fragment, useState, useEffect } from "react";
import { makeStyles } from "@material-ui/core/styles";
import {
    Accordion,
    AccordionSummary,
    Tooltip,
    IconButton,
    AccordionDetails,
    Paper,
    Table,
    TableBody,
} from "@material-ui/core";
import ExpandMore from "@material-ui/icons/ExpandMore";
import WebIcon from "@material-ui/icons/Web";
import AddIcon from "@material-ui/icons/Add";
import BlockIcon from "@material-ui/icons/Block";
import theme from "../../../../configurations/themeConfig";
import SeleccionarBoton from "./SeleccionarBoton";
import PermisoBoton from "./PermisoBoton";
import CGUController from "../../../../controllers/CGUController";
import Confirmacion from "../../../utilidades/Confirmacion";

const useStyles = makeStyles({
    backColor: {
        backgroundColor: theme.palette.secondary.main,
    },
    marginAcc: {
        margin: "0px !important",
        borderBottom: `1px solid ${theme.palette.secondary.main}`,
    },
    hideLineAcc: {
        position: "initial",
    },
});

/*************************************************************
 * Descripcion: Representa un fila de un submódulo con las opciones de agregar permisos a sus botones y quitar el permiso de este submódulo
 * Creado: Cristopher Noh
 * Fecha: 28/08/2020
 * Invocado desde: PermisoModulo
 *************************************************************/
const PermisoSubmodulo = (props) => {
    const {
        entPerfil,
        entSubmodulo,
        lstSubmodulosSistema,
        lstSubmodulosPerfil,
        funcGetPermisosXPerfil,
        usuarioSesion,
        funcLoader,
        funcAlert,
    } = props;

    //Variables de estilos
    const classes = useStyles();
    const colorClass = "color-2";

    //State para mostrar/ocultar la alerta de confirmación para quitar permisos a este submódulo
    const [modalEliminarPermisoSubmoduloOpen, setModalEliminarPermisoSubmoduloOpen] = useState(false);

    //State para mostrar/ocultar el modal para seleccionar los botones disponibles de este submódulo para que el usuario les de permisos
    const [modalSeleccionarBotonesOpen, setModalSeleccionarBotonesOpen] = useState(false);

    //Lista de botones (de este submódulo) disponibles para que el usuario les de permisos
    const [lstBotonesSistema, setLstBotonesSistema] = useState([]);

    //Lista de botones de este submódulo que ya tienen permisos en el perfil
    const [lstBotonesPermiso, setLstBotonesPermiso] = useState([]);

    //Filtrar los botones de este submódulo para mostrar únicamente los pertenecen a este submódulo
    useEffect(() => {
        let lstBotontesSistemaTemp = [];
        try {
            lstBotontesSistemaTemp = lstSubmodulosSistema.find((x) => x.iIdSubModulo === entSubmodulo.iIdSubModulo)
                .lstBotones;
        } catch (error) {}
        setLstBotonesSistema(lstBotontesSistemaTemp);

        let lstBotonesPerfilTemp = [];
        try {
            lstBotonesPerfilTemp = lstSubmodulosPerfil.find((x) => x.iIdSubModulo === entSubmodulo.iIdSubModulo)
                .lstBotones;
        } catch (error) {}
        setLstBotonesPermiso(lstBotonesPerfilTemp);

        // eslint-disable-next-line
    }, [lstSubmodulosPerfil]);

    //Funcion para abrir la alerta de confirmación para quitar permisos a este submódulo
    const handleClickEliminarPermisoSubmodulo = (e) => {
        e.stopPropagation();
        setModalEliminarPermisoSubmoduloOpen(true);
    };

    //Funcion para abrir el modal para seleccionar los botones disponibles de este submódulo para que el usuario les de permisos
    const handleClickSeleccionarBotones = (e) => {
        e.stopPropagation();
        setModalSeleccionarBotonesOpen(true);
    };

    //Consumir servicio para quitar permiso de acceso a este submódulo
    const funcEliminarPermisoSubmodulo = async () => {
        const listaPermisosParaGuardar = [
            {
                iIdPerfil: entPerfil.iIdPerfil,
                iIdModulo: entSubmodulo.iIdModulo,
                iIdSubModulo: entSubmodulo.iIdSubModulo,
                iIdBoton: 0,
                iIdUsuarioMod: usuarioSesion.iIdUsuario,
                bActivo: false,
                bBaja: true,
            },
        ];

        funcLoader(true, "Removiendo permisos de submódulo...");

        const cguController = new CGUController();
        const response = await cguController.funcSavePermiso(listaPermisosParaGuardar);

        if (response.Code !== 0) {
            funcAlert(response.Message);
        } else {
            setModalEliminarPermisoSubmoduloOpen(false);
            funcAlert(response.Message, "success");
            funcGetPermisosXPerfil();
        }

        funcLoader();
    };

    return (
        <Fragment>
            <Accordion elevation={0} classes={{ root: classes.hideLineAcc }}>
                <AccordionSummary
                    expandIcon={
                        <Tooltip title={`Mostrar/Ocultar botones de ${entSubmodulo.sNombre}`} placement="top-end" arrow>
                            <ExpandMore className={colorClass} />
                        </Tooltip>
                    }
                    classes={{ content: classes.marginAcc }}
                >
                    <div className="align-self-center flx-grw-1">
                        <WebIcon className={colorClass + " vertical-align-middle"} />
                        <span className={colorClass + " size-15"}>
                            {entSubmodulo.sNombre} ({entSubmodulo.iIdSubModulo})
                        </span>
                    </div>
                    <Tooltip title={`Agregar permisos a botones de ${entSubmodulo.sNombre}`} placement="top" arrow>
                        <IconButton onClick={handleClickSeleccionarBotones}>
                            <AddIcon className={colorClass} />
                        </IconButton>
                    </Tooltip>
                    <Tooltip title={`Quitar permiso para el submódulo ${entSubmodulo.sNombre}`} placement="top" arrow>
                        <IconButton onClick={handleClickEliminarPermisoSubmodulo}>
                            <BlockIcon className={colorClass} />
                        </IconButton>
                    </Tooltip>
                </AccordionSummary>
                <AccordionDetails>
                    <div className="acc-content">
                        <Paper elevation={0}>
                            <Table size="small">
                                <TableBody>
                                    {lstBotonesPermiso.length > 0 ? (
                                        lstBotonesPermiso.map((boton) => (
                                            <PermisoBoton
                                                key={boton.iIdBoton}
                                                entPerfil={entPerfil}
                                                entBoton={boton}
                                                usuarioSesion={usuarioSesion}
                                                funcGetPermisosXPerfil={funcGetPermisosXPerfil}
                                                funcLoader={funcLoader}
                                                funcAlert={funcAlert}
                                            />
                                        ))
                                    ) : (
                                        <div className="color-3 center">
                                            (Este submódulo no tiene permisos para acceder a los botones de{" "}
                                            {entSubmodulo.sNombre})
                                        </div>
                                    )}
                                </TableBody>
                            </Table>
                        </Paper>
                    </div>
                </AccordionDetails>
            </Accordion>
            <Confirmacion
                title="Quitar permiso submódulo"
                open={modalEliminarPermisoSubmoduloOpen}
                setOpen={setModalEliminarPermisoSubmoduloOpen}
                okFunc={funcEliminarPermisoSubmodulo}
            >
                ¿Desea remover el permiso para el submódulo {entSubmodulo.sNombre} y todos sus botones?
            </Confirmacion>
            <SeleccionarBoton
                entPerfil={entPerfil}
                entSubmodulo={entSubmodulo}
                lstBotonesSistema={lstBotonesSistema}
                lstBotonesPermiso={lstBotonesPermiso}
                open={modalSeleccionarBotonesOpen}
                setOpen={setModalSeleccionarBotonesOpen}
                funcGetPermisosXPerfil={funcGetPermisosXPerfil}
                usuarioSesion={usuarioSesion}
                funcLoader={funcLoader}
                funcAlert={funcAlert}
            />
        </Fragment>
    );
};

PermisoSubmodulo.propTypes = {
    entPerfil: PropTypes.shape({
        iIdPerfil: PropTypes.number,
    }),
    entSubmodulo: PropTypes.shape({
        iIdModulo: PropTypes.number,
        iIdSubModulo: PropTypes.number,
        sNombre: PropTypes.string,
    }),
    funcAlert: PropTypes.func,
    funcGetPermisosXPerfil: PropTypes.func,
    funcLoader: PropTypes.func,
    lstSubmodulosPerfil: PropTypes.array,
    lstSubmodulosSistema: PropTypes.array,
    usuarioSesion: PropTypes.shape({
        iIdUsuario: PropTypes.number,
    }),
};

export default PermisoSubmodulo;
