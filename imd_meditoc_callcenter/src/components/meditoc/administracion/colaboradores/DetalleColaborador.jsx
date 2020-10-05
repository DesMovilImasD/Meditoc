import { Grid } from "@material-ui/core";
import MeditocInfoField from "../../../utilidades/MeditocInfoField";
import MeditocModal from "../../../utilidades/MeditocModal";
import MeditocModalBotones from "../../../utilidades/MeditocModalBotones";
import MeditocSubtitulo from "../../../utilidades/MeditocSubtitulo";
import PropTypes from "prop-types";
import React from "react";

const DetalleColaborador = (props) => {
    const { entColaborador, open, setOpen } = props;

    return (
        <MeditocModal title="Detalle de colaborador" size="small" open={open} setOpen={setOpen}>
            <Grid container spacing={3}>
                <Grid item xs={12}>
                    <MeditocSubtitulo title="DATOS DEL DIRECTORIO MÉDICO" />
                </Grid>
                <Grid item sm={6} xs={12}>
                    <MeditocInfoField label="Nombre:" value={entColaborador.sNombreDirectorio} />
                </Grid>
                <Grid item sm={6} xs={12}>
                    <MeditocInfoField label="Cédula profesional:" value={entColaborador.sCedulaProfecional} />
                </Grid>
                <Grid item sm={6} xs={12}>
                    <MeditocInfoField label="RFC:" value={entColaborador.sRFC} />
                </Grid>
                <Grid item sm={6} xs={12}>
                    <MeditocInfoField label="Teléfono de contacto:" value={entColaborador.sTelefonoDirectorio} />
                </Grid>
                <Grid item sm={6} xs={12}>
                    <MeditocInfoField label="WhatsApp:" value={entColaborador.sWhatsApp} />
                </Grid>
                <Grid item sm={6} xs={12}>
                    <MeditocInfoField label="Correo electrónico:" value={entColaborador.sCorreoDirectorio} />
                </Grid>
                <Grid item sm={6} xs={12}>
                    <MeditocInfoField label="Especialidad:" value={entColaborador.sEspecialidad} />
                </Grid>
                <Grid item sm={6} xs={12}>
                    <MeditocInfoField label="Número de sala IceLink:" value={entColaborador.iNumSala} />
                </Grid>
                <Grid item sm={6} xs={12}>
                    <MeditocInfoField label="Dirección de consultorio:" value={entColaborador.sDireccionConsultorio} />
                </Grid>
                <Grid item sm={6} xs={12}>
                    <MeditocInfoField label="Nombre de consultorio:" value={entColaborador.sNombreConsultorio} />
                </Grid>
                <Grid item sm={6} xs={12}>
                    <MeditocInfoField label="Sitio Web:" value={entColaborador.sURL} />
                </Grid>
                <Grid item sm={6} xs={12}>
                    <MeditocInfoField label="Mapa:" value={entColaborador.sMaps} />
                </Grid>
                <Grid item xs={12}>
                    <MeditocSubtitulo title="DATOS DE CUENTA" />
                </Grid>
                <Grid item sm={6} xs={12}>
                    <MeditocInfoField label="Nombres:" value={entColaborador.sNombresDoctor} />
                </Grid>
                <Grid item sm={6} xs={12}>
                    <MeditocInfoField label="Apellido paterno:" value={entColaborador.sApellidoPaternoDoctor} />
                </Grid>
                <Grid item sm={6} xs={12}>
                    <MeditocInfoField label="Apellido materno:" value={entColaborador.sApellidoMaternoDoctor} />
                </Grid>
                <Grid item sm={6} xs={12}>
                    <MeditocInfoField label="Fecha de nacimiento:" value={entColaborador.sFechaNacimientoDoctor} />
                </Grid>
                <Grid item sm={6} xs={12}>
                    <MeditocInfoField label="Teléfono:" value={entColaborador.sTelefonoDoctor} />
                </Grid>
                <Grid item sm={6} xs={12}>
                    <MeditocInfoField label="Correo electrónico:" value={entColaborador.sCorreoDoctor} />
                </Grid>
                <Grid item sm={6} xs={12}>
                    <MeditocInfoField label="Domicilio particular:" value={entColaborador.sDomicilioDoctor} />
                </Grid>
                <Grid item xs={12}>
                    <MeditocSubtitulo title="USUARIOS" />
                </Grid>
                <Grid item sm={6} xs={12}>
                    <MeditocInfoField label="Titular:" value={entColaborador.sUsuarioTitular} />
                </Grid>
                <Grid item sm={6} xs={12}>
                    <MeditocInfoField label="Administrativo:" value={entColaborador.sUsuarioAdministrativo} />
                </Grid>
                <MeditocModalBotones okMessage="Cerrar detalle" hideCancel setOpen={setOpen} />
            </Grid>
        </MeditocModal>
    );
};

DetalleColaborador.propTypes = {
    entColaborador: PropTypes.shape({
        iNumSala: PropTypes.any,
        sApellidoMaternoDoctor: PropTypes.any,
        sApellidoPaternoDoctor: PropTypes.any,
        sCedulaProfecional: PropTypes.any,
        sCorreoDirectorio: PropTypes.any,
        sCorreoDoctor: PropTypes.any,
        sDireccionConsultorio: PropTypes.any,
        sDomicilioDoctor: PropTypes.any,
        sEspecialidad: PropTypes.any,
        sFechaNacimientoDoctor: PropTypes.any,
        sMaps: PropTypes.any,
        sNombreDirectorio: PropTypes.any,
        sNombresDoctor: PropTypes.any,
        sRFC: PropTypes.any,
        sTelefonoDirectorio: PropTypes.any,
        sTelefonoDoctor: PropTypes.any,
        sURL: PropTypes.any,
        sUsuarioAdministrativo: PropTypes.any,
        sUsuarioTitular: PropTypes.any,
        sWhatsApp: PropTypes.any,
    }),
    open: PropTypes.any,
    setOpen: PropTypes.any,
};

export default DetalleColaborador;
