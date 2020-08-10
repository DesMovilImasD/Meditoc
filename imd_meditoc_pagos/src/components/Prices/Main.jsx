import PropTypes from "prop-types";
import React, { Fragment } from "react";
import Menu from "./Menu";
import Cover from "./Cover";
import Content from "./Content";
import Footer from "../Footer";

/*****************************************************
 * Descripción: Estructura principal de la pagina de precios
 * Autor: Cristopher Noh
 * Fecha: 22/07/2020
 * Modificaciones:
 *****************************************************/
const Main = (props) => {
    const { funcLoader, appInfo } = props;

    return (
        <Fragment>
            <Menu />
            <Cover />
            <Content funcLoader={funcLoader} />
            <Footer appInfo={appInfo} />
        </Fragment>
    );
};

Main.propTypes = {
    funcLoader: PropTypes.func.isRequired,
};

export default Main;
