import PropTypes from "prop-types";
import React, { Fragment } from "react";
import Header from "./Header";
import Contenido from "./Contenido";
import Footer from "../Precios/Footer";

/*****************************************************
 * Descripción: Contenido principal para pagina de pagos
 * Autor: Cristopher Noh
 * Fecha: 23/07/2020
 * Modificaciones:
 *****************************************************/
const Principal = (props) => {
    const { funcLoader } = props;
    window.scrollTo(0, 0);
    return (
        <Fragment>
            <Header />
            <Contenido funcLoader={funcLoader} />
            <Footer />
        </Fragment>
    );
};

Principal.propTypes = {
    funcLoader: PropTypes.func,
};

export default Principal;
