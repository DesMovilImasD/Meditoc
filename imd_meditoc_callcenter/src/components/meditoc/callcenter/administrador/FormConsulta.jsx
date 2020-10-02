import {
    Button,
    Checkbox,
    Collapse,
    Divider,
    FormControlLabel,
    Grid,
    IconButton,
    InputAdornment,
    TextField,
} from "@material-ui/core";
import { DateTimePicker } from "@material-ui/pickers";
import React, { useEffect, useState } from "react";
import { rxCorreo } from "../../../../configurations/regexConfig";
import InputTelefono from "../../../utilidades/InputTelefono";
import MeditocModal from "../../../utilidades/MeditocModal";
import MeditocModalBotones from "../../../utilidades/MeditocModalBotones";
import DateRangeIcon from "@material-ui/icons/DateRange";
import CallCenterController from "../../../../controllers/CallCenterController";
import { CheckBox, SignalCellularNull, TramRounded } from "@material-ui/icons";
import FolioController from "../../../../controllers/FolioController";
import MeditocSubtitulo from "../../../utilidades/MeditocSubtitulo";

const Consulta = (props) => {
    const {
        entConsulta,
        open,
        setOpen,
        usuarioColaborador,
        funcGetConsultas,
        consultaEntidadVacia,
        setConsultaSeleccionada,
        usuarioSesion,
        funcLoader,
        funcAlert,
    } = props;

    const [formConsulta, setFormConsulta] = useState({
        txtFolio: "",
        txtNombrePaciente: "",
        txtTelefonoPaciente: "",
        txtCorreoPaciente: "",
        txtFechaProgramadaInicio: null,
        txtFechaProgramadaFin: null,
    });

    const validacionFormulario = {
        txtFolio: true,
        txtNombrePaciente: true,
        txtTelefonoPaciente: true,
        txtCorreoPaciente: true,
        txtFechaProgramadaInicio: true,
        txtFechaProgramadaFin: true,
    };

    const [formConsultaOK, setFormConsultaOK] = useState(validacionFormulario);

    const [yaCuentoConFolio, setYaCuentoConFolio] = useState(false);
    const [folioValidado, setFolioValidado] = useState(false);

    const [entFolioValidado, setEntFolioValidado] = useState(null);

    useEffect(() => {
        setFormConsulta({
            txtFolio: entConsulta.sFolio === undefined ? "" : entConsulta.sFolio,
            txtNombrePaciente: entConsulta.sNombrePaciente === undefined ? "" : entConsulta.sNombrePaciente,
            txtTelefonoPaciente: entConsulta.sTelefonoPaciente === undefined ? "" : entConsulta.sTelefonoPaciente,
            txtCorreoPaciente: entConsulta.sCorreoPaciente === undefined ? "" : entConsulta.sCorreoPaciente,
            txtFechaProgramadaInicio:
                entConsulta.dtFechaProgramadaInicio === undefined
                    ? null
                    : new Date(entConsulta.dtFechaProgramadaInicio),
            txtFechaProgramadaFin:
                entConsulta.dtFechaProgramadaFin === undefined ? null : new Date(entConsulta.dtFechaProgramadaFin),
        });
        setFormConsultaOK(validacionFormulario);
    }, [entConsulta]);

    const handleChangeFormulario = (e) => {
        const nombreCampo = e.target.name;
        const valorCampo = e.target.value;

        switch (nombreCampo) {
            case "txtFolio":
                if (!formConsultaOK.txtFolio) {
                    if (valorCampo !== "") {
                        setFormConsultaOK({
                            ...formConsultaOK,
                            [nombreCampo]: true,
                        });
                    }
                }
                setFolioValidado(false);
                break;
            case "txtNombrePaciente":
                if (!formConsultaOK.txtNombrePaciente) {
                    if (valorCampo !== "") {
                        setFormConsultaOK({
                            ...formConsultaOK,
                            [nombreCampo]: true,
                        });
                    }
                }
                break;
            case "txtTelefonoPaciente":
                if (!formConsultaOK.txtTelefonoPaciente && entConsulta.iIdConsulta === 0) {
                    const telefonoValidacion = valorCampo.replace(/ /g, "");
                    if (telefonoValidacion !== "" && telefonoValidacion.length === 10) {
                        setFormConsultaOK({
                            ...formConsultaOK,
                            [nombreCampo]: true,
                        });
                    }
                }
                break;
            case "txtCorreoPaciente":
                if (!formConsultaOK.txtCorreoPaciente && entConsulta.iIdConsulta === 0) {
                    if (valorCampo !== "" && rxCorreo.test(valorCampo)) {
                        setFormConsultaOK({
                            ...formConsultaOK,
                            [nombreCampo]: true,
                        });
                    }
                }
                break;

            default:
                break;
        }

        setFormConsulta({
            ...formConsulta,
            [nombreCampo]: valorCampo,
        });
    };

    const handleChangeFechaInicio = (date) => {
        if (!formConsultaOK.txtFechaProgramadaInicio) {
            if (date !== "" && date !== null) {
                setFormConsultaOK({
                    ...formConsultaOK,
                    txtFechaProgramadaInicio: true,
                    txtFechaProgramadaFin: true,
                });
            }
        }

        let date2 = new Date(date.getTime());
        date2.setHours(date2.getHours() + 1);

        setFormConsulta({
            ...formConsulta,
            txtFechaProgramadaInicio: date,
            txtFechaProgramadaFin: date2,
        });
    };

    const handleChangeFechaFin = (date) => {
        if (!formConsultaOK.txtFechaProgramadaFin) {
            if (date !== "" && date !== null) {
                setFormConsultaOK({
                    ...formConsultaOK,
                    txtFechaProgramadaFin: true,
                });
            }
        }
        setFormConsulta({
            ...formConsulta,
            txtFechaProgramadaFin: date,
        });
    };

    const handleClickSaveConsulta = async () => {
        let formError = false;

        let formConsultaOKValidacion = { ...validacionFormulario };

        //if (!yaCuentoConFolio) {
        if (formConsulta.txtNombrePaciente === "") {
            formConsultaOKValidacion.txtNombrePaciente = false;
            formError = true;
        }

        if (entConsulta.iIdConsulta === 0) {
            const telefonoValidacion =
                formConsulta.txtTelefonoPaciente === null ? "" : formConsulta.txtTelefonoPaciente.replace(/ /g, "");

            if (telefonoValidacion === "" || telefonoValidacion.length !== 10) {
                formConsultaOKValidacion.txtTelefonoPaciente = false;
                formError = true;
            }

            if (formConsulta.txtCorreoPaciente === "" || !rxCorreo.test(formConsulta.txtCorreoPaciente)) {
                formConsultaOKValidacion.txtCorreoPaciente = false;
                formError = true;
            }
        }
        //} else {
        if (yaCuentoConFolio) {
            if (formConsulta.txtFolio === "") {
                formConsultaOKValidacion.txtFolio = false;
                formError = true;
            }
        }
        //}

        if (formConsulta.txtFechaProgramadaInicio === null) {
            formConsultaOKValidacion.txtFechaProgramadaInicio = false;
            formError = true;
        }

        if (formConsulta.txtFechaProgramadaFin === null) {
            formConsultaOKValidacion.txtFechaProgramadaFin = false;
            formError = true;
        }

        setFormConsultaOK(formConsultaOKValidacion);

        if (formError) {
            return;
        }

        const entNuevaConsulta = {
            sFolio: formConsulta.txtFolio,
            customerInfo: {
                name: formConsulta.txtNombrePaciente,
                phone: formConsulta.txtTelefonoPaciente,
                email: formConsulta.txtCorreoPaciente,
            },
            consulta: {
                iIdConsulta: entConsulta.iIdConsulta,
                iIdColaborador: usuarioColaborador.iIdColaborador,
                dtFechaProgramadaInicio: formConsulta.txtFechaProgramadaInicio.toLocaleString(),
                dtFechaProgramadaFin: formConsulta.txtFechaProgramadaFin.toLocaleString(),
            },
            iIdUsuarioMod: usuarioSesion.iIdUsuario,
        };

        funcLoader(true, "Guardando consulta...");

        const callcenterController = new CallCenterController();
        const response = await callcenterController.funcNuevaConsulta(entNuevaConsulta);

        if (response.Code === 0) {
            setOpen(false);
            await funcGetConsultas();
            setConsultaSeleccionada(consultaEntidadVacia);
            funcAlert(response.Message, "success");
        } else {
            funcAlert(response.Message);
        }

        funcLoader();
    };

    const funcValidarFolio = async () => {
        let formError = false;

        let formConsultaOKValidacion = { ...validacionFormulario };

        if (formConsulta.txtFolio === "") {
            formConsultaOKValidacion.txtFolio = false;
            formError = true;
        }

        setFormConsultaOK(formConsultaOKValidacion);

        if (formError) {
            return;
        }
        funcLoader(true, "Validando folio...");

        const folioController = new FolioController();
        const response = await folioController.funcGetFolios(
            null,
            null,
            null,
            null,
            formConsulta.txtFolio,
            "",
            null,
            "",
            ""
        );

        if (response.Code === 0) {
            if (response.Result.length > 0) {
                setFolioValidado(true);
                const folioEncontrado = response.Result[0];
                setEntFolioValidado(folioEncontrado);
                setFormConsulta({
                    ...formConsulta,
                    txtFolio: folioEncontrado.sFolio === null ? "" : folioEncontrado.sFolio,
                    txtNombrePaciente: folioEncontrado.sNombrePaciente === null ? "" : folioEncontrado.sNombrePaciente,
                    txtTelefonoPaciente:
                        folioEncontrado.sTelefonoPaciente === null ? "" : folioEncontrado.sTelefonoPaciente,
                    txtCorreoPaciente: folioEncontrado.sCorreoPaciente === null ? "" : folioEncontrado.sCorreoPaciente,
                });
                setFormConsultaOK(validacionFormulario);
            } else {
                funcAlert("No se encontró el folio solicitado");
            }
        } else {
            funcAlert(response.Message);
        }

        funcLoader();
    };

    const funcLimpiarValidarFolio = () => {
        setFolioValidado(false);
        setFormConsulta({
            ...formConsulta,
            txtFolio: entConsulta.sFolio === undefined ? "" : entConsulta.sFolio,
            txtNombrePaciente: entConsulta.sNombrePaciente === undefined ? "" : entConsulta.sNombrePaciente,
            txtTelefonoPaciente: entConsulta.sTelefonoPaciente === undefined ? "" : entConsulta.sTelefonoPaciente,
            txtCorreoPaciente: entConsulta.sCorreoPaciente === undefined ? "" : entConsulta.sCorreoPaciente,
        });
        setFormConsultaOK(validacionFormulario);
    };

    const handleChangeCuentoConFolio = (e) => {
        const cuentaConFolio = e.target.checked;
        setYaCuentoConFolio(cuentaConFolio);
        if (cuentaConFolio === false) {
            setFolioValidado(false);
        }
    };

    return (
        <MeditocModal
            title={entConsulta.iIdConsulta === 0 ? "Nueva consulta" : "Editar consulta"}
            size="normal"
            open={open}
            setOpen={setOpen}
        >
            <Grid container spacing={3}>
                <Grid item xs={12}>
                    <FormControlLabel
                        control={
                            <Checkbox
                                checked={yaCuentoConFolio}
                                onChange={handleChangeCuentoConFolio}
                                name="chkCuentoConFolio"
                            />
                        }
                        label="Seleccione si el paciente ya cuenta con un folio asignado"
                    />
                </Grid>
                <Grid item xs={12}>
                    <Collapse in={yaCuentoConFolio}>
                        <Grid container spacing={3}>
                            <Grid item xs={12}>
                                <MeditocSubtitulo title="FOLIO DEL PACIENTE" />
                            </Grid>
                            <Grid item xs={12}>
                                <TextField
                                    name="txtFolio"
                                    label="Folio:"
                                    variant="outlined"
                                    required={yaCuentoConFolio}
                                    fullWidth
                                    disabled={entConsulta.iIdConsulta !== 0}
                                    value={yaCuentoConFolio ? formConsulta.txtFolio : ""}
                                    onChange={handleChangeFormulario}
                                    error={!formConsultaOK.txtFolio}
                                    helperText={!formConsultaOK.txtFolio ? "El folio es requerido" : ""}
                                />
                            </Grid>
                            <MeditocModalBotones
                                okMessage="Validar folio"
                                okFunc={funcValidarFolio}
                                cancelMessage="Limpiar"
                                cancelFunc={funcLimpiarValidarFolio}
                            />
                        </Grid>
                    </Collapse>
                </Grid>
                <Grid item xs={12}>
                    <Collapse in={!yaCuentoConFolio || folioValidado}>
                        <Grid container spacing={3}>
                            <Grid item xs={12}>
                                <MeditocSubtitulo title="DATOS DEL PACIENTE" />
                            </Grid>
                            <Grid item xs={12}>
                                <TextField
                                    name="txtNombrePaciente"
                                    label="Nombre:"
                                    variant="outlined"
                                    fullWidth
                                    required
                                    disabled={entConsulta.iIdConsulta !== 0}
                                    value={formConsulta.txtNombrePaciente}
                                    onChange={handleChangeFormulario}
                                    error={!formConsultaOK.txtNombrePaciente}
                                    helperText={!formConsultaOK.txtNombrePaciente ? "El nombre es requerido" : ""}
                                />
                            </Grid>
                            <Grid item sm={6} xs={12}>
                                <TextField
                                    name="txtTelefonoPaciente"
                                    label="Teléfono:"
                                    variant="outlined"
                                    fullWidth
                                    required
                                    disabled={entConsulta.iIdConsulta !== 0}
                                    InputProps={{
                                        inputComponent: InputTelefono,
                                    }}
                                    value={formConsulta.txtTelefonoPaciente}
                                    onChange={handleChangeFormulario}
                                    error={!formConsultaOK.txtTelefonoPaciente}
                                    helperText={!formConsultaOK.txtTelefonoPaciente ? "Ingrese un teléfono válido" : ""}
                                />
                            </Grid>
                            <Grid item sm={6} xs={12}>
                                <TextField
                                    name="txtCorreoPaciente"
                                    label="Correo:"
                                    variant="outlined"
                                    fullWidth
                                    required
                                    disabled={entConsulta.iIdConsulta !== 0}
                                    value={formConsulta.txtCorreoPaciente}
                                    onChange={handleChangeFormulario}
                                    error={!formConsultaOK.txtCorreoPaciente}
                                    helperText={
                                        !formConsultaOK.txtCorreoPaciente ? "Ingrese un correo electrónico válido" : ""
                                    }
                                />
                            </Grid>
                        </Grid>
                    </Collapse>
                </Grid>
                <Grid item xs={12}>
                    <MeditocSubtitulo title="HORARIO DE CONSULTA" />
                </Grid>
                <Grid item sm={6} xs={12}>
                    <DateTimePicker
                        name="txtFechaProgramadaInicio"
                        label="Fecha y hora de inicio"
                        variant="inline"
                        inputVariant="outlined"
                        fullWidth
                        required
                        format="dd/MM/yyyy hh:mm a"
                        InputProps={{
                            endAdornment: (
                                <InputAdornment position="end">
                                    <IconButton>
                                        <DateRangeIcon />
                                    </IconButton>
                                </InputAdornment>
                            ),
                        }}
                        disablePast
                        value={formConsulta.txtFechaProgramadaInicio}
                        onChange={handleChangeFechaInicio}
                        error={!formConsultaOK.txtFechaProgramadaInicio}
                        helperText={
                            !formConsultaOK.txtFechaProgramadaInicio
                                ? "La fecha de inicio de consulta es requerido"
                                : ""
                        }
                    />
                </Grid>
                <Grid item sm={6} xs={12}>
                    <DateTimePicker
                        name="txtFechaProgramadaFin"
                        label="Fecha y hora de fin"
                        variant="inline"
                        inputVariant="outlined"
                        fullWidth
                        required
                        format="dd/MM/yyyy hh:mm a"
                        InputProps={{
                            endAdornment: (
                                <InputAdornment position="end">
                                    <IconButton>
                                        <DateRangeIcon />
                                    </IconButton>
                                </InputAdornment>
                            ),
                        }}
                        disablePast
                        value={formConsulta.txtFechaProgramadaFin}
                        onChange={handleChangeFechaFin}
                        error={!formConsultaOK.txtFechaProgramadaInicio}
                        helperText={
                            !formConsultaOK.txtFechaProgramadaInicio ? "La fecha de fin de consulta es requerido" : ""
                        }
                    />
                </Grid>
                <MeditocModalBotones
                    okMessage="GUARDAR CONSULTA"
                    setOpen={setOpen}
                    okFunc={handleClickSaveConsulta}
                    okDisabled={yaCuentoConFolio === true && folioValidado === false}
                />
            </Grid>
        </MeditocModal>
    );
};

export default Consulta;
