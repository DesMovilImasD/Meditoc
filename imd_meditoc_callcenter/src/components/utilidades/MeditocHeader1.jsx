import { Paper } from "@material-ui/core";
import PropTypes from "prop-types";
import React from "react";

/*************************************************************
 * Descripcion: Contiene la estructura y diseño de la Barra de herramientas y trabajo del portal Meditoc (debajo de la Barra principal)
 * Creado: Cristopher Noh
 * Fecha: 26/08/2020
 * Invocado desde: Sistema, Perfiles
 *************************************************************/
const MeditocHeader1 = (props) => {
    const { children, title } = props;

    return (
        <Paper elevation={2}>
            <div className="bar-main">
                <div className="flx-grw-1 align-self-center">{children}</div>
                <div className="ops-nor bold size-25 align-self-center" style={{ marginTop: 10, marginBottom: 10 }}>
                    {typeof title === "string" ? title.toUpperCase() : title}
                </div>
            </div>
        </Paper>
    );
};

MeditocHeader1.propTypes = {
    children: PropTypes.any,
    title: PropTypes.any,
};

export default MeditocHeader1;
