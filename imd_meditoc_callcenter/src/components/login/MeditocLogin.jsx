import React from "react";
import { Grid, TextField, Button, Fade } from "@material-ui/core";
import { imgLogoLogin, imgLogoMeditocCasa } from "../../configurations/imgConfig";
import { makeStyles, withStyles } from "@material-ui/core/styles";
import theme from "../../configurations/themeConfig";
import { useState } from "react";
import { useEffect } from "react";
import CGUController from "../../controllers/CGUController";
import { EnumPerfilesPrincipales, EnumSistema } from "../../configurations/enumConfig";
import { useHistory } from "react-router-dom";
import { urlSystem } from "../../configurations/urlConfig";

const useStyles = makeStyles(() => ({
    button: {
        color: theme.palette.primary.main,
        backgroundColor: "#fff",
    },
}));

const TextFieldWhite = withStyles({
    root: {
        "& label.Mui-focused": {
            color: "#fff",
        },
        "& .MuiInput-underline:after": {
            borderBottomColor: "#fff",
        },
        "& .MuiInputBase-input": {
            color: "#fff",
            borderColor: "#fff",
        },
        "& .MuiFormLabel-root": {
            color: "#fff",
        },
        "& .MuiOutlinedInput-root": {
            "& fieldset": {
                borderColor: "#fff",
            },
            "&:hover fieldset": {
                borderColor: "#fff",
            },
            "&.Mui-focused fieldset": {
                borderColor: "#fff",
            },
        },
    },
})(TextField);

const Login = (props) => {
    const { setUsuarioSesion, setUsuarioActivo, setUsuarioPermisos, funcLoader, funcAlert } = props;

    const classes = useStyles();
    const history = useHistory();

    const cguController = new CGUController();

    const [imgLogoFade, setImgLogoFade] = useState(false);

    const [formLogin, setFormLogin] = useState({
        txtUsuarioMeditoc: "",
        txtPasswordMeditoc: "",
    });

    const handleChangeFormLogin = (e) => {
        setFormLogin({
            ...formLogin,
            [e.target.name]: e.target.value,
        });
    };

    const funcApiLogin = async (MeditocTkn, MeditocKey) => {
        funcLoader(true, "Validando datos de sesión...");

        const response = await cguController.funcGetLogin(MeditocTkn, MeditocKey);

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
        let permisos = {};

        const moduloConfiguracion = lstPermisos.find((x) => x.iIdModulo === EnumSistema.Configuracion); //Configuraciones
        if (moduloConfiguracion !== undefined) {
            permisos.configuracion = {};

            const submoduloUsuarios = moduloConfiguracion.lstSubModulo.find(
                (x) => x.iIdSubModulo === EnumSistema.ConfiguracionSM.Usuarios
            ); //Configuraciones > Usuarios
            if (submoduloUsuarios !== undefined) {
                permisos.configuracion.usuarios = true;
            }

            const submoduloPerfiles = moduloConfiguracion.lstSubModulo.find(
                (x) => x.iIdSubModulo === EnumSistema.ConfiguracionSM.Perfiles
            ); //Configuraciones > Perfiles
            if (submoduloPerfiles !== undefined) {
                permisos.configuracion.perfiles = true;
            }

            const submoduloSistema = moduloConfiguracion.lstSubModulo.find(
                (x) => x.iIdSubModulo === EnumSistema.ConfiguracionSM.Sistema
            ); //Configuraciones > Perfiles
            if (submoduloSistema !== undefined) {
                permisos.configuracion.sistema = true;
            }
        }

        const moduloAdministracion = lstPermisos.find((x) => x.iIdModulo === EnumSistema.Administracion); //Administracion
        if (moduloAdministracion !== undefined) {
            permisos.administracion = {};

            const submoduloColaboradores = moduloAdministracion.lstSubModulo.find(
                (x) => x.iIdSubModulo === EnumSistema.AdministracionSM.Colaboradores
            ); //Administracion > Colaboradores
            if (submoduloColaboradores !== undefined) {
                permisos.administracion.colaboradores = true;
            }

            const submoduloEmpresa = moduloAdministracion.lstSubModulo.find(
                (x) => x.iIdSubModulo === EnumSistema.AdministracionSM.Empresa
            ); //Administracion > Empresa
            if (submoduloEmpresa !== undefined) {
                permisos.administracion.empresa = true;
            }

            const submoduloProductos = moduloAdministracion.lstSubModulo.find(
                (x) => x.iIdSubModulo === EnumSistema.AdministracionSM.Productos
            ); //Administracion > Productos
            if (submoduloProductos !== undefined) {
                permisos.administracion.productos = true;
            }

            const submoduloCupones = moduloAdministracion.lstSubModulo.find(
                (x) => x.iIdSubModulo === EnumSistema.AdministracionSM.Cupones
            ); //Administracion > Cupones
            if (submoduloCupones !== undefined) {
                permisos.administracion.cupones = true;
            }

            const submoduloEspecialidades = moduloAdministracion.lstSubModulo.find(
                (x) => x.iIdSubModulo === EnumSistema.AdministracionSM.Especialidades
            ); //Administracion > Especialidades
            if (submoduloEspecialidades !== undefined) {
                permisos.administracion.especialidades = true;
            }
        }

        const moduloCallCenter = lstPermisos.find((x) => x.iIdModulo === EnumSistema.CallCenter); //CallCenter
        if (moduloCallCenter !== undefined) {
            permisos.callcenter = {};

            const submoduloConsultas = moduloCallCenter.lstSubModulo.find(
                (x) => x.iIdSubModulo === EnumSistema.CallCenterSM.Consultas
            ); //CallCenter > Consultas
            if (submoduloConsultas !== undefined) {
                permisos.callcenter.consultas = true;
            }

            const submoduloAdministrarConsultas = moduloCallCenter.lstSubModulo.find(
                (x) => x.iIdSubModulo === EnumSistema.CallCenterSM.AdministrarConsultas
            ); //CallCenter > Administrar consultas
            if (submoduloAdministrarConsultas !== undefined) {
                permisos.callcenter.administrarconsultas = true;
            }
        }

        const moduloReportes = lstPermisos.find((x) => x.iIdModulo === EnumSistema.Reportes); //Reportes
        if (moduloReportes !== undefined) {
            permisos.reportes = {};

            const submoduloVentas = moduloReportes.lstSubModulo.find(
                (x) => x.iIdSubModulo === EnumSistema.ReportesSM.Ventas
            ); //Reportes > Ventas
            if (submoduloVentas !== undefined) {
                permisos.reportes.ventas = true;
            }

            const submoduloDoctores = moduloReportes.lstSubModulo.find(
                (x) => x.iIdSubModulo === EnumSistema.ReportesSM.Doctores
            ); //Reportes > Doctores
            if (submoduloDoctores !== undefined) {
                permisos.reportes.doctores = true;
            }
        }

        switch (iIdPerfil) {
            case EnumPerfilesPrincipales.Superadministrador:
                history.push(urlSystem.configuracion.perfiles);
                break;
            case EnumPerfilesPrincipales.Administrador:
                history.push(urlSystem.configuracion.usuarios);
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

        console.log(permisos);
        setUsuarioPermisos(permisos);
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
                    <form className="login-form-container" onSubmit={handleSubmitFormLogin}>
                        <Grid container spacing={4}>
                            <Grid item xs={12}>
                                <img src={imgLogoMeditocCasa} alt="LOGOMEDITOCCASA" className="login-form-img" />
                            </Grid>
                            <Grid item xs={12}>
                                <span className="ops-nor bold size-25">ACCESO AL SISTEMA</span>
                            </Grid>
                            <Grid item xs={12}>
                                <TextFieldWhite
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
                                <TextFieldWhite
                                    name="txtPasswordMeditoc"
                                    variant="outlined"
                                    fullWidth
                                    autoComplete="new-password"
                                    label="Contraseña:"
                                    type="password"
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
                                {" "}
                            </Grid>
                        </Grid>
                    </form>
                </Grid>
            </Grid>
        </div>
    );
};

export default Login;
