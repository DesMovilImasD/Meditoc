import { Divider, Grid, IconButton, InputAdornment, TextField } from "@material-ui/core";
import { DateTimePicker } from "@material-ui/pickers";
import React, { useEffect, useState } from "react";
import { rxCorreo } from "../../../../configurations/regexConfig";
import InputTelefono from "../../../utilidades/InputTelefono";
import MeditocModal from "../../../utilidades/MeditocModal";
import MeditocModalBotones from "../../../utilidades/MeditocModalBotones";
import DateRangeIcon from "@material-ui/icons/DateRange";
import CallCenterController from "../../../../controllers/CallCenterController";

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

    const [formConsultaOK, setFormConsultaOK] = useState({
        txtFolio: true,
        txtNombrePaciente: true,
        txtTelefonoPaciente: true,
        txtCorreoPaciente: true,
        txtFechaProgramadaInicio: true,
        txtFechaProgramadaFin: true,
    });

    useEffect(() => {
        setFormConsulta({
            txtFolio: entConsulta.sFolio === undefined ? "" : entConsulta.sFolio,
            txtNombrePaciente: entConsulta.sNombrePaciente === undefined ? "" : entConsulta.sNombrePaciente,
            txtTelefonoPaciente: "",
            txtCorreoPaciente: "",
            txtFechaProgramadaInicio:
                entConsulta.dtFechaProgramadaInicio === undefined
                    ? null
                    : new Date(entConsulta.dtFechaProgramadaInicio),
            txtFechaProgramadaFin:
                entConsulta.dtFechaProgramadaFin === undefined ? null : new Date(entConsulta.dtFechaProgramadaFin),
        });
    }, [entConsulta]);

    const handleChangeFormulario = (e) => {
        const nombreCampo = e.target.name;
        const valorCampo = e.target.value;

        switch (nombreCampo) {
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
                });
            }
        }

        setFormConsulta({
            ...formConsulta,
            txtFechaProgramadaInicio: date,
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

        let formConsultaOKValidacion = {
            txtNombrePaciente: true,
            txtTelefonoPaciente: true,
            txtCorreoPaciente: true,
            txtFechaProgramadaInicio: true,
            txtFechaProgramadaFin: true,
        };

        if (formConsulta.txtNombrePaciente === "") {
            formConsultaOKValidacion.txtNombrePaciente = false;
            formError = true;
        }

        if (entConsulta.iIdConsulta === 0) {
            const telefonoValidacion = formConsulta.txtTelefonoPaciente.replace(/ /g, "");

            if (telefonoValidacion === "" || telefonoValidacion.length !== 10) {
                formConsultaOKValidacion.txtTelefonoPaciente = false;
                formError = true;
            }

            if (formConsulta.txtCorreoPaciente === "" || !rxCorreo.test(formConsulta.txtCorreoPaciente)) {
                formConsultaOKValidacion.txtCorreoPaciente = false;
                formError = true;
            }
        }

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

    return (
        <MeditocModal
            title={entConsulta.iIdConsulta === 0 ? "Nueva consulta" : "Editar consulta"}
            size="normal"
            open={open}
            setOpen={setOpen}
        >
            <Grid container spacing={3}>
                <Grid item xs={12}>
                    <span className="rob-nor bold size-15 color-4">FOLIO DEL PACIENTE</span>
                    <br />
                    {entConsulta.iIdConsulta === 0 ? (
                        <span className="rob-nor size-15 color-4">
                            (Ingresar solamente si el paciente ya cuenta con un folio)
                        </span>
                    ) : null}

                    <Divider />
                </Grid>
                <Grid item xs={12}>
                    <TextField
                        name="txtFolio"
                        label="Folio:"
                        variant="outlined"
                        fullWidth
                        disabled={entConsulta.iIdConsulta !== 0}
                        value={formConsulta.txtFolio}
                        onChange={handleChangeFormulario}
                    />
                </Grid>
                <Grid item xs={12}>
                    <span className="rob-nor bold size-15 color-4">DATOS DEL PACIENTE</span>
                    <Divider />
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
                        helperText={!formConsultaOK.txtCorreoPaciente ? "Ingrese un correo electrónico válido" : ""}
                    />
                </Grid>
                <Grid item xs={12}>
                    <span className="rob-nor bold size-15 color-4">HORARIO DE CONSULTA</span>
                    <Divider />
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
                <MeditocModalBotones okMessage="GUARDAR CONSULTA" setOpen={setOpen} okFunc={handleClickSaveConsulta} />
            </Grid>
        </MeditocModal>
    );
};

export default Consulta;
