import React, { Fragment, useState } from "react";
import { Route, Switch } from "react-router-dom";

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
    const { usuarioSesion, usuarioPermisos, setUsuarioSesion, setUsuarioActivo, funcLoader, funcAlert } = props;
    const [drawerOpen, setDrawerOpen] = useState(false);

    const [funcCerrarTodo, setFuncCerrarTodo] = useState(null);

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
                    funcCerrarTodo={funcCerrarTodo}
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
                            <Sistema
                                usuarioSesion={usuarioSesion}
                                funcLoader={funcLoader}
                                funcAlert={funcAlert}
                                title={usuarioPermisos.configuracion.sistema.name}
                            />
                        </Route>
                        <Route exact path={urlSystem.configuracion.perfiles}>
                            <Perfiles
                                usuarioSesion={usuarioSesion}
                                funcLoader={funcLoader}
                                funcAlert={funcAlert}
                                title={usuarioPermisos.configuracion.perfiles.name}
                            />
                        </Route>
                        <Route exact path={urlSystem.configuracion.usuarios}>
                            <Usuarios
                                usuarioSesion={usuarioSesion}
                                funcLoader={funcLoader}
                                funcAlert={funcAlert}
                                title={usuarioPermisos.configuracion.usuarios.name}
                            />
                        </Route>
                        <Route exact path={urlSystem.administracion.productos}>
                            <Productos
                                usuarioSesion={usuarioSesion}
                                funcLoader={funcLoader}
                                funcAlert={funcAlert}
                                title={usuarioPermisos.administracion.productos.name}
                            />
                        </Route>
                        <Route exact path={urlSystem.administracion.institucion}>
                            <Empresa
                                usuarioSesion={usuarioSesion}
                                funcLoader={funcLoader}
                                funcAlert={funcAlert}
                                title={usuarioPermisos.administracion.empresa.name}
                            />
                        </Route>
                        <Route exact path={urlSystem.administracion.cupones}>
                            <Cupones
                                usuarioSesion={usuarioSesion}
                                funcLoader={funcLoader}
                                funcAlert={funcAlert}
                                title={usuarioPermisos.administracion.cupones.name}
                            />
                        </Route>
                        <Route exact path={urlSystem.administracion.colaboradores}>
                            <Colaboradores
                                usuarioSesion={usuarioSesion}
                                funcLoader={funcLoader}
                                funcAlert={funcAlert}
                                title={usuarioPermisos.administracion.colaboradores.name}
                            />
                        </Route>
                        <Route exact path={urlSystem.administracion.especialidades}>
                            <Especialidades
                                usuarioSesion={usuarioSesion}
                                funcLoader={funcLoader}
                                funcAlert={funcAlert}
                                title={usuarioPermisos.administracion.especialidades.name}
                            />
                        </Route>
                        <Route exact path={urlSystem.folios.folios}>
                            <Folios
                                usuarioSesion={usuarioSesion}
                                funcLoader={funcLoader}
                                funcAlert={funcAlert}
                                title={usuarioPermisos.folios.folios.name}
                            />
                        </Route>
                        <Route exact path={urlSystem.callcenter.administrarConsultas}>
                            <Administrador
                                usuarioSesion={usuarioSesion}
                                funcLoader={funcLoader}
                                funcAlert={funcAlert}
                                title={usuarioPermisos.callcenter.administrarconsultas.name}
                            />
                        </Route>
                        <Route exact path={urlSystem.callcenter.consultas}>
                            <CallCenter
                                usuarioSesion={usuarioSesion}
                                funcLoader={funcLoader}
                                funcAlert={funcAlert}
                                setFuncCerrarTodo={setFuncCerrarTodo}
                                title={usuarioPermisos.callcenter.consultas.name}
                            />
                        </Route>
                        <Route exact path={urlSystem.reportes.doctores}>
                            <ReportesDoctores
                                usuarioSesion={usuarioSesion}
                                funcLoader={funcLoader}
                                funcAlert={funcAlert}
                                title={usuarioPermisos.reportes.doctores.name}
                            />
                        </Route>
                        <Route exact path={urlSystem.reportes.ventas}>
                            <ReportesVentas
                                usuarioSesion={usuarioSesion}
                                funcLoader={funcLoader}
                                funcAlert={funcAlert}
                                title={usuarioPermisos.reportes.ventas.name}
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
    setUsuarioActivo: PropTypes.any,
    setUsuarioSesion: PropTypes.any,
    usuarioPermisos: PropTypes.shape({
        administracion: PropTypes.shape({
            colaboradores: PropTypes.shape({
                name: PropTypes.any,
            }),
            cupones: PropTypes.shape({
                name: PropTypes.any,
            }),
            empresa: PropTypes.shape({
                name: PropTypes.any,
            }),
            especialidades: PropTypes.shape({
                name: PropTypes.any,
            }),
            productos: PropTypes.shape({
                name: PropTypes.any,
            }),
        }),
        callcenter: PropTypes.shape({
            administrarconsultas: PropTypes.shape({
                name: PropTypes.any,
            }),
            consultas: PropTypes.shape({
                name: PropTypes.any,
            }),
        }),
        configuracion: PropTypes.shape({
            perfiles: PropTypes.shape({
                name: PropTypes.any,
            }),
            sistema: PropTypes.shape({
                name: PropTypes.any,
            }),
            usuarios: PropTypes.shape({
                name: PropTypes.any,
            }),
        }),
        folios: PropTypes.shape({
            folios: PropTypes.shape({
                name: PropTypes.any,
            }),
        }),
        reportes: PropTypes.shape({
            doctores: PropTypes.shape({
                name: PropTypes.any,
            }),
            ventas: PropTypes.shape({
                name: PropTypes.any,
            }),
        }),
    }),
    usuarioSesion: PropTypes.object,
};

export default ContentMain;
