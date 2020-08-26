import React, { Fragment, useState } from "react";
import NavBar from "./NavBar";
import DrawerMenu from "./DrawerMenu";
import { Switch, Route } from "react-router-dom";
import { urlSystem } from "../configurations/urlConfig";
import { Button } from "@material-ui/core";
import Sistema from "./configuracion/sistema/Sistema";
import ModalForm from "./ModalForm";
import { lstSistemaTemp } from "../configurations/systemConfigTemp";
import Perfiles from "./configuracion/perfiles/Perfiles";

const ContentMain = () => {
    const [drawerOpen, setDrawerOpen] = useState(false);

    const [listaSistema, setListaSistema] = useState([]);

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
                        <Sistema listaSistema={listaSistema} setListaSistema={setListaSistema} />
                    </Route>
                    <Route exact path={urlSystem.configuracion.perfiles}>
                        <Perfiles listaSistema={listaSistema} setListaSistema={setListaSistema} />
                    </Route>
                </Switch>
            </div>
        </Fragment>
    );
};

export default ContentMain;
