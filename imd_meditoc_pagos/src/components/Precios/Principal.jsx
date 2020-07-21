import React, { Fragment } from "react";
import Menu from "./Menu";
import Portada from "./Portada";
import Contenido from "./Contenido";
import Footer from "./Footer";

const Principal = () => {
    return (
        <Fragment>
            <Menu />
            <Portada />
            <Contenido />
            <Footer />
        </Fragment>
    );
};

export default Principal;
