import React from "react";
import { FaBusinessTime } from "react-icons/fa";
import { Grid, Button } from "@material-ui/core";

const Enterprise = () => {
    return (
        <div className="price-product-container-small">
            <div className="price-product-header-small">
                <div className="price-product-header-icon">
                    <FaBusinessTime />
                </div>
                <div className="price-product-header-title">Empresarial</div>
            </div>
            <Grid container>
                <Grid item xs={12} className="price-product-description-container">
                    <span className="price-product-description">
                        Para paquetes empresariales favor de ponerse en contacto con nostros
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
