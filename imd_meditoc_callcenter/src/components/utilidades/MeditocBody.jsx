import PropTypes from "prop-types";
import React from "react";

/*************************************************************
 * Descripcion: Contiene la estructura y diseño del workspace del portal de Meditoc
 * Creado: Cristopher Noh
 * Fecha: 26/08/2020
 * Invocado desde: Sistema, Perfiles
 *************************************************************/
const MeditocBody = (props) => {
    const { children } = props;
    return <div className="bar-content">{children}</div>;
};

MeditocBody.propTypes = {
    children: PropTypes.any,
};

export default MeditocBody;
