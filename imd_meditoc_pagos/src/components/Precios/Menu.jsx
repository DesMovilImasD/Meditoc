import React from "react";

const Menu = () => {
    return (
        <div className="precios-menu-contenedor">
            <div className="precios-menu-logo-contenedor">
                <img
                    className="precios-menu-logo"
                    alt="MEDITOCLOGO"
                    src="/img/logo-meditoc-main.png"
                />
            </div>
            <div>
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
                        <a href="/">Precios</a>
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
