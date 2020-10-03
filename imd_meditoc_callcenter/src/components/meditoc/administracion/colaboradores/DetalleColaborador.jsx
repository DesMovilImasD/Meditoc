import { Grid } from "@material-ui/core";
import React from "react";
import InfoField from "../../../utilidades/InfoField";
import MeditocModal from "../../../utilidades/MeditocModal";
import MeditocModalBotones from "../../../utilidades/MeditocModalBotones";
import MeditocSubtitulo from "../../../utilidades/MeditocSubtitulo";

const DetalleColaborador = (props) => {
    const { entColaborador, open, setOpen } = props;

    return (
        <MeditocModal title="Detalle de colaborador" size="small" open={open} setOpen={setOpen}>
            <Grid container spacing={3}>
                <Grid item xs={12}>
                    <MeditocSubtitulo title="DATOS DEL DIRECTORIO MÉDICO" />
                </Grid>
                <Grid item xs={6}>
                    <InfoField label="Nombre:" value={entColaborador.sNombreDirectorio} />
                </Grid>
                <Grid item xs={6}>
                    <InfoField label="Cédula profesional:" value={entColaborador.sCedulaProfecional} />
                </Grid>
                <Grid item xs={6}>
                    <InfoField label="RFC:" value={entColaborador.sRFC} />
                </Grid>
                <Grid item xs={6}>
                    <InfoField label="Teléfono de contacto:" value={entColaborador.sTelefonoDirectorio} />
                </Grid>
                <Grid item xs={6}>
                    <InfoField label="WhatsApp:" value={entColaborador.sWhatsApp} />
                </Grid>
                <Grid item xs={6}>
                    <InfoField label="Correo electrónico:" value={entColaborador.sCorreoDirectorio} />
                </Grid>
                <Grid item xs={6}>
                    <InfoField label="Especialidad:" value={entColaborador.sEspecialidad} />
                </Grid>
                <Grid item xs={6}>
                    <InfoField label="Número de sala IceLink:" value={entColaborador.iNumSala} />
                </Grid>
                <Grid item xs={6}>
                    <InfoField label="Dirección de consultorio:" value={entColaborador.sDireccionConsultorio} />
                </Grid>
                <Grid item xs={6}>
                    <InfoField label="Sitio Web:" value={entColaborador.sURL} />
                </Grid>
                <Grid item xs={6}>
                    <InfoField label="Mapa:" value={entColaborador.sMaps} />
                </Grid>
                <Grid item xs={12}>
                    <MeditocSubtitulo title="DATOS DE CUENTA" />
                </Grid>
                <Grid item xs={6}>
                    <InfoField label="Nombres:" value={entColaborador.sNombresDoctor} />
                </Grid>
                <Grid item xs={6}>
                    <InfoField label="Apellido paterno:" value={entColaborador.sApellidoPaternoDoctor} />
                </Grid>
                <Grid item xs={6}>
                    <InfoField label="Apellido materno:" value={entColaborador.sApellidoMaternoDoctor} />
                </Grid>
                <Grid item xs={6}>
                    <InfoField label="Fecha de nacimiento:" value={entColaborador.sFechaNacimientoDoctor} />
                </Grid>
                <Grid item xs={6}>
                    <InfoField label="Teléfono:" value={entColaborador.sTelefonoDoctor} />
                </Grid>
                <Grid item xs={6}>
                    <InfoField label="Correo electrónico:" value={entColaborador.sCorreoDoctor} />
                </Grid>
                <Grid item xs={6}>
                    <InfoField label="Domicilio particular:" value={entColaborador.sDomicilioDoctor} />
                </Grid>
                <Grid item xs={12}>
                    <MeditocSubtitulo title="USUARIOS" />
                </Grid>
                <Grid item xs={6}>
                    <InfoField label="Titular:" value={entColaborador.sUsuarioTitular} />
                </Grid>
                <Grid item xs={6}>
                    <InfoField label="Administrativo:" value={entColaborador.sUsuarioAdministrativo} />
                </Grid>
                <MeditocModalBotones okMessage="Cerrar detalle" hideCancel setOpen={setOpen} />
            </Grid>
        </MeditocModal>
    );
};

export default DetalleColaborador;
