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

/*************************************************************
 * Descripcion: Contiene las secciones y vistas de todo el portal de Meditoc
 * Creado: Cristopher Noh
 * Fecha: 26/08/2020
 * Invocado desde: App
 *************************************************************/
const ContentMain = (props) => {
    const { usuarioSesion, setUsuarioSesion, setUsuarioActivo, funcLoader, funcAlert } = props;
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
            <MeditocNavBar
                toggleDrawer={toggleDrawer}
                setUsuarioSesion={setUsuarioSesion}
                setUsuarioActivo={setUsuarioActivo}
                usuarioSesion={usuarioSesion}
                funcLoader={funcLoader}
                funcAlert={funcAlert}
            />
            <MeditocDrawerLeft drawerOpen={drawerOpen} toggleDrawer={toggleDrawer} />
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
                        <Colaboradores usuarioSesion={usuarioSesion} funcLoader={funcLoader} funcAlert={funcAlert} />
                    </Route>
                </Switch>
            </div>
        </Fragment>
    );
};

ContentMain.propTypes = {
    funcAlert: PropTypes.func,
    funcLoader: PropTypes.func,
    usuarioSesion: PropTypes.object,
};

export default ContentMain;
