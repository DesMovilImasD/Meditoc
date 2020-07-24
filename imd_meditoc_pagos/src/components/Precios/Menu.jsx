import React from "react";
import { logoMeditocMain } from "../../configuration/imgConfig";
import preciosPage from "../../configuration/baseConfig";

/*****************************************************
 * Descripción: Menu de la pagina de precios
 * Autor: Cristopher Noh
 * Fecha: 22/07/2020
 * Modificaciones:
 *****************************************************/
const Menu = () => {
    return (
        <div className="precios-menu-contenedor">
            <div className="precios-menu-logo-contenedor">
                <img
                    className="precios-menu-logo"
                    alt="MEDITOCLOGO"
                    src={logoMeditocMain}
                />
            </div>
            <div className="precios-menu-ul-contenedor">
                <ul className="precios-menu-ul">
                    <li className="precios-menu-li">
                        <a href="https://meditoc.com/">Inicio</a>
                    </li>
                    <li className="precios-menu-li">
                        <a href="https://meditoc.com/#quienes-somos">
                            Quiénes Somos
                        </a>
                    </li>
                    <li className="precios-menu-li">
                        <a href="https://meditoc.com/#meditoc-360">
                            Nuestras Soluciones
                        </a>
                    </li>
                    <li className="precios-menu-li precios-menu-active">
                        <a href={preciosPage}>Precios</a>
                    </li>
                    <li className="precios-menu-li">
                        <a href="https://meditoc.com/#contacto">Contacto</a>
                    </li>
                    <li className="precios-menu-li">
                        <a href="http://portalserviciomedico.com/web/">
                            Iniciar sesión
                        </a>
                    </li>
                </ul>
            </div>
        </div>
    );
};

export default Menu;
