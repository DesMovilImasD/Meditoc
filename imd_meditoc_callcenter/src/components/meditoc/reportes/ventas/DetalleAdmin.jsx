import { Grid } from "@material-ui/core";
import MeditocInfoField from "../../../utilidades/MeditocInfoField";
import MeditocInfoResumen from "../../../utilidades/MeditocInfoResumen";
import MeditocModal from "../../../utilidades/MeditocModal";
import MeditocModalBotones from "../../../utilidades/MeditocModalBotones";
import MeditocSubtitulo from "../../../utilidades/MeditocSubtitulo";
import MeditocTableSimple from "../../../utilidades/MeditocTableSimple";
import PropTypes from "prop-types";
import React from "react";

const DetalleAdmin = (props) => {
    const { entEmpresa, open, setOpen } = props;

    const columnas = [
        { title: "Folio", field: "sFolio", align: "center" },
        { title: "Producto", field: "sNombre", align: "center" },
        { title: "Precio", field: "nUnitPrice", align: "center" },
        { title: "Tipo", field: "sTipoProducto", align: "center" },
        { title: "Vencimiento", field: "sFechaVencimiento", align: "center" },
    ];

    return (
        <MeditocModal title={"Detalle de orden " + entEmpresa.sFolioEmpresa} size="large" open={open} setOpen={setOpen}>
            <Grid container spacing={3}>
                <Grid item xs={12}>
                    <MeditocInfoResumen
                        label="Subtotal"
                        value={"$" + entEmpresa.dTotalSinIva.toLocaleString("en", { minimumFractionDigits: 2 })}
                    />
                    <MeditocInfoResumen
                        label="IVA"
                        value={"+$" + entEmpresa.dTotalIva.toLocaleString("en", { minimumFractionDigits: 2 })}
                    />
                    <MeditocInfoResumen
                        label="Total pagado"
                        value={entEmpresa.dTotal.toLocaleString("en", { minimumFractionDigits: 2 })}
                    />
                </Grid>
                <Grid item xs={12}>
                    <MeditocSubtitulo title="DATOS DE EMPRESA" />
                </Grid>
                <Grid item sm={6} xs={12}>
                    <MeditocInfoField label="Folio:" value={entEmpresa.sFolioEmpresa} />
                </Grid>
                <Grid item sm={6} xs={12}>
                    <MeditocInfoField label="Nombre:" value={entEmpresa.sNombre} />
                </Grid>
                <Grid item sm={6} xs={12}>
                    <MeditocInfoField label="Correo:" value={entEmpresa.sCorreo} />
                </Grid>
                <Grid item sm={6} xs={12}>
                    <MeditocInfoField label="Folios generados:" value={entEmpresa.iTotalFolios} />
                </Grid>
                <Grid item xs={12}>
                    <MeditocSubtitulo title="FOLIOS" />
                </Grid>
                <Grid item xs={12}>
                    <MeditocTableSimple
                        columns={columnas}
                        data={entEmpresa.lstProductos.map((folio) => ({
                            ...folio,
                            sFolio: folio.entFolio.sFolio,
                            sFechaVencimiento: folio.entFolio.sFechaVencimiento,
                            nUnitPrice: "$" + folio.nUnitPrice.toLocaleString("en", { minimumFractionDigits: 2 }),
                        }))}
                    />
                </Grid>
                <MeditocModalBotones cancelMessage="Cerrar detalle de empresa" setOpen={setOpen} hideOk />
            </Grid>
        </MeditocModal>
    );
};

DetalleAdmin.propTypes = {
    entEmpresa: PropTypes.shape({
        dTotal: PropTypes.shape({
            toLocaleString: PropTypes.func,
        }),
        dTotalIva: PropTypes.shape({
            toLocaleString: PropTypes.func,
        }),
        dTotalSinIva: PropTypes.shape({
            toLocaleString: PropTypes.func,
        }),
        iTotalFolios: PropTypes.any,
        lstProductos: PropTypes.shape({
            map: PropTypes.func,
        }),
        sCorreo: PropTypes.any,
        sFolioEmpresa: PropTypes.any,
        sNombre: PropTypes.any,
    }),
    open: PropTypes.any,
    setOpen: PropTypes.any,
};

export default DetalleAdmin;
