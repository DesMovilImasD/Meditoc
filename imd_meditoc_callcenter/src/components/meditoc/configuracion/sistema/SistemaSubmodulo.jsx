import {
    Accordion,
    AccordionDetails,
    AccordionSummary,
    IconButton,
    Paper,
    Table,
    TableBody,
    Tooltip,
} from "@material-ui/core";
import React, { Fragment, useState } from "react";

import AddIcon from "@material-ui/icons/Add";
import CGUController from "../../../../controllers/CGUController";
import DeleteIcon from "@material-ui/icons/Delete";
import EditIcon from "@material-ui/icons/Edit";
import ExpandMore from "@material-ui/icons/ExpandMore";
import FormBoton from "./FormBoton";
import FormSubmodulo from "./FormSubmodulo";
import MeditocConfirmacion from "../../../utilidades/MeditocConfirmacion";
import PropTypes from "prop-types";
import SistemaBoton from "./SistemaBoton";
import WebIcon from "@material-ui/icons/Web";
import { makeStyles } from "@material-ui/core/styles";
import theme from "../../../../configurations/themeConfig";

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
 * Descripcion: Representa una fila de Submódulo con sus botones de "Agregar botón", "Editar submódulo" y "Eliminar submódulo"
 * Creado: Cristopher Noh
 * Fecha: 26/08/2020
 * Invocado desde: SistemaModulo
 *************************************************************/
const SistemaSubmodulo = (props) => {
    const {
        entSubmodulo,
        usuarioSesion,
        funcGetPermisosXPerfil,
        funcUpdSystemInfo,
        permisos,
        funcLoader,
        funcAlert,
    } = props;

    const classes = useStyles();

    //Color para los íconos y letrar de este componente
    const colorClass = "color-2";

    //Servicio API
    const cguController = new CGUController();

    //Entidad vacía de un botón
    const botonEntidadVacia = {
        iIdModulo: entSubmodulo.iIdModulo,
        iIdSubModulo: entSubmodulo.iIdSubModulo,
        iIdBoton: 0,
        sNombre: "",
    };

    //State para mostrar/ocultar el formulario para editar este submódulo
    const [modalEditarSubmoduloOpen, setModalEditarSubmoduloOpen] = useState(false);

    //State para mostrar/ocultar la alerta de confirmación para eliminar este submódulo
    const [modalEliminarSubmoduloOpen, setModalEliminarSubmoduloOpen] = useState(false);

    //State para mostrar/ocultar el formulario para crear un botón de este submódulo
    const [modalNuevoBotonOpen, setModalNuevoBotonOpen] = useState(false);

    //Funcion para mostrar el formulario para editar este submódulo
    const handleClickEditarSubmodulo = (e) => {
        e.stopPropagation();
        setModalEditarSubmoduloOpen(true);
    };

    //Funcion para mostrar la alerta de confirmación para eliminar este submódulo
    const handleClickEliminarSubmodulo = (e) => {
        e.stopPropagation();
        setModalEliminarSubmoduloOpen(true);
    };

    //Función para mostrar el formulario para crear un botón de este submódulo
    const handleClickNuevoBoton = (e) => {
        e.stopPropagation();
        setModalNuevoBotonOpen(true);
    };

    //Consumir servicio para eliminar este submódulo de la base
    const funcEliminarSubmodulo = async () => {
        funcLoader(true, "Removiendo submódulo...");

        const entSaveSubmodulo = {
            iIdModulo: entSubmodulo.iIdModulo,
            iIdSubModulo: entSubmodulo.iIdSubModulo,
            iIdUsuarioMod: usuarioSesion.iIdUsuario,
            sNombre: null,
            bActivo: false,
            bBaja: true,
        };

        const response = await cguController.funcSaveSubmodulo(entSaveSubmodulo);

        if (response.Code === 0) {
            setModalEliminarSubmoduloOpen(false);
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
                    {permisos.Botones["6"] !== undefined && ( //Editar el submódulo
                        <Tooltip
                            title={`${permisos.Botones["6"].Nombre} ${entSubmodulo.sNombre}`}
                            placement="top"
                            arrow
                        >
                            <IconButton onClick={handleClickEditarSubmodulo}>
                                <EditIcon className={colorClass} />
                            </IconButton>
                        </Tooltip>
                    )}
                    {permisos.Botones["7"] !== undefined && ( //Eliminar el submódulo
                        <Tooltip
                            title={`${permisos.Botones["7"].Nombre} ${entSubmodulo.sNombre}`}
                            placement="top"
                            arrow
                        >
                            <IconButton onClick={handleClickEliminarSubmodulo}>
                                <DeleteIcon className={colorClass} />
                            </IconButton>
                        </Tooltip>
                    )}
                    {permisos.Botones["8"] !== undefined && ( //Agregar un botón nuevo a
                        <Tooltip
                            title={`${permisos.Botones["8"].Nombre} ${entSubmodulo.sNombre}`}
                            placement="top"
                            arrow
                        >
                            <IconButton onClick={handleClickNuevoBoton}>
                                <AddIcon className={colorClass} />
                            </IconButton>
                        </Tooltip>
                    )}
                </AccordionSummary>
                <AccordionDetails>
                    <div className="acc-content">
                        <Paper elevation={0}>
                            <Table size="small">
                                <TableBody>
                                    {entSubmodulo.lstBotones.length > 0 ? (
                                        entSubmodulo.lstBotones.map((boton) => (
                                            <SistemaBoton
                                                key={boton.iIdBoton}
                                                entBoton={boton}
                                                usuarioSesion={usuarioSesion}
                                                funcGetPermisosXPerfil={funcGetPermisosXPerfil}
                                                funcUpdSystemInfo={funcUpdSystemInfo}
                                                permisos={permisos}
                                                funcLoader={funcLoader}
                                                funcAlert={funcAlert}
                                            />
                                        ))
                                    ) : (
                                        <div className="center">(No hay botones configurados para este submódulo)</div>
                                    )}
                                </TableBody>
                            </Table>
                        </Paper>
                    </div>
                </AccordionDetails>
            </Accordion>
            <FormSubmodulo
                entSubmodulo={entSubmodulo}
                open={modalEditarSubmoduloOpen}
                setOpen={setModalEditarSubmoduloOpen}
                usuarioSesion={usuarioSesion}
                funcGetPermisosXPerfil={funcGetPermisosXPerfil}
                funcUpdSystemInfo={funcUpdSystemInfo}
                funcLoader={funcLoader}
                funcAlert={funcAlert}
            />
            <MeditocConfirmacion
                title="Eliminar submódulo"
                okFunc={funcEliminarSubmodulo}
                open={modalEliminarSubmoduloOpen}
                setOpen={setModalEliminarSubmoduloOpen}
            >
                ¿Desea eliminar el submódulo {entSubmodulo.sNombre} junto con todos sus botones?
            </MeditocConfirmacion>
            <FormBoton
                entBoton={botonEntidadVacia}
                open={modalNuevoBotonOpen}
                setOpen={setModalNuevoBotonOpen}
                usuarioSesion={usuarioSesion}
                funcGetPermisosXPerfil={funcGetPermisosXPerfil}
                funcUpdSystemInfo={funcUpdSystemInfo}
                funcLoader={funcLoader}
                funcAlert={funcAlert}
            />
        </Fragment>
    );
};

SistemaSubmodulo.propTypes = {
    entSubmodulo: PropTypes.shape({
        iIdModulo: PropTypes.number,
        iIdSubModulo: PropTypes.number,
        lstBotones: PropTypes.array,
        sNombre: PropTypes.string,
    }),
    funcAlert: PropTypes.func,
    funcGetPermisosXPerfil: PropTypes.func,
    funcLoader: PropTypes.func,
    usuarioSesion: PropTypes.shape({
        iIdUsuario: PropTypes.number,
    }),
};

export default SistemaSubmodulo;
