import React, { useEffect } from "react";
import {
    FaClinicMedical,
    FaBriefcaseMedical,
    FaCommentMedical,
    FaNotesMedical,
    FaTrash,
} from "react-icons/fa";
import { IconButton, Grid, Hidden, Typography } from "@material-ui/core";
import { MdAdd, MdRemove } from "react-icons/md";
import Producto from "./Producto";
import ResumenCompra from "./ResumenCompra";

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

export default DetalleCompra;
