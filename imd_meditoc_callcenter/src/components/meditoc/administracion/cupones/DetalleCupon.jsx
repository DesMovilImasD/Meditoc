import PropTypes from "prop-types";
import { Grid } from "@material-ui/core";
import React from "react";
import InfoField from "../../../utilidades/InfoField";
import MeditocModal from "../../../utilidades/MeditocModal";
import MeditocModalBotones from "../../../utilidades/MeditocModalBotones";

const DetalleCupon = (props) => {
    const { entCupon, open, setOpen } = props;

    return (
        <MeditocModal title="Detalle de cupón" size="small" open={open} setOpen={setOpen}>
            <Grid container spacing={3}>
                <Grid item sm={6} xs={12}>
                    <InfoField label="ID de cupón" value={entCupon.fiIdCupon} />
                </Grid>
                <Grid item sm={6} xs={12}>
                    <InfoField label="Código de cupón:" value={entCupon.fsCodigo} />
                </Grid>
                <Grid item sm={6} xs={12}>
                    <InfoField label="Descripción:" value={entCupon.fsDescripcion} />
                </Grid>
                <Grid item sm={6} xs={12}>
                    <InfoField label="Tipo de cupón:" value={entCupon.fsDescripcionCategoria} />
                </Grid>
                <Grid item sm={6} xs={12}>
                    <InfoField label="Monto de descuento:" value={entCupon.sMontoDescuento} />
                </Grid>
                <Grid item sm={6} xs={12}>
                    <InfoField label="Porcentaje de descuento:" value={entCupon.sPorcentajeDescuento} />
                </Grid>
                <Grid item sm={6} xs={12}>
                    <Grid item sm={6} xs={12}>
                        <InfoField label="Total de cupones:" value={entCupon.fiTotalLanzamiento} />
                    </Grid>
                    <Grid item sm={6} xs={12}>
                        <InfoField label="Total canjeado:" value={entCupon.fiTotalCanjeado} />
                    </Grid>
                    <InfoField label="Fecha de creación:" value={entCupon.sFechaCreacion} />
                </Grid>
                <Grid item sm={6} xs={12}>
                    <InfoField label="Fecha de vencimiento:" value={entCupon.sFechaVencimiento} />
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
