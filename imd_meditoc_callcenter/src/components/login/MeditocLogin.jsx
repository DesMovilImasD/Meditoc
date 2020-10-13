import { Button, Collapse, Fade, Grid, IconButton, Tooltip, Typography } from "@material-ui/core";
import { imgLogoLogin, imgLogoMeditocCasa } from "../../configurations/imgConfig";
import { urlDefault, urlSystem } from "../../configurations/urlConfig";

import CGUController from "../../controllers/CGUController";
import { EnumPerfilesPrincipales } from "../../configurations/enumConfig";
import MeditocInputWhite from "../utilidades/MeditocInputWhite";
import PoliticasController from "../../controllers/PoliticasController";
import PropTypes from "prop-types";
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

/*************************************************************
 * Descripcion: Contenido del Login principal de Meditoc
 * Creado: Cristopher Noh
 * Fecha: 26/08/2020
 * Invocado desde: App
 *************************************************************/
const Login = (props) => {
    const { setUsuarioSesion, setUsuarioActivo, setUsuarioPermisos, setEntCatalogos, funcLoader, funcAlert } = props;

    //-----------------------Hooks
    const classes = useStyles();
    const history = useHistory();

    //-----------------------Controllers
    const cguController = new CGUController();
    const politicasController = new PoliticasController();

    //-----------------------States
    const [mostrarPortadaLogin, setMostrarPortadaLogin] = useState(false);
    const [mostrarPassword, setMostrarPassword] = useState(false);
    const [mostrarFormLogin, setMostrarFormLogin] = useState(true);

    const [formLogin, setFormLogin] = useState({
        txtUsuarioMeditoc: "",
        txtPasswordMeditoc: "",
        txtCorreoRecuperacion: "",
    });

    //-----------------------Handlers
    //Capturar inputs del login
    const handleChangeFormLogin = (e) => {
        setFormLogin({
            ...formLogin,
            [e.target.name]: e.target.value,
        });
    };

    //Enviar formulario
    const handleSubmitFormLogin = (e) => {
        e.preventDefault();

        if (formLogin.txtUsuarioMeditoc === "" || formLogin.txtPasswordMeditoc === "") {
            funcAlert("Por favor, ingrese todos los datos", "warning");
            return;
        }

        funcApiLogin(formLogin.txtUsuarioMeditoc, formLogin.txtPasswordMeditoc);
    };

    //Alternar vista Iniciar sesión/recuperar credenciales
    const handleClickLoginMode = () => {
        setMostrarFormLogin(!mostrarFormLogin);
    };

    //Consumir servicio para enviar las credenciales por correo
    const handleClickRecuperarCredenciales = async (e) => {
        e.preventDefault();

        if (formLogin.txtCorreoRecuperacion === "") {
            funcAlert("Por favor, ingrese su correo de recuperación", "warning");
            return;
        }

        if (!rxCorreo.test(formLogin.txtCorreoRecuperacion)) {
            funcAlert("Por favor, ingrese un correo válido", "warning");
            return;
        }

        const entUsuarioSubmit = {
            sCorreo: formLogin.txtCorreoRecuperacion,
        };

        funcLoader(true, "Enviando correo...");

        const response = await cguController.funcRecuperarPassword(entUsuarioSubmit);

        if (response.Code === 0) {
            setFormLogin({
                txtUsuarioMeditoc: "",
                txtPasswordMeditoc: "",
                txtCorreoRecuperacion: "",
            });
            setMostrarFormLogin(true);
            funcAlert(response.Message, "success");
        } else {
            funcAlert(response.Message);
        }
        funcLoader();
    };

    //-----------------------Functions
    //Consumir API para iniciar sesión
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

            await fnObtenerCatalogos();

            setUsuarioSesion(responseLogin.Result);
            setUsuarioActivo(true);
        } else {
            sessionStorage.removeItem("MeditocTkn");
            sessionStorage.removeItem("MeditocKey");
            funcAlert(responseLogin.Message);
        }
    };

    //Obtener los catálogos del sistema
    const fnObtenerCatalogos = async () => {
        funcLoader(true, "Consultando información del sistema...");

        const response = await politicasController.funcGetCatalogos();
        if (response.Code === 0) {
            setEntCatalogos(response.Result);
        }

        funcLoader();
    };

    //Validar los datos de sesión guardados en el sesionStorage
    const fnValidarSesionGuardada = () => {
        const MeditocTkn = sessionStorage.getItem("MeditocTkn");
        const MeditocKey = sessionStorage.getItem("MeditocKey");

        if (MeditocTkn != null && MeditocKey != null) {
            funcApiLogin(MeditocTkn, MeditocKey);
        }
    };

    //-----------------------Effects
    //Validar sesión guardada en el sesionStorage al cargar el Login
    useEffect(() => {
        setMostrarPortadaLogin(true);
        fnValidarSesionGuardada();
        // eslint-disable-next-line
    }, []);

    return (
        <div className="login-container">
            <Grid container spacing={0} className="login-page">
                <Grid item md={8} xs={12} className="align-self-center">
                    <div className="login-logo-container">
                        <Fade in={mostrarPortadaLogin} timeout={1500}>
                            <img src={imgLogoLogin} alt="LOGOLOGIN" className="login-logo-img" />
                        </Fade>
                    </div>
                </Grid>
                <Grid item md={4} xs={12} className="login-form-back">
                    <div>
                        <Collapse in={mostrarFormLogin}>
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
                                            autoFocus
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
                                            type={mostrarPassword ? "text" : "password"}
                                            InputProps={{
                                                endAdornment: (
                                                    <Tooltip
                                                        title={
                                                            mostrarPassword ? "Ocultar contraseña" : "Ver contraseña"
                                                        }
                                                        arrow
                                                        placement="top"
                                                    >
                                                        <IconButton
                                                            onMouseDown={() => setMostrarPassword(true)}
                                                            onMouseUp={() => setMostrarPassword(false)}
                                                        >
                                                            {mostrarPassword ? (
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
                        <Collapse in={!mostrarFormLogin}>
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
                                            value={formLogin.txtCorreoRecuperacion}
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

Login.propTypes = {
    funcAlert: PropTypes.func,
    funcLoader: PropTypes.func,
    setEntCatalogos: PropTypes.func,
    setUsuarioActivo: PropTypes.func,
    setUsuarioPermisos: PropTypes.func,
    setUsuarioSesion: PropTypes.func,
};

export default Login;
