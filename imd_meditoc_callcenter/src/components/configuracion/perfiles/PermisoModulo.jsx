import React, { Fragment, useState, useEffect } from "react";
import { makeStyles } from "@material-ui/core/styles";
import { Accordion, AccordionSummary, Tooltip, IconButton, AccordionDetails } from "@material-ui/core";
import ExpandMore from "@material-ui/icons/ExpandMore";
import AccountTreeIcon from "@material-ui/icons/AccountTree";
import AddIcon from "@material-ui/icons/Add";
import BlockIcon from "@material-ui/icons/Block";
import theme from "../../../configurations/themeConfig";
import EliminarPermisoModulo from "./EliminarPermisoModulo";
import SeleccionarSubmodulo from "./SeleccionarSubmodulo";
import PermisoSubmodulo from "./PermisoSubmodulo";

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

const PermisoModulo = (props) => {
    const {
        entPerfil,
        entModulo,
        listaSistema,
        listaPermisosPerfil,
        funcGetPermisosXPerfil,
        usuarioSesion,
        funcLoader,
        funcAlert,
    } = props;

    const [modalEliminarPermisoModuloOpen, setModalEliminarPermisoModuloOpen] = useState(false);
    const [modalSeleccionarSubmodulosOpen, setModalSeleccionarSubmodulosOpen] = useState(false);

    const [lstSubmodulosSistema, setLstSubmodulosSistema] = useState([]);
    const [lstSubmodulosPerfil, setLstSubmodulosPerfil] = useState([]);

    const handleClickEliminarPermisoModulo = (e) => {
        e.stopPropagation();
        setModalEliminarPermisoModuloOpen(true);
    };

    const handleClickAgregarSubmodulo = (e) => {
        e.stopPropagation();
        setModalSeleccionarSubmodulosOpen(true);
    };

    const classes = useStyles();
    const colorClass = "color-1";

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
    }, [listaPermisosPerfil]);

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

                    <Tooltip title={`Agregar permisos a submódulos de ${entModulo.sNombre}`} placement="top" arrow>
                        <IconButton onClick={handleClickAgregarSubmodulo}>
                            <AddIcon className={colorClass} />
                        </IconButton>
                    </Tooltip>
                    <Tooltip title={`Quitar permiso para el módulo ${entModulo.sNombre}`} placement="top" arrow>
                        <IconButton onClick={handleClickEliminarPermisoModulo}>
                            <BlockIcon className={colorClass} />
                        </IconButton>
                    </Tooltip>
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
            <EliminarPermisoModulo
                entPerfil={entPerfil}
                entModulo={entModulo}
                open={modalEliminarPermisoModuloOpen}
                setOpen={setModalEliminarPermisoModuloOpen}
                funcGetPermisosXPerfil={funcGetPermisosXPerfil}
                usuarioSesion={usuarioSesion}
                funcLoader={funcLoader}
                funcAlert={funcAlert}
            />
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

export default PermisoModulo;
