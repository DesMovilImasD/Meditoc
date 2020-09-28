import { Button, Divider, Grid, IconButton, InputAdornment, TextField, Typography } from "@material-ui/core";
import { DatePicker } from "@material-ui/pickers";
import React, { Fragment, useState } from "react";
import MeditocTable from "../../utilidades/MeditocTable";
import DateRangeIcon from "@material-ui/icons/DateRange";
import ResumenOrdenDetalle from "./ResumenOrdenDetalle";
import ResumeInfo from "./ResumeInfo";

const ResumenOrdenes = (props) => {
    const { entVentas } = props;

    const columnas = [
        { title: "Orden", field: "sOrderId", align: "center" },
        // { title: "Subtotal", field: "nAmount", align: "center" },
        // { title: "Descuento", field: "nAmountDiscount", align: "center" },
        // { title: "IVA", field: "nAmountTax", align: "center" },
        { title: "Origen", field: "sOrigen", align: "center" },
        { title: "Total", field: "nAmountPaid", align: "center" },
        { title: "Cupón", field: "sCodigo", align: "center" },
        { title: "Estatus", field: "sPaymentStatus", align: "center" },
        { title: "Fecha", field: "sRegisterDate", align: "center" },
        { title: "Ver", field: "sDetalle", align: "center" },
    ];

    const [ordenSeleccionada, setOrdenSeleccionada] = useState({
        sOrderId: "",
        nAmount: 0,
        nAmountDiscount: 0,
        nAmountTax: 0,
        nAmountPaid: 0,
        customer_info: {},
        charges: {},
        lstProductos: [],
    });

    const [modalDetalleOrdenOpen, setModalDetalleOrdenOpen] = useState(false);

    const handleClickDetalle = (sOrderId) => {
        setOrdenSeleccionada(entVentas.ResumenOrdenes.lstOrdenes.find((x) => x.sOrderId === sOrderId));
        setModalDetalleOrdenOpen(true);
    };

    return (
        <Fragment>
            <Grid container spacing={3}>
                <Grid item sm={6} xs={12}>
                    <span className="rob-nor bold size-20 color-4">FILTROS</span>
                    <Divider />
                    <br></br>
                    <Grid container spacing={3}>
                        <Grid item sm={6} xs={12}>
                            <DatePicker
                                variant="inline"
                                inputVariant="outlined"
                                label="Fecha inicio:"
                                fullWidth
                                helperText=""
                                format="dd/MM/yyyy"
                                InputProps={{
                                    endAdornment: (
                                        <InputAdornment position="end">
                                            <IconButton>
                                                <DateRangeIcon />
                                            </IconButton>
                                        </InputAdornment>
                                    ),
                                }}
                            />
                        </Grid>
                        <Grid item sm={6} xs={12}>
                            <DatePicker
                                variant="inline"
                                inputVariant="outlined"
                                label="Fecha fin:"
                                fullWidth
                                helperText=""
                                format="dd/MM/yyyy"
                                InputProps={{
                                    endAdornment: (
                                        <InputAdornment position="end">
                                            <IconButton>
                                                <DateRangeIcon />
                                            </IconButton>
                                        </InputAdornment>
                                    ),
                                }}
                            />
                        </Grid>
                        <Grid item sm={6} xs={12}>
                            <TextField name="txtEstatus" label="Estatus:" variant="outlined" fullWidth></TextField>
                        </Grid>
                    </Grid>
                </Grid>
                <Grid item sm={6} xs={12}>
                    <span className="rob-nor bold size-20 color-4">RESUMÉN DE VENTAS</span>
                    <Divider />
                    <ResumeInfo
                        label="Total de venta"
                        value={
                            "$" +
                            entVentas.ResumenOrdenes.dTotalVendido.toLocaleString("en", {
                                minimumFractionDigits: 2,
                            })
                        }
                    />
                    <ResumeInfo label="Total de órdenes vendidas" value={entVentas.ResumenOrdenes.iTotalOrdenes} />
                    <ResumeInfo
                        label="Total de órdenes rechazadas"
                        value={entVentas.ResumenOrdenes.iTotalOrdenesRechazadas}
                    />
                    <ResumeInfo label="Total de folios generados" value={entVentas.ResumenOrdenes.iTotalFolios} />
                    <ResumeInfo
                        label="Total de cupones aplicados"
                        value={entVentas.ResumenOrdenes.iTotalCuponesAplicados}
                    />
                    <ResumeInfo
                        label="Total de descuento"
                        value={
                            "$" +
                            entVentas.ResumenOrdenes.dTotalDescontado.toLocaleString("en", {
                                minimumFractionDigits: 2,
                            })
                        }
                    />
                </Grid>
                <Grid item xs={12}>
                    <span className="rob-nor bold size-20 color-4">RESULTADOS</span>
                    <Divider />
                    <MeditocTable
                        columns={columnas}
                        data={entVentas.ResumenOrdenes.lstOrdenes.map((orden) => ({
                            ...orden,
                            nAmount: "$" + orden.nAmount.toLocaleString("en", { minimumFractionDigits: 2 }),
                            nAmountDiscount:
                                "$" + orden.nAmountDiscount.toLocaleString("en", { minimumFractionDigits: 2 }),
                            nAmountTax: "$" + orden.nAmountTax.toLocaleString("en", { minimumFractionDigits: 2 }),
                            nAmountPaid: "$" + orden.nAmountPaid.toLocaleString("en", { minimumFractionDigits: 2 }),
                            sDetalle: (
                                <Button
                                    variant="contained"
                                    color="primary"
                                    onClick={() => handleClickDetalle(orden.sOrderId)}
                                >
                                    Detalle
                                </Button>
                            ),
                        }))}
                        rowSelected={ordenSeleccionada}
                        setRowSelected={setOrdenSeleccionada}
                        mainField="sOrderId"
                    />
                </Grid>
            </Grid>
            <ResumenOrdenDetalle
                entOrden={ordenSeleccionada}
                open={modalDetalleOrdenOpen}
                setOpen={setModalDetalleOrdenOpen}
            />
        </Fragment>
    );
};

export default ResumenOrdenes;
