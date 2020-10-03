import PropTypes from "prop-types";
import { Button, Grid } from "@material-ui/core";
import React, { Fragment, useState } from "react";
import MeditocTable from "../../utilidades/MeditocTable";
import ResumenEmpresaDetalle from "./ResumenEmpresaDetalle";
import ResumeNumero from "./ResumeNumero";

const ResumenEmpresas = (props) => {
    const { entVentas } = props;

    const columnas = [
        { title: "Folio de empresa", field: "sFolioEmpresa", align: "center" },
        { title: "Nombre", field: "sNombre", align: "center" },
        { title: "Total", field: "dTotal", align: "center" },
        { title: "Folios generados", field: "iTotalFolios", align: "center" },
        { title: "Ver", field: "sDetalle", align: "center" },
    ];

    const [empresaSeleccionada, setEmpresaSeleccionada] = useState({
        sFolioEmpresa: "",
        sCorreo: "",
        dTotalSinIva: 0,
        dTotalIva: 0,
        dTotal: 0,
        iTotalFolios: 0,
        lstProductos: [],
    });

    const [modalDetalleEmpresaOpen, setModalDetalleEmpresaOpen] = useState(false);

    const handleClickDetalle = (sFolioEmpresa) => {
        setEmpresaSeleccionada(entVentas.ResumenEmpresas.lstEmpresas.find((x) => x.sFolioEmpresa === sFolioEmpresa));
        setModalDetalleEmpresaOpen(true);
    };

    return (
        <Fragment>
            <Grid container spacing={3}>
                <Grid item md={4} sm={6} xs={12} className="center">
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
                </Grid>
                <Grid item xs={12} className="center">
                    <MeditocTable
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
                    />
                </Grid>
            </Grid>
            <ResumenEmpresaDetalle
                entEmpresa={empresaSeleccionada}
                open={modalDetalleEmpresaOpen}
                setOpen={setModalDetalleEmpresaOpen}
            />
        </Fragment>
    );
};

ResumenEmpresas.propTypes = {
    entVentas: PropTypes.shape({
        ResumenEmpresas: PropTypes.shape({
            dTotalVendido: PropTypes.shape({
                toLocaleString: PropTypes.func,
            }),
            iTotalEmpresas: PropTypes.any,
            iTotalFolios: PropTypes.any,
            lstEmpresas: PropTypes.shape({
                find: PropTypes.func,
                map: PropTypes.func,
            }),
        }),
    }),
};

export default ResumenEmpresas;
