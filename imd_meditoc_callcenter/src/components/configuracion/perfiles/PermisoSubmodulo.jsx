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
import theme from "../../../configurations/themeConfig";
import EliminarPermisoSubmodulo from "./EliminarPermisoSubmodulo";
import SeleccionarBoton from "./SeleccionarBoton";
import PermisoBoton from "./PermisoBoton";

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

    const classes = useStyles();
    const colorClass = "color-2";

    const [modalEliminarPermisoSubmoduloOpen, setModalEliminarPermisoSubmoduloOpen] = useState(false);
    const [modalSeleccionarBotonesOpen, setModalSeleccionarBotonesOpen] = useState(false);

    const [lstBotonesSistema, setLstBotonesSistema] = useState([]);
    const [lstBotonesPermiso, setLstBotonesPermiso] = useState([]);

    const handleClickEliminarPermisoSubmodulo = (e) => {
        e.stopPropagation();
        setModalEliminarPermisoSubmoduloOpen(true);
    };

    const handleClickSeleccionarBotones = (e) => {
        e.stopPropagation();
        setModalSeleccionarBotonesOpen(true);
    };

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
    }, [lstSubmodulosPerfil]);

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
            <EliminarPermisoSubmodulo
                entPerfil={entPerfil}
                entSubmodulo={entSubmodulo}
                open={modalEliminarPermisoSubmoduloOpen}
                setOpen={setModalEliminarPermisoSubmoduloOpen}
                funcGetPermisosXPerfil={funcGetPermisosXPerfil}
                usuarioSesion={usuarioSesion}
                funcLoader={funcLoader}
                funcAlert={funcAlert}
            />
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

export default PermisoSubmodulo;
