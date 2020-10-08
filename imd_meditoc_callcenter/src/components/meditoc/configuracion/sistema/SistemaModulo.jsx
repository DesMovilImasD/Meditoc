import { Accordion, AccordionDetails, AccordionSummary, IconButton, Tooltip } from "@material-ui/core";
import React, { Fragment, useState } from "react";

import AccountTreeIcon from "@material-ui/icons/AccountTree";
import AddIcon from "@material-ui/icons/Add";
import CGUController from "../../../../controllers/CGUController";
import DeleteIcon from "@material-ui/icons/Delete";
import EditIcon from "@material-ui/icons/Edit";
import ExpandMore from "@material-ui/icons/ExpandMore";
import FormModulo from "./FormModulo";
import FormSubmodulo from "./FormSubmodulo";
import MeditocConfirmacion from "../../../utilidades/MeditocConfirmacion";
import PropTypes from "prop-types";
import SistemaSubmodulo from "./SistemaSubmodulo";
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
 * Descripcion: Representa una fila de Módulo con sus botones de "Agregar submódulo", "Editar módulo" y "Eliminar módulo"
 * Creado: Cristopher Noh
 * Fecha: 26/08/2020
 * Invocado desde: Sistema
 *************************************************************/
const SistemaModulo = (props) => {
    const {
        entModulo,
        usuarioSesion,
        funcGetPermisosXPerfil,
        funcUpdSystemInfo,
        permisos,
        funcLoader,
        funcAlert,
    } = props;

    const classes = useStyles();

    //Color para los íconos y letras de este componente
    const colorClass = "color-1";

    //Servicios API
    const cguController = new CGUController();

    //Entidad vacía de un submódulo
    const submoduloEntidadVacia = {
        iIdModulo: entModulo.iIdModulo,
        iIdSubModulo: 0,
        sNombre: "",
    };

    //State para mostrar/ocultar el formulario para editar este módulo
    const [modalEditarModuloOpen, setModalEditarModuloOpen] = useState(false);

    //State para mostrar/ocultar la alerta de confirmación para eliminar este módulo
    const [modalEliminarModuloOpen, setModalEliminarModuloOpen] = useState(false);

    //State para mostrar/ocultar el formulario para crear un submódulo de este módulo
    const [modalNuevoSubmoduloOpen, setModalNuevoSubmoduloOpen] = useState(false);

    //Funcion para mostrar el formulario para crear un submódulo de este módulo
    const handleClickNuevoSubmodulo = (e) => {
        e.stopPropagation();
        setModalNuevoSubmoduloOpen(true);
    };

    //Funcion para mostrar el formulario para editar este módulo
    const handleClickEditar = (e) => {
        e.stopPropagation();
        setModalEditarModuloOpen(true);
    };

    //Funcion para mostrar la alerta de confirmación para eliminar este módulo
    const handleClickEliminar = (e) => {
        e.stopPropagation();
        setModalEliminarModuloOpen(true);
    };

    //Consumir servicio para eliminar este módulo de la base
    const funcEliminarModulo = async () => {
        funcLoader(true, "Removiendo módulo...");

        const entSaveModulo = {
            iIdModulo: entModulo.iIdModulo,
            iIdUsuarioMod: usuarioSesion.iIdUsuario,
            sNombre: null,
            bActivo: false,
            bBaja: true,
        };

        const response = await cguController.funcSaveModulo(entSaveModulo);

        if (response.Code === 0) {
            setModalEliminarModuloOpen(false);
            await funcGetPermisosXPerfil();
            await funcUpdSystemInfo();
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
                    {permisos.Botones["3"] !== undefined && ( //Editar el módulo
                        <Tooltip title={`${permisos.Botones["3"].Nombre} ${entModulo.sNombre}`} placement="top" arrow>
                            <IconButton onClick={handleClickEditar}>
                                <EditIcon className={colorClass} />
                            </IconButton>
                        </Tooltip>
                    )}
                    {permisos.Botones["4"] !== undefined && ( //Eliminar el módulo
                        <Tooltip title={`${permisos.Botones["4"].Nombre} ${entModulo.sNombre}`} placement="top" arrow>
                            <IconButton onClick={handleClickEliminar}>
                                <DeleteIcon className={colorClass} />
                            </IconButton>
                        </Tooltip>
                    )}
                    {permisos.Botones["5"] !== undefined && ( //Agregar un submódulo nuevo a
                        <Tooltip title={`${permisos.Botones["5"].Nombre} ${entModulo.sNombre}`} placement="top" arrow>
                            <IconButton onClick={handleClickNuevoSubmodulo}>
                                <AddIcon className={colorClass} />
                            </IconButton>
                        </Tooltip>
                    )}
                </AccordionSummary>
                <AccordionDetails>
                    <div className="acc-content">
                        {entModulo.lstSubModulo.length > 0 ? (
                            entModulo.lstSubModulo.map((submodulo) => (
                                <SistemaSubmodulo
                                    key={submodulo.iIdSubModulo}
                                    entSubmodulo={submodulo}
                                    usuarioSesion={usuarioSesion}
                                    funcGetPermisosXPerfil={funcGetPermisosXPerfil}
                                    funcUpdSystemInfo={funcUpdSystemInfo}
                                    permisos={permisos}
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
                entModulo={entModulo}
                open={modalEditarModuloOpen}
                setOpen={setModalEditarModuloOpen}
                usuarioSesion={usuarioSesion}
                funcGetPermisosXPerfil={funcGetPermisosXPerfil}
                funcUpdSystemInfo={funcUpdSystemInfo}
                funcLoader={funcLoader}
                funcAlert={funcAlert}
            />
            <MeditocConfirmacion
                title="Eliminar módulo"
                okFunc={funcEliminarModulo}
                open={modalEliminarModuloOpen}
                setOpen={setModalEliminarModuloOpen}
            >
                ¿Desea eliminar el módulo {entModulo.sNombre} junto con todos sus submódulos y botones?
            </MeditocConfirmacion>
            <FormSubmodulo
                entSubmodulo={submoduloEntidadVacia}
                open={modalNuevoSubmoduloOpen}
                setOpen={setModalNuevoSubmoduloOpen}
                usuarioSesion={usuarioSesion}
                funcGetPermisosXPerfil={funcGetPermisosXPerfil}
                funcUpdSystemInfo={funcUpdSystemInfo}
                funcLoader={funcLoader}
                funcAlert={funcAlert}
            />
        </Fragment>
    );
};

SistemaModulo.propTypes = {
    entModulo: PropTypes.shape({
        iIdModulo: PropTypes.number,
        lstSubModulo: PropTypes.array,
        sNombre: PropTypes.any,
    }),
    funcAlert: PropTypes.func,
    funcGetPermisosXPerfil: PropTypes.func,
    funcLoader: PropTypes.func,
    usuarioSesion: PropTypes.shape({
        iIdUsuario: PropTypes.number,
    }),
};

export default SistemaModulo;
