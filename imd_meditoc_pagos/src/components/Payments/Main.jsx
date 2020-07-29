import PropTypes from "prop-types";
import React, { Fragment } from "react";
import Header from "./Header";
import Content from "./Content";
import Footer from "../Footer";

/*****************************************************
 * Descripción: Contenido principal para pagina de pagos
 * Autor: Cristopher Noh
 * Fecha: 23/07/2020
 * Modificaciones:
 *****************************************************/
const Main = (props) => {
    const { funcLoader } = props;

    window.scrollTo(0, 0);

    return (
        <Fragment>
            <Header />
            <Content funcLoader={funcLoader} />
            <Footer />
        </Fragment>
    );
};

Main.propTypes = {
    funcLoader: PropTypes.func,
};

export default Main;
