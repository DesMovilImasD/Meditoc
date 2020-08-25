import React, { useState } from "react";
import { Button, IconButton, Tooltip, Grid, Typography } from "@material-ui/core";
import AddIcon from "@material-ui/icons/Add";
import SistemaModulo from "./SistemaModulo";
import WebIcon from "@material-ui/icons/Web";
import AccountTreeIcon from "@material-ui/icons/AccountTree";
import ExtensionIcon from "@material-ui/icons/Extension";

const Sistema = () => {
    const [sistemList, setSistemList] = useState([
        {
            iIdModulo: 1,
            sNombre: "Configuración",
            lstSubmodulos: [
                {
                    iIdSubmodulo: 1,
                    sNombre: "Usuarios",
                    lstBotones: [
                        { iIdBoton: 1, sNombre: "Agregar" },
                        { iIdBoton: 2, sNombre: "Ver" },
                        { iIdBoton: 3, sNombre: "Editar" },
                        { iIdBoton: 4, sNombre: "Eliminar" },
                    ],
                },
                {
                    iIdSubmodulo: 2,
                    sNombre: "Perfiles",
                    lstBotones: [
                        { iIdBoton: 1, sNombre: "Agregar" },
                        { iIdBoton: 2, sNombre: "Ver" },
                        { iIdBoton: 3, sNombre: "Editar" },
                        { iIdBoton: 4, sNombre: "Eliminar" },
                    ],
                },
                {
                    iIdSubmodulo: 3,
                    sNombre: "Sistemas",
                    lstBotones: [
                        { iIdBoton: 1, sNombre: "Agregar" },
                        { iIdBoton: 2, sNombre: "Ver" },
                        { iIdBoton: 3, sNombre: "Editar" },
                        { iIdBoton: 4, sNombre: "Eliminar" },
                    ],
                },
            ],
        },
        {
            iIdModulo: 2,
            sNombre: "Administración",
            lstSubmodulos: [
                {
                    iIdSubmodulo: 1,
                    sNombre: "Colaborades",
                    lstBotones: [
                        { iIdBoton: 1, sNombre: "Agregar" },
                        { iIdBoton: 2, sNombre: "Ver" },
                        { iIdBoton: 3, sNombre: "Editar" },
                        { iIdBoton: 4, sNombre: "Eliminar" },
                    ],
                },
                {
                    iIdSubmodulo: 2,
                    sNombre: "Institución",
                    lstBotones: [
                        { iIdBoton: 1, sNombre: "Agregar" },
                        { iIdBoton: 2, sNombre: "Ver" },
                        { iIdBoton: 3, sNombre: "Editar" },
                        { iIdBoton: 4, sNombre: "Eliminar" },
                    ],
                },
                {
                    iIdSubmodulo: 3,
                    sNombre: "Productos",
                    lstBotones: [
                        { iIdBoton: 1, sNombre: "Agregar" },
                        { iIdBoton: 2, sNombre: "Ver" },
                        { iIdBoton: 3, sNombre: "Editar" },
                        { iIdBoton: 4, sNombre: "Eliminar" },
                    ],
                },
                {
                    iIdSubmodulo: 4,
                    sNombre: "Cupones",
                    lstBotones: [
                        { iIdBoton: 1, sNombre: "Agregar" },
                        { iIdBoton: 2, sNombre: "Ver" },
                        { iIdBoton: 3, sNombre: "Editar" },
                        { iIdBoton: 4, sNombre: "Eliminar" },
                    ],
                },
            ],
        },
        {
            iIdModulo: 3,
            sNombre: "Folios",
            lstSubmodulos: [
                {
                    iIdSubmodulo: 1,
                    sNombre: "Folios",
                    lstBotones: [
                        { iIdBoton: 1, sNombre: "Agregar" },
                        { iIdBoton: 2, sNombre: "Ver" },
                        { iIdBoton: 3, sNombre: "Editar" },
                        { iIdBoton: 4, sNombre: "Eliminar" },
                    ],
                },
            ],
        },
    ]);
    return (
        <div>
            <div className="bar-main">
                <div className="flx-grw-1">
                    <Tooltip title="Agregar módulo" arrow>
                        <IconButton>
                            <AddIcon className="color-0" />
                        </IconButton>
                    </Tooltip>
                </div>
                <div className="ops-nor bold size-20 align-self-center"> SISTEMA</div>
            </div>
            <div className="bar-content">
                <Grid container spacing={2}>
                    <Grid item xs={12}>
                        <span className="rob-nor color-3 size-25">Administrar elementos del sistema:</span>
                    </Grid>
                    <Grid item xs={12}>
                        <AccountTreeIcon className={"color-1 vertical-align-middle"} />
                        <span className={"color-1 size-15"}>Módulos</span>
                    </Grid>
                    <Grid item xs={12}>
                        <WebIcon className={"color-2 vertical-align-middle"} />
                        <span className={"color-2 size-15"}>Submódulos</span>
                    </Grid>
                    <Grid item xs={12}>
                        <ExtensionIcon className={"color-3 vertical-align-middle"} />
                        <span className={"color-3 size-15"}>Botones</span>
                    </Grid>
                    <Grid item xs={12}>
                        {sistemList.map((modulo) => (
                            <SistemaModulo key={modulo.iIdModulo} modulo={modulo} />
                        ))}
                    </Grid>
                </Grid>
            </div>
        </div>
    );
};

export default Sistema;
