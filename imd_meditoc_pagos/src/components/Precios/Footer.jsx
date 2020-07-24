import React from "react";
import { Grid, Hidden } from "@material-ui/core";
import { logoMeditocWhite } from "../../configuration/imgConfig";

/*****************************************************
 * Descripción: Footer del sitio
 * Autor: Cristopher Noh
 * Fecha: 22/07/2020
 * Modificaciones:
 *****************************************************/
const Footer = () => {
    return (
        <div className="precios-footer-contenedor">
            <div className="precios-footer-sub">
                <Grid container>
                    <Grid item md={6} xs={12}>
                        <div>
                            <span className="precios-footer-contacto">
                                Contáctanos
                            </span>
                        </div>
                        <div>
                            <img
                                alt="LOGOMEDITOCWHITE"
                                src={logoMeditocWhite}
                            />
                        </div>
                        <div>
                            <span className="precios-footer-direccion">
                                Calle 17 #113, Col. Itzimná, 97100, Mérida, Yuc.
                            </span>
                        </div>
                        <div>
                            <span className="precios-footer-direccion">
                                Tel: 5551-003021
                            </span>
                        </div>
                        <div>
                            <span className="precios-footer-direccion">
                                Mail: contacto@meditoc.com
                            </span>
                        </div>
                    </Grid>
                    <Hidden only={["xs", "sm"]}>
                        <Grid item sm={6} xs={12}>
                            <div className="precios-footer-map"></div>
                        </Grid>
                    </Hidden>
                </Grid>
            </div>
        </div>
    );
};

export default Footer;
