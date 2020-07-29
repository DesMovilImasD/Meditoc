import React from "react";
import { logoMeditocMain } from "../../configuration/imgConfig";
import { urlBase } from "../../configuration/urlConfig";

/*****************************************************
 * Descripción: Menu de la pagina de precios
 * Autor: Cristopher Noh
 * Fecha: 22/07/2020
 * Modificaciones:
 *****************************************************/
const Menu = () => {
    return (
        <div className="price-menu-container">
            <div className="price-menu-logo-container">
                <img className="price-menu-logo" alt="MEDITOCLOGO" src={logoMeditocMain} />
            </div>
            <div className="price-menu-ul-container">
                <ul className="price-menu-ul">
                    <li className="price-menu-li">
                        <a href="https://meditoc.com/">Inicio</a>
                    </li>
                    <li className="price-menu-li">
                        <a href="https://meditoc.com/#quienes-somos">Quiénes Somos</a>
                    </li>
                    <li className="price-menu-li">
                        <a href="https://meditoc.com/#meditoc-360">Nuestras Soluciones</a>
                    </li>
                    <li className="price-menu-li price-menu-active">
                        <a href={urlBase}>Precios</a>
                    </li>
                    <li className="price-menu-li">
                        <a href="https://meditoc.com/#contacto">Contacto</a>
                    </li>
                    <li className="price-menu-li">
                        <a href="http://portalserviciomedico.com/web/">Iniciar sesión</a>
                    </li>
                </ul>
            </div>
        </div>
    );
};

export default Menu;
