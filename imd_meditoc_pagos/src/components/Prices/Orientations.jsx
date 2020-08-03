import PropTypes from "prop-types";
import React, { useState, useEffect } from "react";
import { Grid, Button } from "@material-ui/core";
import { FaClinicMedical } from "react-icons/fa";
import { useHistory } from "react-router-dom";
import { urlPayments } from "../../configuration/urlConfig";
import OrientationProduct from "./OrientationProduct";

/*****************************************************
 * Descripción: Contiene la estructura para mostrar
 * los precios de las orientaciones
 * Autor: Cristopher Noh
 * Fecha: 23/07/2020
 * Modificaciones:
 *****************************************************/
const Orientations = (props) => {
    const { lstOrientationProducts, setLstOrientationProducts } = props;

    const history = useHistory();

    //Guardar descripción de la orientación seleccionada
    const [orientationDescription, setOrientationDescription] = useState("");

    //Comprar las orientaciones seleccionadas (pasar al formulario de pago)
    const handleClickBuy = () => {
        let lstItems = lstOrientationProducts.filter((product) => product.selected === true);

        if (lstItems.length === 0) {
            return;
        }

        sessionStorage.setItem("lstItems", JSON.stringify(lstItems));

        history.push(urlPayments);
    };

    //Marcar como seleccionado la primera orientación al cargar el componente
    useEffect(() => {
        const orientationProducts = [...lstOrientationProducts];
        orientationProducts[0].selected = true;

        setOrientationDescription(orientationProducts[0].info);
        setLstOrientationProducts(orientationProducts);

        // eslint-disable-next-line
    }, []);

    return (
        <div className="price-product-container">
            <div className="price-product-header">
                <div className="price-product-header-icon">
                    <FaClinicMedical />
                </div>
                <div className="price-product-header-title">Orientaciones</div>
            </div>
            <Grid container>
                <Grid item xs={12}>
                    <div className="price-products-display">
                        {lstOrientationProducts.map((product, index) => (
                            <OrientationProduct
                                key={product.id}
                                product={product}
                                index={index}
                                lstOrientationProducts={lstOrientationProducts}
                                setLstOrientationProducts={setLstOrientationProducts}
                                setOrientationDescription={setOrientationDescription}
                            />
                        ))}
                    </div>
                </Grid>
                <Grid item xs={12} className="price-product-description-container">
                    <span className="price-product-description">{orientationDescription}</span>
                </Grid>
                <Grid item xs={12}></Grid>
            </Grid>
            <div className="price-product-btn">
                <Button variant="contained" color="primary" onClick={handleClickBuy}>
                    COMPRAR
                </Button>
            </div>
        </div>
    );
};

Orientations.propTypes = {
    lstOrientationProducts: PropTypes.array.isRequired,
    setLstOrientationProducts: PropTypes.func.isRequired,
};

export default Orientations;
