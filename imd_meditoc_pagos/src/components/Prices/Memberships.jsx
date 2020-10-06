import { Button, Grid } from "@material-ui/core";
import React, { useEffect, useState } from "react";

import { FaUser } from "react-icons/fa";
import MembershipProduct from "./MembershipProduct";
import PropTypes from "prop-types";
import { urlPayments } from "../../configuration/urlConfig";
import { useHistory } from "react-router-dom";

/*****************************************************
 * Descripción: Contiene la estructura para mostrar
 * los precios de las membresías
 * Autor: Cristopher Noh
 * Fecha: 22/07/2020
 * Modificaciones:
 *****************************************************/
const Memberships = (props) => {
    const { lstMembershipProducts } = props;

    const history = useHistory();

    //Guardar membresia seleccionada
    const [rdMembership, setRdMembership] = useState(0);

    //Guardar información de la membresía seleccionada
    const [membershipDescription, setMembershipDescription] = useState("");

    //Contratar/comprar la membresía seleccionada (pasar al formulario de pago)
    const handleClickContract = () => {
        let lstItems = lstMembershipProducts.filter((product) => product.id === rdMembership);

        sessionStorage.setItem("lstItems", JSON.stringify(lstItems));
        history.push(urlPayments);
    };

    //Marcar como seleccionado el primer producto de membresía al cargar el componente
    useEffect(() => {
        setRdMembership(lstMembershipProducts[0].id);
        setMembershipDescription(lstMembershipProducts[0].info);

        // eslint-disable-next-line
    }, []);

    return (
        <div className="price-product-container">
            <div className="price-product-header">
                <div className="price-product-header-icon">
                    <FaUser />
                </div>
                <div className="price-product-header-title">Membresía particular</div>
            </div>
            <Grid container>
                <Grid item xs={12}>
                    <div className="price-products-display">
                        {lstMembershipProducts.map((product, index) => (
                            <MembershipProduct
                                key={product.id}
                                product={product}
                                last={lstMembershipProducts.length === index + 1}
                                rdMembership={rdMembership}
                                setRdMembership={setRdMembership}
                                setMembershipDescription={setMembershipDescription}
                            />
                        ))}
                    </div>
                </Grid>
                <Grid item xs={12} className="price-product-description-container">
                    <span className="price-product-description">{membershipDescription}</span>
                </Grid>
            </Grid>
            <div className="price-product-btn">
                <Button variant="contained" color="primary" onClick={handleClickContract}>
                    CONTRATAR
                </Button>
            </div>
        </div>
    );
};

Memberships.propTypes = {
    lstMembershipProducts: PropTypes.array,
};

export default Memberships;
