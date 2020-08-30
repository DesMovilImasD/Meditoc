import PropTypes from "prop-types";
import React, { useState, useEffect } from "react";
import ModalForm from "../../../utilidades/ModalForm";
import { Grid, List, ListItem, ListItemIcon, Checkbox, ListItemText, Button } from "@material-ui/core";
import CGUController from "../../../../controllers/CGUController";
import AccountTreeIcon from "@material-ui/icons/AccountTree";

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

    //Lista de modulos para seleccionar
    const [modulosParaSeleccionar, setModulosParaSeleccionar] = useState([]);

    //Lista de modulos seleccionados
    const [modulosSeleccionados, setModulosSeleccionados] = useState([]);

    //Filtrar los modulos para mostrar solo los disponibles para seleccionar
    useEffect(() => {
        const listaIdModulo = listaPermisosPerfil.map((x) => x.iIdModulo);
        setModulosParaSeleccionar(listaSistema.filter((x) => !listaIdModulo.includes(x.iIdModulo)));

        // eslint-disable-next-line
    }, [listaPermisosPerfil]);

    //Funcion para cerrar este modal
    const handleClose = () => {
        setOpen(false);
    };

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
            funcAlert("Debe seleccionar al menos un módulo para asignar el permiso", "warning");
            return;
        }

        const listaPermisosParaGuardar = modulosSeleccionados.map((x) => ({
            iIdPerfil: entPerfil.iIdPerfil,
            iIdModulo: x.iIdModulo,
            iIdSubModulo: 0,
            iIdBoton: 0,
            iIdUsuarioMod: usuarioSesion.iIdUsuario,
            bActivo: true,
            bBaja: false,
        }));

        funcLoader(true, "Guardando permisos para módulos...");

        const cguController = new CGUController();
        const response = await cguController.funcSavePermiso(listaPermisosParaGuardar);

        if (response.Code !== 0) {
            funcAlert(response.Message);
        } else {
            setOpen(false);
            funcAlert(response.Message, "success");
            setModulosSeleccionados([]);
            funcGetPermisosXPerfil();
        }

        funcLoader();
    };

    return (
        <ModalForm title="Seleccionar módulos para agregar" size="small" open={open} setOpen={setOpen}>
            <Grid container spacing={3}>
                <Grid item xs={12}>
                    <List>
                        {modulosParaSeleccionar.length > 0 ? (
                            modulosParaSeleccionar.map((modulo) => (
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
                            ))
                        ) : (
                            <div className="center">(No hay más módulos por agregar)</div>
                        )}
                    </List>
                </Grid>
                <Grid item sm={6} xs={12}>
                    <Button variant="contained" color="primary" fullWidth onClick={funcSavePermisosModulo}>
                        Agregar módulos
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
