import React, { useState, Fragment } from "react";
import { Accordion, AccordionSummary, AccordionDetails, IconButton, Tooltip } from "@material-ui/core";
import AddIcon from "@material-ui/icons/Add";
import ExpandMore from "@material-ui/icons/ExpandMore";
import DeleteIcon from "@material-ui/icons/Delete";
import AccountTreeIcon from "@material-ui/icons/AccountTree";
import { makeStyles } from "@material-ui/core/styles";
import theme from "../../../configurations/themeConfig";
import SistemaSubmodulo from "./SistemaSubmodulo";
import EditIcon from "@material-ui/icons/Edit";
import FormModulo from "./FormModulo";
import EliminarModulo from "./EliminarModulo";
import FormSubmodulo from "./FormSubmodulo";

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
 * Descripcion: Representa una fila de Módulo con sus botones de "Agregar submódulo", "Editar módulo" y "Eliminar módulo"
 * Creado: Cristopher Noh
 * Fecha: 26/08/2020
 * Invocado desde: Sistema
 *************************************************************/
const SistemaModulo = (props) => {
    const { modulo, usuarioSesion, funcGetPermisosXPerfil, funcLoader, funcAlert } = props;

    const classes = useStyles();
    const colorClass = "color-1";

    const [modalEditarModuloOpen, setModalEditarModuloOpen] = useState(false);
    const [modalEliminarModuloOpen, setModalEliminarModuloOpen] = useState(false);
    const [modalNuevoSubmoduloOpen, setModalNuevoSubmoduloOpen] = useState(false);

    const entSubmoduloNuevo = {
        iIdModulo: modulo.iIdModulo,
        iIdSubModulo: 0,
        sNombre: "",
    };

    const handleClickNuevoSubmodulo = (e) => {
        e.stopPropagation();
        setModalNuevoSubmoduloOpen(true);
    };

    const handleClickEditar = (e) => {
        e.stopPropagation();
        setModalEditarModuloOpen(true);
    };

    const handleClickEliminar = (e) => {
        e.stopPropagation();
        setModalEliminarModuloOpen(true);
    };

    return (
        <Fragment>
            <Accordion elevation={0} classes={{ root: classes.hideLineAcc }}>
                <AccordionSummary
                    expandIcon={
                        <Tooltip title={`Mostrar/Ocultar submódulos de ${modulo.sNombre}`} placement="top-end" arrow>
                            <ExpandMore className={colorClass} />
                        </Tooltip>
                    }
                    classes={{ content: classes.marginAcc }}
                >
                    <div className="align-self-center flx-grw-1">
                        <AccountTreeIcon className={colorClass + " vertical-align-middle"} />
                        <span className={colorClass + " size-15"}>
                            {modulo.sNombre} ({modulo.iIdModulo})
                        </span>
                    </div>
                    <Tooltip title={`Agregar un submódulo nuevo a ${modulo.sNombre}`} placement="top" arrow>
                        <IconButton onClick={handleClickNuevoSubmodulo}>
                            <AddIcon className={colorClass} />
                        </IconButton>
                    </Tooltip>
                    <Tooltip title={`Editar el módulo ${modulo.sNombre}`} placement="top" arrow>
                        <IconButton onClick={handleClickEditar}>
                            <EditIcon className={colorClass} />
                        </IconButton>
                    </Tooltip>
                    <Tooltip title={`Eliminar el módulo ${modulo.sNombre}`} placement="top" arrow>
                        <IconButton onClick={handleClickEliminar}>
                            <DeleteIcon className={colorClass} />
                        </IconButton>
                    </Tooltip>
                </AccordionSummary>
                <AccordionDetails>
                    <div className="acc-content">
                        {modulo.lstSubModulo.length > 0 ? (
                            modulo.lstSubModulo.map((submodulo) => (
                                <SistemaSubmodulo
                                    key={submodulo.iIdSubModulo}
                                    submodulo={submodulo}
                                    usuarioSesion={usuarioSesion}
                                    funcGetPermisosXPerfil={funcGetPermisosXPerfil}
                                    funcLoader={funcLoader}
                                    funcAlert={funcAlert}
                                />
                            ))
                        ) : (
                            <div className="center">(No hay submódulos configurados para este módulo)</div>
                        )}
                    </div>
                </AccordionDetails>
            </Accordion>
            <FormModulo
                entModulo={modulo}
                open={modalEditarModuloOpen}
                setOpen={setModalEditarModuloOpen}
                usuarioSesion={usuarioSesion}
                funcGetPermisosXPerfil={funcGetPermisosXPerfil}
                funcLoader={funcLoader}
                funcAlert={funcAlert}
            />
            <EliminarModulo
                entModulo={modulo}
                open={modalEliminarModuloOpen}
                setOpen={setModalEliminarModuloOpen}
                usuarioSesion={usuarioSesion}
                funcGetPermisosXPerfil={funcGetPermisosXPerfil}
                funcLoader={funcLoader}
                funcAlert={funcAlert}
            />
            <FormSubmodulo
                entSubmodulo={entSubmoduloNuevo}
                open={modalNuevoSubmoduloOpen}
                setOpen={setModalNuevoSubmoduloOpen}
                usuarioSesion={usuarioSesion}
                funcGetPermisosXPerfil={funcGetPermisosXPerfil}
                funcLoader={funcLoader}
                funcAlert={funcAlert}
            />
        </Fragment>
    );
};

export default SistemaModulo;
