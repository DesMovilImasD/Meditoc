import React from "react";
import MeditocModal from "../../../utilidades/MeditocModal";
import { Grid, TextField, Button } from "@material-ui/core";
import { useState } from "react";
import CGUController from "../../../../controllers/CGUController";

const FormCambiarPassword = (props) => {
    const { open, setOpen, usuarioSesion, funcLoader, funcAlert } = props;

    const [formCambiarPassword, setFormCambiarPassword] = useState({
        txtNuevoPasswordMeditoc: "",
        txtConfirmarPasswordMeditoc: "",
    });

    const handleChangeFormCambiarPassword = (e) => {
        setFormCambiarPassword({
            ...formCambiarPassword,
            [e.target.name]: e.target.value,
        });
    };

    //Funcion para cerrar este modal
    const handleClose = () => {
        setOpen(false);
    };

    const funcCambiarPassword = async () => {
        if (formCambiarPassword.txtNuevoPasswordMeditoc !== formCambiarPassword.txtConfirmarPasswordMeditoc) {
            funcAlert("Las contraseñas no coinciden");
            return;
        }

        funcLoader(true, "Actualizando contraseña...");

        const cguController = new CGUController();
        const response = await cguController.funcCambiarPassword(
            usuarioSesion.iIdUsuario,
            formCambiarPassword.txtConfirmarPasswordMeditoc,
            usuarioSesion.iIdUsuario
        );

        if (response.Code === 0) {
            sessionStorage.setItem("MeditocKey", formCambiarPassword.txtConfirmarPasswordMeditoc);
            setOpen(false);

            funcAlert(response.Message, "success");
        } else {
            funcAlert(response.Message);
        }

        funcLoader();
    };

    return (
        <MeditocModal title="Cambiar contraseña" size="small" open={open} setOpen={setOpen}>
            <Grid container spacing={3}>
                <Grid item xs={12}>
                    <TextField
                        id="txtNuevoPasswordMeditoc"
                        name="txtNuevoPasswordMeditoc"
                        variant="outlined"
                        label="Nueva contraseña:"
                        type="password"
                        autoComplete="new-password"
                        fullWidth
                        value={formCambiarPassword.txtNuevoPasswordMeditoc}
                        onChange={handleChangeFormCambiarPassword}
                    />
                </Grid>
                <Grid item xs={12}>
                    <TextField
                        id="txtConfirmarPasswordMeditoc"
                        name="txtConfirmarPasswordMeditoc"
                        variant="outlined"
                        label="Confirmar contraseña:"
                        type="password"
                        autoComplete="new-password"
                        fullWidth
                        value={formCambiarPassword.txtConfirmarPasswordMeditoc}
                        onChange={handleChangeFormCambiarPassword}
                    />
                </Grid>
                <Grid item sm={6} xs={12}>
                    <Button variant="contained" color="primary" fullWidth onClick={funcCambiarPassword}>
                        Guardar
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

export default FormCambiarPassword;
