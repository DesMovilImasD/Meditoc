import React from "react";
import MeditocModal from "../../../utilidades/MeditocModal";
import { Grid, Button } from "@material-ui/core";
import { DateTimePicker } from "@material-ui/pickers";
import { useState } from "react";

const AgregarVigencia = (props) => {
    const { entEmpresa, open, setOpen } = props;

    const [txtFechaVigencia, setTxtFechaVigencia] = useState(null);

    const handleChangeVigencia = (date) => {
        setTxtFechaVigencia(date);
    };

    //Funcion para cerrar este modal
    const handleClose = () => {
        setOpen(false);
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
                        format="DD/MM/YYYY hh:mm a"
                        value={txtFechaVigencia}
                        onChange={handleChangeVigencia}
                    />
                </Grid>
                <Grid item sm={6} xs={12}>
                    <Button variant="contained" color="primary" fullWidth>
                        CONFIRMAR
                    </Button>
                </Grid>
                <Grid item sm={6} xs={12}>
                    <Button variant="contained" color="secondary" fullWidth onClick={handleClose}>
                        Cancelar
                    </Button>
                </Grid>
            </Grid>
        </MeditocModal>
    );
};

export default AgregarVigencia;
