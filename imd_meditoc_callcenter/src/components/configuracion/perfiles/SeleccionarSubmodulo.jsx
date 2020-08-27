import React, { useState, useEffect } from "react";
import ModalForm from "../../ModalForm";
import { Grid, List, ListItem, ListItemIcon, Checkbox, Button, ListItemText } from "@material-ui/core";
import CGUController from "../../../controllers/CGUController";
import WebIcon from "@material-ui/icons/Web";

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

    const cguController = new CGUController();

    const [submodulosParaSeleccionar, setSubmodulosParaSeleccionar] = useState([]);
    const [submodulosSeleccionados, setSubmodulosSeleccionados] = useState([]);

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

    const funcSavePermisosSubmodulo = async () => {
        if (submodulosSeleccionados.length < 1) {
            funcAlert("Debe seleccionar al menos un submódulo para asignar el permiso", "warning");
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

    const handleClose = () => {
        setOpen(false);
    };

    useEffect(() => {
        const listaIdSubmoduloPerfil = lstSubmodulosPerfil.map((x) => x.iIdSubModulo);
        setSubmodulosParaSeleccionar(
            lstSubmodulosSistema.filter((x) => !listaIdSubmoduloPerfil.includes(x.iIdSubModulo))
        );
    }, [lstSubmodulosPerfil]);

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

export default SeleccionarSubmodulo;
