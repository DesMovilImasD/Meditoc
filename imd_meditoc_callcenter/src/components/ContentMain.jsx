import React, { Fragment, useState } from "react";
import NavBar from "./NavBar";
import DrawerMenu from "./DrawerMenu";
import { Switch, Route } from "react-router-dom";
import { urlSystem } from "../configurations/urlConfig";
import Sistema from "./configuracion/sistema/Sistema";
import Perfiles from "./configuracion/perfiles/Perfiles";

/*************************************************************
 * Descripcion: Contiene las secciones y vistas de todo el portal de Meditoc
 * Creado: Cristopher Noh
 * Fecha: 26/08/2020
 * Invocado desde: App
 *************************************************************/
const ContentMain = (props) => {
    const { funcLoader, funcAlert } = props;
    const [drawerOpen, setDrawerOpen] = useState(false);
    const [usuarioSesion, setUsuarioSesion] = useState({
        iIdUsuario: 1,
    });

    const toggleDrawer = (open) => (event) => {
        if (event.type === "keydown" && (event.key === "Tab" || event.key === "Shift")) {
            return;
        }

        setDrawerOpen(open);
    };

    return (
        <Fragment>
            <NavBar toggleDrawer={toggleDrawer} />
            <DrawerMenu drawerOpen={drawerOpen} toggleDrawer={toggleDrawer} />
            <div>
                <Switch>
                    <Route exact path={urlSystem.configuracion.sistema}>
                        <Sistema usuarioSesion={usuarioSesion} funcLoader={funcLoader} funcAlert={funcAlert} />
                    </Route>
                    <Route exact path={urlSystem.configuracion.perfiles}>
                        <Perfiles />
                    </Route>
                </Switch>
            </div>
        </Fragment>
    );
};

export default ContentMain;
