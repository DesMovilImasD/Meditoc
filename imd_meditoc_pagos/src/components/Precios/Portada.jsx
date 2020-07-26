import React from "react";
import { Grid } from "@material-ui/core";
import { portadaMeditoc } from "../../configuration/imgConfig";

/*****************************************************
 * Descripción: Portada de la pagina
 * Autor: Cristopher Noh
 * Fecha: 22/07/2020
 * Modificaciones:
 *****************************************************/
const Portada = () => {
    return (
        <div className="precios-portada-contenedor">
            <Grid container>
                <Grid item sm={5} xs={12}>
                    <img
                        className="precios-portada-img"
                        alt="MEDITOCPORTADALOGO"
                        src={portadaMeditoc}
                    />
                </Grid>
                <Grid
                    item
                    md={7}
                    xs={12}
                    className="precio-portada-leyenda-contenedor"
                >
                    <div>
                        <p>
                            <span className="precios-portada-leyenda">
                                Adquiere tu membresía,&nbsp;
                                <strong>siempre, en donde sea</strong>
                            </span>
                        </p>
                    </div>
                </Grid>
            </Grid>
        </div>
    );
};

export default Portada;
