import PropTypes from "prop-types";
import React, { useEffect, Fragment } from "react";
import { Typography, Button, makeStyles } from "@material-ui/core";
import Product from "./Product";
import PurchaseSummary from "./PurchaseSummary";
import theme from "../../configuration/themeConfig";
import AddCoupon from "./AddCoupon";
import { useState } from "react";

const useStyles = makeStyles(() => ({
    button: {
        backgroundColor: "#fff",
        color: "#12b6cb",
        marginLeft: 20,
        marginRight: 70,
        [theme.breakpoints.down("sm")]: {
            marginRight: 20,
        },
    },
}));

/*****************************************************
 * Descripción: Contiene la estructura para mostrar el detalle de
 * los artículos y el resumen de compra
 * Autor: Cristopher Noh
 * Fecha: 23/07/2020
 * Modificaciones:
 *****************************************************/
const PurchaseDetail = (props) => {
    const {
        appInfo,
        productList,
        setProductList,
        entCoupon,
        setEntCoupon,
        totalPayment,
        setTotalPayment,
        setFormErrorMessage,
        funcLoader,
    } = props;

    const classes = useStyles();

    //Mostrar diálogo para ingresar cupón
    const [couponDialogOpen, setCouponDialogOpen] = useState(false);

    //Obtener los productos seleccionados a comprar
    const funcGetSavedProducts = () => {
        const savedProducts = sessionStorage.getItem("lstItems");

        if (savedProducts === null) {
            return;
        }

        let lstItems = JSON.parse(savedProducts);
        setProductList(lstItems);
    };

    //Evento Click agregar cupón
    const handleClickRemoveCoupon = () => {
        setEntCoupon(null);
        setFormErrorMessage("");
    };

    //Evento Click eliminar cupón
    const handleClickAddCoupon = () => {
        setCouponDialogOpen(true);
    };

    //Ejecutar funcGetSavedProducts al cargar el componente
    useEffect(() => {
        funcGetSavedProducts();

        // eslint-disable-next-line
    }, []);

    return (
        <Fragment>
            <div className="pay-purchase-detail-container">
                <div className="pay-purchase-detail-products">
                    {productList.length === 0 ? (
                        <Typography>"No hay artículos por mostrar."</Typography>
                    ) : (
                        productList.map((product, index) => (
                            <Product
                                key={index}
                                product={product}
                                index={index}
                                productList={productList}
                                setProductList={setProductList}
                            />
                        ))
                    )}
                </div>
                {entCoupon === null ? (
                    <Button
                        variant="contained"
                        className={classes.button}
                        disabled={productList.length === 0}
                        onClick={handleClickAddCoupon}
                    >
                        Agregar código de descuento
                    </Button>
                ) : (
                    <Button
                        variant="contained"
                        className={classes.button}
                        disabled={productList.length === 0}
                        onClick={handleClickRemoveCoupon}
                    >
                        Quitar código de descuento
                    </Button>
                )}
                <PurchaseSummary
                    appInfo={appInfo}
                    productList={productList}
                    entCoupon={entCoupon}
                    totalPayment={totalPayment}
                    setTotalPayment={setTotalPayment}
                />
            </div>
            <AddCoupon
                couponDialogOpen={couponDialogOpen}
                setCouponDialogOpen={setCouponDialogOpen}
                setEntCoupon={setEntCoupon}
                funcLoader={funcLoader}
            />
        </Fragment>
    );
};

PurchaseDetail.propTypes = {
    appInfo: PropTypes.object.isRequired,
    entCoupon: PropTypes.object,
    productList: PropTypes.array.isRequired,
    setProductList: PropTypes.func.isRequired,
    setTotalPayment: PropTypes.func.isRequired,
    totalPayment: PropTypes.number.isRequired,
};

export default PurchaseDetail;
