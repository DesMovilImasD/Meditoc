import { Checkbox, Grid, List, ListItem, ListItemIcon, ListItemText } from "@material-ui/core";
import React, { Fragment, useEffect, useState } from "react";

import CGUController from "../../../../controllers/CGUController";
import ExtensionIcon from "@material-ui/icons/Extension";
import MeditocModal from "../../../utilidades/MeditocModal";
import MeditocModalBotones from "../../../utilidades/MeditocModalBotones";
import PropTypes from "prop-types";

/*************************************************************
 * Descripcion: Representa un modal con la lista de botones disponibles para dar permisos al perfil en el submódulo previamente seleccionado
 * Creado: Cristopher Noh
 * Fecha: 28/08/2020
 * Invocado desde: PermisoSubmodulo
 *************************************************************/
const SeleccionarBoton = (props) => {
    const {
        entPerfil,
        entSubmodulo,
        lstBotonesPermiso,
        lstBotonesSistema,
        open,
        setOpen,
        usuarioSesion,
        funcGetPermisosXPerfil,
        funcLoader,
        funcAlert,
    } = props;

    //Lista de botones seleccionados
    const [botonesSeleccionados, setBotonesSeleccionados] = useState([]);

    //Filtrar los botones para mostrar solo los disponibles para seleccionar
    useEffect(() => {
        if (open) {
            const listaIdBotonPerfil = lstBotonesPermiso.map((x) => x.iIdBoton);
            setBotonesSeleccionados(lstBotonesSistema.filter((x) => listaIdBotonPerfil.includes(x.iIdBoton)));
        }
        // eslint-disable-next-line
    }, [open]);

    //Funcion para capturar los botones que selecciona el usuario
    const handleChangeBotonCheckbox = (botonSeleccionado) => {
        const botonIndex = botonesSeleccionados.indexOf(botonSeleccionado);
        const nuevosBotonesSeleccionados = [...botonesSeleccionados];

        if (botonIndex === -1) {
            nuevosBotonesSeleccionados.push(botonSeleccionado);
        } else {
            nuevosBotonesSeleccionados.splice(botonIndex, 1);
        }
        setBotonesSeleccionados(nuevosBotonesSeleccionados);
    };

    //Consumir servicio para dar permisos a los botones seleccionados
    const funcSavePermisosBotones = async () => {
        let listaPermisosParaGuardar = [];
        botonesSeleccionados.forEach((x) => {
            listaPermisosParaGuardar.push({
                iIdPerfil: entPerfil.iIdPerfil,
                iIdModulo: entSubmodulo.iIdModulo,
                iIdSubModulo: entSubmodulo.iIdSubModulo,
                iIdBoton: x.iIdBoton,
                iIdUsuarioMod: usuarioSesion.iIdUsuario,
                bActivo: true,
                bBaja: false,
            });
        });

        let botonesNoSeleccionados = lstBotonesSistema.filter(
            (x) => !botonesSeleccionados.map((y) => y.iIdBoton).includes(x.iIdBoton)
        );

        botonesNoSeleccionados.forEach((x) => {
            listaPermisosParaGuardar.push({
                iIdPerfil: entPerfil.iIdPerfil,
                iIdModulo: entSubmodulo.iIdModulo,
                iIdSubModulo: entSubmodulo.iIdSubModulo,
                iIdBoton: x.iIdBoton,
                iIdUsuarioMod: usuarioSesion.iIdUsuario,
                bActivo: false,
                bBaja: true,
            });
        });

        funcLoader(true, "Guardando permisos para botones...");

        const cguController = new CGUController();
        const response = await cguController.funcSavePermiso(listaPermisosParaGuardar);

        if (response.Code === 0) {
            setOpen(false);
            setBotonesSeleccionados([]);
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
            setBotonesSeleccionados(lstBotonesSistema);
        } else {
            setBotonesSeleccionados([]);
        }
        // eslint-disable-next-line
    }, [seleccionarTodo]);

    return (
        <MeditocModal title="Permisos de botones" size="small" open={open} setOpen={setOpen}>
            <Grid container spacing={3}>
                <Grid item xs={12}>
                    <List>
                        {lstBotonesSistema.length > 0 ? (
                            <Fragment>
                                <ListItem key={0} button dense onClick={handleClickSeleccionarTodo}>
                                    <ListItemIcon>
                                        <Checkbox edge="start" disableRipple checked={seleccionarTodo} />
                                    </ListItemIcon>
                                    <ListItemText id={0} primary="Seleccionar todo" />
                                </ListItem>
                                {lstBotonesSistema.map((boton) => (
                                    <ListItem
                                        key={boton.iIdBoton}
                                        button
                                        dense
                                        onClick={() => handleChangeBotonCheckbox(boton)}
                                    >
                                        <ListItemIcon>
                                            <Checkbox
                                                edge="start"
                                                disableRipple
                                                checked={botonesSeleccionados.indexOf(boton) !== -1}
                                            />
                                            <ExtensionIcon className="align-self-center color-3" />
                                        </ListItemIcon>
                                        <ListItemText id={boton.iIdBoton} primary={boton.sNombre} />
                                    </ListItem>
                                ))}
                            </Fragment>
                        ) : (
                            <div className="center">(No hay más botones por agregar)</div>
                        )}
                    </List>
                </Grid>
                <MeditocModalBotones okMessage="Guardar permisos" okFunc={funcSavePermisosBotones} setOpen={setOpen} />
            </Grid>
        </MeditocModal>
    );
};

SeleccionarBoton.propTypes = {
    entPerfil: PropTypes.shape({
        iIdPerfil: PropTypes.number,
    }),
    entSubmodulo: PropTypes.shape({
        iIdModulo: PropTypes.number,
        iIdSubModulo: PropTypes.number,
    }),
    funcAlert: PropTypes.func,
    funcGetPermisosXPerfil: PropTypes.func,
    funcLoader: PropTypes.func,
    lstBotonesPermiso: PropTypes.array,
    lstBotonesSistema: PropTypes.array,
    open: PropTypes.bool,
    setOpen: PropTypes.func,
    usuarioSesion: PropTypes.shape({
        iIdUsuario: PropTypes.number,
    }),
};

export default SeleccionarBoton;
