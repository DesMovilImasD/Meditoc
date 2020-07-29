import React, { useState } from "react";
import { Grid, Radio, Button } from "@material-ui/core";
import { FaUser } from "react-icons/fa";
import { useHistory } from "react-router-dom";
import { productSixMonthMembership, productOneYearMembership } from "../../configuration/productConfig";
import { urlPayments } from "../../configuration/urlConfig";

/*****************************************************
 * Descripción: Contiene la estructura para mostrar
 * los precios de las membresías
 * Autor: Cristopher Noh
 * Fecha: 22/07/2020
 * Modificaciones:
 *****************************************************/
const Memberships = () => {
    const history = useHistory();

    //Guardar membresia seleccionada
    const [rdMembership, setRdMembership] = useState(productSixMonthMembership.id);

    //Evento change para cambio de membresía entre 6 y 12 meses
    const handleChangeMembership = (e) => {
        setRdMembership(parseInt(e.target.value));
    };

    //Contratar/comprar la membresía seleccionada (pasar al formulario de pago)
    const handleClickContract = () => {
        let lstItems = [];

        if (rdMembership === productSixMonthMembership.id) {
            lstItems.push(productSixMonthMembership);
        } else if (rdMembership === productOneYearMembership.id) {
            lstItems.push(productOneYearMembership);
        } else {
            return;
        }
        sessionStorage.setItem("lstItems", JSON.stringify(lstItems));
        history.push(urlPayments);
    };

    return (
        <div className="price-product-container">
            <div className="price-product-header">
                <div className="price-product-header-icon">
                    <FaUser />
                </div>
                <div className="price-product-header-title">Miembro</div>
            </div>
            <Grid container>
                <Grid item xs={6} className="price-product-divider">
                    <div className="price-product-amount">
                        ${productSixMonthMembership.price.toLocaleString("en-US")}
                    </div>
                    <div className="price-product-amount-description">{productSixMonthMembership.shortName}</div>
                    <div>
                        <Radio
                            name="rd-membership"
                            value={productSixMonthMembership.id}
                            color="primary"
                            checked={rdMembership === productSixMonthMembership.id}
                            onChange={handleChangeMembership}
                        />
                    </div>
                </Grid>
                <Grid item xs={6}>
                    <div className="price-product-amount">
                        ${productOneYearMembership.price.toLocaleString("en-US")}
                    </div>
                    <div className="price-product-amount-description">{productOneYearMembership.shortName}</div>
                    <div>
                        <Radio
                            name="rd-membership"
                            value={productOneYearMembership.id}
                            color="primary"
                            checked={rdMembership === productOneYearMembership.id}
                            onChange={handleChangeMembership}
                        />
                    </div>
                </Grid>
                <Grid item xs={12} className="price-product-description-container">
                    <span className="price-product-description">
                        Desde el primer día contará con servicio de orientación médica, nutricional y psicológica los
                        365 días del año para todos los miembros de su familia, empleados de empresas u hogar y sus
                        familias, el cual es simple de usar, tendrá respuesta inmediata y llamadas ilimitadas de
                        orientación médica.
                    </span>
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

export default Memberships;
