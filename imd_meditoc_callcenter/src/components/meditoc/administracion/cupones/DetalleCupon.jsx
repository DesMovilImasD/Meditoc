import { Grid } from "@material-ui/core";
import MeditocInfoField from "../../../utilidades/MeditocInfoField";
import MeditocModal from "../../../utilidades/MeditocModal";
import MeditocModalBotones from "../../../utilidades/MeditocModalBotones";
import PropTypes from "prop-types";
import React from "react";

/*************************************************************
 * Descripcion: Modal para visualizar los detalles de un folio
 * Creado: Cristopher Noh
 * Fecha: 26/08/2020
 * Invocado desde: Cupones
 *************************************************************/
const DetalleCupon = (props) => {
    //PROPS
    const { entCupon, open, setOpen } = props;

    const {
        fiIdCupon,
        fsCodigo,
        fsDescripcion,
        fsDescripcionCategoria,
        sMontoDescuento,
        sPorcentajeDescuento,
        fiTotalLanzamiento,
        fiTotalCanjeado,
        sFechaCreacion,
        sFechaVencimiento,
    } = entCupon;

    return (
        <MeditocModal title="Detalle de cupón" size="small" open={open} setOpen={setOpen}>
            <Grid container spacing={3}>
                <Grid item sm={6} xs={12}>
                    <MeditocInfoField label="ID de cupón" value={fiIdCupon} />
                </Grid>
                <Grid item sm={6} xs={12}>
                    <MeditocInfoField label="Código de cupón:" value={fsCodigo} />
                </Grid>
                <Grid item sm={6} xs={12}>
                    <MeditocInfoField label="Descripción:" value={fsDescripcion} />
                </Grid>
                <Grid item sm={6} xs={12}>
                    <MeditocInfoField label="Tipo de cupón:" value={fsDescripcionCategoria} />
                </Grid>
                <Grid item sm={6} xs={12}>
                    <MeditocInfoField label="Monto de descuento:" value={sMontoDescuento} />
                </Grid>
                <Grid item sm={6} xs={12}>
                    <MeditocInfoField label="Porcentaje de descuento:" value={sPorcentajeDescuento} />
                </Grid>
                <Grid item sm={6} xs={12}>
                    <Grid item sm={6} xs={12}>
                        <MeditocInfoField label="Total de cupones:" value={fiTotalLanzamiento} />
                    </Grid>
                    <Grid item sm={6} xs={12}>
                        <MeditocInfoField label="Total canjeado:" value={fiTotalCanjeado} />
                    </Grid>
                    <MeditocInfoField label="Fecha de creación:" value={sFechaCreacion} />
                </Grid>
                <Grid item sm={6} xs={12}>
                    <MeditocInfoField label="Fecha de vencimiento:" value={sFechaVencimiento} />
                </Grid>
                <MeditocModalBotones hideCancel okMessage="Cerrar detalle" setOpen={setOpen} />
            </Grid>
        </MeditocModal>
    );
};

DetalleCupon.propTypes = {
    entCupon: PropTypes.object,
    open: PropTypes.bool,
    setOpen: PropTypes.func,
};

export default DetalleCupon;
