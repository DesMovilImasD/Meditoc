import React from "react";
import MeditocModal from "../../../utilidades/MeditocModal";
import { Grid, Button } from "@material-ui/core";
import { DateTimePicker } from "@material-ui/pickers";
import { useState } from "react";
import MeditocModalBotones from "../../../utilidades/MeditocModalBotones";

const ModificarVigencia = (props) => {
    const { entEmpresa, open, setOpen } = props;

    const [txtFechaVigencia, setTxtFechaVigencia] = useState(null);
    const [txtFechaVigenciaOK, setTxtFechaVigenciaOK] = useState(true);

    const handleChangeVigencia = (date) => {
        if (date !== null && date !== "" && !txtFechaVigenciaOK) {
            setTxtFechaVigenciaOK(true);
        }

        setTxtFechaVigencia(date);
    };

    const handleClickModificarVigencia = () => {
        if (txtFechaVigencia === null || txtFechaVigencia === "") {
            setTxtFechaVigenciaOK(false);
        }
    };

    return (
        <MeditocModal title="Extender la vigencia de los folios" size="small" open={open} setOpen={setOpen}>
            <Grid container spacing={3}>
                <Grid item xs={12}>
                    <DateTimePicker
                        disablePast
                        label="Nueva fecha de vigencia"
                        fullWidth
                        inputVariant="outlined"
                        views={["year", "month", "date", "hours", "minutes"]}
                        InputAdornmentProps={{ position: "end" }}
                        openTo="year"
                        required
                        format="DD/MM/YYYY hh:mm a"
                        value={txtFechaVigencia}
                        onChange={handleChangeVigencia}
                        error={!txtFechaVigenciaOK}
                        helperText={!txtFechaVigenciaOK ? "Ingrese una fecha de vencimiento válida" : ""}
                    />
                </Grid>
                <MeditocModalBotones okFunc={handleClickModificarVigencia} setOpen={setOpen} />
            </Grid>
        </MeditocModal>
    );
};

export default ModificarVigencia;
