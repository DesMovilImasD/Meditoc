import React, { useState } from "react";
import { Grid, Hidden } from "@material-ui/core";
import DetalleCompra from "./DetalleCompra";
import InformacionPago from "./InformacionPago";

const Contenido = (props) => {
    const { funcLoader } = props;

    const [listaProductos, setListaProductos] = useState([]);

    const [entCupon, setEntCupon] = useState(null);

    const [totalPagar, setTotalPagar] = useState(0);

    const [ordenGenerada, setOrdenGenerada] = useState(null);

    return (
        <div className="pagos-contenido">
            <Grid container>
                <Grid item xs={12}>
                    <span className="precios-contenido-descripcion">
                        Para generar sus credenciales de acceso a Meditoc haga
                        el pago del servicio por medio de una tarjeta de crédito
                        o débito.
                    </span>
                </Grid>
                <Grid item md={6} xs={12}>
                    <span className="pagos-contenido-subtitulo">
                        Detalle de compra
                    </span>
                </Grid>
                <Hidden only={["sm", "xs"]}>
                    <Grid item md={6} xs={12}>
                        <span className="pagos-contenido-subtitulo">
                            Información de pago
                        </span>
                    </Grid>
                </Hidden>
                <Grid item md={6} xs={12}>
                    <DetalleCompra
                        listaProductos={listaProductos}
                        setListaProductos={setListaProductos}
                        entCupon={entCupon}
                        totalPagar={totalPagar}
                        setTotalPagar={setTotalPagar}
                    />
                </Grid>
                <Hidden only={["md", "lg", "xl"]}>
                    <Grid item md={6} xs={12}>
                        <span className="pagos-contenido-subtitulo">
                            Información de pago
                        </span>
                    </Grid>
                </Hidden>
                <Grid item md={6} xs={12}>
                    <InformacionPago
                        listaProductos={listaProductos}
                        funcLoader={funcLoader}
                        entCupon={entCupon}
                        setEntCupon={setEntCupon}
                        setOrdenGenerada={setOrdenGenerada}
                    />
                </Grid>
            </Grid>
        </div>
    );
};

export default Contenido;
