import Enterprise from "./Enterprise";
import { FaNutritionix } from "react-icons/fa";
import GetApp from "../GetApp";
import { Grid } from "@material-ui/core";
import Memberships from "./Memberships";
import PropTypes from "prop-types";
import React from "react";
import { RiPsychotherapyFill } from "react-icons/ri";

/*****************************************************
 * Descripción: Contiene la estructura para mostrar
 * los precios de servicios Meditoc
 * Autor: Cristopher Noh
 * Fecha: 22/07/2020
 * Modificaciones:
 *****************************************************/
const Content = (props) => {
    // eslint-disable-next-line
    const { lstNutritionalProducts, lstPsychologyProducts } = props;

    return (
        <div className="price-content-container">
            <Grid container className="center" spacing={4}>
                <Grid item xs={12}>
                    <div className="price-content-title">
                        <span className="primary-blue">Meditoc</span> <span className="primary-gray">360</span>
                    </div>
                </Grid>
                <Grid item xs={12}>
                    <span className="price-content-description">
                        Con Meditoc puedes tener a tu Nutriólogo o Psicólogo sin salir de casa en donde te encuentres.
                    </span>
                </Grid>
                {/* <Grid item xs={12}>
                    <span className="price-content-description-normal">
                        Podrá adquirir membresías para los siguientes servicios.
                    </span>
                </Grid> */}
                <Grid item xs={12}>
                    <Enterprise />
                    <br />
                    <br />
                </Grid>
                <Grid item md={6} xs={12}>
                    {lstNutritionalProducts.length > 0 ? (
                        <Memberships
                            title="Meditoc Nutriólogo"
                            icon={<FaNutritionix />}
                            lstMembershipProducts={lstNutritionalProducts}
                        />
                    ) : null}
                </Grid>
                <Grid item md={6} xs={12}>
                    {lstPsychologyProducts.length > 0 ? (
                        <Memberships
                            title="Meditoc Psicólogo"
                            icon={<RiPsychotherapyFill />}
                            lstMembershipProducts={lstPsychologyProducts}
                        />
                    ) : null}
                </Grid>
                <Grid item xs={12} style={{ marginBottom: 50 }}>
                    <GetApp />
                </Grid>
            </Grid>
        </div>
    );
};

Content.propTypes = {
    lstMembershipProducts: PropTypes.array,
    lstOrientationProducts: PropTypes.array,
    setLstOrientationProducts: PropTypes.func,
};

export default Content;
