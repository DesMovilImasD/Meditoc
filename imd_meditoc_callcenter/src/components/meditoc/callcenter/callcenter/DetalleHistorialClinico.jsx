import { Grid } from "@material-ui/core";
import MeditocInfoField from "../../../utilidades/MeditocInfoField";
import MeditocModal from "../../../utilidades/MeditocModal";
import MeditocModalBotones from "../../../utilidades/MeditocModalBotones";
import PropTypes from "prop-types";
import React from "react";

const DetalleHistorialClinico = (props) => {
    const { open, setOpen, historial } = props;

    return (
        <MeditocModal title="Detalle de la consulta" size="small" open={open} setOpen={setOpen} level={3}>
            <Grid container spacing={3}>
                <Grid item xs={12}>
                    <MeditocInfoField label="Peso:" value={historial.fPeso} />
                </Grid>
                <Grid item xs={12}>
                    <MeditocInfoField label="Altura:" value={historial.fAltura} />
                </Grid>
                <Grid item xs={12}>
                    <MeditocInfoField label="Alergias:" value={historial.sAlergias} />
                </Grid>
                <Grid item xs={12}>
                    <MeditocInfoField label="Síntomas:" value={historial.sSintomas} />
                </Grid>
                <Grid item xs={12}>
                    <MeditocInfoField label="Diagnóstico:" value={historial.sDiagnostico} />
                </Grid>
                <Grid item xs={12}>
                    <MeditocInfoField label="Tratamiento:" value={historial.sTratamiento} />
                </Grid>
                <Grid item xs={12}>
                    <MeditocInfoField label="Observaciones y comentarios:" value={historial.sComentarios} />
                </Grid>
                <Grid item xs={12}>
                    <MeditocInfoField label="Inicio de consulta:" value={historial.sFechaConsultaInicio} />
                </Grid>
                <Grid item xs={12}>
                    <MeditocInfoField label="Fin de consulta:" value={historial.sFechaConsultaFin} />
                </Grid>
                <Grid item xs={12}>
                    <MeditocInfoField label="Duración de consulta:" value={historial.sDuracionConsulta} />
                </Grid>
                <MeditocModalBotones hideCancel okMessage="Cerrar detalle" open={open} setOpen={setOpen} />
            </Grid>
        </MeditocModal>
    );
};

DetalleHistorialClinico.propTypes = {
    historial: PropTypes.shape({
        fAltura: PropTypes.any,
        fPeso: PropTypes.any,
        sAlergias: PropTypes.any,
        sComentarios: PropTypes.any,
        sDiagnostico: PropTypes.any,
        sDuracionConsulta: PropTypes.any,
        sFechaConsultaFin: PropTypes.any,
        sFechaConsultaInicio: PropTypes.any,
        sSintomas: PropTypes.any,
        sTratamiento: PropTypes.any,
    }),
    open: PropTypes.any,
    setOpen: PropTypes.any,
};

export default DetalleHistorialClinico;
