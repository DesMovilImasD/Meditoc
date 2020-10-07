import PropTypes from "prop-types";
import { Radio } from "@material-ui/core";
import React from "react";

/*****************************************************
 * Descripción: Representa un producto tipo servicio/orientación
 * Autor: Cristopher Noh
 * Fecha: 29/07/2020
 * Modificaciones:
 *****************************************************/
const OrientationProduct = (props) => {
    const { product, index, lstOrientationProducts, setLstOrientationProducts, setOrientationDescription } = props;

    //Clase para mostrar el serparador
    const dividerClass = lstOrientationProducts.length === index + 1 ? "" : " price-product-divider";

    //Evento click para seleccionar el producto
    const handleClickRdOrientation = () => {
        let orientationProducts = [...lstOrientationProducts];

        orientationProducts[index].selected = !product.selected;

        if (orientationProducts[index].selected === true) {
            setOrientationDescription(product.info);
        }

        setLstOrientationProducts(orientationProducts);
    };

    return (
        <div className={"price-product-display" + dividerClass}>
            <div className="price-product-amount">
                ${product.price.toLocaleString("en-US", { minimumFractionDigits: product.price % 1 !== 0 ? 2 : 0 })}
            </div>
            <div className="price-product-amount-description">{product.shortName}</div>
            <div className="price-product-icon">
                <i className="icon" dangerouslySetInnerHTML={{ __html: product.icon }} />
            </div>
            <div>
                <Radio
                    name="rd-orientation"
                    value="medical"
                    color="primary"
                    checked={product.selected}
                    onClick={handleClickRdOrientation}
                />
            </div>
        </div>
    );
};

OrientationProduct.propTypes = {
    index: PropTypes.number,
    lstOrientationProducts: PropTypes.array,
    product: PropTypes.shape({
        icon: PropTypes.string,
        info: PropTypes.string,
        price: PropTypes.number,
        selected: PropTypes.bool,
        shortName: PropTypes.string,
    }),
    setLstOrientationProducts: PropTypes.func,
    setOrientationDescription: PropTypes.func,
};

export default OrientationProduct;
