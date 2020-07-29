import React from "react";
import { Grid } from "@material-ui/core";
import { logoMeditocWhite } from "../configuration/imgConfig";

/*****************************************************
 * Descripción: Footer del sitio
 * Autor: Cristopher Noh
 * Fecha: 22/07/2020
 * Modificaciones:
 *****************************************************/
const Footer = () => {
    return (
        <div className="price-footer-container">
            <div className="price-footer">
                <Grid container spacing={2}>
                    <Grid item md={6} xs={12}>
                        <div>
                            <span className="price-footer-contact">Contáctanos</span>
                        </div>
                        <div>
                            <img alt="LOGOMEDITOCWHITE" src={logoMeditocWhite} />
                        </div>
                        <div>
                            <span className="price-footer-address">
                                Calle 17 #113, Col. Itzimná, 97100, Mérida, Yuc.
                            </span>
                        </div>
                        <div>
                            <span className="price-footer-address">Tel: 5551-003021</span>
                        </div>
                        <div>
                            <span className="price-footer-address">Mail: contacto@meditoc.com</span>
                        </div>
                    </Grid>
                    <Grid item md={6} xs={12}>
                        <iframe
                            className="price-footer-map"
                            title="Meditoc Location"
                            src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d3724.315693635686!2d-89.58824038507066!3d21.02005069345141!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x8f56777d3feca8a9%3A0x966957e4a035a499!2sMEDITOC%20%26%20HS!5e0!3m2!1ses-419!2smx!4v1595687909106!5m2!1ses-419!2smx"
                            frameBorder="0"
                            allowFullScreen=""
                            aria-hidden="false"
                            tabIndex="0"
                        />
                    </Grid>
                </Grid>
            </div>
        </div>
    );
};

export default Footer;
