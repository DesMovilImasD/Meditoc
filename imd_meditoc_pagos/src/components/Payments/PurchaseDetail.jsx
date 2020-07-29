import PropTypes from "prop-types";
import React, { useEffect } from "react";
import { Typography } from "@material-ui/core";
import Product from "./Product";
import PurchaseSummary from "./PurchaseSummary";

/*****************************************************
 * Descripción: Contiene la estructura para mostrar el detalle de
 * los artículos y el resumen de compra
 * Autor: Cristopher Noh
 * Fecha: 23/07/2020
 * Modificaciones:
 *****************************************************/
const PurchaseDetail = (props) => {
    const { productList, setProductList, entCoupon, totalPayment, setTotalPayment } = props;

    //Obtener los productos seleccionados a comprar
    const funcGetSavedProducts = () => {
        const savedProducts = sessionStorage.getItem("lstItems");

        if (savedProducts === null) {
            return;
        }

        let lstItems = JSON.parse(savedProducts);
        setProductList(lstItems);
    };

    useEffect(() => {
        funcGetSavedProducts();

        // eslint-disable-next-line
    }, []);

    return (
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
            <PurchaseSummary
                productList={productList}
                entCoupon={entCoupon}
                totalPayment={totalPayment}
                setTotalPayment={setTotalPayment}
            />
        </div>
    );
};

PurchaseDetail.propTypes = {
    entCoupon: PropTypes.object,
    productList: PropTypes.array,
    setProductList: PropTypes.func,
    setTotalPayment: PropTypes.func,
    totalPayment: PropTypes.number,
};

export default PurchaseDetail;