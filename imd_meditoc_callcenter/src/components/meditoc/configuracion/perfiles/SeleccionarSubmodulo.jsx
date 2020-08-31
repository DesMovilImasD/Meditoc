import PropTypes from "prop-types";
import React, { useState, useEffect } from "react";
import ModalForm from "../../../utilidades/ModalForm";
import { Grid, List, ListItem, ListItemIcon, Checkbox, Button, ListItemText } from "@material-ui/core";
import CGUController from "../../../../controllers/CGUController";
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

    //Lista de submodulos para seleccionar
    const [submodulosParaSeleccionar, setSubmodulosParaSeleccionar] = useState([]);

    //Lista de submodulos seleccionados
    const [submodulosSeleccionados, setSubmodulosSeleccionados] = useState([]);

    //Filtrar los submodulos para mostrar solo los disponibles para seleccionar
    useEffect(() => {
        const listaIdSubmoduloPerfil = lstSubmodulosPerfil.map((x) => x.iIdSubModulo);
        setSubmodulosParaSeleccionar(
            lstSubmodulosSistema.filter((x) => !listaIdSubmoduloPerfil.includes(x.iIdSubModulo))
        );

        // eslint-disable-next-line
    }, [lstSubmodulosPerfil]);

    //Funcion para cerrar este modal
    const handleClose = () => {
        setOpen(false);
    };

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
        if (submodulosSeleccionados.length < 1) {
            funcAlert("Debe seleccionar al menos un submódulo para asignar el permiso");
            return;
        }

        const listaPermisosParaGuardar = submodulosSeleccionados.map((x) => ({
            iIdPerfil: entPerfil.iIdPerfil,
            iIdModulo: entModulo.iIdModulo,
            iIdSubModulo: x.iIdSubModulo,
            iIdBoton: 0,
            iIdUsuarioMod: usuarioSesion.iIdUsuario,
            bActivo: true,
            bBaja: false,
        }));

        funcLoader(true, "Guardando permisos para submódulos...");

        const cguController = new CGUController();
        const response = await cguController.funcSavePermiso(listaPermisosParaGuardar);

        if (response.Code !== 0) {
            funcAlert(response.Message);
        } else {
            setOpen(false);
            funcAlert(response.Message, "success");
            setSubmodulosSeleccionados([]);
            funcGetPermisosXPerfil();
        }

        funcLoader();
    };

    return (
        <ModalForm title="Seleccionar submódulos para agregar" size="small" open={open} setOpen={setOpen}>
            <Grid container spacing={3}>
                <Grid item xs={12}>
                    <List>
                        {submodulosParaSeleccionar.length > 0 ? (
                            submodulosParaSeleccionar.map((submodulo) => (
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
                            ))
                        ) : (
                            <div className="center">(No hay más submódulos por agregar)</div>
                        )}
                    </List>
                </Grid>
                <Grid item sm={6} xs={12}>
                    <Button variant="contained" color="primary" fullWidth onClick={funcSavePermisosSubmodulo}>
                        Agregar submódulos
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
