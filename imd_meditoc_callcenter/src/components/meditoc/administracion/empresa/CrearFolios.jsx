import React from "react";
import MeditocModal from "../../../utilidades/MeditocModal";
import { Grid, Typography, Table, TableBody, TableRow, TableCell, TableContainer } from "@material-ui/core";
import { useState } from "react";
import MeditocTable from "../../../utilidades/MeditocTable";
import { useEffect } from "react";
import MeditocModalBotones from "../../../utilidades/MeditocModalBotones";

const CrearFolios = (props) => {
    const { entEmpresa, open, setOpen, funcAlert } = props;

    const columns = [
        { title: "ID", field: "iIdProducto", align: "center", editable: "never", hidden: true },
        { title: "Nombre", field: "sNombre", align: "center", editable: "never" },
        { title: "Precio", field: "fCosto", align: "center", editable: "never" },
        { title: "Cantidad", field: "iCantidad", align: "center" },
    ];

    const data = [
        { iIdProducto: 1, sNombre: "Membresía 3 meses", fCosto: 300, iCantidad: 0 },
        { iIdProducto: 2, sNombre: "Membresía 6 meses", fCosto: 600, iCantidad: 0 },
        { iIdProducto: 3, sNombre: "Membresía 9 meses", fCosto: 900, iCantidad: 0 },
    ];

    const [listaProductosEmpresa, setListaProductosEmpresa] = useState(data);

    const [listaProductosSeleccionados, setListaProductosSeleccionados] = useState([]);

    const [subtotal, setSubtotal] = useState(0);
    const [montoIva, setMontoIva] = useState(0);

    const handleCellEditable = (newValue, oldValue, productoEditado, columna) => {
        console.log(newValue, oldValue, productoEditado, columna);
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

    //Funcion para cerrar este modal
    const handleClose = () => {
        setOpen(false);
    };

    const handleClickCrearFolios = () => {
        if (listaProductosSeleccionados.length < 1) {
            funcAlert(
                "Para generar los folios debe seleccionar al menos un producto de la lista e ingresar la cantidad (mínimo 1).",
                "warning"
            );
        }
        if (listaProductosSeleccionados.some((x) => x.iCantidad === 0)) {
            funcAlert(
                "Verifique que las cantidades ingresadas para los productos seleccionados sean como mínimo 1.",
                "warning"
            );
        }
    };

    useEffect(() => {
        const calcSubtotal = listaProductosSeleccionados.reduce((a, b) => a + b.iCantidad * b.fCosto, 0);

        const calcMontoIva = calcSubtotal * 0.16;

        setSubtotal(calcSubtotal);
        setMontoIva(calcMontoIva);
    }, [listaProductosEmpresa, listaProductosSeleccionados]);

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
                                        <span className="rob-nor size-20 color-2">IVA (16%):</span>
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
                <MeditocModalBotones
                    okMessage="Generar folios"
                    okFunc={handleClickCrearFolios}
                    cancelFunc={handleClose}
                />
            </Grid>
        </MeditocModal>
    );
};

export default CrearFolios;
