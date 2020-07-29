import PropTypes from "prop-types";
import React, { useState, useEffect } from "react";
import { FaBriefcaseMedical, FaTrash } from "react-icons/fa";
import { IconButton, Hidden } from "@material-ui/core";
import { MdAdd, MdRemove } from "react-icons/md";
import {
    productSixMonthMembership,
    productOneYearMembership,
    productMedicalOrientation,
    productPsychologicalOrientation,
    productNutritionalOrientation,
} from "../../configuration/productConfig";

/*****************************************************
 * Descripción: Contiene la estructura de visualización de un artículo
 * en el detalle de la compra
 * Autor: Cristopher Noh
 * Fecha: 23/07/2020
 * Modificaciones:
 *****************************************************/
const Product = (props) => {
    const { product, index, productList, setProductList } = props;

    //Icono del producto a comprar
    const [productIcon, setProductIcon] = useState(<FaBriefcaseMedical />);

    //Mostrar icono del producto (***mejorar esta sección)
    const funcProductIcon = () => {
        switch (product.id) {
            case productSixMonthMembership.id:
                setProductIcon(productSixMonthMembership.icon);
                break;

            case productOneYearMembership.id:
                setProductIcon(productOneYearMembership.icon);
                break;

            case productMedicalOrientation.id:
                setProductIcon(productMedicalOrientation.icon);
                break;

            case productPsychologicalOrientation.id:
                setProductIcon(productPsychologicalOrientation.icon);
                break;

            case productNutritionalOrientation.id:
                setProductIcon(productNutritionalOrientation.icon);
                break;

            default:
                setProductIcon(<FaBriefcaseMedical />);
                break;
        }
    };

    //Reducir cantidad a comprar
    const handleClickLess = () => {
        let products = [...productList];
        products[index].qty--;
        setProductList(products);
    };

    //Aumentar cantidad a comprar
    const handleClickMore = () => {
        let products = [...productList];
        products[index].qty++;
        setProductList(products);
    };

    //Eliminar producto de la lista de compra
    const handleClickRemove = () => {
        let products = [...productList];
        products.splice(index, 1);
        setProductList(products);
    };

    useEffect(() => {
        funcProductIcon();

        // eslint-disable-next-line
    }, []);

    return (
        <div className="pay-purchase-detail-item">
            <Hidden only="xs">
                <div className="pay-purchase-detail-item-icon">{productIcon}</div>
            </Hidden>
            <div className="pay-purchase-detail-item-info">
                <div className="pay-purchase-detail-item-name">{product.name}</div>
                <div className="pay-purchase-detail-item-price">
                    ${product.price.toLocaleString("en-US", { minimumFractionDigits: 2 })}
                </div>
                <div className="pay-purchase-detail-item-qty">
                    Cantidad:&nbsp;
                    <IconButton size="medium" disabled={product.qty <= 1} onClick={handleClickLess}>
                        <MdRemove className="pay-purchase-detail-item-btn" />
                    </IconButton>
                    &nbsp; {product.qty} &nbsp;
                    <IconButton size="medium" disabled={product.qty >= 10} onClick={handleClickMore}>
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
    productList: PropTypes.array,
    product: PropTypes.shape({
        qty: PropTypes.number,
        id: PropTypes.number,
        name: PropTypes.string,
        price: PropTypes.number,
    }),
    setProductList: PropTypes.func,
};

export default Product;
