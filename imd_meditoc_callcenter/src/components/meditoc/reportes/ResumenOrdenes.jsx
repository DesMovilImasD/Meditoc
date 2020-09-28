import { Button, Divider, Grid, IconButton, InputAdornment, TextField, Typography } from "@material-ui/core";
import { DatePicker } from "@material-ui/pickers";
import React, { Fragment, useState } from "react";
import MeditocTable from "../../utilidades/MeditocTable";
import DateRangeIcon from "@material-ui/icons/DateRange";
import ResumenOrdenDetalle from "./ResumenOrdenDetalle";
import ResumeInfo from "./ResumeInfo";
import { EnumStatusConekta } from "../../../configurations/enumConfig";
import CloseIcon from "@material-ui/icons/Close";
import DoneIcon from "@material-ui/icons/Done";
import { green, red } from "@material-ui/core/colors";
import ResumeNumero from "./ResumeNumero";

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
        { title: "Estatus", field: "sPaymentStatusWI", align: "center" },
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
                {/* <Grid item xs={12}>
                    <span className="rob-nor bold size-20 color-4">RESUMÉN DE VENTAS</span>
                    <Divider />
                </Grid> */}
                <Grid item md={3} sm={6} xs={12} className="center">
                    <ResumeNumero
                        label="TOTAL DE VENTA"
                        value={
                            "$" +
                            entVentas.ResumenOrdenes.dTotalVendido.toLocaleString("en", {
                                minimumFractionDigits: 2,
                            })
                        }
                        color="color-1"
                    />
                </Grid>
                <Grid item md={3} sm={6} xs={12} className="center">
                    <ResumeNumero
                        label="ÓRDENES VENDIDAS"
                        value={entVentas.ResumenOrdenes.iTotalOrdenes}
                        color="color-6"
                    />
                </Grid>
                <Grid item md={3} sm={6} xs={12} className="center">
                    <ResumeNumero
                        label="ÓRDENES RECHAZADAS"
                        value={entVentas.ResumenOrdenes.iTotalOrdenesRechazadas}
                        color="color-5"
                    />
                </Grid>
                <Grid item md={3} sm={6} xs={12} className="center">
                    <ResumeNumero
                        label="FOLIOS GENERADOS"
                        value={entVentas.ResumenOrdenes.iTotalFolios}
                        color="color-3"
                    />
                </Grid>
                {/* <Grid item xs={12}>
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
                </Grid> */}
                <Grid item xs={12}>
                    {/* <span className="rob-nor bold size-20 color-4">RESULTADOS</span>
                    <Divider /> */}
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
                            sPaymentStatusWI: (
                                <span>
                                    {orden.sPaymentStatus}
                                    {orden.sPaymentStatus === EnumStatusConekta.Declined ? (
                                        <CloseIcon style={{ color: red[500], verticalAlign: "middle" }} />
                                    ) : (
                                        <DoneIcon style={{ color: green[500], verticalAlign: "middle" }} />
                                    )}
                                </span>
                            ),
                        }))}
                        rowSelected={ordenSeleccionada}
                        setRowSelected={setOrdenSeleccionada}
                        mainField="sOrderId"
                        search={false}
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
