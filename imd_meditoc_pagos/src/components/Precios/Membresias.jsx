import React, { useState } from "react";
import { Grid, Radio, Button } from "@material-ui/core";
import { FaUser } from "react-icons/fa";

const Membresias = () => {
    const [rdMiembro, setRdMiembro] = useState("sixmonths");

    const handleChangeRdMiembro = (e) => {
        setRdMiembro(e.target.value);
    };

    return (
        <div className="precios-articulo-contenedor">
            <div className="precios-articulo-header">
                <div className="precios-articulo-header-icon">
                    <FaUser />
                </div>
                <div className="precios-articulo-header-title">Miembro</div>
            </div>
            <Grid container>
                <Grid item xs={6} className="precios-articulo-separador">
                    <div className="precios-ariculo-monto">$800</div>
                    <div className="precios-articulo-monto-descripcion">
                        6 meses
                    </div>
                    <div>
                        <Radio
                            name="rd-miembro"
                            value="sixmonths"
                            color="primary"
                            checked={rdMiembro === "sixmonths"}
                            onChange={handleChangeRdMiembro}
                        />
                    </div>
                </Grid>
                <Grid item xs={6}>
                    <div className="precios-ariculo-monto">$1,450</div>
                    <div className="precios-articulo-monto-descripcion">
                        1 año
                    </div>
                    <div>
                        <Radio
                            name="rd-miembro"
                            value="oneyear"
                            color="primary"
                            checked={rdMiembro === "oneyear"}
                            onChange={handleChangeRdMiembro}
                        />
                    </div>
                </Grid>
                <Grid
                    item
                    xs={12}
                    className="precios-articulo-descripcion-contenedor"
                >
                    <span className="precios-articulo-descripcion">
                        Desde el primer día contará con servicio de orientación
                        médica, nutricional y psicológica los 365 días del año
                        para todos los miembros de su familia, empleados de
                        empresas u hogar y sus familias, el cual es simple de
                        usar, tendrá respuesta inmediata y llamadas ilimitadas
                        de orientación médica.
                    </span>
                </Grid>
            </Grid>
            <div className="precios-articulo-contratar">
                <Button variant="contained" color="primary">
                    CONTRATAR
                </Button>
            </div>
        </div>
    );
};

export default Membresias;
