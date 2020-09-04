import React from "react";

/*************************************************************
 * Descripcion: Contiene la estructura y diseño de la Barra de herramientas y trabajo del portal Meditoc (debajo de la Barra principal)
 * Creado: Cristopher Noh
 * Fecha: 26/08/2020
 * Invocado desde: Sistema, Perfiles
 *************************************************************/
const SubmoduloBarra = (props) => {
    const { children, title } = props;

    return (
        <div className="bar-main">
            <div className="flx-grw-1">{children}</div>
            <div className="ops-nor bold size-30 align-self-center">{title}</div>
        </div>
    );
};

export default SubmoduloBarra;
