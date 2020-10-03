import PropTypes from "prop-types";
import React, { Fragment } from "react";
import MeditocModal from "../../../utilidades/MeditocModal";
import { Grid, TextField, MenuItem, IconButton, Tooltip, Typography, InputAdornment } from "@material-ui/core";
import { useState } from "react";
import MeditocTabHeader from "../../../utilidades/MeditocTabHeader";
import MeditocTabBody from "../../../utilidades/MeditocTabBody";
import MeditocTabPanel from "../../../utilidades/MeditocTabPanel";
import InputTelefono from "../../../utilidades/InputTelefono";
import { DatePicker } from "@material-ui/pickers";
import MeditocModalBotones from "../../../utilidades/MeditocModalBotones";
import { rxCorreo, rxUrl } from "../../../../configurations/regexConfig";
import ColaboradorController from "../../../../controllers/ColaboradorController";
import VisibilityOffIcon from "@material-ui/icons/VisibilityOff";
import VisibilityIcon from "@material-ui/icons/Visibility";
import DateRangeIcon from "@material-ui/icons/DateRange";
import InfoIcon from "@material-ui/icons/Info";
import { useEffect } from "react";
import { EnumEspecialidadPrincipal, EnumTipoDoctor } from "../../../../configurations/enumConfig";
import MeditocSubtitulo from "../../../utilidades/MeditocSubtitulo";

const FormColaborador = (props) => {
    const {
        entColaborador,
        open,
        setOpen,
        listaEspecialidades,
        funcGetColaboradores,
        usuarioSesion,
        funcLoader,
        funcAlert,
    } = props;

    const [formColaborador, setFormColaborador] = useState({
        txtNombreDirectorio: "",
        txtCedulaProfesional: "",
        txtRFC: "",
        txtTelefonoContacto: "",
        txtWhatsApp: "",
        txtCorreoElectronicoContacto: "",
        txtEspecialidad: "",
        txtNumeroSala: "",
        txtDireccionConsultorio: "",
        txtUrlDoctor: "",
        txtMapsDoctor: "",
        txtNombreDoctor: "",
        txtApellidoPaterno: "",
        txtApellidoMaterno: "",
        txtFechaNacimiento: null,
        txtTelefono: "",
        txtCorreoElectronico: "",
        txtDomicilio: "",
        txtUsuarioTitular: "",
        txtPasswordTitular: "",
        txtUsuarioAdministrativo: "",
        txtPasswordAdministrativo: "",
    });

    const validacionesFormulario = {
        txtNombreDirectorio: true,
        txtCedulaProfesional: true,
        txtRFC: true,
        txtTelefonoContacto: true,
        txtWhatsApp: true,
        txtCorreoElectronicoContacto: true,
        txtEspecialidad: true,
        txtNumeroSala: true,
        txtDireccionConsultorio: true,
        txtUrlDoctor: true,
        txtMapsDoctor: true,
        txtNombreDoctor: true,
        txtApellidoPaterno: true,
        txtApellidoMaterno: true,
        txtFechaNacimiento: true,
        txtTelefono: true,
        txtCorreoElectronico: true,
        txtDomicilio: true,
        txtUsuarioTitular: true,
        txtPasswordTitular: true,
        txtUsuarioAdministrativo: true,
        txtPasswordAdministrativo: true,
    };

    const [formColaboradorOK, setFormColaboradorOK] = useState(validacionesFormulario);

    useEffect(() => {
        setFormColaborador({
            ...formColaborador,
            txtNombreDirectorio: entColaborador.sNombreDirectorio,
            txtCedulaProfesional: entColaborador.sCedulaProfecional,
            txtRFC: entColaborador.sRFC,
            txtTelefonoContacto: entColaborador.sTelefonoDirectorio,
            txtWhatsApp: entColaborador.sWhatsApp,
            txtCorreoElectronicoContacto: entColaborador.sCorreoDirectorio,
            txtEspecialidad: entColaborador.iIdEspecialidad,
            txtNumeroSala: entColaborador.iNumSala,
            txtDireccionConsultorio: entColaborador.sDireccionConsultorio,
            txtUrlDoctor: entColaborador.sURL,
            txtMapsDoctor: entColaborador.sMaps,
            txtNombreDoctor: entColaborador.sNombresDoctor,
            txtApellidoPaterno: entColaborador.sApellidoPaternoDoctor,
            txtApellidoMaterno: entColaborador.sApellidoMaternoDoctor,
            txtFechaNacimiento:
                entColaborador.dtFechaNacimientoDoctor === null
                    ? null
                    : new Date(entColaborador.dtFechaNacimientoDoctor),
            txtTelefono: entColaborador.sTelefonoDoctor,
            txtCorreoElectronico: entColaborador.sCorreoDoctor,
            txtDomicilio: entColaborador.sDomicilioDoctor,
            txtUsuarioTitular: entColaborador.sUsuarioTitular,
            txtUsuarioAdministrativo: entColaborador.sUsuarioAdministrativo,
        });
        setFormColaboradorOK(validacionesFormulario);
        // eslint-disable-next-line
    }, [entColaborador]);

    const [tabIndex, setTabIndex] = useState(0);

    // const [sclTipoColaborador, setSclTipoColaborador] = useState("1");

    // const handleChangeTipoColaborador = (e) => {
    //     setSclTipoColaborador(e.target.value);
    // };

    const handleChangeFormColaborador = (e) => {
        const campoNombre = e.target.name;
        const campoValor = e.target.value;

        switch (campoNombre) {
            case "txtNombreDirectorio":
                if (!formColaboradorOK.txtNombreDirectorio) {
                    if (campoValor !== "") {
                        setFormColaboradorOK({ ...formColaboradorOK, [campoNombre]: true });
                    }
                }
                break;

            case "txtCedulaProfesional":
                if (campoValor % 1 !== 0) {
                    return;
                }
                if (!formColaboradorOK.txtCedulaProfesional) {
                    if (campoValor !== "") {
                        setFormColaboradorOK({ ...formColaboradorOK, [campoNombre]: true });
                    }
                }
                break;

            case "txtRFC":
                if (!formColaboradorOK.txtRFC) {
                    if (campoValor !== "") {
                        setFormColaboradorOK({ ...formColaboradorOK, [campoNombre]: true });
                    }
                }
                break;

            case "txtTelefonoContacto":
                if (!formColaboradorOK.txtTelefonoContacto) {
                    const telefonoValidacion = campoValor.replace(/ /g, "");
                    if (telefonoValidacion !== "" && telefonoValidacion.length === 10) {
                        setFormColaboradorOK({ ...formColaboradorOK, [campoNombre]: true });
                    }
                }
                break;
            case "txtWhatsApp":
                if (!formColaboradorOK.txtWhatsApp) {
                    const telefonoValidacion = campoValor.replace(/ /g, "");
                    if (telefonoValidacion === "" || telefonoValidacion.length === 10) {
                        setFormColaboradorOK({ ...formColaboradorOK, [campoNombre]: true });
                    }
                }
                break;

            case "txtCorreoElectronicoContacto":
                if (!formColaboradorOK.txtCorreoElectronicoContacto) {
                    if (campoValor !== "" && rxCorreo.test(campoValor)) {
                        setFormColaboradorOK({ ...formColaboradorOK, [campoNombre]: true });
                    }
                }
                break;

            case "txtEspecialidad":
                if (
                    !formColaboradorOK.txtEspecialidad &&
                    entColaborador.iIdTipoDoctor === EnumTipoDoctor.Especialista
                ) {
                    if (campoValor !== "" && campoValor !== 0) {
                        setFormColaboradorOK({ ...formColaboradorOK, [campoNombre]: true });
                    }
                }
                break;

            case "txtNumeroSala":
                if (!formColaboradorOK.txtNumeroSala) {
                    if (campoValor !== "") {
                        setFormColaboradorOK({ ...formColaboradorOK, [campoNombre]: true });
                    }
                }
                break;

            case "txtUrlDoctor":
                if (!formColaboradorOK.txtUrlDoctor) {
                    if (campoValor === "" || rxUrl.test(campoValor)) {
                        setFormColaboradorOK({ ...formColaboradorOK, [campoNombre]: true });
                    }
                }
                break;

            case "txtMapsDoctor":
                if (!formColaboradorOK.txtMapsDoctor) {
                    if (campoValor === "" || rxUrl.test(campoValor)) {
                        setFormColaboradorOK({ ...formColaboradorOK, [campoNombre]: true });
                    }
                }
                break;

            case "txtNombreDoctor":
                if (!formColaboradorOK.txtNombreDoctor) {
                    if (campoValor !== "") {
                        setFormColaboradorOK({ ...formColaboradorOK, [campoNombre]: true });
                    }
                }
                break;

            case "txtApellidoPaterno":
                if (!formColaboradorOK.txtApellidoPaterno) {
                    if (campoValor !== "") {
                        setFormColaboradorOK({ ...formColaboradorOK, [campoNombre]: true });
                    }
                }
                break;

            case "txtFechaNacimiento":
                if (!formColaboradorOK.txtFechaNacimiento) {
                    if (campoValor !== "" && campoValor !== null) {
                        setFormColaboradorOK({ ...formColaboradorOK, [campoNombre]: true });
                    }
                }

                break;

            case "txtTelefono":
                if (!formColaboradorOK.txtTelefono) {
                    const telefonoValidacion = campoValor.replace(/ /g, "");
                    if (telefonoValidacion !== "" && telefonoValidacion.length === 10) {
                        setFormColaboradorOK({ ...formColaboradorOK, [campoNombre]: true });
                    }
                }
                break;

            case "txtCorreoElectronico":
                if (!formColaboradorOK.txtCorreoElectronico) {
                    if (campoValor !== "" && rxCorreo.test(campoValor)) {
                        setFormColaboradorOK({ ...formColaboradorOK, [campoNombre]: true });
                    }
                }
                break;

            case "txtTipoColaborador":
                if (!formColaboradorOK.txtTipoColaborador) {
                    if (campoValor !== "") {
                        setFormColaboradorOK({ ...formColaboradorOK, [campoNombre]: true });
                    }
                }
                break;

            case "txtUsuarioTitular":
                if (!formColaboradorOK.txtUsuarioTitular) {
                    if (campoValor !== "") {
                        setFormColaboradorOK({ ...formColaboradorOK, [campoNombre]: true });
                    }
                }
                break;

            case "txtPasswordTitular":
                if (!formColaboradorOK.txtPasswordTitular) {
                    if (campoValor !== "") {
                        setFormColaboradorOK({ ...formColaboradorOK, [campoNombre]: true });
                    }
                }
                break;
            case "txtUsuarioAdministrativo":
                if (
                    !formColaboradorOK.txtUsuarioAdministrativo &&
                    entColaborador.iIdTipoDoctor === EnumTipoDoctor.Especialista
                ) {
                    if (campoValor !== "") {
                        setFormColaboradorOK({ ...formColaboradorOK, [campoNombre]: true });
                    }
                }
                break;

            case "txtPasswordAdministrativo":
                if (
                    !formColaboradorOK.txtPasswordAdministrativo &&
                    entColaborador.iIdTipoDoctor === EnumTipoDoctor.Especialista
                ) {
                    if (campoValor !== "") {
                        setFormColaboradorOK({ ...formColaboradorOK, [campoNombre]: true });
                    }
                }
                break;

            default:
                break;
        }

        setFormColaborador({
            ...formColaborador,
            [e.target.name]: e.target.value,
        });
    };

    const handleChangeFechaNacimiento = (date) => {
        if (!formColaboradorOK.txtFechaNacimiento) {
            if (date !== "" && date !== null) {
                setFormColaboradorOK({ ...formColaboradorOK, txtFechaNacimiento: true });
            }
        }

        setFormColaborador({ ...formColaborador, txtFechaNacimiento: date });
    };

    const handleClickGuardarColaborador = async () => {
        let formColaboradorOKValidacion = { ...validacionesFormulario };

        let errorDatosDoctor = false;
        let errorDatosCuenta = false;
        let errorUsuarioPassword = false;

        if (formColaborador.txtNombreDirectorio === "") {
            formColaboradorOKValidacion.txtNombreDirectorio = false;
            errorDatosDoctor = true;
        }

        if (formColaborador.txtCedulaProfesional === "") {
            formColaboradorOKValidacion.txtCedulaProfesional = false;
            errorDatosDoctor = true;
        }
        if (formColaborador.txtRFC === "") {
            formColaboradorOKValidacion.txtRFC = false;
            errorDatosDoctor = true;
        }

        const telefonoValidacion = formColaborador.txtTelefonoContacto.replace(/ /g, "");

        if (telefonoValidacion === "" || telefonoValidacion.length !== 10) {
            formColaboradorOKValidacion.txtTelefonoContacto = false;
            errorDatosDoctor = true;
        }

        const telefonoValidacionWhatsApp =
            formColaborador.txtWhatsApp === null ? "" : formColaborador.txtWhatsApp.replace(/ /g, "");
        if (telefonoValidacionWhatsApp.length > 0 && telefonoValidacionWhatsApp.length !== 10) {
            formColaboradorOKValidacion.txtWhatsApp = false;
            errorDatosDoctor = true;
        }
        if (
            formColaborador.txtCorreoElectronicoContacto === "" ||
            !rxCorreo.test(formColaborador.txtCorreoElectronicoContacto)
        ) {
            formColaboradorOKValidacion.txtCorreoElectronicoContacto = false;
            errorDatosDoctor = true;
        }

        if (
            (formColaborador.txtEspecialidad === "" || formColaborador.txtEspecialidad === 0) &&
            entColaborador.iIdTipoDoctor === EnumTipoDoctor.Especialista
        ) {
            formColaboradorOKValidacion.txtEspecialidad = false;
            errorDatosDoctor = true;
        }

        if (formColaborador.txtNumeroSala === "") {
            formColaboradorOKValidacion.txtNumeroSala = false;
            errorDatosDoctor = true;
        }

        if (formColaborador.txtUrlDoctor !== "") {
            if (!rxUrl.test(formColaborador.txtUrlDoctor)) {
                formColaboradorOKValidacion.txtUrlDoctor = false;
                errorDatosDoctor = true;
            }
        }

        if (formColaborador.txtMapsDoctor !== "") {
            if (!rxUrl.test(formColaborador.txtMapsDoctor)) {
                formColaboradorOKValidacion.txtMapsDoctor = false;
                errorDatosDoctor = true;
            }
        }

        if (formColaborador.txtNombreDoctor === "") {
            formColaboradorOKValidacion.txtNombreDoctor = false;
            errorDatosCuenta = true;
        }

        if (formColaborador.txtApellidoPaterno === "") {
            formColaboradorOKValidacion.txtApellidoPaterno = false;
            errorDatosCuenta = true;
        }

        if (formColaborador.txtFechaNacimiento === "" || formColaborador.txtFechaNacimiento === null) {
            formColaboradorOKValidacion.txtFechaNacimiento = false;
            errorDatosCuenta = true;
        }

        const telefonoValidacionUsuario = formColaborador.txtTelefono.replace(/ /g, "");

        if (telefonoValidacionUsuario === "" || telefonoValidacionUsuario.length !== 10) {
            formColaboradorOKValidacion.txtTelefono = false;
            errorDatosCuenta = true;
        }

        if (formColaborador.txtCorreoElectronico === "" || !rxCorreo.test(formColaborador.txtCorreoElectronico)) {
            formColaboradorOKValidacion.txtCorreoElectronico = false;
            errorDatosCuenta = true;
        }

        if (formColaborador.txtTipoColaborador === "") {
            formColaboradorOKValidacion.txtTipoColaborador = false;
            errorUsuarioPassword = true;
        }

        if (formColaborador.txtUsuarioTitular === "") {
            formColaboradorOKValidacion.txtUsuarioTitular = false;
            errorUsuarioPassword = true;
        }

        if (formColaborador.txtPasswordTitular === "") {
            if (entColaborador.iIdColaborador === 0) {
                formColaboradorOKValidacion.txtPasswordTitular = false;
                errorUsuarioPassword = true;
            }
        }

        if (entColaborador.iIdTipoDoctor === EnumTipoDoctor.Especialista) {
            if (formColaborador.txtUsuarioAdministrativo === "") {
                formColaboradorOKValidacion.txtUsuarioAdministrativo = false;
                errorUsuarioPassword = true;
            }
            if (formColaborador.txtPasswordAdministrativo === "") {
                if (entColaborador.iIdColaborador === 0) {
                    formColaboradorOKValidacion.txtPasswordAdministrativo = false;
                    errorUsuarioPassword = true;
                }
            }
        }

        setFormColaboradorOK(formColaboradorOKValidacion);

        if (errorUsuarioPassword || errorDatosCuenta || errorDatosDoctor) {
            let regresarTab = 2;
            if (errorUsuarioPassword) {
                regresarTab = 2;
            }

            if (errorDatosCuenta) {
                regresarTab = 1;
            }

            if (errorDatosDoctor) {
                regresarTab = 0;
            }
            setTabIndex(regresarTab);
            return;
        }

        const entColaboradorSubmit = {
            iIdColaborador: entColaborador.iIdColaborador,
            iIdTipoDoctor: entColaborador.iIdTipoDoctor,
            iIdEspecialidad:
                entColaborador.iIdTipoDoctor === EnumTipoDoctor.CallCenter
                    ? 0
                    : parseInt(formColaborador.txtEspecialidad),
            iIdUsuarioCGU: entColaborador.iIdColaborador === 0 ? 0 : entColaborador.iIdUsuarioCGU,
            iNumSala: parseInt(formColaborador.txtNumeroSala),
            sNombreDirectorio: formColaborador.txtNombreDirectorio,
            sCedulaProfecional: formColaborador.txtCedulaProfesional,
            sWhatsApp: formColaborador.txtWhatsApp,
            sTelefonoDirectorio: formColaborador.txtTelefonoContacto,
            sCorreoDirectorio: formColaborador.txtCorreoElectronicoContacto,
            sDireccionConsultorio: formColaborador.txtDireccionConsultorio,
            sRFC: formColaborador.txtRFC,
            sURL: formColaborador.txtUrlDoctor,
            sMaps: formColaborador.txtMapsDoctor,
            sUsuarioTitular: formColaborador.txtUsuarioTitular,
            sPasswordTitular: formColaborador.txtPasswordTitular === "" ? null : formColaborador.txtPasswordTitular,
            sUsuarioAdministrativo:
                entColaborador.iIdTipoDoctor === EnumTipoDoctor.Especialista
                    ? formColaborador.txtUsuarioAdministrativo
                    : null,
            sPasswordAdministrativo:
                entColaborador.iIdTipoDoctor === EnumTipoDoctor.Especialista
                    ? formColaborador.txtPasswordAdministrativo === ""
                        ? null
                        : formColaborador.txtPasswordAdministrativo
                    : null,
            sNombresDoctor: formColaborador.txtNombreDoctor,
            sApellidoPaternoDoctor: formColaborador.txtApellidoPaterno,
            sApellidoMaternoDoctor: formColaborador.txtApellidoMaterno,
            dtFechaNacimientoDoctor: formColaborador.txtFechaNacimiento.toLocaleDateString(),
            sTelefonoDoctor: formColaborador.txtTelefono,
            sCorreoDoctor: formColaborador.txtCorreoElectronico,
            sDomicilioDoctor: formColaborador.txtDomicilio,
            iIdUsuarioMod: usuarioSesion.iIdUsuario,
            bActivo: true,
            bBaja: false,
        };

        const colaboradorController = new ColaboradorController();

        funcLoader(true, "Guardando colaborador...");

        const response = await colaboradorController.funcSaveColaborador(entColaboradorSubmit);

        if (response.Code === 0) {
            setOpen(false);
            await funcGetColaboradores();
            funcAlert(response.Message, "success");
        } else {
            funcAlert(response.Message);
        }

        funcLoader();
    };

    const [verPasswordTitular, setVerPasswordTitular] = useState(false);
    const [verPasswordAdministrativo, setVerPasswordAdministrativo] = useState(false);

    return (
        <MeditocModal
            size="normal"
            title={
                entColaborador.iIdTipoDoctor === EnumTipoDoctor.CallCenter
                    ? "Nuevo Colaborador CallCenter"
                    : "Nuevo Colaborador Especialista"
            }
            open={open}
            setOpen={setOpen}
        >
            <MeditocTabHeader
                tabs={["DATOS DEL DOCTOR", "DATOS DE CUENTA", "USUARIO Y CONTRASEÑA"]}
                index={tabIndex}
                setIndex={setTabIndex}
            />
            <Grid container spacing={0}>
                <Grid item xs={12}>
                    <MeditocTabBody index={tabIndex} setIndex={setTabIndex}>
                        <MeditocTabPanel id={0} index={tabIndex}>
                            <Grid container spacing={3}>
                                <Grid item xs={12}>
                                    <Typography variant="caption">
                                        Los datos ingresados seran mostrados en el directorio de Médicos de Meditoc.
                                    </Typography>
                                </Grid>
                                <Grid item xs={12}>
                                    <TextField
                                        name="txtNombreDirectorio"
                                        label="Nombre corto para mostrar en directorio:"
                                        variant="outlined"
                                        placeholder="Ej. Dr. Ignacio Ochoa"
                                        fullWidth
                                        required
                                        value={formColaborador.txtNombreDirectorio}
                                        onChange={handleChangeFormColaborador}
                                        error={!formColaboradorOK.txtNombreDirectorio}
                                        helperText={
                                            !formColaboradorOK.txtNombreDirectorio
                                                ? "El nombre para el directorio médico es requerido"
                                                : ""
                                        }
                                    />
                                </Grid>
                                <Grid item sm={6} xs={12}>
                                    <TextField
                                        name="txtCedulaProfesional"
                                        label="Cédula profesional:"
                                        variant="outlined"
                                        fullWidth
                                        required
                                        value={formColaborador.txtCedulaProfesional}
                                        onChange={handleChangeFormColaborador}
                                        error={!formColaboradorOK.txtCedulaProfesional}
                                        helperText={
                                            !formColaboradorOK.txtCedulaProfesional
                                                ? "La cédula profesional es requerida"
                                                : ""
                                        }
                                    />
                                </Grid>
                                <Grid item sm={6} xs={12}>
                                    <TextField
                                        name="txtRFC"
                                        label="RFC:"
                                        variant="outlined"
                                        fullWidth
                                        required
                                        value={formColaborador.txtRFC}
                                        onChange={handleChangeFormColaborador}
                                        error={!formColaboradorOK.txtRFC}
                                        helperText={!formColaboradorOK.txtRFC ? "El RFC es requerido" : ""}
                                    />
                                </Grid>
                                <Grid item sm={6} xs={12}>
                                    <TextField
                                        name="txtTelefonoContacto"
                                        label="Teléfono de contacto:"
                                        variant="outlined"
                                        fullWidth
                                        InputProps={{
                                            inputComponent: InputTelefono,
                                        }}
                                        required
                                        value={formColaborador.txtTelefonoContacto}
                                        onChange={handleChangeFormColaborador}
                                        error={!formColaboradorOK.txtTelefonoContacto}
                                        helperText={
                                            !formColaboradorOK.txtTelefonoContacto
                                                ? "El teléfono de contacto es requerido"
                                                : ""
                                        }
                                    />
                                </Grid>
                                <Grid item sm={6} xs={12}>
                                    <TextField
                                        name="txtWhatsApp"
                                        label="WhatsApp:"
                                        variant="outlined"
                                        fullWidth
                                        InputProps={{
                                            inputComponent: InputTelefono,
                                        }}
                                        value={formColaborador.txtWhatsApp}
                                        onChange={handleChangeFormColaborador}
                                        error={!formColaboradorOK.txtWhatsApp}
                                        helperText={!formColaboradorOK.txtWhatsApp ? "Ingrese un número válido" : ""}
                                    />
                                </Grid>
                                <Grid item xs={12}>
                                    <TextField
                                        name="txtCorreoElectronicoContacto"
                                        label="Correo electrónico de contacto:"
                                        variant="outlined"
                                        fullWidth
                                        required
                                        value={formColaborador.txtCorreoElectronicoContacto}
                                        onChange={handleChangeFormColaborador}
                                        error={!formColaboradorOK.txtCorreoElectronicoContacto}
                                        helperText={
                                            !formColaboradorOK.txtCorreoElectronicoContacto
                                                ? "El correo electrónico de contacto es requerido"
                                                : ""
                                        }
                                    />
                                </Grid>
                                {entColaborador.iIdTipoDoctor === EnumTipoDoctor.Especialista ? (
                                    <Grid item sm={6} xs={12}>
                                        <TextField
                                            name="txtEspecialidad"
                                            label="Especialidad:"
                                            variant="outlined"
                                            fullWidth
                                            select
                                            SelectProps={{ MenuProps: { PaperProps: { style: { maxHeight: 300 } } } }}
                                            required
                                            value={formColaborador.txtEspecialidad}
                                            onChange={handleChangeFormColaborador}
                                            error={!formColaboradorOK.txtEspecialidad}
                                            helperText={
                                                !formColaboradorOK.txtEspecialidad ? "La especialidad es requerida" : ""
                                            }
                                        >
                                            {listaEspecialidades
                                                .filter(
                                                    (x) =>
                                                        x.iIdEspecialidad !== EnumEspecialidadPrincipal.MedicinaGeneral
                                                )
                                                .sort((a, b) => (a.sNombre > b.sNombre ? 1 : -1))
                                                .map((especialidad) => (
                                                    <MenuItem
                                                        key={especialidad.iIdEspecialidad}
                                                        value={especialidad.iIdEspecialidad}
                                                    >
                                                        {especialidad.sNombre}
                                                    </MenuItem>
                                                ))}
                                        </TextField>
                                    </Grid>
                                ) : null}

                                <Grid item sm={6} xs={12}>
                                    <TextField
                                        name="txtNumeroSala"
                                        label="Número de sala IceLink:"
                                        variant="outlined"
                                        fullWidth
                                        required
                                        value={formColaborador.txtNumeroSala}
                                        onChange={handleChangeFormColaborador}
                                        error={!formColaboradorOK.txtNumeroSala}
                                        helperText={
                                            !formColaboradorOK.txtNumeroSala
                                                ? "El número de asignación de sala es requerido"
                                                : ""
                                        }
                                    />
                                </Grid>
                                <Grid
                                    item
                                    sm={entColaborador.iIdTipoDoctor === EnumTipoDoctor.CallCenter ? 6 : 12}
                                    xs={12}
                                >
                                    <TextField
                                        name="txtDireccionConsultorio"
                                        label="Dirección del consultorio:"
                                        variant="outlined"
                                        fullWidth
                                        value={formColaborador.txtDireccionConsultorio}
                                        onChange={handleChangeFormColaborador}
                                    />
                                </Grid>
                                <Grid item xs={12}>
                                    <TextField
                                        name="txtUrlDoctor"
                                        label="Sitio web del doctor:"
                                        variant="outlined"
                                        fullWidth
                                        value={formColaborador.txtUrlDoctor}
                                        onChange={handleChangeFormColaborador}
                                        error={!formColaboradorOK.txtUrlDoctor}
                                        helperText={
                                            !formColaboradorOK.txtUrlDoctor
                                                ? "Ingrese una URL válida. Debe comenzar con http o https."
                                                : ""
                                        }
                                    />
                                </Grid>
                                <Grid item xs={12}>
                                    <TextField
                                        name="txtMapsDoctor"
                                        label="Ubicación en Google Maps:"
                                        variant="outlined"
                                        placeholder="Ejemplo: https://goo.gl/maps/oD3FDomx7WVGRHN39"
                                        fullWidth
                                        value={formColaborador.txtMapsDoctor}
                                        onChange={handleChangeFormColaborador}
                                        error={!formColaboradorOK.txtMapsDoctor}
                                        helperText={
                                            !formColaboradorOK.txtMapsDoctor
                                                ? "Ingrese una URL de Google Maps válida. Debe comenzar con http o https."
                                                : ""
                                        }
                                        InputProps={{
                                            endAdornment: (
                                                <Tooltip
                                                    title={
                                                        <Fragment>
                                                            Para obtener su enlace de ubicación
                                                            <br />
                                                            1. Ingrese a maps.google.com.mx
                                                            <br />
                                                            2. Localice la ubicación de su consultorio
                                                            <br />
                                                            3. Seleccione Compartir del ménu lateral izquierdo
                                                            <br />
                                                            4. Seleccione en Copiar Vínculo
                                                        </Fragment>
                                                    }
                                                    placement="top"
                                                    arrow
                                                >
                                                    <IconButton>
                                                        <InfoIcon />
                                                    </IconButton>
                                                </Tooltip>
                                            ),
                                        }}
                                    />
                                </Grid>
                            </Grid>
                        </MeditocTabPanel>
                        <MeditocTabPanel id={1} index={tabIndex}>
                            <Grid container spacing={3}>
                                <Grid item xs={12}>
                                    <TextField
                                        name="txtNombreDoctor"
                                        label="Nombres:"
                                        variant="outlined"
                                        fullWidth
                                        required
                                        value={formColaborador.txtNombreDoctor}
                                        onChange={handleChangeFormColaborador}
                                        error={!formColaboradorOK.txtNombreDoctor}
                                        helperText={
                                            !formColaboradorOK.txtNombreDoctor
                                                ? "El nombre del doctor es requerido"
                                                : ""
                                        }
                                    />
                                </Grid>
                                <Grid item sm={6} xs={12}>
                                    <TextField
                                        name="txtApellidoPaterno"
                                        label="Apellido paterno:"
                                        variant="outlined"
                                        fullWidth
                                        required
                                        value={formColaborador.txtApellidoPaterno}
                                        onChange={handleChangeFormColaborador}
                                        error={!formColaboradorOK.txtApellidoPaterno}
                                        helperText={
                                            !formColaboradorOK.txtApellidoPaterno
                                                ? "El apellido paterno del doctor es requerido"
                                                : ""
                                        }
                                    />
                                </Grid>
                                <Grid item sm={6} xs={12}>
                                    <TextField
                                        name="txtApellidoMaterno"
                                        label="Apellido materno:"
                                        variant="outlined"
                                        fullWidth
                                        value={formColaborador.txtApellidoMaterno}
                                        onChange={handleChangeFormColaborador}
                                    />
                                </Grid>
                                <Grid item sm={6} xs={12}>
                                    <DatePicker
                                        name="txtFechaNacimiento"
                                        variant="inline"
                                        disableFuture
                                        label="Fecha de nacimiento:"
                                        inputVariant="outlined"
                                        openTo="year"
                                        format="dd/MM/yyyy"
                                        views={["year", "month", "date"]}
                                        InputProps={{
                                            endAdornment: (
                                                <InputAdornment position="end">
                                                    <IconButton>
                                                        <DateRangeIcon />
                                                    </IconButton>
                                                </InputAdornment>
                                            ),
                                        }}
                                        fullWidth
                                        required
                                        value={formColaborador.txtFechaNacimiento}
                                        onChange={handleChangeFechaNacimiento}
                                        error={!formColaboradorOK.txtFechaNacimiento}
                                        helperText={
                                            !formColaboradorOK.txtFechaNacimiento
                                                ? "La fecha de nacimiento es requerida"
                                                : ""
                                        }
                                    />
                                </Grid>
                                <Grid item sm={6} xs={12}>
                                    <TextField
                                        name="txtTelefono"
                                        label="Teléfono personal:"
                                        variant="outlined"
                                        fullWidth
                                        InputProps={{
                                            inputComponent: InputTelefono,
                                        }}
                                        required
                                        value={formColaborador.txtTelefono}
                                        onChange={handleChangeFormColaborador}
                                        error={!formColaboradorOK.txtTelefono}
                                        helperText={
                                            !formColaboradorOK.txtTelefono ? "El teléfono personal es requerido" : ""
                                        }
                                    />
                                </Grid>
                                <Grid item sm={6} xs={12}>
                                    <TextField
                                        name="txtCorreoElectronico"
                                        variant="outlined"
                                        label="Correo electrónico personal:"
                                        fullWidth
                                        required
                                        value={formColaborador.txtCorreoElectronico}
                                        onChange={handleChangeFormColaborador}
                                        error={!formColaboradorOK.txtCorreoElectronico}
                                        helperText={
                                            !formColaboradorOK.txtCorreoElectronico
                                                ? "El correo electrónico personal es requerido"
                                                : ""
                                        }
                                    />
                                </Grid>
                                <Grid item sm={6} xs={12}>
                                    <TextField
                                        name="txtDomicilio"
                                        variant="outlined"
                                        label="Domicilio particular:"
                                        fullWidth
                                        value={formColaborador.txtDomicilio}
                                        onChange={handleChangeFormColaborador}
                                    />
                                </Grid>
                            </Grid>
                        </MeditocTabPanel>
                        <MeditocTabPanel id={2} index={tabIndex}>
                            <Grid container spacing={3}>
                                <Grid item xs={12}>
                                    <MeditocSubtitulo title="USUARIO TITULAR" />
                                </Grid>
                                <Grid item sm={6} xs={12}>
                                    <TextField
                                        variant="outlined"
                                        label="Usuario:"
                                        autoComplete="new-password"
                                        fullWidth
                                        name="txtUsuarioTitular"
                                        required
                                        value={formColaborador.txtUsuarioTitular}
                                        onChange={handleChangeFormColaborador}
                                        error={!formColaboradorOK.txtUsuarioTitular}
                                        helperText={
                                            !formColaboradorOK.txtUsuarioTitular
                                                ? "El usuario titular es requerido"
                                                : ""
                                        }
                                    />
                                </Grid>
                                <Grid item sm={6} xs={12}>
                                    <TextField
                                        variant="outlined"
                                        label="Contraseña:"
                                        type={verPasswordTitular ? "text" : "password"}
                                        autoComplete="new-password"
                                        fullWidth
                                        name="txtPasswordTitular"
                                        required
                                        value={formColaborador.txtPasswordTitular}
                                        onChange={handleChangeFormColaborador}
                                        error={!formColaboradorOK.txtPasswordTitular}
                                        helperText={
                                            !formColaboradorOK.txtPasswordTitular
                                                ? "La contraseña titular es requerida"
                                                : ""
                                        }
                                        InputProps={{
                                            endAdornment: (
                                                <Tooltip
                                                    title={verPasswordTitular ? "Ocultar contraseña" : "Ver contraseña"}
                                                    arrow
                                                    placement="top"
                                                >
                                                    <IconButton
                                                        onMouseDown={() => setVerPasswordTitular(true)}
                                                        onMouseUp={() => setVerPasswordTitular(false)}
                                                    >
                                                        {verPasswordTitular ? (
                                                            <VisibilityIcon />
                                                        ) : (
                                                            <VisibilityOffIcon />
                                                        )}
                                                    </IconButton>
                                                </Tooltip>
                                            ),
                                        }}
                                    />
                                </Grid>
                                {entColaborador.iIdTipoDoctor === EnumTipoDoctor.Especialista ? (
                                    <Fragment>
                                        <Grid item xs={12}>
                                            <MeditocSubtitulo title="USUARIO ADMINISTRATIVO" />
                                        </Grid>
                                        <Grid item sm={6} xs={12}>
                                            <TextField
                                                variant="outlined"
                                                label="Usuario:"
                                                autoComplete="new-password"
                                                fullWidth
                                                name="txtUsuarioAdministrativo"
                                                required
                                                value={formColaborador.txtUsuarioAdministrativo}
                                                onChange={handleChangeFormColaborador}
                                                error={!formColaboradorOK.txtUsuarioAdministrativo}
                                                helperText={
                                                    !formColaboradorOK.txtUsuarioAdministrativo
                                                        ? "El usuario administrativo es requerido"
                                                        : ""
                                                }
                                            />
                                        </Grid>
                                        <Grid item sm={6} xs={12}>
                                            <TextField
                                                variant="outlined"
                                                label="Contraseña:"
                                                type={verPasswordAdministrativo ? "text" : "password"}
                                                autoComplete="new-password"
                                                fullWidth
                                                name="txtPasswordAdministrativo"
                                                required
                                                value={formColaborador.txtPasswordAdministrativo}
                                                onChange={handleChangeFormColaborador}
                                                error={!formColaboradorOK.txtPasswordAdministrativo}
                                                helperText={
                                                    !formColaboradorOK.txtPasswordAdministrativo
                                                        ? "La contraseña administrativa es requerida"
                                                        : ""
                                                }
                                                InputProps={{
                                                    endAdornment: (
                                                        <Tooltip
                                                            title={
                                                                verPasswordAdministrativo
                                                                    ? "Ocultar contraseña"
                                                                    : "Ver contraseña"
                                                            }
                                                            arrow
                                                            placement="top"
                                                        >
                                                            <IconButton
                                                                onMouseDown={() => setVerPasswordAdministrativo(true)}
                                                                onMouseUp={() => setVerPasswordAdministrativo(false)}
                                                            >
                                                                {verPasswordAdministrativo ? (
                                                                    <VisibilityIcon />
                                                                ) : (
                                                                    <VisibilityOffIcon />
                                                                )}
                                                            </IconButton>
                                                        </Tooltip>
                                                    ),
                                                }}
                                            />
                                        </Grid>
                                    </Fragment>
                                ) : null}
                            </Grid>
                        </MeditocTabPanel>
                    </MeditocTabBody>
                </Grid>
                <MeditocModalBotones okMessage="Guardar" okFunc={handleClickGuardarColaborador} setOpen={setOpen} />
            </Grid>
        </MeditocModal>
    );
};

FormColaborador.propTypes = {
    entColaborador: PropTypes.shape({
        dtFechaNacimientoDoctor: PropTypes.any,
        iIdColaborador: PropTypes.number,
        iIdEspecialidad: PropTypes.any,
        iIdTipoDoctor: PropTypes.any,
        iIdUsuarioCGU: PropTypes.any,
        iNumSala: PropTypes.any,
        sApellidoMaternoDoctor: PropTypes.any,
        sApellidoPaternoDoctor: PropTypes.any,
        sCedulaProfecional: PropTypes.any,
        sCorreoDirectorio: PropTypes.any,
        sCorreoDoctor: PropTypes.any,
        sDireccionConsultorio: PropTypes.any,
        sDomicilioDoctor: PropTypes.any,
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
    funcAlert: PropTypes.func,
    funcGetColaboradores: PropTypes.func,
    funcLoader: PropTypes.func,
    listaEspecialidades: PropTypes.shape({
        filter: PropTypes.func,
    }),
    open: PropTypes.any,
    setOpen: PropTypes.func,
    usuarioSesion: PropTypes.shape({
        iIdUsuario: PropTypes.any,
    }),
};

export default FormColaborador;
