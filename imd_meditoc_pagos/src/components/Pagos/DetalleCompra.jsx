import PropTypes from "prop-types";
import React, { useEffect } from "react";
import { Typography } from "@material-ui/core";
import Producto from "./Producto";
import ResumenCompra from "./ResumenCompra";

/*****************************************************
 * Descripción: Contiene la estructura para mostrar el detalle de
 * los artículos y el resumen de compra
 * Autor: Cristopher Noh
 * Fecha: 23/07/2020
 * Modificaciones:
 *****************************************************/
const DetalleCompra = (props) => {
    const {
        listaProductos,
        setListaProductos,
        entCupon,
        totalPagar,
        setTotalPagar,
    } = props;

    const funcGetArticulos = () => {
        const strArticulos = sessionStorage.getItem("lstItems");

        if (strArticulos === null) {
            return;
        }

        let lstItems = JSON.parse(strArticulos);
        setListaProductos(lstItems);
    };

    useEffect(() => {
        funcGetArticulos();
        // eslint-disable-next-line
    }, []);
    return (
        <div className="pagos-detalle-compra-contenedor">
            <div className="pagos-detalle-compra-articulos">
                {listaProductos.length === 0 ? (
                    <Typography>"No hay artículos por mostrar."</Typography>
                ) : (
                    listaProductos.map((producto, index) => (
                        <Producto
                            key={index}
                            producto={producto}
                            index={index}
                            listaProductos={listaProductos}
                            setListaProductos={setListaProductos}
                        />
                    ))
                )}
            </div>
            <ResumenCompra
                listaProductos={listaProductos}
                entCupon={entCupon}
                totalPagar={totalPagar}
                setTotalPagar={setTotalPagar}
            />
        </div>
    );
};

DetalleCompra.propTypes = {
    entCupon: PropTypes.object,
    listaProductos: PropTypes.array,
    setListaProductos: PropTypes.func,
    setTotalPagar: PropTypes.func,
    totalPagar: PropTypes.number,
};

export default DetalleCompra;
