import React from "react";
import { Grid } from "@material-ui/core";
import Memberships from "./Memberships";
import Orientations from "./Orientations";

/*****************************************************
 * Descripción: Contiene la estructura para mostrar
 * los precios de servicios Meditoc
 * Autor: Cristopher Noh
 * Fecha: 22/07/2020
 * Modificaciones:
 *****************************************************/
const Content = () => {
    return (
        <div className="price-content-container">
            <Grid container className="center" spacing={4}>
                <Grid item xs={12}>
                    <div className="price-content-title">
                        <span className="primary-blue">Meditoc</span>
                        &nbsp;
                        <span className="primary-gray">360</span>
                    </div>
                </Grid>
                <Grid item xs={12}>
                    <span className="price-content-description">
                        Meditoc 360 ofrece orientación médica, nutricional y psicológica a distancia los 365 días del
                        año,
                        <br /> brindando acceso a servicio de salud de calidad.
                    </span>
                </Grid>
                <Grid item xs={12}>
                    <span className="price-content-description-normal">
                        Podrá adquirir membresías para los siguientes servicios.
                    </span>
                </Grid>
                <Grid item md={6} xs={12}>
                    <Memberships />
                </Grid>
                <Grid item md={6} xs={12}>
                    <Orientations />
                </Grid>
            </Grid>
        </div>
    );
};

export default Content;
