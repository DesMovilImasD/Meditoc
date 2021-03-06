import {
    Checkbox,
    Collapse,
    FormControlLabel,
    Grid,
    IconButton,
    InputAdornment,
    MenuItem,
    TextField,
    Tooltip,
    Typography,
} from "@material-ui/core";
import { EnumEspecialidadPrincipal, EnumTipoDoctor } from "../../../../configurations/enumConfig";
import React, { Fragment } from "react";
import { blurPrevent, funcPrevent } from "../../../../configurations/preventConfig";
import { rxCorreo, rxUrl } from "../../../../configurations/regexConfig";

import ColaboradorController from "../../../../controllers/ColaboradorController";
import { DatePicker } from "@material-ui/pickers";
import DateRangeIcon from "@material-ui/icons/DateRange";
import InfoIcon from "@material-ui/icons/Info";
import MeditocInputPhone from "../../../utilidades/MeditocInputPhone";
import MeditocModal from "../../../utilidades/MeditocModal";
import MeditocModalBotones from "../../../utilidades/MeditocModalBotones";
import MeditocSubtitulo from "../../../utilidades/MeditocSubtitulo";
import MeditocTabBody from "../../../utilidades/MeditocTabBody";
import MeditocTabHeader from "../../../utilidades/MeditocTabHeader";
import MeditocTabPanel from "../../../utilidades/MeditocTabPanel";
import PropTypes from "prop-types";
import VisibilityIcon from "@material-ui/icons/Visibility";
import VisibilityOffIcon from "@material-ui/icons/VisibilityOff";
import { useEffect } from "react";
import { useState } from "react";

/*************************************************************
 * Descripcion: Modal crear/editar un colaborador
 * Creado: Cristopher Noh
 * Fecha: 26/08/2020
 * Invocado desde: Colaboradores
 *************************************************************/
const FormColaborador = (props) => {
    //PROPS
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

    //CONSTANTES
    const validacionesFormulario = {
        txtNombreDirectorio: true,
        txtCedulaProfesional: true,
        txtRFC: true,
        txtTelefonoContacto: true,
        txtWhatsApp: true,
        txtCorreoElectronicoContacto: true,
        txtEspecialidad: true,
        txtNumeroSala: true,
        txtNombreConsultorio: true,
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
        txtAdministrativo: true,
        txtAcceso: true,
    };

    //STATES
    const [mostrarPasswordTitular, setMostrarPasswordTitular] = useState(false);
    const [mostrarPasswordAdministrativo, setMostrarPasswordAdministrativo] = useState(false);

    const [indiceTabForm, setIndiceTabForm] = useState(0);

    const [formularioValidado, setFormularioValidado] = useState(validacionesFormulario);
    const [formColaborador, setFormColaborador] = useState({
        txtNombreDirectorio: "",
        txtCedulaProfesional: "",
        txtRFC: "",
        txtTelefonoContacto: "",
        txtWhatsApp: "",
        txtCorreoElectronicoContacto: "",
        txtEspecialidad: "",
        txtNumeroSala: "",
        txtNombreConsultorio: "",
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
        txtAdministrativo: false,
        txtAcceso: true,
    });

    //HANDLERS

    //Capturar los inputs del formulario
    const handleChangeFormColaborador = (e) => {
        const nombreCampo = e.target.name;
        const valorCampo = e.target.value;

        switch (nombreCampo) {
            case "txtNombreDirectorio":
                if (!formularioValidado.txtNombreDirectorio) {
                    if (valorCampo !== "") {
                        setFormularioValidado({ ...formularioValidado, [nombreCampo]: true });
                    }
                }
                break;

            case "txtCedulaProfesional":
                if (valorCampo % 1 !== 0) {
                    return;
                }
                if (!formularioValidado.txtCedulaProfesional) {
                    if (valorCampo !== "") {
                        setFormularioValidado({ ...formularioValidado, [nombreCampo]: true });
                    }
                }
                break;

            case "txtRFC":
                if (!formularioValidado.txtRFC) {
                    if (valorCampo !== "") {
                        setFormularioValidado({ ...formularioValidado, [nombreCampo]: true });
                    }
                }
                break;

            case "txtTelefonoContacto":
                if (!formularioValidado.txtTelefonoContacto) {
                    const telefonoValidacion = valorCampo.replace(/ /g, "");
                    if (telefonoValidacion !== "" && telefonoValidacion.length === 10) {
                        setFormularioValidado({ ...formularioValidado, [nombreCampo]: true });
                    }
                }
                break;
            case "txtWhatsApp":
                if (!formularioValidado.txtWhatsApp) {
                    const telefonoValidacion = valorCampo.replace(/ /g, "");
                    if (telefonoValidacion === "" || telefonoValidacion.length === 10) {
                        setFormularioValidado({ ...formularioValidado, [nombreCampo]: true });
                    }
                }
                break;

            case "txtCorreoElectronicoContacto":
                if (!formularioValidado.txtCorreoElectronicoContacto) {
                    if (valorCampo !== "" && rxCorreo.test(valorCampo)) {
                        setFormularioValidado({ ...formularioValidado, [nombreCampo]: true });
                    }
                }
                break;

            case "txtEspecialidad":
                if (
                    !formularioValidado.txtEspecialidad &&
                    entColaborador.iIdTipoDoctor === EnumTipoDoctor.Especialista
                ) {
                    if (valorCampo !== "" && valorCampo !== 0) {
                        setFormularioValidado({ ...formularioValidado, [nombreCampo]: true });
                    }
                }
                break;

            case "txtNumeroSala":
                if (!formularioValidado.txtNumeroSala) {
                    if (valorCampo !== "") {
                        setFormularioValidado({ ...formularioValidado, [nombreCampo]: true });
                    }
                }
                break;

            case "txtUrlDoctor":
                if (!formularioValidado.txtUrlDoctor) {
                    if (valorCampo === "" || rxUrl.test(valorCampo)) {
                        setFormularioValidado({ ...formularioValidado, [nombreCampo]: true });
                    }
                }
                break;

            case "txtMapsDoctor":
                if (!formularioValidado.txtMapsDoctor) {
                    if (valorCampo === "" || rxUrl.test(valorCampo)) {
                        setFormularioValidado({ ...formularioValidado, [nombreCampo]: true });
                    }
                }
                break;

            case "txtNombreDoctor":
                if (!formularioValidado.txtNombreDoctor) {
                    if (valorCampo !== "") {
                        setFormularioValidado({ ...formularioValidado, [nombreCampo]: true });
                    }
                }
                break;

            case "txtApellidoPaterno":
                if (!formularioValidado.txtApellidoPaterno) {
                    if (valorCampo !== "") {
                        setFormularioValidado({ ...formularioValidado, [nombreCampo]: true });
                    }
                }
                break;

            case "txtFechaNacimiento":
                if (!formularioValidado.txtFechaNacimiento) {
                    if (valorCampo !== "" && valorCampo !== null) {
                        setFormularioValidado({ ...formularioValidado, [nombreCampo]: true });
                    }
                }

                break;

            case "txtTelefono":
                if (!formularioValidado.txtTelefono) {
                    const telefonoValidacion = valorCampo.replace(/ /g, "");
                    if (telefonoValidacion !== "" && telefonoValidacion.length === 10) {
                        setFormularioValidado({ ...formularioValidado, [nombreCampo]: true });
                    }
                }
                break;

            case "txtCorreoElectronico":
                if (!formularioValidado.txtCorreoElectronico) {
                    if (valorCampo !== "" && rxCorreo.test(valorCampo)) {
                        setFormularioValidado({ ...formularioValidado, [nombreCampo]: true });
                    }
                }
                break;

            case "txtTipoColaborador":
                if (!formularioValidado.txtTipoColaborador) {
                    if (valorCampo !== "") {
                        setFormularioValidado({ ...formularioValidado, [nombreCampo]: true });
                    }
                }
                break;

            case "txtUsuarioTitular":
                if (!formularioValidado.txtUsuarioTitular) {
                    if (valorCampo !== "") {
                        setFormularioValidado({ ...formularioValidado, [nombreCampo]: true });
                    }
                }
                break;

            case "txtPasswordTitular":
                if (!formularioValidado.txtPasswordTitular) {
                    if (valorCampo !== "") {
                        setFormularioValidado({ ...formularioValidado, [nombreCampo]: true });
                    }
                }
                break;
            case "txtUsuarioAdministrativo":
                if (
                    !formularioValidado.txtUsuarioAdministrativo &&
                    entColaborador.iIdTipoDoctor === EnumTipoDoctor.Especialista
                ) {
                    if (valorCampo !== "") {
                        setFormularioValidado({ ...formularioValidado, [nombreCampo]: true });
                    }
                }
                break;

            case "txtPasswordAdministrativo":
                if (
                    !formularioValidado.txtPasswordAdministrativo &&
                    entColaborador.iIdTipoDoctor === EnumTipoDoctor.Especialista
                ) {
                    if (valorCampo !== "") {
                        setFormularioValidado({ ...formularioValidado, [nombreCampo]: true });
                    }
                }
                break;

            default:
                break;
        }
        setFormColaborador({
            ...formColaborador,
            [nombreCampo]: valorCampo,
        });
    };

    //Capturar la fecha de nacimiento
    const handleChangeFechaNacimiento = (date) => {
        if (!formularioValidado.txtFechaNacimiento) {
            if (date !== "" && date !== null) {
                setFormularioValidado({ ...formularioValidado, txtFechaNacimiento: true });
            }
        }

        setFormColaborador({ ...formColaborador, txtFechaNacimiento: date });
    };

    //Consumir API para guardar los datos del colaborador
    const handleClickGuardarColaborador = async (e) => {
        funcPrevent(e);
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

        if (formColaborador.txtAcceso === true) {
            if (formColaborador.txtUsuarioTitular === "") {
                formColaboradorOKValidacion.txtUsuarioTitular = false;
                errorUsuarioPassword = true;
            }

            if (formColaborador.txtPasswordTitular === "") {
                if (
                    entColaborador.iIdColaborador === 0 ||
                    entColaborador.sUsuarioTitular === "" ||
                    entColaborador.sUsuarioTitular === null
                ) {
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
                    if (
                        entColaborador.iIdColaborador === 0 ||
                        entColaborador.sUsuarioAdministrativo === "" ||
                        entColaborador.sUsuarioAdministrativo === null
                    ) {
                        formColaboradorOKValidacion.txtPasswordAdministrativo = false;
                        errorUsuarioPassword = true;
                    }
                }
            }
        }

        setFormularioValidado(formColaboradorOKValidacion);

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
            setIndiceTabForm(regresarTab);
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
            sNombreConsultorio: formColaborador.txtNombreConsultorio,
            sDireccionConsultorio: formColaborador.txtDireccionConsultorio,
            sRFC: formColaborador.txtRFC,
            sURL: formColaborador.txtUrlDoctor,
            sMaps: formColaborador.txtMapsDoctor,
            sUsuarioTitular: formColaborador.txtUsuarioTitular === "" ? null : formColaborador.txtUsuarioTitular,
            sPasswordTitular: formColaborador.txtPasswordTitular === "" ? null : formColaborador.txtPasswordTitular,
            sUsuarioAdministrativo:
                entColaborador.iIdTipoDoctor === EnumTipoDoctor.Especialista
                    ? formColaborador.txtUsuarioAdministrativo === ""
                        ? null
                        : formColaborador.txtUsuarioAdministrativo
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
            bAcceso: formColaborador.txtAcceso,
            bAdministrador: formColaborador.txtAdministrativo,
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
        blurPrevent();
    };

    //EFFECTS
    //Actualizar el formulario al modicar la entidad de los props
    useEffect(() => {
        setFormColaborador({
            ...formColaborador,
            txtNombreDirectorio: entColaborador.sNombreDirectorio,
            txtCedulaProfesional: entColaborador.sCedulaProfecional,
            txtRFC: entColaborador.sRFC,
            txtTelefonoContacto: entColaborador.sTelefonoDirectorio,
            txtWhatsApp: entColaborador.sWhatsApp === null ? "" : entColaborador.sWhatsApp,
            txtCorreoElectronicoContacto: entColaborador.sCorreoDirectorio,
            txtEspecialidad: entColaborador.iIdEspecialidad,
            txtNumeroSala: entColaborador.iNumSala,
            txtNombreConsultorio: entColaborador.sNombreConsultorio === null ? "" : entColaborador.sNombreConsultorio,
            txtDireccionConsultorio:
                entColaborador.sDireccionConsultorio === null ? "" : entColaborador.sDireccionConsultorio,
            txtUrlDoctor: entColaborador.sURL === null ? "" : entColaborador.sURL,
            txtMapsDoctor: entColaborador.sMaps === null ? "" : entColaborador.sMaps,
            txtNombreDoctor: entColaborador.sNombresDoctor,
            txtApellidoPaterno: entColaborador.sApellidoPaternoDoctor,
            txtApellidoMaterno:
                entColaborador.sApellidoMaternoDoctor === null ? "" : entColaborador.sApellidoMaternoDoctor,
            txtFechaNacimiento:
                entColaborador.dtFechaNacimientoDoctor === null
                    ? null
                    : new Date(entColaborador.dtFechaNacimientoDoctor),
            txtTelefono: entColaborador.sTelefonoDoctor,
            txtCorreoElectronico: entColaborador.sCorreoDoctor,
            txtDomicilio: entColaborador.sDomicilioDoctor === null ? "" : entColaborador.sDomicilioDoctor,
            txtUsuarioTitular: entColaborador.sUsuarioTitular === null ? "" : entColaborador.sUsuarioTitular,
            txtUsuarioAdministrativo:
                entColaborador.sUsuarioAdministrativo === null ? "" : entColaborador.sUsuarioAdministrativo,
            txtAcceso: entColaborador.bAcceso,
            txtAdministrativo: entColaborador.bAdministrador,
        });
        setFormularioValidado(validacionesFormulario);
        setIndiceTabForm(0);
        // eslint-disable-next-line
    }, [entColaborador]);

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
            <form id="form-colaborador" onSubmit={handleClickGuardarColaborador} noValidate>
                <MeditocTabHeader
                    tabs={["DATOS DEL DOCTOR", "DATOS DE CUENTA", "USUARIO Y CONTRASEÑA"]}
                    index={indiceTabForm}
                    setIndex={setIndiceTabForm}
                />
                <Grid container spacing={0}>
                    <Grid item xs={12}>
                        <MeditocTabBody index={indiceTabForm} setIndex={setIndiceTabForm}>
                            <MeditocTabPanel id={0} index={indiceTabForm}>
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
                                            autoFocus
                                            value={formColaborador.txtNombreDirectorio}
                                            onChange={handleChangeFormColaborador}
                                            error={!formularioValidado.txtNombreDirectorio}
                                            helperText={
                                                !formularioValidado.txtNombreDirectorio
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
                                            error={!formularioValidado.txtCedulaProfesional}
                                            helperText={
                                                !formularioValidado.txtCedulaProfesional
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
                                            error={!formularioValidado.txtRFC}
                                            helperText={!formularioValidado.txtRFC ? "El RFC es requerido" : ""}
                                        />
                                    </Grid>
                                    <Grid item sm={6} xs={12}>
                                        <TextField
                                            name="txtTelefonoContacto"
                                            label="Teléfono de contacto:"
                                            variant="outlined"
                                            fullWidth
                                            InputProps={{
                                                inputComponent: MeditocInputPhone,
                                            }}
                                            required
                                            value={formColaborador.txtTelefonoContacto}
                                            onChange={handleChangeFormColaborador}
                                            error={!formularioValidado.txtTelefonoContacto}
                                            helperText={
                                                !formularioValidado.txtTelefonoContacto
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
                                                inputComponent: MeditocInputPhone,
                                            }}
                                            value={formColaborador.txtWhatsApp}
                                            onChange={handleChangeFormColaborador}
                                            error={!formularioValidado.txtWhatsApp}
                                            helperText={
                                                !formularioValidado.txtWhatsApp ? "Ingrese un número válido" : ""
                                            }
                                        />
                                    </Grid>
                                    <Grid item sm={6} xs={12}>
                                        <TextField
                                            name="txtCorreoElectronicoContacto"
                                            label="Correo electrónico de contacto:"
                                            variant="outlined"
                                            fullWidth
                                            required
                                            value={formColaborador.txtCorreoElectronicoContacto}
                                            onChange={handleChangeFormColaborador}
                                            error={!formularioValidado.txtCorreoElectronicoContacto}
                                            helperText={
                                                !formularioValidado.txtCorreoElectronicoContacto
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
                                                SelectProps={{
                                                    MenuProps: { PaperProps: { style: { maxHeight: 300 } } },
                                                }}
                                                required
                                                value={formColaborador.txtEspecialidad}
                                                onChange={handleChangeFormColaborador}
                                                error={!formularioValidado.txtEspecialidad}
                                                helperText={
                                                    !formularioValidado.txtEspecialidad
                                                        ? "La especialidad es requerida"
                                                        : ""
                                                }
                                            >
                                                {listaEspecialidades
                                                    .filter(
                                                        (x) =>
                                                            x.iIdEspecialidad !==
                                                            EnumEspecialidadPrincipal.MedicinaGeneral
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

                                    {/* <Grid item sm={6} xs={12}>
                                        <TextField
                                            name="txtNumeroSala"
                                            label="Número de sala IceLink:"
                                            variant="outlined"
                                            fullWidth
                                            value={formColaborador.txtNumeroSala}
                                            onChange={handleChangeFormColaborador}
                                            error={!formularioValidado.txtNumeroSala}
                                            helperText={
                                                !formularioValidado.txtNumeroSala
                                                    ? "El número de asignación de sala es requerido"
                                                    : ""
                                            }
                                        />
                                    </Grid> */}
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
                                            name="txtNombreConsultorio"
                                            label="Nombre del consultorio:"
                                            variant="outlined"
                                            fullWidth
                                            value={formColaborador.txtNombreConsultorio}
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
                                            error={!formularioValidado.txtUrlDoctor}
                                            helperText={
                                                !formularioValidado.txtUrlDoctor
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
                                            error={!formularioValidado.txtMapsDoctor}
                                            helperText={
                                                !formularioValidado.txtMapsDoctor
                                                    ? "Ingrese una URL de Google Maps válida. Debe comenzar con http o https."
                                                    : ""
                                            }
                                            InputProps={{
                                                endAdornment: (
                                                    <Tooltip
                                                        title={
                                                            <Fragment>
                                                                Para obtener su enlace de ubicación:
                                                                <br />
                                                                1. Ingrese a maps.google.com.mx
                                                                <br />
                                                                2. Localice la ubicación de su consultorio
                                                                <br />
                                                                3. Seleccione Compartir del ménu lateral izquierdo
                                                                <br />
                                                                4. Seleccione en Copiar Vínculo y pegue la url
                                                                compartida
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
                            <MeditocTabPanel id={1} index={indiceTabForm}>
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
                                            error={!formularioValidado.txtNombreDoctor}
                                            helperText={
                                                !formularioValidado.txtNombreDoctor
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
                                            error={!formularioValidado.txtApellidoPaterno}
                                            helperText={
                                                !formularioValidado.txtApellidoPaterno
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
                                            error={!formularioValidado.txtFechaNacimiento}
                                            helperText={
                                                !formularioValidado.txtFechaNacimiento
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
                                                inputComponent: MeditocInputPhone,
                                            }}
                                            required
                                            value={formColaborador.txtTelefono}
                                            onChange={handleChangeFormColaborador}
                                            error={!formularioValidado.txtTelefono}
                                            helperText={
                                                !formularioValidado.txtTelefono
                                                    ? "El teléfono personal es requerido"
                                                    : ""
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
                                            InputProps={{
                                                endAdornment: (
                                                    <Tooltip
                                                        title="El correo proporcionado será usado para la recuperación de las credenciales."
                                                        arrow
                                                        placement="top"
                                                    >
                                                        <IconButton>
                                                            <InfoIcon />
                                                        </IconButton>
                                                    </Tooltip>
                                                ),
                                            }}
                                            value={formColaborador.txtCorreoElectronico}
                                            onChange={handleChangeFormColaborador}
                                            error={!formularioValidado.txtCorreoElectronico}
                                            helperText={
                                                !formularioValidado.txtCorreoElectronico
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
                            <MeditocTabPanel id={2} index={indiceTabForm}>
                                <Grid container spacing={3}>
                                    <Grid item xs={12} className="center">
                                        <FormControlLabel
                                            control={
                                                <Checkbox
                                                    checked={formColaborador.txtAcceso}
                                                    onChange={(e) => {
                                                        setFormColaborador({
                                                            ...formColaborador,
                                                            txtAcceso: e.target.checked,
                                                        });
                                                    }}
                                                />
                                            }
                                            label="Dar permisos de acceso al Portal Meditoc CallCenter"
                                        />
                                    </Grid>
                                    <Grid item xs={12}>
                                        <Collapse in={formColaborador.txtAcceso}>
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
                                                        required={formColaborador.txtAcceso}
                                                        value={formColaborador.txtUsuarioTitular}
                                                        onChange={handleChangeFormColaborador}
                                                        error={!formularioValidado.txtUsuarioTitular}
                                                        helperText={
                                                            !formularioValidado.txtUsuarioTitular
                                                                ? "El usuario titular es requerido"
                                                                : ""
                                                        }
                                                    />
                                                </Grid>
                                                <Grid item sm={6} xs={12}>
                                                    <TextField
                                                        variant="outlined"
                                                        label="Contraseña:"
                                                        type={mostrarPasswordTitular ? "text" : "password"}
                                                        autoComplete="new-password"
                                                        fullWidth
                                                        name="txtPasswordTitular"
                                                        required={formColaborador.txtAcceso}
                                                        value={formColaborador.txtPasswordTitular}
                                                        onChange={handleChangeFormColaborador}
                                                        error={!formularioValidado.txtPasswordTitular}
                                                        helperText={
                                                            !formularioValidado.txtPasswordTitular
                                                                ? "La contraseña titular es requerida"
                                                                : ""
                                                        }
                                                        InputProps={{
                                                            endAdornment: (
                                                                <Tooltip
                                                                    title={
                                                                        mostrarPasswordTitular
                                                                            ? "Ocultar contraseña"
                                                                            : "Ver contraseña"
                                                                    }
                                                                    arrow
                                                                    placement="top"
                                                                >
                                                                    <IconButton
                                                                        onMouseDown={() =>
                                                                            setMostrarPasswordTitular(true)
                                                                        }
                                                                        onMouseUp={() =>
                                                                            setMostrarPasswordTitular(false)
                                                                        }
                                                                    >
                                                                        {mostrarPasswordTitular ? (
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
                                                                required={formColaborador.txtAcceso}
                                                                value={formColaborador.txtUsuarioAdministrativo}
                                                                onChange={handleChangeFormColaborador}
                                                                error={!formularioValidado.txtUsuarioAdministrativo}
                                                                helperText={
                                                                    !formularioValidado.txtUsuarioAdministrativo
                                                                        ? "El usuario administrativo es requerido"
                                                                        : ""
                                                                }
                                                            />
                                                        </Grid>
                                                        <Grid item sm={6} xs={12}>
                                                            <TextField
                                                                variant="outlined"
                                                                label="Contraseña:"
                                                                type={
                                                                    mostrarPasswordAdministrativo ? "text" : "password"
                                                                }
                                                                autoComplete="new-password"
                                                                fullWidth
                                                                name="txtPasswordAdministrativo"
                                                                required={formColaborador.txtAcceso}
                                                                value={formColaborador.txtPasswordAdministrativo}
                                                                onChange={handleChangeFormColaborador}
                                                                error={!formularioValidado.txtPasswordAdministrativo}
                                                                helperText={
                                                                    !formularioValidado.txtPasswordAdministrativo
                                                                        ? "La contraseña administrativa es requerida"
                                                                        : ""
                                                                }
                                                                InputProps={{
                                                                    endAdornment: (
                                                                        <Tooltip
                                                                            title={
                                                                                mostrarPasswordAdministrativo
                                                                                    ? "Ocultar contraseña"
                                                                                    : "Ver contraseña"
                                                                            }
                                                                            arrow
                                                                            placement="top"
                                                                        >
                                                                            <IconButton
                                                                                onMouseDown={() =>
                                                                                    setMostrarPasswordAdministrativo(
                                                                                        true
                                                                                    )
                                                                                }
                                                                                onMouseUp={() =>
                                                                                    setMostrarPasswordAdministrativo(
                                                                                        false
                                                                                    )
                                                                                }
                                                                            >
                                                                                {mostrarPasswordAdministrativo ? (
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

                                                {entColaborador.iIdTipoDoctor !== EnumTipoDoctor.Especialista && (
                                                    <Grid item xs={12} className="center">
                                                        <FormControlLabel
                                                            control={
                                                                <Checkbox
                                                                    checked={formColaborador.txtAdministrativo}
                                                                    onChange={(e) => {
                                                                        setFormColaborador({
                                                                            ...formColaborador,
                                                                            txtAdministrativo: e.target.checked,
                                                                        });
                                                                    }}
                                                                />
                                                            }
                                                            label="Dar permisos administrativos como Director"
                                                        />
                                                    </Grid>
                                                )}
                                            </Grid>
                                        </Collapse>
                                    </Grid>
                                </Grid>
                            </MeditocTabPanel>
                        </MeditocTabBody>
                    </Grid>
                    <MeditocModalBotones okMessage="Guardar" okFunc={handleClickGuardarColaborador} setOpen={setOpen} />
                </Grid>
            </form>
        </MeditocModal>
    );
};

FormColaborador.propTypes = {
    entColaborador: PropTypes.shape({
        bAcceso: PropTypes.bool,
        bAdministrador: PropTypes.bool,
        dtFechaNacimientoDoctor: PropTypes.object,
        iIdColaborador: PropTypes.number,
        iIdEspecialidad: PropTypes.number,
        iIdTipoDoctor: PropTypes.number,
        iIdUsuarioCGU: PropTypes.number,
        iNumSala: PropTypes.number,
        sApellidoMaternoDoctor: PropTypes.string,
        sApellidoPaternoDoctor: PropTypes.string,
        sCedulaProfecional: PropTypes.string,
        sCorreoDirectorio: PropTypes.string,
        sCorreoDoctor: PropTypes.string,
        sDireccionConsultorio: PropTypes.string,
        sDomicilioDoctor: PropTypes.string,
        sMaps: PropTypes.string,
        sNombreConsultorio: PropTypes.string,
        sNombreDirectorio: PropTypes.string,
        sNombresDoctor: PropTypes.string,
        sRFC: PropTypes.string,
        sTelefonoDirectorio: PropTypes.string,
        sTelefonoDoctor: PropTypes.string,
        sURL: PropTypes.string,
        sUsuarioAdministrativo: PropTypes.string,
        sUsuarioTitular: PropTypes.string,
        sWhatsApp: PropTypes.string,
    }),
    funcAlert: PropTypes.func,
    funcGetColaboradores: PropTypes.func,
    funcLoader: PropTypes.func,
    listaEspecialidades: PropTypes.array,
    open: PropTypes.bool,
    setOpen: PropTypes.func,
    usuarioSesion: PropTypes.shape({
        iIdUsuario: PropTypes.number,
    }),
};

export default FormColaborador;
