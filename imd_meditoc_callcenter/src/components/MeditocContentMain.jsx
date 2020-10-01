import PropTypes from "prop-types";
import React, { Fragment, useState } from "react";
import MeditocNavBar from "./MeditocNavBar";
import MeditocDrawerLeft from "./MeditocDrawerLeft";
import { Switch, Route } from "react-router-dom";
import { urlSystem } from "../configurations/urlConfig";
import Sistema from "./meditoc/configuracion/sistema/Sistema";
import Perfiles from "./meditoc/configuracion/perfiles/Perfiles";
import Usuarios from "./meditoc/configuracion/usuarios/Usuarios";
import Productos from "./meditoc/administracion/productos/Productos";
import Empresa from "./meditoc/administracion/empresa/Empresa";
import Cupones from "./meditoc/administracion/cupones/Cupones";
import MeditocPortada from "./MeditocPortada";
import Colaboradores from "./meditoc/administracion/colaboradores/Colaboradores";
import Especialidades from "./meditoc/administracion/especialidades/Especialidades";
import Administrador from "./meditoc/callcenter/administrador/Administrador";
import CallCenter from "./meditoc/callcenter/callcenter/CallCenter";
import ReportesDoctores from "./meditoc/reportes/ReportesDoctores";
import ReportesVentas from "./meditoc/reportes/ReportesVentas";
import Folios from "./meditoc/folios/Folios";
import MeditocFooter from "./MeditocFooter";

/*************************************************************
 * Descripcion: Contiene las secciones y vistas de todo el portal de Meditoc
 * Creado: Cristopher Noh
 * Fecha: 26/08/2020
 * Invocado desde: App
 *************************************************************/
const ContentMain = (props) => {
    const { usuarioSesion, usuarioPermisos, setUsuarioSesion, setUsuarioActivo, funcLoader, funcAlert } = props;
    const [drawerOpen, setDrawerOpen] = useState(false);

    //Desplegar/Ocultar menu lateral izquerdo
    const toggleDrawer = (open) => (event) => {
        if (event.type === "keydown" && (event.key === "Tab" || event.key === "Shift")) {
            return;
        }

        setDrawerOpen(open);
    };

    return (
        <Fragment>
            <div className="flx-grw-1 pos-rel">
                <MeditocNavBar
                    toggleDrawer={toggleDrawer}
                    setUsuarioSesion={setUsuarioSesion}
                    setUsuarioActivo={setUsuarioActivo}
                    usuarioSesion={usuarioSesion}
                    funcLoader={funcLoader}
                    funcAlert={funcAlert}
                />
                <MeditocDrawerLeft
                    drawerOpen={drawerOpen}
                    usuarioPermisos={usuarioPermisos}
                    toggleDrawer={toggleDrawer}
                />
                <div>
                    <Switch>
                        <Route exact path="/">
                            <MeditocPortada />
                        </Route>
                        <Route exact path={urlSystem.configuracion.sistema}>
                            <Sistema usuarioSesion={usuarioSesion} funcLoader={funcLoader} funcAlert={funcAlert} />
                        </Route>
                        <Route exact path={urlSystem.configuracion.perfiles}>
                            <Perfiles usuarioSesion={usuarioSesion} funcLoader={funcLoader} funcAlert={funcAlert} />
                        </Route>
                        <Route exact path={urlSystem.configuracion.usuarios}>
                            <Usuarios usuarioSesion={usuarioSesion} funcLoader={funcLoader} funcAlert={funcAlert} />
                        </Route>
                        <Route exact path={urlSystem.administracion.productos}>
                            <Productos usuarioSesion={usuarioSesion} funcLoader={funcLoader} funcAlert={funcAlert} />
                        </Route>
                        <Route exact path={urlSystem.administracion.institucion}>
                            <Empresa usuarioSesion={usuarioSesion} funcLoader={funcLoader} funcAlert={funcAlert} />
                        </Route>
                        <Route exact path={urlSystem.administracion.cupones}>
                            <Cupones usuarioSesion={usuarioSesion} funcLoader={funcLoader} funcAlert={funcAlert} />
                        </Route>
                        <Route exact path={urlSystem.administracion.colaboradores}>
                            <Colaboradores
                                usuarioSesion={usuarioSesion}
                                funcLoader={funcLoader}
                                funcAlert={funcAlert}
                            />
                        </Route>
                        <Route exact path={urlSystem.administracion.especialidades}>
                            <Especialidades
                                usuarioSesion={usuarioSesion}
                                funcLoader={funcLoader}
                                funcAlert={funcAlert}
                            />
                        </Route>
                        <Route exact path={urlSystem.folios.folios}>
                            <Folios usuarioSesion={usuarioSesion} funcLoader={funcLoader} funcAlert={funcAlert} />
                        </Route>
                        <Route exact path={urlSystem.callcenter.administrarConsultas}>
                            <Administrador
                                usuarioSesion={usuarioSesion}
                                funcLoader={funcLoader}
                                funcAlert={funcAlert}
                            />
                        </Route>
                        <Route exact path={urlSystem.callcenter.consultas}>
                            <CallCenter usuarioSesion={usuarioSesion} funcLoader={funcLoader} funcAlert={funcAlert} />
                        </Route>
                        <Route exact path={urlSystem.reportes.doctores}>
                            <ReportesDoctores
                                usuarioSesion={usuarioSesion}
                                funcLoader={funcLoader}
                                funcAlert={funcAlert}
                            />
                        </Route>
                        <Route exact path={urlSystem.reportes.ventas}>
                            <ReportesVentas
                                usuarioSesion={usuarioSesion}
                                funcLoader={funcLoader}
                                funcAlert={funcAlert}
                            />
                        </Route>
                    </Switch>
                </div>
            </div>
            <MeditocFooter />
        </Fragment>
    );
};

ContentMain.propTypes = {
    funcAlert: PropTypes.func,
    funcLoader: PropTypes.func,
    usuarioSesion: PropTypes.object,
};

export default ContentMain;
