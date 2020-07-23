import React, { useState } from "react";
import { Grid, Radio, Button } from "@material-ui/core";
import {
    FaClinicMedical,
    FaBriefcaseMedical,
    FaCommentMedical,
    FaNotesMedical,
} from "react-icons/fa";
import { useHistory } from "react-router-dom";

const Orientaciones = () => {
    const history = useHistory();
    const [rdOrientacion, setRdOrientacion] = useState({
        medica: true,
        psicologica: false,
        nutricional: false,
    });

    const handleClickComprar = () => {
        let lstItems = [];
        if (rdOrientacion.medica) {
            lstItems.push({
                nombre: "Orientación Médica",
                precio: 55,
                cantidad: 1,
                id: 294,
                tipo: 2,
            });
        }
        if (rdOrientacion.psicologica) {
            lstItems.push({
                nombre: "Orientación Psicológica",
                precio: 55,
                cantidad: 1,
                id: 295,
                tipo: 2,
            });
        }
        if (rdOrientacion.nutricional) {
            lstItems.push({
                nombre: "Orientación Nutricional",
                precio: 55,
                cantidad: 1,
                id: 296,
                tipo: 2,
            });
        }
        if (lstItems.length === 0) {
            return;
        }
        sessionStorage.setItem("lstItems", JSON.stringify(lstItems));
        history.push("/pagos");
    };

    return (
        <div className="precios-articulo-contenedor">
            <div className="precios-articulo-header">
                <div className="precios-articulo-header-icon">
                    <FaClinicMedical />
                </div>
                <div className="precios-articulo-header-title">
                    Orientaciones
                </div>
            </div>
            <Grid container>
                <Grid item xs={12}>
                    <div className="precios-ariculo-monto">$55</div>
                    <div className="precios-articulo-monto-descripcion precios-ariculo-space">
                        por orientación
                    </div>
                </Grid>
                <Grid item xs={4} className="precios-articulo-separador">
                    <div className="precios-articulo-icon-orientacion">
                        <FaBriefcaseMedical />
                    </div>
                    <div className="precios-articulo-monto-descripcion">
                        Médica
                    </div>
                    <div>
                        <Radio
                            name="rd-miembro"
                            value="medica"
                            color="primary"
                            checked={rdOrientacion.medica === true}
                            onClick={() =>
                                setRdOrientacion({
                                    ...rdOrientacion,
                                    medica: !rdOrientacion.medica,
                                })
                            }
                        />
                    </div>
                </Grid>
                <Grid item xs={4} className="precios-articulo-separador">
                    <div className="precios-articulo-icon-orientacion">
                        <FaCommentMedical />
                    </div>
                    <div className="precios-articulo-monto-descripcion">
                        Psicológica
                    </div>
                    <div>
                        <Radio
                            name="rd-miembro"
                            value="psicologica"
                            color="primary"
                            checked={rdOrientacion.psicologica}
                            onClick={() =>
                                setRdOrientacion({
                                    ...rdOrientacion,
                                    psicologica: !rdOrientacion.psicologica,
                                })
                            }
                        />
                    </div>
                </Grid>
                <Grid item xs={4}>
                    <div className="precios-articulo-icon-orientacion">
                        <FaNotesMedical />
                    </div>
                    <div className="precios-articulo-monto-descripcion">
                        Nutricional
                    </div>
                    <div>
                        <Radio
                            name="rd-miembro"
                            value="nutricional"
                            color="primary"
                            checked={rdOrientacion.nutricional}
                            onClick={() =>
                                setRdOrientacion({
                                    ...rdOrientacion,
                                    nutricional: !rdOrientacion.nutricional,
                                })
                            }
                        />
                    </div>
                </Grid>
                <Grid
                    item
                    xs={12}
                    className="precios-articulo-descripcion-contenedor"
                >
                    <span className="precios-articulo-descripcion">
                        Contará con un servicio de orientación médica,
                        nutricional o psicológica válido por 24 horas, el cual
                        le dará acceso a tener una llamada telefónica, chat y
                        videollamada.
                    </span>
                </Grid>
                <Grid item xs={12}></Grid>
            </Grid>
            <div className="precios-articulo-contratar">
                <Button
                    variant="contained"
                    color="primary"
                    onClick={handleClickComprar}
                >
                    COMPRAR
                </Button>
            </div>
        </div>
    );
};

export default Orientaciones;
