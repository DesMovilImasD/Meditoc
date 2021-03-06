import { Hidden, IconButton } from "@material-ui/core";
import { MdAdd, MdRemove } from "react-icons/md";
import React, { useState } from "react";

import { FaTrash } from "react-icons/fa";
import InputWhite from "../Inputs/InputWhite";
import PropTypes from "prop-types";
import { useEffect } from "react";

/*****************************************************
 * Descripción: Contiene la estructura de visualización de un artículo
 * en el detalle de la compra
 * Autor: Cristopher Noh
 * Fecha: 23/07/2020
 * Modificaciones:
 *****************************************************/
const Product = (props) => {
    const { product, index, productList, setProductList } = props;

    const [qtyCapture, setQtyCapture] = useState(product.qty.toString());

    //Reducir cantidad a comprar
    const handleClickLess = () => {
        let products = [...productList];
        products[index].qty--;
        setQtyCapture(products[index].qty.toString());
        setProductList(products);
    };

    //Aumentar cantidad a comprar
    const handleClickMore = () => {
        let products = [...productList];
        products[index].qty++;
        setQtyCapture(products[index].qty.toString());
        setProductList(products);
    };

    //Eliminar producto de la lista de compra
    const handleClickRemove = () => {
        let products = [...productList];
        products.splice(index, 1);
        setProductList(products);
    };

    //Evento Change para capturar la cantidad
    const handleChangeQtyCapture = (e) => {
        if (isNaN(e.target.value)) {
            if (e.target.value !== "") {
                return;
            }
        }
        setQtyCapture(e.target.value);
    };

    //Evento blur para validar la cantidad ingresada
    const handleBlurQtyCapture = () => {
        let products = [...productList];

        if (qtyCapture === "" || isNaN(qtyCapture)) {
            products[index].qty = 1;
        } else {
            products[index].qty = parseInt(qtyCapture);

            if (products[index].qty < 1) {
                products[index].qty = 1;
            }
        }
        setQtyCapture(products[index].qty.toString());
        setProductList(products);
    };

    //Actualizar la cantidad de los productos del carrito al editar/eliminar otro producto
    useEffect(() => {
        setQtyCapture(productList[index].qty.toString());
        // eslint-disable-next-line
    }, [productList]);

    return (
        <div className="pay-purchase-detail-item">
            <Hidden only="xs">
                <div className="pay-purchase-detail-item-icon">
                    <i className="icon" dangerouslySetInnerHTML={{ __html: product.icon }} />
                </div>
            </Hidden>
            <div className="pay-purchase-detail-item-info">
                <div className="pay-purchase-detail-item-name">{product.name}</div>
                <div className="pay-purchase-detail-item-price">
                    ${product.price.toLocaleString("en-US", { minimumFractionDigits: 2 })}
                </div>
                <div className="pay-purchase-detail-item-qty">
                    Cantidad:{" "}
                    <IconButton size="medium" disabled={product.qty <= 1} onClick={handleClickLess}>
                        <MdRemove className="pay-purchase-detail-item-btn" />
                    </IconButton>{" "}
                    <InputWhite
                        id="custom-css-standard-input"
                        margin="dense"
                        style={{ width: 50, textAlign: "center" }}
                        inputProps={{ style: { textAlign: "center" } }}
                        value={qtyCapture}
                        onBlur={handleBlurQtyCapture}
                        onChange={handleChangeQtyCapture}
                        autoComplete="off"
                    />
                    <IconButton size="medium" onClick={handleClickMore}>
                        <MdAdd className="pay-purchase-detail-item-btn" />
                    </IconButton>
                </div>
            </div>
            <div className="pay-purchase-detail-item-remove">
                <IconButton onClick={handleClickRemove}>
                    <FaTrash className="pay-purchase-detail-item-btn" />
                </IconButton>
            </div>
        </div>
    );
};

Product.propTypes = {
    index: PropTypes.number,
    product: PropTypes.shape({
        icon: PropTypes.string,
        name: PropTypes.string,
        price: PropTypes.number,
        qty: PropTypes.number,
    }),
    productList: PropTypes.array,
    setProductList: PropTypes.func,
};

export default Product;
