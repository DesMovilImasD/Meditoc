import PropTypes from "prop-types";
import React from "react";
import MeditocModal from "../../../utilidades/MeditocModal";
import { Grid } from "@material-ui/core";
import InfoField from "../../../utilidades/InfoField";
import MeditocModalBotones from "../../../utilidades/MeditocModalBotones";

const DetalleProducto = (props) => {
    const { entProducto, open, setOpen } = props;

    return (
        <MeditocModal title="Detalle de producto" size="normal" open={open} setOpen={setOpen}>
            <Grid container spacing={3}>
                <Grid item sm={6} xs={12}>
                    <InfoField label="ID de producto:" value={entProducto.iIdProducto} />
                </Grid>
                <Grid item sm={6} xs={12}>
                    <InfoField label="Tipo de producto:" value={entProducto.sTipoProducto} />
                </Grid>
                <Grid item sm={6} xs={12}>
                    <InfoField label="Nombre:" value={entProducto.sNombre} />
                </Grid>
                <Grid item sm={6} xs={12}>
                    <InfoField label="Nombre corto:" value={entProducto.sNombreCorto} />
                </Grid>
                <Grid item sm={6} xs={12}>
                    <InfoField label="Descripción:" value={entProducto.sDescripcion} />
                </Grid>
                <Grid item sm={6} xs={12}>
                    <InfoField label="Costo:" value={entProducto.sCosto} />
                </Grid>
                <Grid item sm={6} xs={12}>
                    <InfoField label="Meses de vigencia:" value={entProducto.iMesVigencia} />
                </Grid>
                <Grid item sm={6} xs={12}>
                    <InfoField label="Ícono:" value={entProducto.sIcon} />
                </Grid>
                <Grid item sm={6} xs={12}>
                    <InfoField label="Comercial:" value={entProducto.sComercial} />
                </Grid>
                <Grid item sm={6} xs={12}>
                    <InfoField label="Foliador:" value={entProducto.sPrefijoFolio} />
                </Grid>
                <MeditocModalBotones okMessage="Aceptar" setOpen={setOpen} hideCancel />
            </Grid>
        </MeditocModal>
    );
};

DetalleProducto.propTypes = {
    entProducto: PropTypes.shape({
        iIdProducto: PropTypes.any,
        iMesVigencia: PropTypes.any,
        sComercial: PropTypes.any,
        sCosto: PropTypes.any,
        sDescripcion: PropTypes.any,
        sIcon: PropTypes.any,
        sNombre: PropTypes.any,
        sNombreCorto: PropTypes.any,
        sPrefijoFolio: PropTypes.any,
        sTipoProducto: PropTypes.any,
    }),
    open: PropTypes.any,
    setOpen: PropTypes.any,
};

export default DetalleProducto;
