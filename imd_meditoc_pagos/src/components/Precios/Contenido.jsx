import React from "react";
import { Grid } from "@material-ui/core";
import Membresias from "./Membresias";
import Orientaciones from "./Orientaciones";

const Contenido = () => {
    return (
        <div className="precios-cotenido-contenedor">
            <Grid container className="centrar" spacing={4}>
                <Grid item xs={12}>
                    <div className="precios-contenido-titulo">
                        <span className="primary-blue">Meditoc</span>
                        &nbsp;
                        <span className="primary-gray">360</span>
                    </div>
                </Grid>
                <Grid item xs={12}>
                    <span className="precios-contenido-descripcion">
                        Meditoc 360 ofrece orientación médica, nutricional y
                        psicológica a distancia los 365 días del año,
                        <br /> brindando acceso a servicio de salud de calidad.
                    </span>
                </Grid>
                <Grid item xs={12}>
                    <span className="precios-contenido-descripcion-nr">
                        Podrá adquirir membresías para los siguientes servicios.
                    </span>
                </Grid>
                <Grid item sm={6} xs={12}>
                    <Membresias />
                </Grid>
                <Grid item sm={6} xs={12}>
                    <Orientaciones />
                </Grid>
            </Grid>
        </div>
    );
};

export default Contenido;
