import React, { Fragment } from "react";
import MeditocHeader1 from "../../../utilidades/MeditocHeader1";
import InsertDriveFileIcon from "@material-ui/icons/InsertDriveFile";
import AddIcon from "@material-ui/icons/Add";
import EditIcon from "@material-ui/icons/Edit";
import DeleteIcon from "@material-ui/icons/Delete";
import FormatListBulletedIcon from "@material-ui/icons/FormatListBulleted";
import { Tooltip, IconButton } from "@material-ui/core";
import MeditocBody from "../../../utilidades/MeditocBody";
import { useState } from "react";
import MeditocTable from "../../../utilidades/MeditocTable";
import FormProducto from "./FormProducto";
import MeditocConfirmacion from "../../../utilidades/MeditocConfirmacion";
import DetalleProducto from "./DetalleProducto";

/*************************************************************
 * Descripcion: Submódulo para vista principal "PRODUCTOS" del portal Meditoc
 * Creado: Cristopher Noh
 * Fecha: 01/09/2020
 * Invocado desde: ContentMain
 *************************************************************/
const Productos = (props) => {
    const { funcAlert } = props;

    const columns = [
        { title: "ID", field: "iIdProducto", align: "center", hidden: true },
        { title: "Nombre", field: "sNombre", align: "center" },
        { title: "Tipo", field: "sTipoProducto", align: "center" },
        { title: "Costo", field: "fCosto", align: "center" },
        { title: "Meses de vigencia", field: "iMesVigencia", align: "center" },
        { title: "Comercial", field: "bComercial", align: "center" },
    ];

    const data = [
        {
            iIdProducto: 1,
            iIdTipoProducto: 1,
            sTipoProducto: "Servicio",
            sNombre: "Membresía 3 meses",
            sNombreCorto: "3 meses",
            sDescripcion: "Servicio méditoc online por 3 meses",
            fCosto: 300,
            iMesVigencia: 3,
            sIcon: "f479",
            bComercial: true,
            sPrefijoFolio: "VS",
        },
        {
            iIdProducto: 2,
            iIdTipoProducto: 1,
            sTipoProducto: "Servicio",
            sNombre: "Membresía 6 meses",
            sNombreCorto: "6 meses",
            sDescripcion: "Servicio méditoc online por 6 meses",
            fCosto: 600,
            iMesVigencia: 6,
            sIcon: "f479",
            bComercial: true,
            sPrefijoFolio: "VS",
        },
    ];

    const productoEntidadVacia = {
        iIdProducto: 0,
        iIdTipoProducto: 1,
        sTipoProducto: "",
        sNombre: "",
        sNombreCorto: "",
        sDescripcion: "",
        fCosto: 0,
        iMesVigencia: 0,
        sIcon: "",
        bComercial: false,
        sPrefijoFolio: "",
    };

    const [listaProductos, setListaProductos] = useState(data);
    const [productoSeleccionado, setProductoSeleccionado] = useState(productoEntidadVacia);
    const [productoParaModalForm, setProductoParaModalForm] = useState(productoEntidadVacia);

    const [modalFormProductoOpen, setModalFormProductoOpen] = useState(false);
    const [modalEliminarProductoOpen, setModalEliminarProductoOpen] = useState(false);
    const [modalDetalleProductoOpen, setModalDetalleProductoOpen] = useState(false);

    const handleClickNuevoProducto = () => {
        setProductoParaModalForm(productoEntidadVacia);
        setModalFormProductoOpen(true);
    };

    const handleClickEditarProducto = () => {
        if (productoSeleccionado.iIdProducto === 0) {
            funcAlert("Seleccione un producto de la tabla para continuar");
            return;
        }
        setProductoParaModalForm(productoSeleccionado);
        setModalFormProductoOpen(true);
    };

    const handleClickEliminarProducto = () => {
        if (productoSeleccionado.iIdProducto === 0) {
            funcAlert("Seleccione un producto de la tabla para continuar");
            return;
        }
        setModalEliminarProductoOpen(true);
    };

    const handleClickDetallesProducto = () => {
        if (productoSeleccionado.iIdProducto === 0) {
            funcAlert("Seleccione un producto de la tabla para continuar");
            return;
        }
        setModalDetalleProductoOpen(true);
    };

    const funcEliminarProducto = () => {
        setModalEliminarProductoOpen(false);
    };

    return (
        <Fragment>
            <MeditocHeader1 title="PRODUCTOS">
                <Tooltip title="Nuevo producto" arrow>
                    <IconButton onClick={handleClickNuevoProducto}>
                        <AddIcon className="color-0" />
                    </IconButton>
                </Tooltip>
                <Tooltip title="Detalle producto" arrow>
                    <IconButton onClick={handleClickDetallesProducto}>
                        <FormatListBulletedIcon className="color-0" />
                    </IconButton>
                </Tooltip>
                <Tooltip title="Editar producto" arrow>
                    <IconButton onClick={handleClickEditarProducto}>
                        <EditIcon className="color-0" />
                    </IconButton>
                </Tooltip>
                <Tooltip title="Eliminar producto" arrow>
                    <IconButton onClick={handleClickEliminarProducto}>
                        <DeleteIcon className="color-0" />
                    </IconButton>
                </Tooltip>
            </MeditocHeader1>
            <MeditocBody>
                <MeditocTable
                    columns={columns}
                    data={listaProductos}
                    rowSelected={productoSeleccionado}
                    setRowSelected={setProductoSeleccionado}
                    mainField="iIdProducto"
                    doubleClick={handleClickDetallesProducto}
                />
            </MeditocBody>
            <FormProducto
                entProducto={productoParaModalForm}
                open={modalFormProductoOpen}
                setOpen={setModalFormProductoOpen}
            />
            <MeditocConfirmacion
                title="Eliminar prodicto"
                open={modalEliminarProductoOpen}
                setOpen={setModalEliminarProductoOpen}
                okFunc={funcEliminarProducto}
            >
                ¿Desea eliminar el producto "{productoSeleccionado.sNombre}"?
            </MeditocConfirmacion>
            <DetalleProducto
                entProducto={productoSeleccionado}
                open={modalDetalleProductoOpen}
                setOpen={setModalDetalleProductoOpen}
            />
        </Fragment>
    );
};

export default Productos;
