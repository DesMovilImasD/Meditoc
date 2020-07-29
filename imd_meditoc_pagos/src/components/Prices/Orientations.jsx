import React, { useState } from "react";
import { Grid, Radio, Button } from "@material-ui/core";
import { FaClinicMedical, FaBriefcaseMedical, FaCommentMedical, FaNotesMedical } from "react-icons/fa";
import { useHistory } from "react-router-dom";
import {
    productOrientationPrice,
    productMedicalOrientation,
    productPsychologicalOrientation,
    productNutritionalOrientation,
} from "../../configuration/productConfig";
import { urlPayments } from "../../configuration/urlConfig";

/*****************************************************
 * Descripción: Contiene la estructura para mostrar
 * los precios de las orientaciones
 * Autor: Cristopher Noh
 * Fecha: 23/07/2020
 * Modificaciones:
 *****************************************************/
const Orientations = () => {
    const history = useHistory();

    //Guardar orientaciones seleccionadas
    const [rdOrientation, setRdOrientation] = useState({
        medical: true,
        psychological: false,
        nutritional: false,
    });

    //Comprar las orientaciones seleccionadas (pasar al formulario de pago)
    const handleClickBuy = () => {
        let lstItems = [];

        if (rdOrientation.medical) {
            lstItems.push(productMedicalOrientation);
        }
        if (rdOrientation.psychological) {
            lstItems.push(productPsychologicalOrientation);
        }
        if (rdOrientation.nutritional) {
            lstItems.push(productNutritionalOrientation);
        }
        if (lstItems.length === 0) {
            return;
        }
        sessionStorage.setItem("lstItems", JSON.stringify(lstItems));
        history.push(urlPayments);
    };

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
                    <div className="price-product-amount">${productOrientationPrice.toLocaleString("en-US")}</div>
                    <div className="price-product-amount-description price-product-space">por orientación</div>
                </Grid>
                <Grid item xs={4} className="price-product-divider">
                    <div className="price-product-icon">
                        <FaBriefcaseMedical />
                    </div>
                    <div className="price-product-amount-description">{productMedicalOrientation.shortName}</div>
                    <div>
                        <Radio
                            name="rd-orientation"
                            value="medical"
                            color="primary"
                            checked={rdOrientation.medical === true}
                            onClick={() =>
                                setRdOrientation({
                                    ...rdOrientation,
                                    medical: !rdOrientation.medical,
                                })
                            }
                        />
                    </div>
                </Grid>
                <Grid item xs={4} className="price-product-divider">
                    <div className="price-product-icon">
                        <FaCommentMedical />
                    </div>
                    <div className="price-product-amount-description">{productPsychologicalOrientation.shortName}</div>
                    <div>
                        <Radio
                            name="rd-orientation"
                            value="psychological"
                            color="primary"
                            checked={rdOrientation.psychological}
                            onClick={() =>
                                setRdOrientation({
                                    ...rdOrientation,
                                    psychological: !rdOrientation.psychological,
                                })
                            }
                        />
                    </div>
                </Grid>
                <Grid item xs={4}>
                    <div className="price-product-icon">
                        <FaNotesMedical />
                    </div>
                    <div className="price-product-amount-description">{productNutritionalOrientation.shortName}</div>
                    <div>
                        <Radio
                            name="rd-orientation"
                            value="nutritional"
                            color="primary"
                            checked={rdOrientation.nutritional}
                            onClick={() =>
                                setRdOrientation({
                                    ...rdOrientation,
                                    nutritional: !rdOrientation.nutritional,
                                })
                            }
                        />
                    </div>
                </Grid>
                <Grid item xs={12} className="price-product-description-container">
                    <span className="price-product-description">
                        Contará con un servicio de orientación médica, nutricional o psicológica válido por 24 horas, el
                        cual le dará acceso a tener una llamada telefónica, chat y videollamada.
                    </span>
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

export default Orientations;
