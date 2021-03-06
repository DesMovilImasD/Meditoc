import React, { Fragment, useEffect, useState } from "react";
import { Route, Switch, useHistory } from "react-router-dom";

import Administrador from "./callcenter/administrador/Administrador";
import CallCenter from "./callcenter/callcenter/CallCenter";
import Colaboradores from "./administracion/colaboradores/Colaboradores";
import Cupones from "./administracion/cupones/Cupones";
import Empresa from "./administracion/empresa/Empresa";
import Especialidades from "./administracion/especialidades/Especialidades";
import Folios from "./folios/Folios";
import MeditocDrawerLeft from "./MeditocDrawerLeft";
import MeditocFooter from "./MeditocFooter";
import MeditocNavBar from "./MeditocNavBar";
import MeditocPortada from "./MeditocPortada";
import MisConsultas from "./callcenter/misconsultas/MisConsultas";
import Perfiles from "./configuracion/perfiles/Perfiles";
import Productos from "./administracion/productos/Productos";
import PropTypes from "prop-types";
import ReportesDoctores from "./reportes/doctores/ReportesDoctores";
import ReportesVentas from "./reportes/ventas/ReportesVentas";
import Sistema from "./configuracion/sistema/Sistema";
import Usuarios from "./configuracion/usuarios/Usuarios";
import { urlSystem } from "../../configurations/urlConfig";

/*************************************************************
 * Descripcion: Contiene las secciones y vistas de todo el portal de Meditoc
 * Creado: Cristopher Noh
 * Fecha: 26/08/2020
 * Invocado desde: App
 *************************************************************/
const ContentMain = (props) => {
    const {
        usuarioSesion,
        usuarioPermisos,
        setUsuarioSesion,
        setUsuarioActivo,
        setUsuarioPermisos,
        rutaActual,
        setRutaActual,
        entCatalogos,
        funcLoader,
        funcAlert,
    } = props;

    //const history = useHistory();

    const [drawerOpen, setDrawerOpen] = useState(false);

    const f = {};
    f.e = () => {};
    const [funcCerrarTodo, setFuncCerrarTodo] = useState(f);

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
                    usuarioSesion={usuarioSesion}
                    funcLoader={funcLoader}
                    funcAlert={funcAlert}
                    funcCerrarTodo={funcCerrarTodo}
                    setUsuarioSesion={setUsuarioSesion}
                    setUsuarioActivo={setUsuarioActivo}
                    setUsuarioPermisos={setUsuarioPermisos}
                    rutaActual={rutaActual}
                    setRutaActual={setRutaActual}
                />
                <MeditocDrawerLeft
                    drawerOpen={drawerOpen}
                    usuarioPermisos={usuarioPermisos}
                    toggleDrawer={toggleDrawer}
                    funcCerrarTodo={funcCerrarTodo}
                    rutaActual={rutaActual}
                    setRutaActual={setRutaActual}
                />
                <div>
                    <Switch>
                        <Route exact path="/">
                            <MeditocPortada />
                        </Route>
                        {/* FAKE LOGIN COMPONENT */}
                        <Route exact path={urlSystem.login}>
                            <MeditocPortada />
                        </Route>
                        <Route exact path={urlSystem.configuracion.usuarios}>
                            <Usuarios
                                usuarioSesion={usuarioSesion}
                                permisos={
                                    usuarioPermisos["1"] === undefined ? {} : usuarioPermisos["1"].Submodulos["1"]
                                }
                                funcLoader={funcLoader}
                                funcAlert={funcAlert}
                            />
                        </Route>
                        <Route exact path={urlSystem.configuracion.perfiles}>
                            <Perfiles
                                usuarioSesion={usuarioSesion}
                                permisos={
                                    usuarioPermisos["1"] === undefined ? {} : usuarioPermisos["1"].Submodulos["2"]
                                }
                                funcLoader={funcLoader}
                                funcAlert={funcAlert}
                            />
                        </Route>
                        <Route exact path={urlSystem.configuracion.sistema}>
                            <Sistema
                                usuarioSesion={usuarioSesion}
                                permisos={
                                    usuarioPermisos["1"] === undefined ? {} : usuarioPermisos["1"].Submodulos["3"]
                                }
                                setUsuarioPermisos={setUsuarioPermisos}
                                funcLoader={funcLoader}
                                funcAlert={funcAlert}
                            />
                        </Route>
                        <Route exact path={urlSystem.administracion.colaboradores}>
                            <Colaboradores
                                usuarioSesion={usuarioSesion}
                                permisos={
                                    usuarioPermisos["2"] === undefined ? {} : usuarioPermisos["2"].Submodulos["1"]
                                }
                                funcLoader={funcLoader}
                                funcAlert={funcAlert}
                            />
                        </Route>
                        <Route exact path={urlSystem.administracion.institucion}>
                            <Empresa
                                usuarioSesion={usuarioSesion}
                                permisos={
                                    usuarioPermisos["2"] === undefined ? {} : usuarioPermisos["2"].Submodulos["2"]
                                }
                                entCatalogos={entCatalogos}
                                funcLoader={funcLoader}
                                funcAlert={funcAlert}
                            />
                        </Route>
                        <Route exact path={urlSystem.administracion.productos}>
                            <Productos
                                usuarioSesion={usuarioSesion}
                                permisos={
                                    usuarioPermisos["2"] === undefined ? {} : usuarioPermisos["2"].Submodulos["3"]
                                }
                                entCatalogos={entCatalogos}
                                funcLoader={funcLoader}
                                funcAlert={funcAlert}
                            />
                        </Route>
                        <Route exact path={urlSystem.administracion.cupones}>
                            <Cupones
                                usuarioSesion={usuarioSesion}
                                permisos={
                                    usuarioPermisos["2"] === undefined ? {} : usuarioPermisos["2"].Submodulos["4"]
                                }
                                entCatalogos={entCatalogos}
                                funcLoader={funcLoader}
                                funcAlert={funcAlert}
                            />
                        </Route>
                        <Route exact path={urlSystem.administracion.especialidades}>
                            <Especialidades
                                usuarioSesion={usuarioSesion}
                                permisos={
                                    usuarioPermisos["2"] === undefined ? {} : usuarioPermisos["2"].Submodulos["5"]
                                }
                                funcLoader={funcLoader}
                                funcAlert={funcAlert}
                            />
                        </Route>
                        <Route exact path={urlSystem.folios.folios}>
                            <Folios
                                usuarioSesion={usuarioSesion}
                                permisos={
                                    usuarioPermisos["3"] === undefined ? {} : usuarioPermisos["3"].Submodulos["1"]
                                }
                                entCatalogos={entCatalogos}
                                funcLoader={funcLoader}
                                funcAlert={funcAlert}
                            />
                        </Route>
                        <Route exact path={urlSystem.callcenter.consultas}>
                            <CallCenter
                                usuarioSesion={usuarioSesion}
                                permisos={
                                    usuarioPermisos["4"] === undefined ? {} : usuarioPermisos["4"].Submodulos["1"]
                                }
                                funcLoader={funcLoader}
                                funcAlert={funcAlert}
                                funcCerrarTodo={funcCerrarTodo}
                                setFuncCerrarTodo={setFuncCerrarTodo}
                            />
                        </Route>
                        <Route exact path={urlSystem.callcenter.administrarConsultas}>
                            <Administrador
                                usuarioSesion={usuarioSesion}
                                permisos={
                                    usuarioPermisos["4"] === undefined ? {} : usuarioPermisos["4"].Submodulos["2"]
                                }
                                funcLoader={funcLoader}
                                funcAlert={funcAlert}
                            />
                        </Route>
                        <Route exact path={urlSystem.callcenter.misconsultas}>
                            <MisConsultas
                                usuarioSesion={usuarioSesion}
                                permisos={
                                    usuarioPermisos["4"] === undefined ? {} : usuarioPermisos["4"].Submodulos["3"]
                                }
                                entCatalogos={entCatalogos}
                                funcLoader={funcLoader}
                                funcAlert={funcAlert}
                            />
                        </Route>
                        <Route exact path={urlSystem.reportes.doctores}>
                            <ReportesDoctores
                                usuarioSesion={usuarioSesion}
                                entCatalogos={entCatalogos}
                                permisos={
                                    usuarioPermisos["5"] === undefined ? {} : usuarioPermisos["5"].Submodulos["2"]
                                }
                                funcLoader={funcLoader}
                                funcAlert={funcAlert}
                            />
                        </Route>
                        <Route exact path={urlSystem.reportes.ventas}>
                            <ReportesVentas
                                usuarioSesion={usuarioSesion}
                                permisos={
                                    usuarioPermisos["5"] === undefined ? {} : usuarioPermisos["5"].Submodulos["1"]
                                }
                                entCatalogos={entCatalogos}
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
    funcAlert: PropTypes.any,
    funcLoader: PropTypes.any,
    setUsuarioActivo: PropTypes.any,
    setUsuarioPermisos: PropTypes.any,
    setUsuarioSesion: PropTypes.any,
    usuarioPermisos: PropTypes.any,
    usuarioSesion: PropTypes.any,
};

export default ContentMain;
