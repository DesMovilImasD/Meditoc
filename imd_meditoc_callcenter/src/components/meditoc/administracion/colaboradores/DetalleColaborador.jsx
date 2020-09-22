import { Divider, Grid } from "@material-ui/core";
import React from "react";
import InfoField from "../../../utilidades/InfoField";
import MeditocModal from "../../../utilidades/MeditocModal";
import MeditocModalBotones from "../../../utilidades/MeditocModalBotones";

const DetalleColaborador = (props) => {
    const { entColaborador, open, setOpen } = props;
    // bActivo: true;
    // bBaja: false;
    // bOcupado: false;
    // bOnline: false;
    // dtFechaCreacion: "2020-09-21T11:51:10";
    // dtFechaNacimientoDoctor: "1986-05-23T00:00:00";
    // iIdColaborador: 5;
    // iIdEspecialidad: 14;
    // iIdTipoCuenta: 1;
    // iIdTipoDoctor: 2;
    // iIdUsuarioCGU: 6;
    // iNumSala: 214;
    // sApellidoMaternoDoctor: "Sosa";
    // sApellidoPaternoDoctor: "Gongora";
    // sCedulaProfecional: "8375823";
    // sCorreoDirectorio: "alberto@correo.com";
    // sCorreoDoctor: "carlosg@correo.com";
    // sDireccionConsultorio: "carlosg@correo.com";
    // sDomicilioDoctor: "Calle 24 #91 entre 30 y 32, Residencial pensiones.";
    // sEspecialidad: "Pediatría";
    // sFechaCreacion: "21/09/2020 11:51";
    // sFechaNacimientoDoctor: "23/05/1986";
    // sMaps: "https://goo.gl/maps/1L4oDyNv5FHCntMT9";
    // sNombreDirectorio: "Alberto Gongora Sosa";
    // sNombresDoctor: "Alberto Gabriel";
    // sPasswordAdministrativo: "wmtn5Wt7Dx74gZWIjDbTJQ==";
    // sPasswordTitular: "wmtn5Wt7Dx74gZWIjDbTJQ==";
    // sRFC: "JASFO325129";
    // sTelefonoDirectorio: "991 391 8419";
    // sTelefonoDoctor: "999 102 5825";
    // sTipoCuenta: "Titular";
    // sTipoDoctor: "Médico Especialista";
    // sURL: "www.prueba.com.mx";
    // sUsuarioAdministrativo: "aortiz1";
    // sUsuarioTitular: "agongora";
    return (
        <MeditocModal title="Detalle de colaborador" size="normal" open={open} setOpen={setOpen}>
            <Grid container spacing={3}>
                <Grid item xs={12}>
                    <span className="rob-nor bold size-15 color-4">DATOS DEL DIRECTORIO MÉDICO</span>
                    <Divider />
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
                    <span className="rob-nor bold size-15 color-4">DATOS DE CUENTA</span>
                    <Divider />
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
                    <span className="rob-nor bold size-15 color-4">USUARIOS</span>
                    <Divider />
                </Grid>
                <Grid item xs={6}>
                    <InfoField label="Titular:" value={entColaborador.sUsuarioTitular} />
                </Grid>
                <Grid item xs={6}>
                    <InfoField label="Administrativo:" value={entColaborador.sUsuarioAdministrativo} />
                </Grid>
                <MeditocModalBotones okMessage="Aceptar" hideCancel setOpen={setOpen} />
            </Grid>
        </MeditocModal>
    );
};

export default DetalleColaborador;
