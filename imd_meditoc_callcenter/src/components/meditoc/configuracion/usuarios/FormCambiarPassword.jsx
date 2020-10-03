import PropTypes from "prop-types";
import React from "react";
import MeditocModal from "../../../utilidades/MeditocModal";
import { Grid, TextField, Tooltip, IconButton } from "@material-ui/core";
import { useState } from "react";
import CGUController from "../../../../controllers/CGUController";
import VisibilityOffIcon from "@material-ui/icons/VisibilityOff";
import VisibilityIcon from "@material-ui/icons/Visibility";
import MeditocModalBotones from "../../../utilidades/MeditocModalBotones";

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

    const [verPassword, setVerPassword] = useState(false);

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
                        type={verPassword ? "text" : "password"}
                        autoComplete="new-password"
                        fullWidth
                        value={formCambiarPassword.txtConfirmarPasswordMeditoc}
                        onChange={handleChangeFormCambiarPassword}
                        InputProps={{
                            endAdornment: (
                                <Tooltip
                                    title={verPassword ? "Ocultar contraseña" : "Ver contraseña"}
                                    arrow
                                    placement="top"
                                >
                                    <IconButton
                                        onMouseDown={() => setVerPassword(true)}
                                        onMouseUp={() => setVerPassword(false)}
                                    >
                                        {verPassword ? <VisibilityIcon /> : <VisibilityOffIcon />}
                                    </IconButton>
                                </Tooltip>
                            ),
                        }}
                    />
                </Grid>
                <MeditocModalBotones okMessage="Cambiar contraseña" setOpen={setOpen} okFunc={funcCambiarPassword} />
            </Grid>
        </MeditocModal>
    );
};

FormCambiarPassword.propTypes = {
    funcAlert: PropTypes.func,
    funcLoader: PropTypes.func,
    open: PropTypes.any,
    setOpen: PropTypes.func,
    usuarioSesion: PropTypes.shape({
        iIdUsuario: PropTypes.any,
    }),
};

export default FormCambiarPassword;
