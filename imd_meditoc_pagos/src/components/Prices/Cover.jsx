import React from "react";
import { Grid } from "@material-ui/core";
import { logoMeditocCover } from "../../configuration/imgConfig";

/*****************************************************
 * Descripción: Portada de la pagina
 * Autor: Cristopher Noh
 * Fecha: 22/07/2020
 * Modificaciones:
 *****************************************************/
const Cover = () => {
    return (
        <div className="price-cover-container">
            <Grid container>
                <Grid item sm={5} xs={12}>
                    <img className="price-cover-img" alt="logoMeditocCover" src={logoMeditocCover} />
                </Grid>
                <Grid item md={7} xs={12} className="price-conver-caption-container">
                    <div>
                        <p>
                            <span className="price-conver-caption">
                                Adquiere tu membresía, <strong>siempre, en donde sea</strong>
                            </span>
                        </p>
                    </div>
                </Grid>
            </Grid>
        </div>
    );
};

export default Cover;
