import React from "react";
import { Grid } from "@material-ui/core";
import { IoIosCloseCircle } from "react-icons/io";

/*****************************************************
 * Descripción: Contenido de mensaje para pagos no procesados y órdenes no generadas
 * Autor: Cristopher Noh
 * Fecha: 23/07/2020
 * Modificaciones:
 *****************************************************/
const PagoError = () => {
    window.scrollTo(0, 0);
    return (
        <Grid container spacing={2}>
            <Grid item xs={12}>
                <IoIosCloseCircle className="pagos-orden-error-icono" />
            </Grid>
            <Grid item xs={12}>
                <span className="pagos-contenido-subtitulo">
                    ERROR EN EL PAGO.
                </span>
            </Grid>
            <Grid item xs={12}>
                <span className="precios-contenido-descripcion">
                    No pudimos procesar el pago de tu pedido, revisa nuevamente
                    los datos ingresados o intenta con otra tarjeta.
                </span>
            </Grid>
        </Grid>
    );
};

export default PagoError;
