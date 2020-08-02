import React, { useEffect } from "react";
import { Grid } from "@material-ui/core";
import { IoIosCloseCircle } from "react-icons/io";

/*****************************************************
 * Descripción: Contenido de mensaje para pagos no procesados y órdenes no generadas
 * Autor: Cristopher Noh
 * Fecha: 23/07/2020
 * Modificaciones:
 *****************************************************/
const PaymentError = () => {
    useEffect(() => {
        window.scrollTo(0, 0);
    }, []);

    return (
        <Grid container spacing={2}>
            <Grid item xs={12}>
                <IoIosCloseCircle className="pay-ord-error-icon" />
            </Grid>
            <Grid item xs={12}>
                <span className="pay-content-subtitle">ERROR EN EL PAGO.</span>
            </Grid>
            <Grid item xs={12}>
                <span className="price-content-description">
                    No pudimos procesar el pago de tu pedido, revisa nuevamente los datos ingresados o intenta con otra
                    tarjeta.
                </span>
            </Grid>
        </Grid>
    );
};

export default PaymentError;
