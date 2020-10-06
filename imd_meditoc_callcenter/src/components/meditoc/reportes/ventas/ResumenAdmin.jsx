import { Button, Grid } from "@material-ui/core";
import React, { Fragment, useState } from "react";
import { green, red } from "@material-ui/core/colors";

import CloseIcon from "@material-ui/icons/Close";
import DetalleConekta from "./DetalleConekta";
import DoneIcon from "@material-ui/icons/Done";
import { EnumStatusConekta } from "../../../../configurations/enumConfig";
import MeditocInfoNumber from "../../../utilidades/MeditocInfoNumber";
import MeditocTable from "../../../utilidades/MeditocTable";
import PropTypes from "prop-types";

const ResumenAdmin = (props) => {
    const { entVentas } = props;

    const columnas = [
        { title: "Orden", field: "sOrderId", align: "center" },
        { title: "Folio de empresa", field: "sFolioEmpresa", align: "center" },
        { title: "Total", field: "nAmountPaid", align: "center" },
        { title: "Nombre", field: "sNombre", align: "center" },
        //{ title: "Total", field: "dTotal", align: "center" },
        //{ title: "Folios generados", field: "iTotalFolios", align: "center" },
        { title: "Fecha", field: "sRegisterDate", align: "center" },
        { title: "Ver", field: "sDetalle", align: "center" },
    ];

    // const [empresaSeleccionada, setEmpresaSeleccionada] = useState({
    //     sFolioEmpresa: "",
    //     sCorreo: "",
    //     dTotalSinIva: 0,
    //     dTotalIva: 0,
    //     dTotal: 0,
    //     iTotalFolios: 0,
    //     lstProductos: [],
    // });

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

    // const [modalDetalleEmpresaOpen, setModalDetalleEmpresaOpen] = useState(false);

    // const handleClickDetalle = (sFolioEmpresa) => {
    //     setEmpresaSeleccionada(entVentas.ResumenEmpresas.lstEmpresas.find((x) => x.sFolioEmpresa === sFolioEmpresa));
    //     setModalDetalleEmpresaOpen(true);
    // };

    const [modalDetalleOrdenOpen, setModalDetalleOrdenOpen] = useState(false);

    const handleClickDetalle = (sOrderId) => {
        setOrdenSeleccionada(entVentas.ResumenOrdenesAdmin.lstOrdenes.find((x) => x.sOrderId === sOrderId));
        setModalDetalleOrdenOpen(true);
    };

    return (
        <Fragment>
            <Grid container spacing={3}>
                {/* <Grid item md={4} sm={6} xs={12} className="center">
                    <ResumeNumero
                        label="TOTAL DE VENTA"
                        value={
                            "$" +
                            entVentas.ResumenEmpresas.dTotalVendido.toLocaleString("en", {
                                minimumFractionDigits: 2,
                            })
                        }
                        color="color-1"
                    />
                </Grid>
                <Grid item md={4} sm={6} xs={12} className="center">
                    <ResumeNumero
                        label="TOTAL DE EMPRESAS"
                        value={entVentas.ResumenEmpresas.iTotalEmpresas}
                        color="color-3"
                    />
                </Grid>
                <Grid item md={4} sm={6} xs={12} className="center">
                    <ResumeNumero
                        label="FOLIOS GENERADOS"
                        value={entVentas.ResumenEmpresas.iTotalFolios}
                        color="color-3"
                    />
                </Grid> */}
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
                    {/* <MeditocTable
                        columns={columnas}
                        data={entVentas.ResumenEmpresas.lstEmpresas.map((empresa) => ({
                            ...empresa,
                            dTotal: "$" + empresa.dTotal.toLocaleString("en", { minimumFractionDigits: 2 }),
                            sDetalle: (
                                <Button
                                    variant="contained"
                                    color="primary"
                                    onClick={() => handleClickDetalle(empresa.sFolioEmpresa)}
                                >
                                    Detalle
                                </Button>
                            ),
                        }))}
                        rowClick={false}
                        mainField="sFolioEmpresa"
                        search={false}
                    /> */}
                    <MeditocTable
                        columns={columnas}
                        data={entVentas.ResumenOrdenesAdmin.lstOrdenes.map((orden) => ({
                            ...orden,
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
            {/* <ResumenEmpresaDetalle
                entEmpresa={empresaSeleccionada}
                open={modalDetalleEmpresaOpen}
                setOpen={setModalDetalleEmpresaOpen}
            /> */}
            <DetalleConekta
                entOrden={ordenSeleccionada}
                open={modalDetalleOrdenOpen}
                setOpen={setModalDetalleOrdenOpen}
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
