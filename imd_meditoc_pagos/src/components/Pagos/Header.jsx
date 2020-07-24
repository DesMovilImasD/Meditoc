import React from "react";
import { logoSoluciones } from "../../configuration/imgConfig";

/*****************************************************
 * Descripción: Header con el logo de Meditoc en la sección de pagos
 * Autor: Cristopher Noh
 * Fecha: 23/07/2020
 * Modificaciones:
 *****************************************************/
const Header = () => {
    return (
        <div className="pagos-header">
            <img
                className="pagos-header-logo"
                alt="LOGOSOLUCIONES"
                src={logoSoluciones}
            />
        </div>
    );
};

export default Header;
