import { Grid } from "@material-ui/core";
import MeditocInfoField from "../../../utilidades/MeditocInfoField";
import MeditocModal from "../../../utilidades/MeditocModal";
import MeditocModalBotones from "../../../utilidades/MeditocModalBotones";
import PropTypes from "prop-types";
import React from "react";

const DetalleCupon = (props) => {
    const { entCupon, open, setOpen } = props;

    return (
        <MeditocModal title="Detalle de cupón" size="small" open={open} setOpen={setOpen}>
            <Grid container spacing={3}>
                <Grid item sm={6} xs={12}>
                    <MeditocInfoField label="ID de cupón" value={entCupon.fiIdCupon} />
                </Grid>
                <Grid item sm={6} xs={12}>
                    <MeditocInfoField label="Código de cupón:" value={entCupon.fsCodigo} />
                </Grid>
                <Grid item sm={6} xs={12}>
                    <MeditocInfoField label="Descripción:" value={entCupon.fsDescripcion} />
                </Grid>
                <Grid item sm={6} xs={12}>
                    <MeditocInfoField label="Tipo de cupón:" value={entCupon.fsDescripcionCategoria} />
                </Grid>
                <Grid item sm={6} xs={12}>
                    <MeditocInfoField label="Monto de descuento:" value={entCupon.sMontoDescuento} />
                </Grid>
                <Grid item sm={6} xs={12}>
                    <MeditocInfoField label="Porcentaje de descuento:" value={entCupon.sPorcentajeDescuento} />
                </Grid>
                <Grid item sm={6} xs={12}>
                    <Grid item sm={6} xs={12}>
                        <MeditocInfoField label="Total de cupones:" value={entCupon.fiTotalLanzamiento} />
                    </Grid>
                    <Grid item sm={6} xs={12}>
                        <MeditocInfoField label="Total canjeado:" value={entCupon.fiTotalCanjeado} />
                    </Grid>
                    <MeditocInfoField label="Fecha de creación:" value={entCupon.sFechaCreacion} />
                </Grid>
                <Grid item sm={6} xs={12}>
                    <MeditocInfoField label="Fecha de vencimiento:" value={entCupon.sFechaVencimiento} />
                </Grid>
                <MeditocModalBotones hideCancel okMessage="Cerrar detalle" setOpen={setOpen} />
            </Grid>
        </MeditocModal>
    );
};

DetalleCupon.propTypes = {
    entCupon: PropTypes.shape({
        fiIdCupon: PropTypes.any,
        fiTotalCanjeado: PropTypes.any,
        fiTotalLanzamiento: PropTypes.any,
        fsCodigo: PropTypes.any,
        fsDescripcion: PropTypes.any,
        fsDescripcionCategoria: PropTypes.any,
        sFechaCreacion: PropTypes.any,
        sFechaVencimiento: PropTypes.any,
        sMontoDescuento: PropTypes.any,
        sPorcentajeDescuento: PropTypes.any,
    }),
    open: PropTypes.any,
    setOpen: PropTypes.any,
};

export default DetalleCupon;
