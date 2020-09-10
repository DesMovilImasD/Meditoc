import React from "react";
import MeditocModal from "../../../utilidades/MeditocModal";
import { Grid, Button } from "@material-ui/core";
import { DateTimePicker } from "@material-ui/pickers";
import { useState } from "react";
import MeditocModalBotones from "../../../utilidades/MeditocModalBotones";
import FolioController from "../../../../controllers/FolioController";

const ModificarVigencia = (props) => {
    const {
        entEmpresa,
        open,
        setOpen,
        foliosEmpresaSeleccionado,
        funcGetFoliosEmpresa,
        usuarioSesion,
        funcLoader,
        funcAlert,
    } = props;

    const [txtFechaVigencia, setTxtFechaVigencia] = useState(null);
    const [txtFechaVigenciaOK, setTxtFechaVigenciaOK] = useState(true);

    const handleChangeVigencia = (date) => {
        if (date !== null && date !== "" && !txtFechaVigenciaOK) {
            setTxtFechaVigenciaOK(true);
        }

        setTxtFechaVigencia(date);
    };

    const handleClickModificarVigencia = async () => {
        if (txtFechaVigencia === null || txtFechaVigencia === "") {
            setTxtFechaVigenciaOK(false);
            return;
        }

        const entFolioFVSubmit = {
            iIdEmpresa: entEmpresa.iIdEmpresa,
            iIdUsuario: usuarioSesion.iIdUsuario,
            dtFechaVencimiento: txtFechaVigencia.toLocaleString(),
            lstFolios: foliosEmpresaSeleccionado.map((folio) => ({ iIdFolio: folio.iIdFolio })),
        };

        funcLoader(true, "Actualizado registros...");

        const folioController = new FolioController();

        const response = await folioController.funcUpdFechaVencimiento(entFolioFVSubmit);

        if (response.Code === 0) {
            funcAlert(response.Message, "success");
            setOpen(false);
            setTxtFechaVigencia(null);
            await funcGetFoliosEmpresa();
        } else {
            funcAlert(response.Message);
        }
        funcLoader();
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
                        format="dd/MM/yyy hh:mm a"
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
