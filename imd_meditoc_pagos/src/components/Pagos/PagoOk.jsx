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
const PagoOk = (props) => {
    const { ordenGenerada } = props;
    window.scrollTo(0, 0);
    return (
        <Grid container spacing={2}>
            <Grid item xs={12}>
                <IoIosCheckmarkCircle className="pagos-orden-ok-icono" />
            </Grid>
            <Grid item xs={12}>
                <span className="pagos-contenido-subtitulo">
                    GRACIAS POR TU COMPRA. <br /> TU PAGO HA SIDO PROCESADO
                    CORRECTAMENTE.
                </span>
            </Grid>
            <Grid item xs={12}>
                <span className="precios-contenido-descripcion">
                    Tu número de orden es:&nbsp;
                    <strong>{ordenGenerada.sOrden}</strong>.
                </span>
            </Grid>
            <Grid item xs={12}>
                <span className="precios-contenido-descripcion">
                    Recibirás un correo electrónico con los detalles de tu
                    pedido y las credenciales de acceso, si no lo recibes favor
                    de comunicarte con nosotros enviando un correo electrónico a
                    prueba@correo para brindarte soporte.
                </span>
            </Grid>
        </Grid>
    );
};

PagoOk.propTypes = {
    ordenGenerada: PropTypes.shape({
        sOrden: PropTypes.string,
    }),
};

export default PagoOk;
