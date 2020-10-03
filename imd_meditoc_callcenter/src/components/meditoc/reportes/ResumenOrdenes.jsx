import PropTypes from "prop-types";
import { Button, Grid } from "@material-ui/core";
import React, { Fragment, useState } from "react";
import MeditocTable from "../../utilidades/MeditocTable";
import ResumenOrdenDetalle from "./ResumenOrdenDetalle";
import { EnumStatusConekta } from "../../../configurations/enumConfig";
import CloseIcon from "@material-ui/icons/Close";
import DoneIcon from "@material-ui/icons/Done";
import { green, red } from "@material-ui/core/colors";
import ResumeNumero from "./ResumeNumero";

const ResumenOrdenes = (props) => {
    const { entVentas } = props;

    const columnas = [
        { title: "Orden", field: "sOrderId", align: "center" },
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
                <Grid item xs={12}>
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
                        rowClick={false}
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

ResumenOrdenes.propTypes = {
    entVentas: PropTypes.shape({
        ResumenOrdenes: PropTypes.shape({
            dTotalVendido: PropTypes.shape({
                toLocaleString: PropTypes.func,
            }),
            iTotalFolios: PropTypes.any,
            iTotalOrdenes: PropTypes.any,
            iTotalOrdenesRechazadas: PropTypes.any,
            lstOrdenes: PropTypes.shape({
                find: PropTypes.func,
                map: PropTypes.func,
            }),
        }),
    }),
};

export default ResumenOrdenes;
