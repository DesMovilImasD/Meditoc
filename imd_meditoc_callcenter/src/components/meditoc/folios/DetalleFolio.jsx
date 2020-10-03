import PropTypes from "prop-types";
import { Grid } from "@material-ui/core";
import React from "react";
import InfoField from "../../utilidades/InfoField";
import MeditocModal from "../../utilidades/MeditocModal";
import MeditocModalBotones from "../../utilidades/MeditocModalBotones";
import MeditocSubtitulo from "../../utilidades/MeditocSubtitulo";

const DetalleFolio = (props) => {
    const { entFolio, open, setOpen } = props;

    return (
        <MeditocModal title="Detalle de folio" size="small" open={open} setOpen={setOpen}>
            <Grid container spacing={3}>
                <Grid item xs={12}>
                    <MeditocSubtitulo title="DATOS DE FOLIO" />
                </Grid>
                <Grid item sm={6} xs={12}>
                    <InfoField label="ID de folio:" value={entFolio.iIdFolio} />
                </Grid>
                <Grid item sm={6} xs={12}>
                    <InfoField label="Folio:" value={entFolio.sFolio} />
                </Grid>
                <Grid item sm={6} xs={12}>
                    <InfoField label="Origen:" value={entFolio.sOrigen} />
                </Grid>
                <Grid item sm={6} xs={12}>
                    <InfoField label="Orden de compra:" value={entFolio.sOrdenConekta} />
                </Grid>
                <Grid item sm={6} xs={12}>
                    <InfoField label="Fecha de registro:" value={entFolio.sFechaCreacion} />
                </Grid>
                <Grid item sm={6} xs={12}>
                    <InfoField label="Fecha de vencimiento:" value={entFolio.sFechaVencimiento} />
                </Grid>
                <Grid item xs={12}>
                    <MeditocSubtitulo title="DATOS DEL PRODUCTO" />
                </Grid>
                <Grid item sm={6} xs={12}>
                    <InfoField label="Nombre de producto:" value={entFolio.sNombreProducto} />
                </Grid>
                <Grid item sm={6} xs={12}>
                    <InfoField label="Tipo de producto:" value={entFolio.sTipoProducto} />
                </Grid>
                <Grid item xs={12}>
                    <MeditocSubtitulo title="DATOS DEL PACIENTE" />
                </Grid>
                <Grid item xs={12}>
                    <InfoField label="Nombre:" value={entFolio.sNombrePaciente} />
                </Grid>
                <Grid item sm={6} xs={12}>
                    <InfoField label="Teléfono:" value={entFolio.sTelefonoPaciente} />
                </Grid>
                <Grid item sm={6} xs={12}>
                    <InfoField label="Correo:" value={entFolio.sCorreoPaciente} />
                </Grid>
                <Grid item xs={12}>
                    <MeditocSubtitulo title="DATOS DE EMPRESA O CLIENTE" />
                </Grid>
                <Grid item xs={12}>
                    <InfoField label="Nombre de empresa:" value={entFolio.sNombreEmpresa} />
                </Grid>
                <Grid item sm={6} xs={12}>
                    <InfoField label="Folio de empresa:" value={entFolio.sFolioEmpresa} />
                </Grid>
                <Grid item sm={6} xs={12}>
                    <InfoField label="Correo de empresa:" value={entFolio.sCorreoEmpresa} />
                </Grid>
                <MeditocModalBotones hideCancel okMessage="Cerrar detalle" setOpen={setOpen} />
            </Grid>
        </MeditocModal>
    );
};

DetalleFolio.propTypes = {
    entFolio: PropTypes.shape({
        iIdFolio: PropTypes.any,
        sCorreoEmpresa: PropTypes.any,
        sCorreoPaciente: PropTypes.any,
        sFechaCreacion: PropTypes.any,
        sFechaVencimiento: PropTypes.any,
        sFolio: PropTypes.any,
        sFolioEmpresa: PropTypes.any,
        sNombreEmpresa: PropTypes.any,
        sNombrePaciente: PropTypes.any,
        sNombreProducto: PropTypes.any,
        sOrdenConekta: PropTypes.any,
        sOrigen: PropTypes.any,
        sTelefonoPaciente: PropTypes.any,
        sTipoProducto: PropTypes.any,
    }),
    open: PropTypes.any,
    setOpen: PropTypes.any,
};

export default DetalleFolio;