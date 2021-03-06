import { IconButton, TableCell, TableRow, Tooltip } from "@material-ui/core";
import React, { Fragment, useState } from "react";

import CGUController from "../../../../controllers/CGUController";
import DeleteIcon from "@material-ui/icons/Delete";
import EditIcon from "@material-ui/icons/Edit";
import ExtensionIcon from "@material-ui/icons/Extension";
import FormBoton from "./FormBoton";
import MeditocConfirmacion from "../../../utilidades/MeditocConfirmacion";
import PropTypes from "prop-types";
import { makeStyles } from "@material-ui/core/styles";

const useStyles = makeStyles({
    cell: {
        padding: "0px 24px 0px 16px",
    },
});

/*************************************************************
 * Descripcion: Representa una fila de Boton con sus botones de "Editar botón" y "Eliminar botón"
 * Creado: Cristopher Noh
 * Fecha: 26/08/2020
 * Invocado desde: SistemaSubmodulo
 *************************************************************/
const SistemaBoton = (props) => {
    const {
        entBoton,
        usuarioSesion,
        funcGetPermisosXPerfil,
        funcUpdSystemInfo,
        permisos,
        funcLoader,
        funcAlert,
    } = props;

    const classes = useStyles();

    //Color del ícono para botón
    const colorClass = "color-3";

    //Servicios API
    const cguController = new CGUController();

    //State para mostrar/ocultar el formulario para editar este botón
    const [modalEditarBotonOpen, setModalEditarBotonOpen] = useState(false);

    //State para mostrar/ocultar la alerta de confirmación para eliminar este botón
    const [modalEliminarBotonOpen, setModalEliminarBotonOpen] = useState(false);

    //Funcion para mostrar el formulario para editar este botón
    const handleClickEditarBoton = (e) => {
        e.stopPropagation();
        setModalEditarBotonOpen(true);
    };

    //Funcion para mostrar la alerta de confirmación para eliminar este botón
    const handleClickEliminarBoton = (e) => {
        e.stopPropagation();
        setModalEliminarBotonOpen(true);
    };

    //Consumir servicio para eliminar este botón en la base
    const funcEliminarBoton = async () => {
        funcLoader(true, "Guardando botón...");

        const entSaveBoton = {
            iIdModulo: entBoton.iIdModulo,
            iIdSubModulo: entBoton.iIdSubModulo,
            iIdBoton: entBoton.iIdBoton,
            iIdUsuarioMod: usuarioSesion.iIdUsuario,
            sNombre: null,
            bActivo: false,
            bBaja: true,
        };

        const response = await cguController.funcSaveBoton(entSaveBoton);

        if (response.Code === 0) {
            setModalEliminarBotonOpen(false);
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
            <TableRow>
                <TableCell classes={{ sizeSmall: classes.cell }} align="left">
                    <ExtensionIcon className={colorClass + " vertical-align-middle"} />
                    <span className={colorClass + " size-15"}>
                        {entBoton.sNombre} ({entBoton.iIdBoton})
                    </span>
                </TableCell>
                <TableCell classes={{ sizeSmall: classes.cell }} align="right">
                    {permisos.Botones["9"] !== undefined && ( //Editar el botón
                        <Tooltip title={`${permisos.Botones["9"].Nombre} ${entBoton.sNombre}`} placement="top" arrow>
                            <IconButton onClick={handleClickEditarBoton}>
                                <EditIcon />
                            </IconButton>
                        </Tooltip>
                    )}
                    {permisos.Botones["10"] !== undefined && ( //Eliminar el botón
                        <Tooltip title={`${permisos.Botones["10"].Nombre} ${entBoton.sNombre}`} placement="top" arrow>
                            <IconButton onClick={handleClickEliminarBoton}>
                                <DeleteIcon />
                            </IconButton>
                        </Tooltip>
                    )}
                </TableCell>
            </TableRow>
            <FormBoton
                entBoton={entBoton}
                open={modalEditarBotonOpen}
                setOpen={setModalEditarBotonOpen}
                usuarioSesion={usuarioSesion}
                funcGetPermisosXPerfil={funcGetPermisosXPerfil}
                funcUpdSystemInfo={funcUpdSystemInfo}
                funcLoader={funcLoader}
                funcAlert={funcAlert}
            />
            <MeditocConfirmacion
                title="Eliminar botón"
                okFunc={funcEliminarBoton}
                open={modalEliminarBotonOpen}
                setOpen={setModalEliminarBotonOpen}
            >
                ¿Desea eliminar el botón {entBoton.sNombre}?
            </MeditocConfirmacion>
        </Fragment>
    );
};

SistemaBoton.propTypes = {
    entBoton: PropTypes.shape({
        iIdBoton: PropTypes.number,
        iIdModulo: PropTypes.number,
        iIdSubModulo: PropTypes.number,
        sNombre: PropTypes.string,
    }),
    funcAlert: PropTypes.func,
    funcGetPermisosXPerfil: PropTypes.func,
    funcLoader: PropTypes.func,
    usuarioSesion: PropTypes.shape({
        iIdUsuario: PropTypes.number,
    }),
};

export default SistemaBoton;
