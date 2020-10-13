import AccountTreeIcon from "@material-ui/icons/AccountTree";
import ExtensionIcon from "@material-ui/icons/Extension";
import MeditocHelper from "../../../utilidades/MeditocHelper";
import React from "react";
import WebIcon from "@material-ui/icons/Web";

/*************************************************************
 * Descripcion: Boton de ayuda para mostrar la simbología de los
 * componentes (Módulo, Submódulo y Botón) para el adminsitrador de CGU
 * Creado: Cristopher Noh
 * Fecha: 26/08/2020
 * Invocado desde: Sistema, Permisos
 *************************************************************/
const Simbologia = () => {
    return (
        <MeditocHelper title="Simbología:">
            <div>
                <AccountTreeIcon className={"color-1 vertical-align-middle"} />
                {"  "}
                Módulos
            </div>
            <div>
                <WebIcon className={"color-2 vertical-align-middle"} />
                {"  "}
                Submódulos
            </div>
            <div>
                <ExtensionIcon className={"color-3 vertical-align-middle"} />
                {"  "}
                Botones
            </div>
        </MeditocHelper>
    );
};

export default Simbologia;
