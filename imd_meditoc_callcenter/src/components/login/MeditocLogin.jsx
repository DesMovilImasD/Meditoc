import { Button, Collapse, Fade, Grid, IconButton, Tooltip, Typography } from "@material-ui/core";
import { imgLogoLogin, imgLogoMeditocCasa } from "../../configurations/imgConfig";
import { urlDefault, urlSystem } from "../../configurations/urlConfig";

import CGUController from "../../controllers/CGUController";
import { EnumPerfilesPrincipales } from "../../configurations/enumConfig";
import MeditocInputWhite from "../utilidades/MeditocInputWhite";
import React from "react";
import VisibilityIcon from "@material-ui/icons/Visibility";
import VisibilityOffIcon from "@material-ui/icons/VisibilityOff";
import { makeStyles } from "@material-ui/core/styles";
import { rxCorreo } from "../../configurations/regexConfig";
import theme from "../../configurations/themeConfig";
import { useEffect } from "react";
import { useHistory } from "react-router-dom";
import { useState } from "react";

const useStyles = makeStyles(() => ({
    button: {
        color: theme.palette.primary.main,
        backgroundColor: "#fff",
    },
}));

const Login = (props) => {
    const { setUsuarioSesion, setUsuarioActivo, setUsuarioPermisos, funcLoader, funcAlert } = props;

    const classes = useStyles();
    const history = useHistory();

    const cguController = new CGUController();

    const [imgLogoFade, setImgLogoFade] = useState(false);
    const [verPassword, setVerPassword] = useState(false);

    const [formLogin, setFormLogin] = useState({
        txtUsuarioMeditoc: "",
        txtPasswordMeditoc: "",
        txtRecuperacion: "",
    });

    const handleChangeFormLogin = (e) => {
        setFormLogin({
            ...formLogin,
            [e.target.name]: e.target.value,
        });
    };

    const funcApiLogin = async (MeditocTkn, MeditocKey) => {
        funcLoader(true, "Validando datos de sesión...");

        const entUserSubmit = {
            sUsuario: MeditocTkn,
            sPassword: MeditocKey,
        };
        const responseLogin = await cguController.funcGetLogin(entUserSubmit);

        funcLoader();
        if (responseLogin.Code === 0) {
            sessionStorage.setItem("MeditocTkn", MeditocTkn);
            sessionStorage.setItem("MeditocKey", MeditocKey);

            funcLoader(true, "Obteniendo permisos..");
            const responsePermiso = await cguController.funcGetPermisosUsuario(responseLogin.Result.iIdPerfil);
            funcLoader();

            if (responsePermiso.Code === 0) {
                setUsuarioPermisos(responsePermiso.Result);

                switch (responseLogin.Result.iIdPerfil) {
                    case EnumPerfilesPrincipales.Superadministrador:
                        history.push(urlDefault);
                        break;
                    case EnumPerfilesPrincipales.Administrador:
                        history.push(urlDefault);
                        break;

                    case EnumPerfilesPrincipales.DoctorCallCenter:
                        history.push(urlSystem.callcenter.consultas);
                        break;

                    case EnumPerfilesPrincipales.DoctorEspecialista:
                        history.push(urlSystem.callcenter.consultas);
                        break;
                    case EnumPerfilesPrincipales.AdministradorEspecialiesta:
                        history.push(urlSystem.callcenter.administrarConsultas);
                        break;

                    default:
                        history.push(urlDefault);
                        break;
                }
            }
            setUsuarioSesion(responseLogin.Result);
            setUsuarioActivo(true);
        } else {
            sessionStorage.removeItem("MeditocTkn");
            sessionStorage.removeItem("MeditocKey");
            funcAlert(responseLogin.Message);
        }
    };

    const handleSubmitFormLogin = (e) => {
        e.preventDefault();

        if (formLogin.txtUsuarioMeditoc === "" || formLogin.txtPasswordMeditoc === "") {
            funcAlert("Por favor, ingrese todos los datos", "warning");
            return;
        }

        funcApiLogin(formLogin.txtUsuarioMeditoc, formLogin.txtPasswordMeditoc);
    };

    const funcValidarSesion = () => {
        const MeditocTkn = sessionStorage.getItem("MeditocTkn");
        const MeditocKey = sessionStorage.getItem("MeditocKey");

        if (MeditocTkn != null && MeditocKey != null) {
            funcApiLogin(MeditocTkn, MeditocKey);
        }
    };

    useEffect(() => {
        setImgLogoFade(true);
        funcValidarSesion();
        // eslint-disable-next-line
    }, []);

    const [loginMode, setLoginMode] = useState(true);

    const handleClickLoginMode = () => {
        setLoginMode(!loginMode);
    };

    const handleClickRecuperarCredenciales = async (e) => {
        e.preventDefault();

        if (formLogin.txtRecuperacion === "") {
            funcAlert("Por favor, ingrese su correo de recuperación", "warning");
            return;
        }

        if (!rxCorreo.test(formLogin.txtRecuperacion)) {
            funcAlert("Por favor, ingrese un correo válido", "warning");
            return;
        }

        const entUsuarioSubmit = {
            sCorreo: formLogin.txtRecuperacion,
        };

        funcLoader(true, "Enviando correo...");

        const response = await cguController.funcRecuperarPassword(entUsuarioSubmit);

        if (response.Code === 0) {
            setFormLogin({
                txtUsuarioMeditoc: "",
                txtPasswordMeditoc: "",
                txtRecuperacion: "",
            });
            setLoginMode(true);
            funcAlert(response.Message, "success");
        } else {
            funcAlert(response.Message);
        }
        funcLoader();
    };

    return (
        <div className="login-container">
            <Grid container spacing={0} className="login-page">
                <Grid item md={8} xs={12} className="align-self-center">
                    <div className="login-logo-container">
                        <Fade in={imgLogoFade} timeout={1500}>
                            <img src={imgLogoLogin} alt="LOGOLOGIN" className="login-logo-img" />
                        </Fade>
                    </div>
                </Grid>
                <Grid item md={4} xs={12} className="login-form-back">
                    <div>
                        <Collapse in={loginMode}>
                            <form className="login-form-container" onSubmit={handleSubmitFormLogin}>
                                <Grid container spacing={4}>
                                    <Grid item xs={12}>
                                        <img
                                            src={imgLogoMeditocCasa}
                                            alt="LOGOMEDITOCCASA"
                                            className="login-form-img"
                                        />
                                    </Grid>
                                    <Grid item xs={12}>
                                        <span className="ops-nor bold size-25">ACCESO AL SISTEMA</span>
                                    </Grid>
                                    <Grid item xs={12}>
                                        <MeditocInputWhite
                                            name="txtUsuarioMeditoc"
                                            variant="outlined"
                                            fullWidth
                                            autoComplete="off"
                                            label="Usuario:"
                                            value={formLogin.txtUsuarioMeditoc}
                                            onChange={handleChangeFormLogin}
                                        />
                                    </Grid>
                                    <Grid item xs={12}>
                                        <MeditocInputWhite
                                            name="txtPasswordMeditoc"
                                            variant="outlined"
                                            fullWidth
                                            autoComplete="new-password"
                                            label="Contraseña:"
                                            type={verPassword ? "text" : "password"}
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
                                                            {verPassword ? (
                                                                <VisibilityIcon className="color-0" />
                                                            ) : (
                                                                <VisibilityOffIcon className="color-0" />
                                                            )}
                                                        </IconButton>
                                                    </Tooltip>
                                                ),
                                            }}
                                            value={formLogin.txtPasswordMeditoc}
                                            onChange={handleChangeFormLogin}
                                        />
                                    </Grid>
                                    <Grid item xs={12}>
                                        <Button variant="contained" type="submit" className={classes.button} fullWidth>
                                            ENTRAR
                                        </Button>
                                    </Grid>
                                    <Grid item xs={12}>
                                        <Button
                                            variant="text"
                                            color="inherit"
                                            type="button"
                                            fullWidth
                                            onClick={handleClickLoginMode}
                                        >
                                            ¿Olvidaste tus credenciales?
                                        </Button>
                                    </Grid>
                                    <Grid item xs={12}>
                                        {" "}
                                    </Grid>
                                </Grid>
                            </form>
                        </Collapse>
                        <Collapse in={!loginMode}>
                            <form
                                className="login-form-container"
                                onSubmit={handleClickRecuperarCredenciales}
                                noValidate
                            >
                                <Grid container spacing={4}>
                                    <Grid item xs={12}>
                                        <span className="ops-nor bold size-25">RECUPERAR ACCESO</span>
                                    </Grid>
                                    <Grid item xs={12}>
                                        <Typography>
                                            Para recuperar sus credenciales de acceso ingrese el correo electrónico
                                            registrado en el sistema.
                                        </Typography>
                                    </Grid>
                                    <Grid item xs={12}>
                                        <MeditocInputWhite
                                            name="txtRecuperacion"
                                            variant="outlined"
                                            fullWidth
                                            autoComplete="off"
                                            label="Correo de recuperación:"
                                            value={formLogin.txtRecuperacion}
                                            onChange={handleChangeFormLogin}
                                        />
                                    </Grid>
                                    <Grid item xs={12}>
                                        <Button variant="contained" type="submit" className={classes.button} fullWidth>
                                            Enviar credenciales
                                        </Button>
                                    </Grid>
                                    <Grid item xs={12}>
                                        <Button
                                            variant="text"
                                            color="inherit"
                                            type="button"
                                            fullWidth
                                            onClick={handleClickLoginMode}
                                        >
                                            Regresar al Login
                                        </Button>
                                    </Grid>
                                </Grid>
                            </form>
                        </Collapse>
                    </div>
                </Grid>
            </Grid>
        </div>
    );
};

export default Login;
