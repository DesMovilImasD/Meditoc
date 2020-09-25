import {
    FormControl,
    FormControlLabel,
    FormLabel,
    Grid,
    IconButton,
    InputAdornment,
    MenuItem,
    Radio,
    RadioGroup,
    TextField,
} from "@material-ui/core";
import { DatePicker } from "@material-ui/pickers";
import React, { useState } from "react";
import MeditocModalBotones from "../../../utilidades/MeditocModalBotones";
import DateRangeIcon from "@material-ui/icons/DateRange";
import CallCenterController from "../../../../controllers/CallCenterController";
import InputTelefono from "../../../utilidades/InputTelefono";
import { EnumCatSexo } from "../../../../configurations/enumConfig";

const FormPaciente = (props) => {
    const { usuarioSesion, funcLoader, funcAlert, usuarioColaborador, entCallCenter, setEntCallCenter } = props;

    const [formPaciente, setFormPaciente] = useState({
        txtCCFolio: entCallCenter.entFolio.sFolio,
        txtCCNombre: entCallCenter.entPaciente.name,
        txtCCTelefono: entCallCenter.entPaciente.phone,
        txtCCCorreo: entCallCenter.entPaciente.email,
        txtCCFechaNacimiento:
            entCallCenter.entPaciente.dtFechaNacimiento === null
                ? null
                : new Date(entCallCenter.entPaciente.dtFechaNacimiento),
        txtCCTipoSangre: entCallCenter.entPaciente.sTipoSangre,
        txtCCSexo: entCallCenter.entPaciente.iIdSexo.toString(),
    });

    const handleChangeFormPaciente = (e) => {
        setFormPaciente({
            ...formPaciente,
            [e.target.name]: e.target.value,
        });
    };

    const handleClickSavePaciente = async () => {
        const entUpdPaciente = {
            iIdPaciente: entCallCenter.entPaciente.iIdPaciente,
            sNombre: formPaciente.txtCCNombre,
            sCorreo: formPaciente.txtCCCorreo,
            sTelefono: formPaciente.txtCCTelefono,
            sTipoSangre: formPaciente.txtCCTipoSangre,
            dtFechaNacimiento:
                formPaciente.txtCCFechaNacimiento === null
                    ? null
                    : formPaciente.txtCCFechaNacimiento.toLocaleDateString(),
            iIdSexo: parseInt(formPaciente.txtCCSexo),
            iIdUsuarioMod: usuarioSesion.iIdUsuario,
        };

        const callcenterController = new CallCenterController();

        funcLoader(true, "Actualizando datos del paciente...");

        const response = await callcenterController.funcSavePaciente(entUpdPaciente);

        if (response.Code === 0) {
            funcAlert(response.Message, "success");
            setEntCallCenter({
                ...entCallCenter,
                entPaciente: {
                    ...entCallCenter.entPaciente,
                    name: entUpdPaciente.sNombre,
                    email: entUpdPaciente.sCorreo,
                    phone: entUpdPaciente.sTelefono,
                    dtFechaNacimiento: formPaciente.txtCCFechaNacimiento,
                    sTipoSangre: entUpdPaciente.sTipoSangre,
                    iIdSexo: entUpdPaciente.iIdSexo,
                },
            });
        } else {
            funcAlert(response.Message);
        }
        funcLoader();
    };

    return (
        <Grid container spacing={3}>
            <Grid item sm={6} xs={12}>
                <TextField
                    name="txtCCFolio"
                    label="Folio:"
                    variant="outlined"
                    fullWidth
                    disabled
                    value={formPaciente.txtCCFolio}
                    onChange={handleChangeFormPaciente}
                />
            </Grid>
            <Grid item sm={6} xs={12}>
                <TextField
                    name="txtCCNombre"
                    label="Nombre:"
                    variant="outlined"
                    fullWidth
                    value={formPaciente.txtCCNombre}
                    onChange={handleChangeFormPaciente}
                />
            </Grid>
            <Grid item sm={6} xs={12}>
                <TextField
                    name="txtCCTelefono"
                    label="Teléfono:"
                    variant="outlined"
                    InputProps={{
                        inputComponent: InputTelefono,
                    }}
                    fullWidth
                    value={formPaciente.txtCCTelefono}
                    onChange={handleChangeFormPaciente}
                />
            </Grid>
            <Grid item sm={6} xs={12}>
                <TextField
                    name="txtCCCorreo"
                    label="Correo electrónico:"
                    variant="outlined"
                    fullWidth
                    value={formPaciente.txtCCCorreo}
                    onChange={handleChangeFormPaciente}
                />
            </Grid>

            <Grid item sm={6} xs={12}>
                <DatePicker
                    name="txtCCFechaNacimiento"
                    label="Fecha de nacimiento:"
                    variant="inline"
                    inputVariant="outlined"
                    fullWidth
                    views={["year", "month", "date"]}
                    openTo="year"
                    format="dd/MM/yyyy"
                    InputProps={{
                        endAdornment: (
                            <InputAdornment position="end">
                                <IconButton>
                                    <DateRangeIcon />
                                </IconButton>
                            </InputAdornment>
                        ),
                    }}
                    disableFuture
                    value={formPaciente.txtCCFechaNacimiento}
                    onChange={(date) =>
                        setFormPaciente({
                            ...formPaciente,
                            txtCCFechaNacimiento: date,
                        })
                    }
                />
            </Grid>
            <Grid item sm={6} xs={12}>
                <TextField
                    name="txtCCTipoSangre"
                    label="Tipo de sangre:"
                    variant="outlined"
                    fullWidth
                    select
                    value={formPaciente.txtCCTipoSangre}
                    onChange={handleChangeFormPaciente}
                >
                    <MenuItem value="A+">A+</MenuItem>
                    <MenuItem value="A-">A-</MenuItem>
                    <MenuItem value="B+">B+</MenuItem>
                    <MenuItem value="B-">B-</MenuItem>
                    <MenuItem value="AB+">AB+</MenuItem>
                    <MenuItem value="AB-">AB-</MenuItem>
                    <MenuItem value="O+">O+</MenuItem>
                    <MenuItem value="O-">O-</MenuItem>
                </TextField>
            </Grid>
            <Grid item xs={12}>
                <FormControl>
                    <FormLabel>Sexo</FormLabel>
                    <RadioGroup name="txtCCSexo" row value={formPaciente.txtCCSexo} onChange={handleChangeFormPaciente}>
                        <FormControlLabel value={EnumCatSexo.Hombre.toString()} control={<Radio />} label="Hombre" />
                        <FormControlLabel value={EnumCatSexo.Mujer.toString()} control={<Radio />} label="Mujer" />
                    </RadioGroup>
                </FormControl>
            </Grid>
            <MeditocModalBotones
                hideCancel
                okMessage="Actualizar datos del paciente"
                okFunc={handleClickSavePaciente}
            />
        </Grid>
    );
};

export default FormPaciente;
