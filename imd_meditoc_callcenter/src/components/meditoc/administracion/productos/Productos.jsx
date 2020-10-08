import { EnumProductos, EnumTipoProducto, ListProductos } from "../../../../configurations/enumConfig";
import { IconButton, Tooltip } from "@material-ui/core";
import React, { Fragment } from "react";

import AddIcon from "@material-ui/icons/Add";
import DeleteIcon from "@material-ui/icons/Delete";
import DetalleProducto from "./DetalleProducto";
import EditIcon from "@material-ui/icons/Edit";
import FormProducto from "./FormProducto";
import MeditocBody from "../../../utilidades/MeditocBody";
import MeditocConfirmacion from "../../../utilidades/MeditocConfirmacion";
import MeditocHeader1 from "../../../utilidades/MeditocHeader1";
import MeditocTable from "../../../utilidades/MeditocTable";
import ProductoController from "../../../../controllers/ProductoController";
import PropTypes from "prop-types";
import ReplayIcon from "@material-ui/icons/Replay";
import VisibilityIcon from "@material-ui/icons/Visibility";
import { emptyFunc } from "../../../../configurations/preventConfig";
import { useEffect } from "react";
import { useState } from "react";

/*************************************************************
 * Descripcion: Submódulo para vista principal "PRODUCTOS" del portal Meditoc
 * Creado: Cristopher Noh
 * Fecha: 01/09/2020
 * Invocado desde: ContentMain
 *************************************************************/
const Productos = (props) => {
    const { usuarioSesion, permisos, funcLoader, funcAlert } = props;

    const productoController = new ProductoController();

    const columns = [
        { title: "ID", field: "iIdProducto", align: "center", hidden: true },
        { title: "Nombre", field: "sNombre", align: "center" },
        { title: "Tipo", field: "sTipoProducto", align: "center" },
        { title: "Grupo de productos", field: "sGrupoProducto", align: "center" },
        { title: "Costo", field: "sCosto", align: "center" },
        { title: "Duración de vigencia (Meses)", field: "iMesVigencia", align: "center" },
        { title: "Comercial", field: "sComercial", align: "center" },
    ];

    const productoEntidadVacia = {
        iIdProducto: 0,
        iIdTipoProducto: EnumTipoProducto.Membresia,
        sNombre: "",
        sNombreCorto: "",
        sDescripcion: "",
        iIdGrupoProducto: 1,
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
        if (productoSeleccionado.iIdProducto === EnumProductos.OrientacionEspecialistaID) {
            funcAlert(
                "Este producto no se puede editar debito a que solo está habilitado para orientaciones de médicos especialistas",
                "info"
            );
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
        if (ListProductos.includes(productoSeleccionado.iIdProducto)) {
            funcAlert("El producto seleccionado no puede eliminarse", "warning");
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
        // eslint-disable-next-line
    }, []);

    return (
        <Fragment>
            <MeditocHeader1 title={permisos.Nombre}>
                {permisos.Botones["1"] !== undefined && ( //Nuevo producto
                    <Tooltip title={permisos.Botones["1"].Nombre} arrow>
                        <IconButton onClick={handleClickNuevoProducto}>
                            <AddIcon className="color-0" />
                        </IconButton>
                    </Tooltip>
                )}
                {permisos.Botones["2"] !== undefined && ( //Ver detalle de producto
                    <Tooltip title={permisos.Botones["2"].Nombre} arrow>
                        <IconButton onClick={handleClickDetallesProducto}>
                            <VisibilityIcon className="color-0" />
                        </IconButton>
                    </Tooltip>
                )}
                {permisos.Botones["3"] !== undefined && ( //Editar producto
                    <Tooltip title={permisos.Botones["3"].Nombre} arrow>
                        <IconButton onClick={handleClickEditarProducto}>
                            <EditIcon className="color-0" />
                        </IconButton>
                    </Tooltip>
                )}
                {permisos.Botones["4"] !== undefined && ( //Eliminar producto
                    <Tooltip title={permisos.Botones["4"].Nombre} arrow>
                        <IconButton onClick={handleClickEliminarProducto}>
                            <DeleteIcon className="color-0" />
                        </IconButton>
                    </Tooltip>
                )}
                {permisos.Botones["5"] !== undefined && ( //Actualizar tabla
                    <Tooltip title={permisos.Botones["5"].Nombre} arrow>
                        <IconButton onClick={funcConsultarProductos}>
                            <ReplayIcon className="color-0" />
                        </IconButton>
                    </Tooltip>
                )}
            </MeditocHeader1>
            <MeditocBody>
                <MeditocTable
                    columns={columns}
                    data={listaProductos}
                    rowSelected={productoSeleccionado}
                    setRowSelected={setProductoSeleccionado}
                    mainField="iIdProducto"
                    doubleClick={permisos.Botones["2"] !== undefined ? handleClickDetallesProducto : emptyFunc}
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

Productos.propTypes = {
    funcAlert: PropTypes.func,
    funcLoader: PropTypes.func,
    title: PropTypes.any,
    usuarioSesion: PropTypes.shape({
        iIdUsuario: PropTypes.any,
    }),
};

export default Productos;
