import { logoappleAvalible, logoplayAvalible } from "../configuration/imgConfig";

import React from "react";

const GetApp = () => {
    return (
        <div className="center">
            <p>
                <span className="price-footer-address">
                    Para utilizar el servicio, descarga la app “Meditoc 360” disponible en Appstore y Playstore.
                </span>
            </p>
            <span>
                <a
                    href="https://apps.apple.com/mx/app/meditoc-360/id1521078394"
                    target="_blank"
                    rel="noopener noreferrer"
                >
                    <img src={logoappleAvalible} alt="app-store" />
                </a>
            </span>
            <span style={{ marginLeft: 10 }}>
                <a
                    href="https://play.google.com/store/apps/details?id=com.meditoc.callCenter.comercial"
                    target="_blank"
                    rel="noopener noreferrer"
                >
                    <img src={logoplayAvalible} alt="play-store" />
                </a>
            </span>
        </div>
    );
};

export default GetApp;
