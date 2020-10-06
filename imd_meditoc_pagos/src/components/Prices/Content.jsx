import Enterprise from "./Enterprise";
import GetApp from "../GetApp";
import { Grid } from "@material-ui/core";
import Memberships from "./Memberships";
import Orientations from "./Orientations";
import PropTypes from "prop-types";
import React from "react";

/*****************************************************
 * Descripción: Contiene la estructura para mostrar
 * los precios de servicios Meditoc
 * Autor: Cristopher Noh
 * Fecha: 22/07/2020
 * Modificaciones:
 *****************************************************/
const Content = (props) => {
    // eslint-disable-next-line
    const { lstOrientationProducts, setLstOrientationProducts, lstMembershipProducts } = props;

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
                        Meditoc 360 ofrece orientación médica, nutricional y psicológica a distancia,
                        <br /> brindando acceso a servicios de salud de calidad.
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
                    {lstMembershipProducts.length > 0 ? (
                        <Memberships lstMembershipProducts={lstMembershipProducts} />
                    ) : null}
                </Grid>
                <Grid item md={6} xs={12}>
                    {lstOrientationProducts.length > 0 ? (
                        <Orientations
                            lstOrientationProducts={lstOrientationProducts}
                            setLstOrientationProducts={setLstOrientationProducts}
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
