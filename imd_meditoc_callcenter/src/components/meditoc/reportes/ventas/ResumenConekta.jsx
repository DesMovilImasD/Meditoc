import { Grid, IconButton, Tooltip } from "@material-ui/core";
import React, { Fragment, useEffect, useState } from "react";
import { green, red } from "@material-ui/core/colors";

import CloseIcon from "@material-ui/icons/Close";
import DetalleOrden from "./DetalleOrden";
import DoneIcon from "@material-ui/icons/Done";
import { EnumStatusConekta } from "../../../../configurations/enumConfig";
import MeditocInfoNumber from "../../../utilidades/MeditocInfoNumber";
import MeditocTable from "../../../utilidades/MeditocTable";
import PropTypes from "prop-types";
import VisibilityIcon from "@material-ui/icons/Visibility";
import { cellProps } from "../../../../configurations/dataTableIconsConfig";

const ResumenConekta = (props) => {
    const { entVentas, funcLoader, funcAlert, permisos } = props;

    const columnas = [
        { ...cellProps, title: "Orden", field: "sOrderId" },
        { ...cellProps, title: "Origen", field: "sOrigen" },
        { ...cellProps, title: "Total", field: "nAmountPaid" },
        { ...cellProps, title: "Cupón", field: "sCodigo" },
        { ...cellProps, title: "Estatus", field: "sPaymentStatusWI" },
        { ...cellProps, title: "Fecha", field: "sRegisterDate" },
        { ...cellProps, title: "Detalle", field: "sDetalle", sorting: false },
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

    const [listaOrgenesConekta, setListaOrgenesConekta] = useState([]);

    const [modalDetalleOrdenOpen, setModalDetalleOrdenOpen] = useState(false);

    const handleClickVerDetalle = () => {
        if (ordenSeleccionada.sOrderId === "") {
            funcAlert("Seleccione una orden para continuar");
            return;
        }

        setModalDetalleOrdenOpen(true);
    };

    const handleClickDetalle = (sOrderId) => {
        setOrdenSeleccionada(entVentas.ResumenOrdenes.lstOrdenes.find((x) => x.sOrderId === sOrderId));
        setModalDetalleOrdenOpen(true);
    };

    useEffect(() => {
        setListaOrgenesConekta(
            entVentas.ResumenOrdenes.lstOrdenes.map((orden) => ({
                ...orden,
                nAmount: "$" + orden.nAmount.toLocaleString("en", { minimumFractionDigits: 2 }),
                nAmountDiscount: "$" + orden.nAmountDiscount.toLocaleString("en", { minimumFractionDigits: 2 }),
                nAmountTax: "$" + orden.nAmountTax.toLocaleString("en", { minimumFractionDigits: 2 }),
                nAmountPaid: "$" + orden.nAmountPaid.toLocaleString("en", { minimumFractionDigits: 2 }),
                sDetalle: (
                    <Tooltip title="Ver detalle" arrow placement="top">
                        <IconButton onClick={handleClickDetalle}>
                            <VisibilityIcon className="color-2" />
                        </IconButton>
                    </Tooltip>
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
            }))
        );
        // eslint-disable-next-line
    }, [entVentas]);

    return (
        <Fragment>
            <Grid container spacing={3}>
                <Grid item md={3} sm={6} xs={12} className="center">
                    <MeditocInfoNumber
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
                    <MeditocInfoNumber
                        label="ÓRDENES VENDIDAS"
                        value={entVentas.ResumenOrdenes.iTotalOrdenes}
                        color="color-6"
                    />
                </Grid>
                <Grid item md={3} sm={6} xs={12} className="center">
                    <MeditocInfoNumber
                        label="ÓRDENES RECHAZADAS"
                        value={entVentas.ResumenOrdenes.iTotalOrdenesRechazadas}
                        color="color-5"
                    />
                </Grid>
                <Grid item md={3} sm={6} xs={12} className="center">
                    <MeditocInfoNumber
                        label="FOLIOS GENERADOS"
                        value={entVentas.ResumenOrdenes.iTotalFolios}
                        color="color-3"
                    />
                </Grid>
                <Grid item xs={12}>
                    <MeditocTable
                        columns={columnas}
                        data={listaOrgenesConekta}
                        rowSelected={ordenSeleccionada}
                        setRowSelected={setOrdenSeleccionada}
                        doubleClick={handleClickVerDetalle}
                        mainField="sOrderId"
                    />
                </Grid>
            </Grid>
            <DetalleOrden
                entOrden={ordenSeleccionada}
                open={modalDetalleOrdenOpen}
                setOpen={setModalDetalleOrdenOpen}
                funcAlert={funcAlert}
                funcLoader={funcLoader}
                permisos={permisos}
            />
        </Fragment>
    );
};

ResumenConekta.propTypes = {
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

export default ResumenConekta;
