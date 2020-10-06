import { Grid } from "@material-ui/core";
import React from "react";
import { logoMeditocDoctores } from "../../configuration/imgConfig";

/*****************************************************
 * Descripción: Header del directorio médico
 * Autor: Cristopher Noh
 * Fecha: 07/09/2020
 * Modificaciones:
 *****************************************************/
const DirectoryHeader = () => {
    return (
        <div className="directory-cover-container">
            <Grid container spacing={0}>
                <Grid item md={8} sm={6} xs={12} className="directory-header-description-container">
                    <span className="directory-header-description">DIRECTORIO MÉDICO</span>
                </Grid>
                <Grid item md={4} sm={6} xs={12}>
                    <img src={logoMeditocDoctores} alt="LOGOMEDITOCDOCTORES" className="directory-header-logo" />
                </Grid>
            </Grid>
        </div>
    );
};

export default DirectoryHeader;
