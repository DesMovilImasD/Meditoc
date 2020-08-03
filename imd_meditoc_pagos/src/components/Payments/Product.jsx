import PropTypes from "prop-types";
import React from "react";
import { FaTrash } from "react-icons/fa";
import { IconButton, Hidden } from "@material-ui/core";
import { MdAdd, MdRemove } from "react-icons/md";

/*****************************************************
 * Descripción: Contiene la estructura de visualización de un artículo
 * en el detalle de la compra
 * Autor: Cristopher Noh
 * Fecha: 23/07/2020
 * Modificaciones:
 *****************************************************/
const Product = (props) => {
    const { product, index, productList, setProductList } = props;

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
    index: PropTypes.number.isRequired,
    product: PropTypes.shape({
        icon: PropTypes.string.isRequired,
        name: PropTypes.string.isRequired,
        price: PropTypes.number.isRequired,
        qty: PropTypes.number.isRequired,
    }),
    productList: PropTypes.array.isRequired,
    setProductList: PropTypes.func.isRequired,
};

export default Product;
