import React, { Fragment } from "react";

import Content from "./Content";
import Footer from "../Footer";
import Header from "../Header";
import Menu from "../Menu";
import PropTypes from "prop-types";

/*****************************************************
 * Descripción: Estructura principal de la pagina de precios
 * Autor: Cristopher Noh
 * Fecha: 22/07/2020
 * Modificaciones:
 *****************************************************/
const Main = (props) => {
    const { funcLoader, appInfo, lstNutritionalProducts, lstPsychologyProducts } = props;

    return (
        <Fragment>
            <Menu />
            {/* <Cover /> */}
            <Header />
            <Content
                funcLoader={funcLoader}
                lstNutritionalProducts={lstNutritionalProducts}
                lstPsychologyProducts={lstPsychologyProducts}
            />
            <Footer appInfo={appInfo} />
        </Fragment>
    );
};

Main.propTypes = {
    appInfo: PropTypes.object,
    funcLoader: PropTypes.func,
    lstMembershipProducts: PropTypes.array,
    lstOrientationProducts: PropTypes.array,
    setLstMembershipProducts: PropTypes.func,
    setLstOrientationProducts: PropTypes.func,
};

export default Main;
