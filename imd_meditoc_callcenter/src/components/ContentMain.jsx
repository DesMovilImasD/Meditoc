import React, { Fragment, useState } from "react";
import NavBar from "./NavBar";
import DrawerMenu from "./DrawerMenu";

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
        </Fragment>
    );
};

export default ContentMain;
