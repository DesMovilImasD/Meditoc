import React, { Fragment } from "react";
import MeditocHeader1 from "../../../utilidades/MeditocHeader1";
import InsertDriveFileIcon from "@material-ui/icons/InsertDriveFile";
import AddIcon from "@material-ui/icons/Add";
import EditIcon from "@material-ui/icons/Edit";
import DeleteIcon from "@material-ui/icons/Delete";
import VisibilityIcon from "@material-ui/icons/Visibility";
import { Tooltip, IconButton } from "@material-ui/core";
import MeditocBody from "../../../utilidades/MeditocBody";
import { useState } from "react";
import MeditocTable from "../../../utilidades/MeditocTable";
import FormProducto from "./FormProducto";
import MeditocConfirmacion from "../../../utilidades/MeditocConfirmacion";
import DetalleProducto from "./DetalleProducto";
import ProductoController from "../../../../controllers/ProductoController";
import { useEffect } from "react";
import { EnumTipoProducto } from "../../../../configurations/enumConfig";

/*************************************************************
 * Descripcion: Submódulo para vista principal "PRODUCTOS" del portal Meditoc
 * Creado: Cristopher Noh
 * Fecha: 01/09/2020
 * Invocado desde: ContentMain
 *************************************************************/
const Productos = (props) => {
    const { usuarioSesion, funcLoader, funcAlert } = props;

    const productoController = new ProductoController();

    const columns = [
        { title: "ID", field: "iIdProducto", align: "center", hidden: true },
        { title: "Nombre", field: "sNombre", align: "center" },
        { title: "Tipo", field: "sTipoProducto", align: "center" },
        { title: "Costo", field: "sCosto", align: "center" },
        { title: "Meses de vigencia", field: "iMesVigencia", align: "center" },
        { title: "Comercial", field: "sComercial", align: "center" },
    ];

    const productoEntidadVacia = {
        iIdProducto: 0,
        iIdTipoProducto: EnumTipoProducto.Membresia,
        sNombre: "",
        sNombreCorto: "",
        sDescripcion: "",
        fCosto: 0,
        iMesVigencia: 0,
        sIcon: "",
        bComercial: false,
        sPrefijoFolio: "",
        iIdUsuarioMod: 0,
        bActivo: false,
        bBaja: false,
    };

    const [listaProductos, setListaProductos] = useState([]);
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

    const funcEliminarProducto = async () => {
        funcLoader(true, "Eliminando producto...");

        const entProductoSubmit = {
            iIdProducto: productoSeleccionado.iIdProducto,
            iIdUsuarioMod: usuarioSesion.iIdUsuario,
            bActivo: false,
            bBaja: true,
        };

        const response = await productoController.funcSaveProducto(entProductoSubmit);

        if (response.Code === 0) {
            await funcConsultarProductos();
            setProductoSeleccionado(productoEntidadVacia);
            funcAlert(response.Message, "success");
        } else {
            funcAlert(response.Message);
        }
        setModalEliminarProductoOpen(false);

        funcLoader();
    };

    const funcConsultarProductos = async () => {
        funcLoader(true, "Consultando productos...");

        const response = await productoController.funcGetProductos();

        if (response.Code === 0) {
            setListaProductos(response.Result);
        } else {
            funcAlert(response.Message);
        }

        funcLoader();
    };

    useEffect(() => {
        funcConsultarProductos();
    }, []);

    return (
        <Fragment>
            <MeditocHeader1 title="PRODUCTOS">
                <Tooltip title="Nuevo producto" arrow>
                    <IconButton onClick={handleClickNuevoProducto}>
                        <AddIcon className="color-0" />
                    </IconButton>
                </Tooltip>
                <Tooltip title="Ver detalle de producto" arrow>
                    <IconButton onClick={handleClickDetallesProducto}>
                        <VisibilityIcon className="color-0" />
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
                funcConsultarProductos={funcConsultarProductos}
                usuarioSesion={usuarioSesion}
                funcLoader={funcLoader}
                funcAlert={funcAlert}
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
