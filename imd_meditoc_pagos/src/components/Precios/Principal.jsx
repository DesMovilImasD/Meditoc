import React, { Fragment } from "react";
import Menu from "./Menu";
import Portada from "./Portada";
import Contenido from "./Contenido";
import Footer from "./Footer";

/*****************************************************
 * Descripción: Estructura principal de la pagina de precios
 * Autor: Cristopher Noh
 * Fecha: 22/07/2020
 * Modificaciones:
 *****************************************************/
const Principal = () => {
    window.scrollTo(0, 0);
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
