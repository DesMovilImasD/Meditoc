import React from "react";
import {
    FaBookMedical,
    FaBriefcaseMedical,
    FaCommentMedical,
    FaNotesMedical,
    FaTrash,
} from "react-icons/fa";
import { IconButton, Grid, Hidden } from "@material-ui/core";
import { MdAdd, MdRemove } from "react-icons/md";

const Producto = (props) => {
    const { producto, index, listaProductos, setListaProductos } = props;

    let iconoProducto;

    switch (producto.id) {
        case 289:
            iconoProducto = <FaBookMedical />;
            break;
        case 290:
            iconoProducto = <FaBookMedical />;
            break;
        case 294:
            iconoProducto = <FaBriefcaseMedical />;
            break;
        case 295:
            iconoProducto = <FaCommentMedical />;
            break;
        case 296:
            iconoProducto = <FaNotesMedical />;
            break;
        default:
            iconoProducto = <FaBriefcaseMedical />;
            break;
    }

    const handleClickMenos = () => {
        let listaProductosCopy = [...listaProductos];
        listaProductosCopy[index].cantidad--;
        setListaProductos(listaProductosCopy);
    };

    const handleClickMas = () => {
        let listaProductosCopy = [...listaProductos];
        listaProductosCopy[index].cantidad++;
        setListaProductos(listaProductosCopy);
    };

    const handleClickEliminar = () => {
        let listaProductosCopy = [...listaProductos];
        listaProductosCopy.splice(index, 1);
        setListaProductos(listaProductosCopy);
    };

    return (
        <div className="pagos-detalle-compra-articulo">
            <Hidden only="xs">
                <div className="pagos-detalle-compra-articulo-icono">
                    {iconoProducto}
                </div>
            </Hidden>
            <div className="pagos-detalle-compra-articulo-detalles">
                <div className="pagos-detalle-compra-articulo-nombre">
                    {producto.nombre}
                </div>
                <div className="pagos-detalle-compra-articulo-precio">
                    ${producto.precio.toFixed(2)}
                </div>
                <div className="pagos-detalle-compra-articulo-cantidad">
                    Cantidad:&nbsp;
                    <IconButton
                        size="medium"
                        disabled={producto.cantidad <= 1}
                        onClick={handleClickMenos}
                    >
                        <MdRemove className="pagos-detalle-compra-articulo-cantidad-btn" />
                    </IconButton>
                    &nbsp; {producto.cantidad} &nbsp;
                    <IconButton
                        size="medium"
                        disabled={producto.cantidad >= 10}
                        onClick={handleClickMas}
                    >
                        <MdAdd className="pagos-detalle-compra-articulo-cantidad-btn" />
                    </IconButton>
                </div>
            </div>
            <div className="pagos-detalle-compra-articulo-eliminar">
                <IconButton onClick={handleClickEliminar}>
                    <FaTrash className="pagos-detalle-compra-articulo-cantidad-btn" />
                </IconButton>
            </div>
        </div>
    );
};

export default Producto;
