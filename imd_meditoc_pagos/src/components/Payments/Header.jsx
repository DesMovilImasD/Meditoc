import React from "react";
import { logoMeditocSolutions } from "../../configuration/imgConfig";

/*****************************************************
 * Descripción: Header con el logo de Meditoc en la sección de pagos
 * Autor: Cristopher Noh
 * Fecha: 23/07/2020
 * Modificaciones:
 *****************************************************/
const Header = () => {
    return (
        <div className="pay-header">
            <img className="pay-header-logo" alt="logo-solutions" src={logoMeditocSolutions} />
        </div>
    );
};

export default Header;
