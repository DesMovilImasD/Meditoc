import React from "react";
import { Grid } from "@material-ui/core";

const Footer = () => {
    return (
        <div className="precios-footer-contenedor">
            <div className="precios-footer-sub">
                <Grid container>
                    <Grid item sm={6} xs={12}>
                        <div>
                            <span className="precios-footer-contacto">
                                Contáctanos
                            </span>
                        </div>
                        <div>
                            <img
                                alt="LOGOMEDITOCWHITE"
                                src="/img/logo-meditoc-white.png"
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
                    <Grid item sm={6} xs={12}>
                        <div className="precios-footer-map"></div>
                    </Grid>
                </Grid>
            </div>
        </div>
    );
};

export default Footer;
