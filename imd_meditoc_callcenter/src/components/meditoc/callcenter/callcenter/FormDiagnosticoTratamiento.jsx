import PropTypes from "prop-types";
import { Grid, TextField } from "@material-ui/core";
import React from "react";

const FormDiagnosticoTratamiento = (props) => {
    const { formDiagnosticoTratamiento, setFormDiagnosticoTratamiento } = props;

    const handleChangeFormDYT = (e) => {
        const nombreCampo = e.target.name;
        const valorCampo = e.target.value;

        switch (nombreCampo) {
            case "txtCCPeso":
                if (valorCampo !== "") {
                    if (isNaN(valorCampo)) {
                        return;
                    }
                }
                break;

            case "txtCCAltura":
                if (valorCampo !== "") {
                    if (isNaN(valorCampo)) {
                        return;
                    }
                }
                break;

            default:
                break;
        }

        setFormDiagnosticoTratamiento({
            ...formDiagnosticoTratamiento,
            [nombreCampo]: valorCampo,
        });
    };

    return (
        <Grid container spacing={3}>
            <Grid item xs={12} className="center">
                <span className="rob-con normal size-15 color-3">
                    (Los datos de esta sección se guardarán automáticamente al finalizar la consulta).
                </span>
            </Grid>
            <Grid item sm={6} xs={12}>
                <TextField
                    name="txtCCPeso"
                    label="Peso (kg):"
                    variant="outlined"
                    fullWidth
                    value={formDiagnosticoTratamiento.txtCCPeso}
                    onChange={handleChangeFormDYT}
                />
            </Grid>
            <Grid item sm={6} xs={12}>
                <TextField
                    name="txtCCAltura"
                    label="Altura (m):"
                    variant="outlined"
                    fullWidth
                    value={formDiagnosticoTratamiento.txtCCAltura}
                    onChange={handleChangeFormDYT}
                />
            </Grid>
            <Grid item xs={12}>
                <TextField
                    name="txtCCAlergias"
                    label="Alergias:"
                    variant="outlined"
                    fullWidth
                    multiline
                    value={formDiagnosticoTratamiento.txtCCAlergias}
                    onChange={handleChangeFormDYT}
                />
            </Grid>
            <Grid item xs={12}>
                <TextField
                    name="txtCCSintomas"
                    label="Síntomas:"
                    variant="outlined"
                    fullWidth
                    multiline
                    value={formDiagnosticoTratamiento.txtCCSintomas}
                    onChange={handleChangeFormDYT}
                />
            </Grid>
            <Grid item xs={12}>
                <TextField
                    name="txtCCDiagnostico"
                    label="Diagnóstico:"
                    variant="outlined"
                    fullWidth
                    multiline
                    value={formDiagnosticoTratamiento.txtCCDiagnostico}
                    onChange={handleChangeFormDYT}
                />
            </Grid>
            <Grid item xs={12}>
                <TextField
                    name="txtCCTratamiento"
                    label="Tratamiento:"
                    variant="outlined"
                    fullWidth
                    multiline
                    value={formDiagnosticoTratamiento.txtCCTratamiento}
                    onChange={handleChangeFormDYT}
                />
            </Grid>
            <Grid item xs={12}>
                <TextField
                    name="txtCCComentarios"
                    label="Observaciones y comentarios:"
                    variant="outlined"
                    fullWidth
                    multiline
                    value={formDiagnosticoTratamiento.txtCCComentarios}
                    onChange={handleChangeFormDYT}
                />
            </Grid>
        </Grid>
    );
};

FormDiagnosticoTratamiento.propTypes = {
    formDiagnosticoTratamiento: PropTypes.shape({
        txtCCAlergias: PropTypes.any,
        txtCCAltura: PropTypes.any,
        txtCCComentarios: PropTypes.any,
        txtCCDiagnostico: PropTypes.any,
        txtCCPeso: PropTypes.any,
        txtCCSintomas: PropTypes.any,
        txtCCTratamiento: PropTypes.any,
    }),
    setFormDiagnosticoTratamiento: PropTypes.func,
};

export default FormDiagnosticoTratamiento;
