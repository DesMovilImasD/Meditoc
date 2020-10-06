import React, { Fragment, useEffect } from "react";

import Content from "./Content";
import DirectoryHeader from "./DirectoryHeader";
import Footer from "../Footer";
import Menu from "../Menu";
import PropTypes from "prop-types";

/*****************************************************
 * Descripción: Estructura de la página del directorio
 * Autor: Cristopher Noh
 * Fecha: 07/09/2020
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
            <Menu />
            <DirectoryHeader />
            <Content funcLoader={funcLoader} />
            <Footer appInfo={appInfo} />
        </Fragment>
    );
};

Main.propTypes = {
    appInfo: PropTypes.object,
    funcLoader: PropTypes.func,
};

export default Main;
