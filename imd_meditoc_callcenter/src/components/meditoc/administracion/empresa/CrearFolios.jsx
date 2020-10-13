import { EnumIVA, EnumOrigen } from "../../../../configurations/enumConfig";
import { Grid, Table, TableBody, TableCell, TableContainer, TableRow, Typography } from "@material-ui/core";

import FolioController from "../../../../controllers/FolioController";
import MeditocModal from "../../../utilidades/MeditocModal";
import MeditocModalBotones from "../../../utilidades/MeditocModalBotones";
import MeditocTable from "../../../utilidades/MeditocTable";
import PropTypes from "prop-types";
import React from "react";
import { useEffect } from "react";
import { useState } from "react";

const CrearFolios = (props) => {
    const {
        entEmpresa,
        open,
        setOpen,
        listaProductos,
        funcGetFoliosEmpresa,
        usuarioSesion,
        funcLoader,
        funcAlert,
    } = props;

    const columns = [
        { title: "ID", field: "iIdProducto", align: "left", editable: "never", hidden: true },
        { title: "", field: "sIconStatus", align: "left", editable: "never", sorting: false },
        { title: "Nombre", field: "sNombre", align: "left", editable: "never" },
        { title: "Precio", field: "sCosto", align: "left", editable: "never" },
        { title: "Cantidad", field: "iCantidad", align: "left" },
    ];

    const [listaProductosEmpresa, setListaProductosEmpresa] = useState([]);

    const [listaProductosSeleccionados, setListaProductosSeleccionados] = useState([]);

    const [subtotal, setSubtotal] = useState(0);
    const [montoIva, setMontoIva] = useState(0);

    const handleCellEditable = (newValue, oldValue, productoEditado, columna) => {
        if (isNaN(newValue)) {
            return;
        }

        if (newValue < 0) {
            return;
        }

        const cantidad = Math.round(newValue);

        const listaProductosEmpresaCopia = [...listaProductosEmpresa];
        const index = listaProductosEmpresaCopia.indexOf(productoEditado);
        listaProductosEmpresaCopia[index].iCantidad = cantidad;
        setListaProductosEmpresa(listaProductosEmpresaCopia);
    };

    const handleClickCrearFolios = async () => {
        if (listaProductosSeleccionados.length < 1) {
            funcAlert(
                "Para generar los folios debe seleccionar al menos un producto de la lista e ingresar la cantidad (mínimo 1).",
                "warning"
            );
            return;
        }
        if (listaProductosSeleccionados.some((x) => x.iCantidad === 0)) {
            funcAlert(
                "Verifique que las cantidades ingresadas para los productos seleccionados sean como mínimo 1.",
                "warning"
            );
            return;
        }

        funcLoader(true, "Generando folios nuevos...");

        const folioController = new FolioController();

        const entFoliosEmpresaSubmit = {
            iIdEmpresa: entEmpresa.iIdEmpresa,
            iIdOrigen: EnumOrigen.PanelAdministrativo,
            iIdUsuarioMod: usuarioSesion.iIdUsuario,
            line_items: listaProductosSeleccionados.map((prod) => ({
                product_id: prod.iIdProducto,
                quantity: prod.iCantidad,
            })),
        };

        const response = await folioController.funcCrearFoliosEmpresa(entFoliosEmpresaSubmit);

        if (response.Code === 0) {
            setOpen(false);
            setListaProductosEmpresa(
                listaProductos.map((producto) => ({
                    iIdProducto: producto.iIdProducto,
                    sNombre: producto.sNombre,
                    fCosto: producto.fCosto,
                    sCosto: producto.sCosto,
                    iCantidad: 0,
                    sIconStatus: (
                        <span>
                            <i
                                className="icon size-20 color-1"
                                dangerouslySetInnerHTML={{ __html: `&#x${producto.sIcon}` }}
                            />
                        </span>
                    ),
                }))
            );
            setListaProductosSeleccionados([]);
            await funcGetFoliosEmpresa(true);
            funcAlert(response.Message, "success");
        } else {
            funcAlert(response.Message);
        }

        funcLoader();
    };

    useEffect(() => {
        const calcSubtotal = listaProductosSeleccionados.reduce((a, b) => a + b.iCantidad * b.fCosto, 0);

        const calcMontoIva = calcSubtotal * EnumIVA.IVA;

        setSubtotal(calcSubtotal);
        setMontoIva(calcMontoIva);
        // eslint-disable-next-line
    }, [listaProductosEmpresa, listaProductosSeleccionados]);

    useEffect(() => {
        if (open) {
            setListaProductosSeleccionados([]);
            setListaProductosEmpresa(
                listaProductos.map((producto) => ({
                    iIdProducto: producto.iIdProducto,
                    sNombre: producto.sNombre,
                    fCosto: producto.fCosto,
                    sCosto: producto.sCosto,
                    iCantidad: 0,
                    sIconStatus: (
                        <span>
                            <i
                                className="icon size-20 color-1"
                                dangerouslySetInnerHTML={{ __html: `&#x${producto.sIcon}` }}
                            />
                        </span>
                    ),
                }))
            );
        }
        // eslint-disable-next-line
    }, [open]);

    return (
        <MeditocModal
            title={`Crear folios para ${entEmpresa.sNombre} (${entEmpresa.sFolioEmpresa})`}
            size="large"
            open={open}
            setOpen={setOpen}
        >
            <Grid container spacing={2}>
                <Grid item xs={12}>
                    <Typography variant="body1" color="secondary" align="center">
                        Seleccione los productos disponibles de la tabla e ingrese la cantidad acordada para generar los
                        nuevos folios
                    </Typography>
                </Grid>
                <Grid item xs={12}>
                    <MeditocTable
                        columns={columns}
                        data={listaProductosEmpresa}
                        setData={setListaProductosEmpresa}
                        rowsSelected={listaProductosSeleccionados}
                        setRowsSelected={setListaProductosSeleccionados}
                        selection={true}
                        mainField="iIdProducto"
                        cellEditable={true}
                        onCellEditable={handleCellEditable}
                    />
                </Grid>
                <Grid item xs={12} className="center">
                    <TableContainer>
                        <Table size="small">
                            <TableBody>
                                <TableRow hover>
                                    <TableCell>
                                        <span className="rob-nor size-20 color-2">Subtotal:</span>
                                    </TableCell>
                                    <TableCell align="right">
                                        <span className="rob-nor size-20 color-3">
                                            ${subtotal.toLocaleString("en-US", { minimumFractionDigits: 2 })}
                                        </span>
                                    </TableCell>
                                </TableRow>
                                <TableRow hover>
                                    <TableCell>
                                        <span className="rob-nor size-20 color-2">IVA ({EnumIVA.IVA * 100}%):</span>
                                    </TableCell>
                                    <TableCell align="right">
                                        <span className="rob-nor size-20 color-3">
                                            + ${montoIva.toLocaleString("en-US", { minimumFractionDigits: 2 })}
                                        </span>
                                    </TableCell>
                                </TableRow>
                                <TableRow hover>
                                    <TableCell>
                                        <span className="rob-nor size-20 color-2">Monto total:</span>
                                    </TableCell>
                                    <TableCell align="right">
                                        <span className="rob-nor size-20 color-3 bold">
                                            $
                                            {(subtotal + montoIva).toLocaleString("en-US", {
                                                minimumFractionDigits: 2,
                                            })}
                                        </span>
                                    </TableCell>
                                </TableRow>
                            </TableBody>
                        </Table>
                    </TableContainer>
                </Grid>
                <MeditocModalBotones okMessage="Generar folios" okFunc={handleClickCrearFolios} setOpen={setOpen} />
            </Grid>
        </MeditocModal>
    );
};

CrearFolios.propTypes = {
    entEmpresa: PropTypes.shape({
        iIdEmpresa: PropTypes.any,
        sFolioEmpresa: PropTypes.any,
        sNombre: PropTypes.any,
    }),
    funcAlert: PropTypes.func,
    funcGetFoliosEmpresa: PropTypes.func,
    funcLoader: PropTypes.func,
    listaProductos: PropTypes.shape({
        map: PropTypes.func,
    }),
    open: PropTypes.any,
    setOpen: PropTypes.func,
};

export default CrearFolios;
