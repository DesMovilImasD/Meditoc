import PropTypes from "prop-types";
import React from "react";
import { Grid } from "@material-ui/core";
import { logoMeditocWhite } from "../configuration/imgConfig";



/*****************************************************
 * Descripción: Footer del sitio
 * Autor: Cristopher Noh
 * Fecha: 22/07/2020
 * Modificaciones:
 *****************************************************/
const Footer = (props) => {
    const { appInfo } = props;

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
                            <span className="price-footer-address">{appInfo.sDireccionEmpresa}</span>
                        </div>
                        <div>
                            <span className="price-footer-address">Tel: {appInfo.sTelefonoEmpresa}</span>
                        </div>
                        <div>
                            <span className="price-footer-address">Mail: {appInfo.sCorreoContacto}</span>
                        </div>                        
                    </Grid>
                    <Grid item md={6} xs={12}>
                        <iframe
                            className="price-footer-map"
                            title="Meditoc Location"
                            src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d3724.958440195036!2d-89.61451628507096!3d20.994302994329722!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x8f56714c5cf1af03%3A0x3d073af1dacc236a!2sCalle%2017%20113%2C%20Itzimn%C3%A1%2C%2097100%20M%C3%A9rida%2C%20Yuc.!5e0!3m2!1ses-419!2smx!4v1598395529016!5m2!1ses-419!2smx"
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

Footer.propTypes = {
    appInfo: PropTypes.object.isRequired,
};

export default Footer;
