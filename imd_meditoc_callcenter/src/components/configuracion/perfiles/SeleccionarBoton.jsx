import React, { useState, useEffect } from "react";
import CGUController from "../../../controllers/CGUController";
import ModalForm from "../../ModalForm";
import { Grid, List, ListItem, ListItemIcon, ListItemText, Button, Checkbox } from "@material-ui/core";
import ExtensionIcon from "@material-ui/icons/Extension";

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

    const cguController = new CGUController();

    const [botonesParaSeleccionar, setBotonesParaSeleccionar] = useState([]);
    const [botonesSeleccionados, setBotonesSeleccionados] = useState([]);

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

    const funcSavePermisosBotones = async () => {
        if (botonesSeleccionados.length < 1) {
            funcAlert("Debe seleccionar al menos un botón para asignar el permiso", "warning");
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

    const handleClose = () => {
        setOpen(false);
    };

    useEffect(() => {
        const listaIdBotonPerfil = lstBotonesPermiso.map((x) => x.iIdBoton);
        setBotonesParaSeleccionar(lstBotonesSistema.filter((x) => !listaIdBotonPerfil.includes(x.iIdBoton)));
    }, [lstBotonesPermiso]);

    return (
        <ModalForm title="Seleccionar botones para agregar" size="small" open={open} setOpen={setOpen}>
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
        </ModalForm>
    );
};

export default SeleccionarBoton;
