import { Checkbox, Grid, List, ListItem, ListItemIcon, ListItemText } from "@material-ui/core";
import React, { Fragment, useEffect, useState } from "react";

import CGUController from "../../../../controllers/CGUController";
import MeditocModal from "../../../utilidades/MeditocModal";
import MeditocModalBotones from "../../../utilidades/MeditocModalBotones";
import PropTypes from "prop-types";
import WebIcon from "@material-ui/icons/Web";

/*************************************************************
 * Descripcion: Representa un modal con la lista de submódulos disponibles para dar permisos al perfil en el módulo previamente seleccionado
 * Creado: Cristopher Noh
 * Fecha: 28/08/2020
 * Invocado desde: PermisoModulo
 *************************************************************/
const SeleccionarSubmodulo = (props) => {
    const {
        entPerfil,
        entModulo,
        lstSubmodulosPerfil,
        lstSubmodulosSistema,
        open,
        setOpen,
        usuarioSesion,
        funcGetPermisosXPerfil,
        funcLoader,
        funcAlert,
    } = props;

    //Lista de submodulos seleccionados
    const [submodulosSeleccionados, setSubmodulosSeleccionados] = useState([]);

    //Filtrar los submodulos para mostrar solo los disponibles para seleccionar
    useEffect(() => {
        if (open) {
            const listaIdSubmoduloPerfil = lstSubmodulosPerfil.map((x) => x.iIdSubModulo);
            setSubmodulosSeleccionados(
                lstSubmodulosSistema.filter((x) => listaIdSubmoduloPerfil.includes(x.iIdSubModulo))
            );
        }

        // eslint-disable-next-line
    }, [open]);

    //Funcion para capturar los submodulos que el usuario seleccione
    const handleChangeSubmoduloCheckbox = (submoduloSeleccionado) => {
        const moduloIndex = submodulosSeleccionados.indexOf(submoduloSeleccionado);
        const nuevosSubmodulosSeleccionados = [...submodulosSeleccionados];

        if (moduloIndex === -1) {
            nuevosSubmodulosSeleccionados.push(submoduloSeleccionado);
        } else {
            nuevosSubmodulosSeleccionados.splice(moduloIndex, 1);
        }

        setSubmodulosSeleccionados(nuevosSubmodulosSeleccionados);
    };

    //Consumir servicio para dar permisos a los submodulos seleccionados (al perfil)
    const funcSavePermisosSubmodulo = async () => {
        let listaPermisosParaGuardar = [];
        submodulosSeleccionados.forEach((x) => {
            listaPermisosParaGuardar.push({
                iIdPerfil: entPerfil.iIdPerfil,
                iIdModulo: entModulo.iIdModulo,
                iIdSubModulo: x.iIdSubModulo,
                iIdBoton: 0,
                iIdUsuarioMod: usuarioSesion.iIdUsuario,
                bActivo: true,
                bBaja: false,
            });
        });

        let submodulosNoSeleccionados = lstSubmodulosSistema.filter(
            (x) => !submodulosSeleccionados.map((y) => y.iIdSubModulo).includes(x.iIdSubModulo)
        );
        submodulosNoSeleccionados.forEach((x) => {
            listaPermisosParaGuardar.push({
                iIdPerfil: entPerfil.iIdPerfil,
                iIdModulo: entModulo.iIdModulo,
                iIdSubModulo: x.iIdSubModulo,
                iIdBoton: 0,
                iIdUsuarioMod: usuarioSesion.iIdUsuario,
                bActivo: false,
                bBaja: true,
            });
        });

        funcLoader(true, "Guardando permisos para submódulos...");

        const cguController = new CGUController();
        const response = await cguController.funcSavePermiso(listaPermisosParaGuardar);

        if (response.Code === 0) {
            setOpen(false);
            setSubmodulosSeleccionados([]);

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
            setSubmodulosSeleccionados(lstSubmodulosSistema);
        } else {
            setSubmodulosSeleccionados([]);
        }
        // eslint-disable-next-line
    }, [seleccionarTodo]);

    return (
        <MeditocModal title="Permisos de submódulos" size="small" open={open} setOpen={setOpen}>
            <Grid container spacing={3}>
                <Grid item xs={12}>
                    <List>
                        {lstSubmodulosSistema.length > 0 ? (
                            <Fragment>
                                <ListItem key={0} button dense onClick={handleClickSeleccionarTodo}>
                                    <ListItemIcon>
                                        <Checkbox edge="start" disableRipple checked={seleccionarTodo} />
                                    </ListItemIcon>
                                    <ListItemText id={0} primary="Seleccionar todo" />
                                </ListItem>
                                {lstSubmodulosSistema.map((submodulo) => (
                                    <ListItem
                                        key={submodulo.iIdSubModulo}
                                        button
                                        dense
                                        onClick={() => handleChangeSubmoduloCheckbox(submodulo)}
                                    >
                                        <ListItemIcon>
                                            <Checkbox
                                                edge="start"
                                                disableRipple
                                                checked={submodulosSeleccionados.indexOf(submodulo) !== -1}
                                            />
                                            <WebIcon className="align-self-center color-2" />
                                        </ListItemIcon>
                                        <ListItemText id={submodulo.iIdSubModulo} primary={submodulo.sNombre} />
                                    </ListItem>
                                ))}
                            </Fragment>
                        ) : (
                            <div className="center">(No hay más submódulos por agregar)</div>
                        )}
                    </List>
                </Grid>
                <MeditocModalBotones
                    okMessage="Guardar permisos"
                    okFunc={funcSavePermisosSubmodulo}
                    setOpen={setOpen}
                />
            </Grid>
        </MeditocModal>
    );
};

SeleccionarSubmodulo.propTypes = {
    entModulo: PropTypes.shape({
        iIdModulo: PropTypes.number,
    }),
    entPerfil: PropTypes.shape({
        iIdPerfil: PropTypes.number,
    }),
    funcAlert: PropTypes.func,
    funcGetPermisosXPerfil: PropTypes.func,
    funcLoader: PropTypes.func,
    lstSubmodulosPerfil: PropTypes.array,
    lstSubmodulosSistema: PropTypes.array,
    open: PropTypes.bool,
    setOpen: PropTypes.func,
    usuarioSesion: PropTypes.shape({
        iIdUsuario: PropTypes.number,
    }),
};

export default SeleccionarSubmodulo;
