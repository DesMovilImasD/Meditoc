import PropTypes from "prop-types";
import React, { Fragment, useState } from "react";
import NavBar from "./NavBar";
import DrawerMenu from "./DrawerMenu";
import { Switch, Route } from "react-router-dom";
import { urlSystem } from "../configurations/urlConfig";
import Sistema from "./meditoc/configuracion/sistema/Sistema";
import Perfiles from "./meditoc/configuracion/perfiles/Perfiles";
import Usuarios from "./meditoc/configuracion/usuarios/Usuarios";

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
            <NavBar
                toggleDrawer={toggleDrawer}
                setUsuarioSesion={setUsuarioSesion}
                setUsuarioActivo={setUsuarioActivo}
                usuarioSesion={usuarioSesion}
                funcLoader={funcLoader}
                funcAlert={funcAlert}
            />
            <DrawerMenu drawerOpen={drawerOpen} toggleDrawer={toggleDrawer} />
            <div>
                <Switch>
                    <Route exact path={urlSystem.configuracion.sistema}>
                        <Sistema usuarioSesion={usuarioSesion} funcLoader={funcLoader} funcAlert={funcAlert} />
                    </Route>
                    <Route exact path={urlSystem.configuracion.perfiles}>
                        <Perfiles usuarioSesion={usuarioSesion} funcLoader={funcLoader} funcAlert={funcAlert} />
                    </Route>
                    <Route exact path={urlSystem.configuracion.usuarios}>
                        <Usuarios usuarioSesion={usuarioSesion} funcLoader={funcLoader} funcAlert={funcAlert} />
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
