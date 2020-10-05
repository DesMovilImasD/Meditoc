import { Grid, IconButton, TextField, Tooltip } from "@material-ui/core";
import { blurPrevent, funcPrevent } from "../../../../configurations/preventConfig";

import CGUController from "../../../../controllers/CGUController";
import MeditocModal from "../../../utilidades/MeditocModal";
import MeditocModalBotones from "../../../utilidades/MeditocModalBotones";
import PropTypes from "prop-types";
import React from "react";
import VisibilityIcon from "@material-ui/icons/Visibility";
import VisibilityOffIcon from "@material-ui/icons/VisibilityOff";
import { useState } from "react";

const FormCambiarPassword = (props) => {
    const { open, setOpen, usuarioSesion, setAnchorEl, funcLoader, funcAlert } = props;

    const [formCambiarPassword, setFormCambiarPassword] = useState({
        txtNuevoPasswordMeditoc: "",
        txtConfirmarPasswordMeditoc: "",
    });

    const validacionFormulario = {
        txtNuevoPasswordMeditoc: true,
        txtConfirmarPasswordMeditoc: true,
    };

    const [formCambiarPasswordOK, setFormCambiarPasswordOK] = useState(validacionFormulario);
    const [passwordNoCoinciden, setPasswordNoCoinciden] = useState(false);

    const handleChangeFormCambiarPassword = (e) => {
        const nombreCampo = e.target.name;
        const valorCampo = e.target.value;

        switch (nombreCampo) {
            case "txtNuevoPasswordMeditoc":
                if (passwordNoCoinciden) {
                    if (valorCampo !== "" && valorCampo === formCambiarPassword.txtConfirmarPasswordMeditoc) {
                        setPasswordNoCoinciden(false);
                    }
                } else {
                    if (!formCambiarPasswordOK.txtNuevoPasswordMeditoc) {
                        if (valorCampo !== "") {
                            setFormCambiarPasswordOK({
                                ...formCambiarPassword,
                                [nombreCampo]: true,
                            });
                        }
                    }
                }
                break;

            case "txtConfirmarPasswordMeditoc":
                if (passwordNoCoinciden) {
                    if (valorCampo !== "" && valorCampo === formCambiarPassword.txtNuevoPasswordMeditoc) {
                        setPasswordNoCoinciden(false);
                    }
                } else {
                    if (!formCambiarPasswordOK.txtConfirmarPasswordMeditoc) {
                        if (valorCampo !== "") {
                            setFormCambiarPasswordOK({
                                ...formCambiarPassword,
                                [nombreCampo]: true,
                            });
                        }
                    }
                }
                break;

            default:
                break;
        }
        setFormCambiarPassword({
            ...formCambiarPassword,
            [nombreCampo]: valorCampo,
        });
    };

    const funcCambiarPassword = async (e) => {
        funcPrevent(e);
        let formCambiarPasswordOKValidacion = { ...formCambiarPasswordOK };
        let formError = false;

        if (formCambiarPassword.txtNuevoPasswordMeditoc === "") {
            formCambiarPasswordOKValidacion.txtNuevoPasswordMeditoc = false;
            formError = true;
        }
        if (formCambiarPassword.txtConfirmarPasswordMeditoc === "") {
            formCambiarPasswordOKValidacion.txtConfirmarPasswordMeditoc = false;
            formError = true;
        }

        setFormCambiarPasswordOK(formCambiarPasswordOKValidacion);
        if (formError) {
            return;
        }

        if (formCambiarPassword.txtNuevoPasswordMeditoc !== formCambiarPassword.txtConfirmarPasswordMeditoc) {
            //funcAlert("Las contraseñas no coinciden");
            setPasswordNoCoinciden(true);
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
            setAnchorEl(null);
            funcAlert(response.Message, "success");
        } else {
            funcAlert(response.Message);
        }

        funcLoader();
        blurPrevent();
    };

    const [verPassword, setVerPassword] = useState(false);

    return (
        <MeditocModal title="Cambiar contraseña" size="small" open={open} setOpen={setOpen}>
            <form id="form-cambiar-password" onSubmit={funcCambiarPassword} noValidate>
                <Grid container spacing={3}>
                    <Grid item xs={12}>
                        <TextField
                            id="txtNuevoPasswordMeditoc"
                            name="txtNuevoPasswordMeditoc"
                            variant="outlined"
                            label="Nueva contraseña:"
                            type="password"
                            autoComplete="new-password"
                            autoFocus
                            fullWidth
                            value={formCambiarPassword.txtNuevoPasswordMeditoc}
                            onChange={handleChangeFormCambiarPassword}
                            error={!formCambiarPasswordOK.txtNuevoPasswordMeditoc || passwordNoCoinciden}
                            helperText={
                                !formCambiarPasswordOK.txtNuevoPasswordMeditoc ? "Ingrese la nueva contraseña" : ""
                            }
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
                            error={!formCambiarPasswordOK.txtConfirmarPasswordMeditoc || passwordNoCoinciden}
                            helperText={
                                !formCambiarPasswordOK.txtConfirmarPasswordMeditoc
                                    ? "Confirme la nueva contraseña"
                                    : passwordNoCoinciden
                                    ? "Las contraseñas no coinciden"
                                    : ""
                            }
                        />
                    </Grid>
                    <MeditocModalBotones
                        okMessage="Cambiar contraseña"
                        setOpen={setOpen}
                        okFunc={funcCambiarPassword}
                    />
                </Grid>
            </form>
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
