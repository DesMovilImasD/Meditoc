import PropTypes from "prop-types";
import React, { useState, useEffect } from "react";
import { Grid, Button } from "@material-ui/core";
import Memberships from "./Memberships";
import Orientations from "./Orientations";
import Enterprise from "./Enterprise";
import { logoappleAvalible, logoplayAvalible } from "../../configuration/imgConfig";

/*****************************************************
 * Descripción: Contiene la estructura para mostrar
 * los precios de servicios Meditoc
 * Autor: Cristopher Noh
 * Fecha: 22/07/2020
 * Modificaciones:
 *****************************************************/
const Content = (props) => {
    // eslint-disable-next-line
    const {
        funcLoader,
        lstOrientationProducts,
        setLstOrientationProducts,
        lstMembershipProducts,
        setLstMembershipProducts,
    } = props;

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
                    <div>
                        <p>
                            <span className="price-footer-address">
                                Para utilizar el servicio, descarga la app “Meditoc 360” disponible en Appstore y
                                Playstore.
                            </span>
                        </p>
                        <span>
                            <a href="https://apps.apple.com/mx/app/meditoc-360/id1521078394" target="_blank">
                                <img src={logoappleAvalible} alt="app-store" />
                            </a>
                        </span>
                        <span style={{ marginLeft: 10 }}>
                            <a
                                href="https://play.google.com/store/apps/details?id=com.meditoc.callCenter.comercial"
                                target="_blank"
                            >
                                <img src={logoplayAvalible} alt="play-store" />
                            </a>
                        </span>
                    </div>
                </Grid>
            </Grid>
        </div>
    );
};

Content.propTypes = {
    funcLoader: PropTypes.func.isRequired,
};

export default Content;
