import PropTypes from "prop-types";
import React, { useState, useEffect } from "react";
import CGUController from "../../../../controllers/CGUController";
import MeditocModal from "../../../utilidades/MeditocModal";
import { Grid, List, ListItem, ListItemIcon, ListItemText, Button, Checkbox } from "@material-ui/core";
import ExtensionIcon from "@material-ui/icons/Extension";

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

    //Lista de botones para seleccionar
    const [botonesParaSeleccionar, setBotonesParaSeleccionar] = useState([]);

    //Lista de botones seleccionados
    const [botonesSeleccionados, setBotonesSeleccionados] = useState([]);

    //Filtrar los botones para mostrar solo los disponibles para seleccionar
    useEffect(() => {
        const listaIdBotonPerfil = lstBotonesPermiso.map((x) => x.iIdBoton);
        setBotonesParaSeleccionar(lstBotonesSistema.filter((x) => !listaIdBotonPerfil.includes(x.iIdBoton)));

        // eslint-disable-next-line
    }, [lstBotonesPermiso]);

    //Funcion para cerrar este modal
    const handleClose = () => {
        setOpen(false);
    };

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
        if (botonesSeleccionados.length < 1) {
            funcAlert("Debe seleccionar al menos un botón para asignar el permiso");
            return;
        }

        const listaPermisosParaGuardar = botonesSeleccionados.map((x) => ({
            iIdPerfil: entPerfil.iIdPerfil,
            iIdModulo: entSubmodulo.iIdModulo,
            iIdSubModulo: entSubmodulo.iIdSubModulo,
            iIdBoton: x.iIdBoton,
            iIdUsuarioMod: usuarioSesion.iIdUsuario,
            bActivo: true,
            bBaja: false,
        }));

        funcLoader(true, "Guardando permisos para botones...");

        const cguController = new CGUController();
        const response = await cguController.funcSavePermiso(listaPermisosParaGuardar);

        if (response.Code !== 0) {
            funcAlert(response.Message);
        } else {
            setOpen(false);
            funcAlert(response.Message, "success");
            setBotonesSeleccionados([]);
            funcGetPermisosXPerfil();
        }

        funcLoader();
    };

    return (
        <MeditocModal title="Seleccionar botones para agregar" size="small" open={open} setOpen={setOpen}>
            <Grid container spacing={3}>
                <Grid item xs={12}>
                    <List>
                        {botonesParaSeleccionar.length > 0 ? (
                            botonesParaSeleccionar.map((boton) => (
                                <ListItem
                                    key={boton.iIdSubModulo}
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
                            ))
                        ) : (
                            <div className="center">(No hay más botones por agregar)</div>
                        )}
                    </List>
                </Grid>
                <Grid item sm={6} xs={12}>
                    <Button variant="contained" color="primary" fullWidth onClick={funcSavePermisosBotones}>
                        Agregar botones
                    </Button>
                </Grid>
                <Grid item sm={6} xs={12}>
                    <Button variant="contained" color="secondary" fullWidth onClick={handleClose}>
                        Cancelar
                    </Button>
                </Grid>
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
