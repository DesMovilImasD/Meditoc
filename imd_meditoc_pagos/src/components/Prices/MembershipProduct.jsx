import PropTypes from "prop-types";
import React from "react";
import { Radio } from "@material-ui/core";

/*****************************************************
 * Descripción: Representa un producto tipo membresía
 * Autor: Cristopher Noh
 * Fecha: 29/07/2020
 * Modificaciones:
 *****************************************************/
const MembershipProduct = (props) => {
    const { product, last, rdMembership, setRdMembership, setMembershipDescription } = props;

    //Clase para mostrar el serparador
    const dividerClass = last ? "" : " price-product-divider";

    //Evento change para cambio de membresía entre 6 y 12 meses
    const handleChangeMembership = (e) => {
        setRdMembership(parseInt(e.target.value));
        setMembershipDescription(product.info);
    };

    return (
        <div className={"price-product-large-display" + dividerClass}>
            <div className="price-product-amount">${product.price.toLocaleString("en-US")}</div>
            <div className="price-product-amount-description">{product.shortName}</div>
            <div>
                <Radio
                    name="rd-membership"
                    value={product.id}
                    color="primary"
                    checked={rdMembership === product.id}
                    onChange={handleChangeMembership}
                />
            </div>
        </div>
    );
};

MembershipProduct.propTypes = {
    last: PropTypes.bool,
    product: PropTypes.shape({
        id: PropTypes.number,
        info: PropTypes.string,
        price: PropTypes.number,
        shortName: PropTypes.string,
    }),
    rdMembership: PropTypes.number,
    setMembershipDescription: PropTypes.func,
    setRdMembership: PropTypes.func,
};

export default MembershipProduct;
