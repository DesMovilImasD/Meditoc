import PropTypes from "prop-types";
import React from "react";
import { Grid } from "@material-ui/core";
import { IoIosCheckmarkCircle } from "react-icons/io";

/*****************************************************
 * Descripción: Contenido de mensaje para pagos procesados y órdenes generadas
 * Autor: Cristopher Noh
 * Fecha: 23/07/2020
 * Modificaciones:
 *****************************************************/
const PaymentOk = (props) => {
    const { entOrder } = props;

    window.scrollTo(0, 0);

    return (
        <Grid container spacing={2}>
            <Grid item xs={12}>
                <IoIosCheckmarkCircle className="pay-ord-ok-icon" />
            </Grid>
            <Grid item xs={12}>
                <span className="pay-content-subtitle">
                    GRACIAS POR TU COMPRA. <br /> TU PAGO HA SIDO PROCESADO CORRECTAMENTE.
                </span>
            </Grid>
            <Grid item xs={12}>
                <span className="price-content-description">
                    Tu número de orden es:&nbsp;
                    <strong>{entOrder.sOrden}</strong>.
                </span>
            </Grid>
            <Grid item xs={12}>
                <span className="price-content-description">
                    Recibirás un correo electrónico con los detalles de tu pedido y las credenciales de acceso, si no lo
                    recibes favor de comunicarte con nosotros enviando un correo electrónico a prueba@correo para
                    brindarte soporte.
                </span>
            </Grid>
        </Grid>
    );
};

PaymentOk.propTypes = {
    entOrder: PropTypes.shape({
        sOrden: PropTypes.string,
    }),
};

export default PaymentOk;
