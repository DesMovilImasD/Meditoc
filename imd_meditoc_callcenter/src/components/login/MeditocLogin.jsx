import { Button, Collapse, Fade, Grid, IconButton, Tooltip, Typography } from "@material-ui/core";
import { EnumPerfilesPrincipales, EnumSistema } from "../../configurations/enumConfig";
import { imgLogoLogin, imgLogoMeditocCasa } from "../../configurations/imgConfig";

import CGUController from "../../controllers/CGUController";
import MeditocInputWhite from "../utilidades/MeditocInputWhite";
import React from "react";
import VisibilityIcon from "@material-ui/icons/Visibility";
import VisibilityOffIcon from "@material-ui/icons/VisibilityOff";
import { makeStyles } from "@material-ui/core/styles";
import permisosSistema from "../../configurations/systemConfig";
import { rxCorreo } from "../../configurations/regexConfig";
import theme from "../../configurations/themeConfig";
import { urlSystem } from "../../configurations/urlConfig";
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

    const [usuarioSistema, setUsuarioSistema] = useState([]);
    const [usuarioPermiso, setusuarioPermiso] = useState({});

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
        const response = await cguController.funcGetLogin(entUserSubmit);

        funcLoader();
        if (response.Code === 0) {
            sessionStorage.setItem("MeditocTkn", MeditocTkn);
            sessionStorage.setItem("MeditocKey", MeditocKey);

            funcLoader(true, "Obteniendo permisos..");
            const responsePerfil = await cguController.funcGetPermisosXPeril(response.Result.iIdPerfil);
            funcLoader();

            if (responsePerfil.Code === 0) {
                funcSetPermisos(response.Result.iIdPerfil, responsePerfil.Result);
            }
            setUsuarioSesion(response.Result);
            setUsuarioActivo(true);
        } else {
            sessionStorage.removeItem("MeditocTkn");
            sessionStorage.removeItem("MeditocKey");
            funcAlert(response.Message);
        }
    };

    const funcSetPermisos = (iIdPerfil = 0, lstPermisos = []) => {
        let usuarioPermisos = JSON.parse(JSON.stringify(permisosSistema));

        const moduloConfiguracion = lstPermisos.find((x) => x.iIdModulo === EnumSistema.Configuracion); //Configuraciones
        if (moduloConfiguracion !== undefined) {
            usuarioPermisos.configuracion.set = true;
            usuarioPermisos.configuracion.name = moduloConfiguracion.sNombre;

            const submoduloUsuarios = moduloConfiguracion.lstSubModulo.find(
                (x) => x.iIdSubModulo === EnumSistema.ConfiguracionSM.Usuarios
            );
            if (submoduloUsuarios !== undefined) {
                usuarioPermisos.configuracion.usuarios.set = true;
                usuarioPermisos.configuracion.usuarios.name = submoduloUsuarios.sNombre;
            }

            const submoduloPerfiles = moduloConfiguracion.lstSubModulo.find(
                (x) => x.iIdSubModulo === EnumSistema.ConfiguracionSM.Perfiles
            );
            if (submoduloPerfiles !== undefined) {
                usuarioPermisos.configuracion.perfiles.set = true;
                usuarioPermisos.configuracion.perfiles.name = submoduloPerfiles.sNombre;
            }

            const submoduloSistema = moduloConfiguracion.lstSubModulo.find(
                (x) => x.iIdSubModulo === EnumSistema.ConfiguracionSM.Sistema
            );
            if (submoduloSistema !== undefined) {
                usuarioPermisos.configuracion.sistema.set = true;
                usuarioPermisos.configuracion.sistema.name = submoduloSistema.sNombre;
            }
        }

        const moduloAdministracion = lstPermisos.find((x) => x.iIdModulo === EnumSistema.Administracion); //Administracion
        if (moduloAdministracion !== undefined) {
            usuarioPermisos.administracion.set = true;
            usuarioPermisos.administracion.name = moduloAdministracion.sNombre;

            const submoduloColaboradores = moduloAdministracion.lstSubModulo.find(
                (x) => x.iIdSubModulo === EnumSistema.AdministracionSM.Colaboradores
            );
            if (submoduloColaboradores !== undefined) {
                usuarioPermisos.administracion.colaboradores.set = true;
                usuarioPermisos.administracion.colaboradores.name = submoduloColaboradores.sNombre;
            }

            const submoduloEmpresa = moduloAdministracion.lstSubModulo.find(
                (x) => x.iIdSubModulo === EnumSistema.AdministracionSM.Empresa
            );
            if (submoduloEmpresa !== undefined) {
                usuarioPermisos.administracion.empresa.set = true;
                usuarioPermisos.administracion.empresa.name = submoduloEmpresa.sNombre;
            }

            const submoduloProductos = moduloAdministracion.lstSubModulo.find(
                (x) => x.iIdSubModulo === EnumSistema.AdministracionSM.Productos
            );
            if (submoduloProductos !== undefined) {
                usuarioPermisos.administracion.productos.set = true;
                usuarioPermisos.administracion.productos.name = submoduloProductos.sNombre;
            }

            const submoduloCupones = moduloAdministracion.lstSubModulo.find(
                (x) => x.iIdSubModulo === EnumSistema.AdministracionSM.Cupones
            );
            if (submoduloCupones !== undefined) {
                usuarioPermisos.administracion.cupones.set = true;
                usuarioPermisos.administracion.cupones.name = submoduloCupones.sNombre;
            }

            const submoduloEspecialidades = moduloAdministracion.lstSubModulo.find(
                (x) => x.iIdSubModulo === EnumSistema.AdministracionSM.Especialidades
            );
            if (submoduloEspecialidades !== undefined) {
                usuarioPermisos.administracion.especialidades.set = true;
                usuarioPermisos.administracion.especialidades.name = submoduloEspecialidades.sNombre;
            }
        }

        const moduloFolios = lstPermisos.find((x) => x.iIdModulo === EnumSistema.Folios); // Folios
        if (moduloFolios !== undefined) {
            usuarioPermisos.folios.set = true;
            usuarioPermisos.folios.name = moduloFolios.sNombre;

            const submoduloFolios = moduloFolios.lstSubModulo.find(
                (x) => x.iIdSubModulo === EnumSistema.FoliosSM.Folios
            );
            if (submoduloFolios !== undefined) {
                usuarioPermisos.folios.folios.set = true;
                usuarioPermisos.folios.folios.name = submoduloFolios.sNombre;
            }
        }

        const moduloCallCenter = lstPermisos.find((x) => x.iIdModulo === EnumSistema.CallCenter); //CallCenter
        if (moduloCallCenter !== undefined) {
            usuarioPermisos.callcenter.set = true;
            usuarioPermisos.callcenter.name = moduloCallCenter.sNombre;

            const submoduloConsultas = moduloCallCenter.lstSubModulo.find(
                (x) => x.iIdSubModulo === EnumSistema.CallCenterSM.Consultas
            );
            if (submoduloConsultas !== undefined) {
                usuarioPermisos.callcenter.consultas.set = true;
                usuarioPermisos.callcenter.consultas.name = submoduloConsultas.sNombre;
            }

            const submoduloAdministrarConsultas = moduloCallCenter.lstSubModulo.find(
                (x) => x.iIdSubModulo === EnumSistema.CallCenterSM.AdministrarConsultas
            );
            if (submoduloAdministrarConsultas !== undefined) {
                usuarioPermisos.callcenter.administrarconsultas.set = true;
                usuarioPermisos.callcenter.administrarconsultas.name = submoduloAdministrarConsultas.sNombre;
            }
        }

        const moduloReportes = lstPermisos.find((x) => x.iIdModulo === EnumSistema.Reportes); //Reportes
        if (moduloReportes !== undefined) {
            usuarioPermisos.reportes.set = true;
            usuarioPermisos.reportes.name = moduloReportes.sNombre;

            const submoduloVentas = moduloReportes.lstSubModulo.find(
                (x) => x.iIdSubModulo === EnumSistema.ReportesSM.Ventas
            );
            if (submoduloVentas !== undefined) {
                usuarioPermisos.reportes.ventas.set = true;
                usuarioPermisos.reportes.ventas.name = submoduloVentas.sNombre;
            }

            const submoduloDoctores = moduloReportes.lstSubModulo.find(
                (x) => x.iIdSubModulo === EnumSistema.ReportesSM.Doctores
            );
            if (submoduloDoctores !== undefined) {
                usuarioPermisos.reportes.doctores.set = true;
                usuarioPermisos.reportes.doctores.name = submoduloDoctores.sNombre;
            }
        }

        switch (iIdPerfil) {
            case EnumPerfilesPrincipales.Superadministrador:
                history.push("/");
                break;
            case EnumPerfilesPrincipales.Administrador:
                history.push("/");
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
                break;
        }

        setUsuarioPermisos(usuarioPermisos);
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
