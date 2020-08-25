import React, { Fragment, useState } from "react";
import NavBar from "./NavBar";
import DrawerMenu from "./DrawerMenu";
import { Switch, Route } from "react-router-dom";
import { urlSystem } from "../configurations/urlConfig";
import { Button } from "@material-ui/core";
import Sistema from "./configuracion/Sistema";

const ContentMain = () => {
    const [drawerOpen, setDrawerOpen] = useState(false);

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
                        <Sistema />
                    </Route>
                </Switch>
            </div>
        </Fragment>
    );
};

export default ContentMain;
