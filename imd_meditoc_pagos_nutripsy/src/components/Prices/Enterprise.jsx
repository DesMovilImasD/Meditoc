import { Button, Grid } from "@material-ui/core";

import { MdPermPhoneMsg } from "react-icons/md";
import React from "react";

const Enterprise = () => {
    return (
        <div className="price-product-container-small">
            <div className="price-product-header-small">
                <div className="price-product-header-icon">
                    <MdPermPhoneMsg />
                </div>
                <div className="price-product-header-title">Nosotros te llamamos</div>
            </div>
            <Grid container>
                <Grid item xs={12} className="price-product-description-container">
                    <span className="price-product-description">
                        ¿Quieres más información? ¿Tienes alguna duda? Déjanos tus datos y nosotros te contactamos.
                    </span>
                </Grid>
            </Grid>
            <div className="price-product-btn">
                <Button href="https://meditoc.com/#contacto" target="_blank" color="primary" variant="contained">
                    Contáctanos
                </Button>
            </div>
        </div>
    );
};

export default Enterprise;
