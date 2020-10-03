import React from "react";
const MeditocFooter = () => {
    return (
        <div className="price-footer-container">
            <div className="price-footer">
                <div>
                    <span className="price-footer-contact">
                        &copy;
                        {new Date().getFullYear()} Meditoc | Health Solutions | Soluciones para la salud
                    </span>
                </div>
            </div>
        </div>
    );
};

export default MeditocFooter;
