import PropTypes from "prop-types";
import React, { useState, useEffect } from "react";
import { Grid, Hidden } from "@material-ui/core";
import DetalleCompra from "./DetalleCompra";
import InformacionPago from "./InformacionPago";
import PagoOk from "./PagoOk";
import PagoError from "./PagoError";

/*****************************************************
 * Descripción: Contiene la estructura de contenido para el
 * formulario de pagos y mensaje de orden ok y orden error
 * Autor: Cristopher Noh
 * Fecha: 23/07/2020
 * Modificaciones:
 *****************************************************/
const Contenido = (props) => {
    const { funcLoader } = props;

    const [listaProductos, setListaProductos] = useState([]);

    const [entCupon, setEntCupon] = useState(null);

    const [totalPagar, setTotalPagar] = useState(0);

    const [ordenGenerada, setOrdenGenerada] = useState(null);

    const [orgenGeneradaError, setOrgenGeneradaError] = useState(true);

    const [listaDiferimientoPagos, setListaDiferimientoPagos] = useState([]);

    const funcRecalcularDiferimiento = () => {
        let listaDiferimientoEval = [];
        if (totalPagar >= 300) {
            listaDiferimientoEval.push({
                value: "3",
                label: "3 meses sin intereses",
            });
        }
        if (totalPagar >= 600) {
            listaDiferimientoEval.push({
                value: "6",
                label: "6 meses sin intereses",
            });
        }
        if (totalPagar >= 900) {
            listaDiferimientoEval.push({
                value: "9",
                label: "9 meses sin intereses",
            });
        }
        if (totalPagar >= 1200) {
            listaDiferimientoEval.push({
                value: "12",
                label: "12 meses sin intereses",
            });
        }
        setListaDiferimientoPagos(listaDiferimientoEval);
    };

    useEffect(() => {
        funcRecalcularDiferimiento();
        // eslint-disable-next-line
    }, [totalPagar]);

    return (
        <div className="pagos-contenido">
            <div className="pagos-orden-ok-margin">
                {ordenGenerada === null ? (
                    <Grid container>
                        <Grid item xs={12}>
                            <span className="precios-contenido-descripcion">
                                Para generar sus credenciales de acceso a
                                Meditoc haga el pago del servicio por medio de
                                una tarjeta de crédito o débito.
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
                                listaDiferimientoPagos={listaDiferimientoPagos}
                                funcLoader={funcLoader}
                                entCupon={entCupon}
                                setEntCupon={setEntCupon}
                                setOrdenGenerada={setOrdenGenerada}
                                setOrgenGeneradaError={setOrgenGeneradaError}
                            />
                        </Grid>
                    </Grid>
                ) : orgenGeneradaError === false ? (
                    <PagoOk ordenGenerada={ordenGenerada} />
                ) : (
                    <PagoError />
                )}
            </div>
        </div>
    );
};

Contenido.propTypes = {
    funcLoader: PropTypes.func,
};

export default Contenido;
