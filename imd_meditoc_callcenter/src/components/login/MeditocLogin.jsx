import { Button, Fade, Grid } from "@material-ui/core";
import { EnumPerfilesPrincipales, EnumSistema } from "../../configurations/enumConfig";
import { imgLogoLogin, imgLogoMeditocCasa } from "../../configurations/imgConfig";

import CGUController from "../../controllers/CGUController";
import MeditocInputWhite from "../utilidades/MeditocInputWhite";
import React from "react";
import { makeStyles } from "@material-ui/core/styles";
import permisosSistema from "../../configurations/systemConfig";
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
