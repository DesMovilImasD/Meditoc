import React, { Fragment, useEffect } from "react";

import Content from "./Content";
import Footer from "../Footer";
import Header from "../Header";
import PropTypes from "prop-types";

/*****************************************************
 * Descripción: Contenido principal para pagina de pagos
 * Autor: Cristopher Noh
 * Fecha: 23/07/2020
 * Modificaciones:
 *****************************************************/
const Main = (props) => {
    const { appInfo, funcLoader } = props;

    //Scrollear al inicio cuando se cargue el componente
    useEffect(() => {
        window.scrollTo(0, 0);

        // eslint-disable-next-line
    }, []);

    return (
        <Fragment>
            <Header />
            <Content appInfo={appInfo} funcLoader={funcLoader} />
            <Footer appInfo={appInfo} />
        </Fragment>
    );
};

Main.propTypes = {
    appInfo: PropTypes.object,
    funcLoader: PropTypes.func,
};

export default Main;
