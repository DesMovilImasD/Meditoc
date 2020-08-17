import PropTypes from "prop-types";
import React, { Fragment, useEffect, useState } from "react";
import { Grid, Button, Typography } from "@material-ui/core";
import { useHistory } from "react-router-dom";
import { urlProducts } from "../../configuration/urlConfig";

/*****************************************************
 * Descripción: Contiene la estructura para visualizar los montos
 * del detalle de la compra
 * Autor: Cristopher Noh
 * Fecha: 23/07/2020
 * Modificaciones:
 *****************************************************/
const PurchaseSummary = (props) => {
    const { appInfo, productList, entCoupon, totalPayment, setTotalPayment } = props;

    const history = useHistory();

    //Guardar subtotal de la compra
    const [subtotal, setSubtotal] = useState(0);

    //Guardar IVA
    const [taxAmount, setTaxAmount] = useState(0);

    //Gurdaar descuento
    const [discountAmount, setDiscountAmount] = useState(0);

    //Actualizar montos cuando hay un cambio en el total a pagar
    const funcUpdateAmounts = () => {
        const subtotalAmount = productList.reduce((total, product) => total + product.qty * product.price, 0);

        let couponAmount = 0;
        if (entCoupon !== null) {
            switch (entCoupon.fiIdCuponCategoria) {
                case 1:
                    let discount = entCoupon.fnMontoDescuento / 100;
                    const maxDiscount = subtotalAmount * appInfo.nMaximoDescuento;

                    if (discount > maxDiscount) {
                        discount = maxDiscount;
                    }
                    couponAmount = discount;
                    break;
                case 2:
                    if (entCoupon.fnPorcentajeDescuento > appInfo.nMaximoDescuento) {
                        couponAmount = subtotalAmount * appInfo.nMaximoDescuento;
                    } else {
                        couponAmount = subtotalAmount * entCoupon.fnPorcentajeDescuento;
                    }
                    break;
                default:
                    couponAmount = 0;
                    break;
            }
        }

        const subTotalWithDiscount = subtotalAmount - couponAmount;

        const tax = subTotalWithDiscount * appInfo.nIVA;
        const total = subTotalWithDiscount + tax;

        setSubtotal(subtotalAmount);
        setTaxAmount(tax);
        setDiscountAmount(couponAmount);
        setTotalPayment(total);
    };

    //Regresar a la sección de productos
    const handleClickBackProducts = () => {
        history.push(urlProducts);
    };

    //Actualizar el monto a pagar cuando se agrega, aumenta o reduce un producto o se agrega/elimina un cupón
    useEffect(() => {
        funcUpdateAmounts();

        // eslint-disable-next-line
    }, [productList, entCoupon]);

    return (
        <div className="pay-purchase-detail-total">
            {productList.length === 0 ? (
                <Button variant="contained" color="secondary" size="large" fullWidth onClick={handleClickBackProducts}>
                    <Typography variant="caption">IR A PRODUCTOS Y SERVICIOS MEDITOC</Typography>
                </Button>
            ) : (
                <Grid container className="pay-purchase-detail-total-container">
                    <Grid item xs={6} className="left">
                        <span className="pay-purchase-detail-subtotal-text">Subtotal</span>
                    </Grid>
                    <Grid item xs={6} className="right">
                        <span className="pay-purchase-detail-subtotal-text">
                            ${subtotal.toLocaleString("en-US", { minimumFractionDigits: 2 })}
                        </span>
                    </Grid>
                    {entCoupon === null ? null : (
                        <Fragment>
                            <Grid item xs={6} className="left">
                                <span className="pay-purchase-detail-subtotal-text">{entCoupon.fsCodigo}</span>
                            </Grid>
                            <Grid item xs={6} className="right">
                                <span className="pay-purchase-detail-subtotal-text">
                                    -$
                                    {discountAmount.toLocaleString("en-US", {
                                        minimumFractionDigits: 2,
                                    })}
                                </span>
                            </Grid>
                        </Fragment>
                    )}
                    <Grid item xs={6} className="left">
                        <span className="pay-purchase-detail-subtotal-text">IVA ({appInfo.nIVA * 100}%)</span>
                    </Grid>
                    <Grid item xs={6} className="right">
                        <span className="pay-purchase-detail-subtotal-text">
                            +${taxAmount.toLocaleString("en-US", { minimumFractionDigits: 2 })}
                        </span>
                    </Grid>
                    <Grid item xs={6} className="left">
                        <span className="pay-purchase-detail-total-text">Total</span>
                    </Grid>
                    <Grid item xs={6} className="right">
                        <span className="pay-purchase-detail-total-text">
                            ${totalPayment.toLocaleString("en-US", { minimumFractionDigits: 2 })}
                        </span>
                    </Grid>
                </Grid>
            )}
        </div>
    );
};

PurchaseSummary.propTypes = {
    appInfo: PropTypes.object.isRequired,
    entCoupon: PropTypes.shape({
        fnMontoDescuento: PropTypes.number,
        fsCodigo: PropTypes.string,
    }),
    productList: PropTypes.array.isRequired,
    setTotalPayment: PropTypes.func.isRequired,
    totalPayment: PropTypes.number.isRequired,
};

export default PurchaseSummary;
