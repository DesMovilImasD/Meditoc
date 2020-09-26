import PropTypes from "prop-types";
import React, { Fragment } from "react";
import Menu from "../Menu";
import Cover from "./Cover";
import Content from "./Content";
import Footer from "../Footer";
import Header from "../Header";
import Directory from "./Directory";

/*****************************************************
 * Descripción: Estructura principal de la pagina de precios
 * Autor: Cristopher Noh
 * Fecha: 22/07/2020
 * Modificaciones:
 *****************************************************/
const Main = (props) => {
    const {
        funcLoader,
        appInfo,
        lstOrientationProducts,
        setLstOrientationProducts,
        lstMembershipProducts,
        setLstMembershipProducts,
    } = props;

    return (
        <Fragment>
            <Menu />
            {/* <Cover /> */}
            <Header />
            <Content
                funcLoader={funcLoader}
                lstOrientationProducts={lstOrientationProducts}
                setLstOrientationProducts={setLstOrientationProducts}
                lstMembershipProducts={lstMembershipProducts}
                setLstMembershipProducts={setLstMembershipProducts}
            />
            <Directory />
            <Footer appInfo={appInfo} />
        </Fragment>
    );
};

Main.propTypes = {
    funcLoader: PropTypes.func.isRequired,
};

export default Main;
