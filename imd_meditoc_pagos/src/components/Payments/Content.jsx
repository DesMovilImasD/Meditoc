import PropTypes from "prop-types";
import React, { useState, useEffect } from "react";
import { Grid, Hidden } from "@material-ui/core";
import PurchaseDetail from "./PurchaseDetail";
import PaymentForm from "./PaymentForm";
import PaymentOk from "./PaymentOk";
import PaymentError from "./PaymentError";

/*****************************************************
 * Descripción: Contiene la estructura de contenido para el
 * formulario de pagos y mensaje de orden ok y orden error
 * Autor: Cristopher Noh
 * Fecha: 23/07/2020
 * Modificaciones:
 *****************************************************/
const Content = (props) => {
    const { appInfo, funcLoader } = props;

    //Lista de productos a comprar/contratar
    const [productList, setProductList] = useState([]);

    //Entidad de cupón
    const [entCoupon, setEntCoupon] = useState(null);

    //Total a pagar
    const [totalPayment, setTotalPayment] = useState(0);

    //Entidad de orden generada
    const [entOrder, setEntOrder] = useState(null);

    //Error al generar la orden
    const [errorOrder, setErrorOrder] = useState(true);

    //Lista de diferimientos de pagos aplicables al monto de compra
    const [monthlyPayments, setMonthlyPayments] = useState([]);

    //Actualizar lista de diferimiento de pagos cuando se modifica el total
    const funcCalcMonthlyPayments = () => {
        let monthlyPaymentsPreview = [];

        if (totalPayment >= 300) {
            monthlyPaymentsPreview.push({
                value: "3",
                label: "3 meses sin intereses",
            });
        }

        if (totalPayment >= 600) {
            monthlyPaymentsPreview.push({
                value: "6",
                label: "6 meses sin intereses",
            });
        }

        if (totalPayment >= 900) {
            monthlyPaymentsPreview.push({
                value: "9",
                label: "9 meses sin intereses",
            });
        }

        if (totalPayment >= 1200) {
            monthlyPaymentsPreview.push({
                value: "12",
                label: "12 meses sin intereses",
            });
        }

        setMonthlyPayments(monthlyPaymentsPreview);
    };

    //Ejecutar funcCalcMonthlyPayments cada vex que se actualice el monto a pagar
    useEffect(() => {
        funcCalcMonthlyPayments();

        // eslint-disable-next-line
    }, [totalPayment]);

    return (
        <div className="pay-content center">
            <div className="pay-order-ok-margin">
                {entOrder === null ? (
                    <Grid container>
                        <Grid item xs={12} style={{ marginBottom: 50 }}>
                            <span className="price-content-description">
                                Para generar sus credenciales de acceso a Meditoc haga el pago del servicio por medio de
                                una tarjeta de crédito o débito.
                            </span>
                        </Grid>
                        <Grid item md={6} xs={12}>
                            <span className="pay-content-subtitle">Detalle de compra</span>
                        </Grid>
                        <Hidden only={["sm", "xs"]}>
                            <Grid item md={6} xs={12}>
                                <span className="pay-content-subtitle">Información de pago</span>
                            </Grid>
                        </Hidden>
                        <Grid item md={6} xs={12}>
                            <PurchaseDetail
                                appInfo={appInfo}
                                productList={productList}
                                setProductList={setProductList}
                                entCoupon={entCoupon}
                                totalPayment={totalPayment}
                                setTotalPayment={setTotalPayment}
                            />
                        </Grid>
                        <Hidden only={["md", "lg", "xl"]}>
                            <Grid item md={6} xs={12}>
                                <span className="pay-content-subtitle">Información de pago</span>
                            </Grid>
                        </Hidden>
                        <Grid item md={6} xs={12}>
                            <PaymentForm
                                appInfo={appInfo}
                                productList={productList}
                                monthlyPayments={monthlyPayments}
                                totalPayment={totalPayment}
                                funcLoader={funcLoader}
                                entCoupon={entCoupon}
                                setEntCoupon={setEntCoupon}
                                setEntOrder={setEntOrder}
                                setErrorOrder={setErrorOrder}
                            />
                        </Grid>
                    </Grid>
                ) : errorOrder === false ? (
                    <PaymentOk appInfo={appInfo} entOrder={entOrder} />
                ) : (
                    <PaymentError />
                )}
            </div>
        </div>
    );
};

Content.propTypes = {
    appInfo: PropTypes.object.isRequired,
    funcLoader: PropTypes.func.isRequired,
};

export default Content;
