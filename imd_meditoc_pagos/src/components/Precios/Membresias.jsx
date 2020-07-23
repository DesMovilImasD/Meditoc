import React, { useState } from "react";
import { Grid, Radio, Button } from "@material-ui/core";
import { FaUser } from "react-icons/fa";
import { useHistory } from "react-router-dom";

const Membresias = () => {
    const nombre6Meses = "sixmonths";
    const nombre12Meses = "oneyear";

    const history = useHistory();

    const [rdMiembro, setRdMiembro] = useState(nombre6Meses);

    const handleChangeRdMiembro = (e) => {
        setRdMiembro(e.target.value);
    };

    const handleClickContratar = () => {
        let lstItems = [];
        if (rdMiembro === nombre6Meses) {
            lstItems.push({
                nombre: "Membresía 6 meses",
                precio: 800,
                cantidad: 1,
                id: 289,
                tipo: 1,
            });
        } else if (rdMiembro === nombre12Meses) {
            lstItems.push({
                nombre: "Membresía 12 meses",
                precio: 1450,
                cantidad: 1,
                id: 290,
                tipo: 1,
            });
        } else {
            return;
        }
        sessionStorage.setItem("lstItems", JSON.stringify(lstItems));
        history.push("/pagos");
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
                            value={nombre6Meses}
                            color="primary"
                            checked={rdMiembro === nombre6Meses}
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
                            value={nombre12Meses}
                            color="primary"
                            checked={rdMiembro === nombre12Meses}
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
                <Button
                    variant="contained"
                    color="primary"
                    onClick={handleClickContratar}
                >
                    CONTRATAR
                </Button>
            </div>
        </div>
    );
};

export default Membresias;
