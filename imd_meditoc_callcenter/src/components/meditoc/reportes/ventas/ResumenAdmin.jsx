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

const ResumenAdmin = (props) => {
    const { entVentas, funcLoader, funcAlert, permisos } = props;

    const columnas = [
        { ...cellProps, title: "Orden", field: "sOrderId" },
        { ...cellProps, title: "Folio de empresa", field: "sFolioEmpresa" },
        { ...cellProps, title: "Total", field: "nAmountPaid" },
        { ...cellProps, title: "Nombre", field: "sNombre" },
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

    const [modalDetalleOrdenOpen, setModalDetalleOrdenOpen] = useState(false);

    const [listaOrdenesAdmin, setListaOrgenesAdmin] = useState([]);

    const handleClickVerDetalle = () => {
        if (ordenSeleccionada.sOrderId === "") {
            funcAlert("Seleccione una orden para continuar");
            return;
        }

        setModalDetalleOrdenOpen(true);
    };

    const handleClickDetalle = (sOrderId) => {
        setOrdenSeleccionada(entVentas.ResumenOrdenesAdmin.lstOrdenes.find((x) => x.sOrderId === sOrderId));
        setModalDetalleOrdenOpen(true);
    };

    useEffect(() => {
        setListaOrgenesAdmin(
            entVentas.ResumenOrdenesAdmin.lstOrdenes.map((orden) => ({
                ...orden,
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
                <Grid item sm={4} xs={12} className="center">
                    <MeditocInfoNumber
                        label="TOTAL DE VENTA"
                        value={
                            "$" +
                            entVentas.ResumenOrdenesAdmin.dTotalVendido.toLocaleString("en", {
                                minimumFractionDigits: 2,
                            })
                        }
                        color="color-1"
                    />
                </Grid>
                <Grid item sm={4} xs={12} className="center">
                    <MeditocInfoNumber
                        label="ÓRDENES VENDIDAS"
                        value={entVentas.ResumenOrdenesAdmin.iTotalOrdenes}
                        color="color-6"
                    />
                </Grid>
                <Grid item sm={4} xs={12} className="center">
                    <MeditocInfoNumber
                        label="FOLIOS GENERADOS"
                        value={entVentas.ResumenOrdenesAdmin.iTotalFolios}
                        color="color-3"
                    />
                </Grid>
                <Grid item xs={12} className="center">
                    <MeditocTable
                        columns={columnas}
                        data={listaOrdenesAdmin}
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

ResumenAdmin.propTypes = {
    entVentas: PropTypes.shape({
        ResumenOrdenesAdmin: PropTypes.shape({
            dTotalVendido: PropTypes.shape({
                toLocaleString: PropTypes.func,
            }),
            iTotalFolios: PropTypes.any,
            iTotalOrdenes: PropTypes.any,
            lstOrdenes: PropTypes.shape({
                find: PropTypes.func,
                map: PropTypes.func,
            }),
        }),
    }),
};

export default ResumenAdmin;
