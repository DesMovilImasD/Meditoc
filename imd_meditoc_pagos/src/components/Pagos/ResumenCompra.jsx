import PropTypes from "prop-types";
import React, { Fragment, useEffect, useState } from "react";
import { Grid, Button } from "@material-ui/core";
import { useHistory } from "react-router-dom";

/*****************************************************
 * Descripción: Contiene la estructura para visualizar los montos
 * del detalle de la compra
 * Autor: Cristopher Noh
 * Fecha: 23/07/2020
 * Modificaciones:
 *****************************************************/
const ResumenCompra = (props) => {
    const history = useHistory();

    const { listaProductos, entCupon, totalPagar, setTotalPagar } = props;

    const [subtotal, setSubtotal] = useState(0);

    const funcRecalcularMontos = () => {
        const subtotalVal = listaProductos.reduce(
            (total, articulo) => total + articulo.cantidad * articulo.precio,
            0
        );

        const montoCupon =
            entCupon === null ? 0 : entCupon.fnMontoDescuento / 100;

        let total = subtotalVal - montoCupon;

        setSubtotal(subtotalVal);
        setTotalPagar(total);
    };

    const handleClickIrProductos = () => {
        history.push("/precios");
    };

    useEffect(() => {
        funcRecalcularMontos();
        // eslint-disable-next-line
    }, [listaProductos, entCupon]);

    return (
        <div className="pagos-detalle-compra-total">
            {listaProductos.length === 0 ? (
                <Button
                    variant="contained"
                    color="secondary"
                    size="large"
                    fullWidth
                    onClick={handleClickIrProductos}
                >
                    IR A PRODUCTOS Y SERVICIOS MEDITOC
                </Button>
            ) : (
                <Grid
                    container
                    className="pagos-detalle-compra-total-contenedor"
                >
                    <Grid item xs={6} className="izquierda">
                        <span className="pagos-detalle-compra-subtotal-txt">
                            Subtotal
                        </span>
                    </Grid>
                    <Grid item xs={6} className="derecha">
                        <span className="pagos-detalle-compra-subtotal-txt">
                            ${subtotal.toFixed(2)}
                        </span>
                    </Grid>
                    {entCupon === null ? null : (
                        <Fragment>
                            <Grid item xs={6} className="izquierda">
                                <span className="pagos-detalle-compra-subtotal-txt">
                                    {entCupon.fsCodigo}
                                </span>
                            </Grid>
                            <Grid item xs={6} className="derecha">
                                <span className="pagos-detalle-compra-subtotal-txt">
                                    -$
                                    {(entCupon.fnMontoDescuento / 100).toFixed(
                                        2
                                    )}
                                </span>
                            </Grid>
                        </Fragment>
                    )}
                    <Grid item xs={6} className="izquierda">
                        <span className="pagos-detalle-compra-total-txt">
                            Total
                        </span>
                    </Grid>
                    <Grid item xs={6} className="derecha">
                        <span className="pagos-detalle-compra-total-txt">
                            ${totalPagar.toFixed(2)}
                        </span>
                    </Grid>
                </Grid>
            )}
        </div>
    );
};

ResumenCompra.propTypes = {
    entCupon: PropTypes.shape({
        fnMontoDescuento: PropTypes.number,
        fsCodigo: PropTypes.string,
    }),
    listaProductos: PropTypes.array,
    setTotalPagar: PropTypes.func,
    totalPagar: PropTypes.number,
};

export default ResumenCompra;
