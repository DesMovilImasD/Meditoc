import { Checkbox, Grid, List, ListItem, ListItemIcon, ListItemText } from "@material-ui/core";
import React, { Fragment, useEffect, useState } from "react";

import AccountTreeIcon from "@material-ui/icons/AccountTree";
import CGUController from "../../../../controllers/CGUController";
import MeditocModal from "../../../utilidades/MeditocModal";
import MeditocModalBotones from "../../../utilidades/MeditocModalBotones";
import PropTypes from "prop-types";

/*************************************************************
 * Descripcion: Representa un modal con la lista de modulos disponibles para dar permisos al perfil
 * Creado: Cristopher Noh
 * Fecha: 28/08/2020
 * Invocado desde: Permisos
 *************************************************************/
const SeleccionarModulos = (props) => {
    const {
        entPerfil,
        listaPermisosPerfil,
        listaSistema,
        open,
        setOpen,
        usuarioSesion,
        funcGetPermisosXPerfil,
        funcLoader,
        funcAlert,
    } = props;

    //Lista de modulos seleccionados
    const [modulosSeleccionados, setModulosSeleccionados] = useState([]);

    //Filtrar los modulos para mostrar solo los disponibles para seleccionar
    useEffect(() => {
        const listaIdModulo = listaPermisosPerfil.map((x) => x.iIdModulo);
        setModulosSeleccionados(listaSistema.filter((x) => listaIdModulo.includes(x.iIdModulo)));

        // eslint-disable-next-line
    }, [listaPermisosPerfil]);

    //Función para capturar los modulos que el usuario seleccione
    const handleChangeModuloCheckbox = (moduloSeleccionado) => {
        const moduloIndex = modulosSeleccionados.indexOf(moduloSeleccionado);
        const nuevosModulosSeleccionados = [...modulosSeleccionados];

        if (moduloIndex === -1) {
            nuevosModulosSeleccionados.push(moduloSeleccionado);
        } else {
            nuevosModulosSeleccionados.splice(moduloIndex, 1);
        }

        setModulosSeleccionados(nuevosModulosSeleccionados);
    };

    //Consumir servicio para dar permisos a los módulos seleccionados
    const funcSavePermisosModulo = async () => {
        if (modulosSeleccionados.length < 1) {
            funcAlert("Debe seleccionar al menos un módulo para asignar el permiso");
            return;
        }

        let listaPermisosParaGuardar = [];
        modulosSeleccionados.forEach((x) => {
            listaPermisosParaGuardar.push({
                iIdPerfil: entPerfil.iIdPerfil,
                iIdModulo: x.iIdModulo,
                iIdSubModulo: 0,
                iIdBoton: 0,
                iIdUsuarioMod: usuarioSesion.iIdUsuario,
                bActivo: true,
                bBaja: false,
            });
        });

        let modulosNoSeleccionados = listaSistema.filter(
            (x) => !modulosSeleccionados.map((y) => y.iIdModulo).includes(x.iIdModulo)
        );
        modulosNoSeleccionados.forEach((x) => {
            listaPermisosParaGuardar.push({
                iIdPerfil: entPerfil.iIdPerfil,
                iIdModulo: x.iIdModulo,
                iIdSubModulo: 0,
                iIdBoton: 0,
                iIdUsuarioMod: usuarioSesion.iIdUsuario,
                bActivo: false,
                bBaja: true,
            });
        });

        funcLoader(true, "Guardando permisos para módulos...");

        const cguController = new CGUController();
        const response = await cguController.funcSavePermiso(listaPermisosParaGuardar);

        if (response.Code === 0) {
            setOpen(false);
            setModulosSeleccionados([]);

            await funcGetPermisosXPerfil();

            funcAlert(response.Message, "success");
        } else {
            funcAlert(response.Message);
        }

        funcLoader();
    };

    const [seleccionarTodo, setSeleccionarTodo] = useState(false);

    const handleClickSeleccionarTodo = () => {
        setSeleccionarTodo(!seleccionarTodo);
    };

    useEffect(() => {
        if (seleccionarTodo) {
            setModulosSeleccionados(listaSistema);
        } else {
            setModulosSeleccionados([]);
        }
        // eslint-disable-next-line
    }, [seleccionarTodo]);

    return (
        <MeditocModal title="Permisos a módulos" size="small" open={open} setOpen={setOpen}>
            <Grid container spacing={3}>
                <Grid item xs={12}>
                    <List>
                        {listaSistema.length > 0 ? (
                            <Fragment>
                                <ListItem key={0} button dense onClick={handleClickSeleccionarTodo}>
                                    <ListItemIcon>
                                        <Checkbox edge="start" disableRipple checked={seleccionarTodo} />
                                    </ListItemIcon>
                                    <ListItemText id={0} primary="Seleccionar todo" />
                                </ListItem>
                                {listaSistema.map((modulo) => (
                                    <ListItem
                                        key={modulo.iIdModulo}
                                        button
                                        dense
                                        onClick={() => handleChangeModuloCheckbox(modulo)}
                                    >
                                        <ListItemIcon>
                                            <Checkbox
                                                edge="start"
                                                disableRipple
                                                checked={modulosSeleccionados.indexOf(modulo) !== -1}
                                            />
                                            <AccountTreeIcon className="align-self-center color-1" />
                                        </ListItemIcon>
                                        <ListItemText id={modulo.iIdModulo} primary={modulo.sNombre} />
                                    </ListItem>
                                ))}
                            </Fragment>
                        ) : (
                            <div className="center">(No hay más módulos por agregar)</div>
                        )}
                    </List>
                </Grid>
                <MeditocModalBotones setOpen={setOpen} okMessage="Guardar permisos" okFunc={funcSavePermisosModulo} />
            </Grid>
        </MeditocModal>
    );
};

SeleccionarModulos.propTypes = {
    entPerfil: PropTypes.shape({
        iIdPerfil: PropTypes.number,
    }),
    funcAlert: PropTypes.func,
    funcGetPermisosXPerfil: PropTypes.func,
    funcLoader: PropTypes.func,
    listaPermisosPerfil: PropTypes.array,
    listaSistema: PropTypes.array,
    open: PropTypes.bool,
    setOpen: PropTypes.func,
    usuarioSesion: PropTypes.shape({
        iIdUsuario: PropTypes.number,
    }),
};

export default SeleccionarModulos;
