import React, { useState } from "react";
import ModalForm from "../../ModalForm";
import { Grid, TextField, Button } from "@material-ui/core";

const FormSubmodulo = (props) => {
    const { entSubmodulo, open, setOpen } = props;

    const [formSubmodulo, setFormSubmodulo] = useState({
        txtIdModulo: entSubmodulo.iIdModulo,
        txtIdSubmodulo: entSubmodulo.iIdSubmodulo,
        txtNombre: entSubmodulo.sNombre,
    });

    const handleClose = () => {
        setOpen(false);
    };

    const handleChangeForm = (e) => {
        setFormSubmodulo({
            ...formSubmodulo,
            [e.target.name]: e.target.value,
        });
    };

    return (
        <ModalForm
            title={entSubmodulo.iIdSubmodulo == 0 ? "Nuevo submódulo" : "Editar submódulo"}
            size="small"
            open={open}
            setOpen={setOpen}
        >
            <Grid container spacing={3}>
                {entSubmodulo.iIdModulo > 0 ? (
                    <Grid item xs={12}>
                        <TextField
                            name="txtIdModulo"
                            label="ID de módulo:"
                            variant="outlined"
                            color="secondary"
                            fullWidth
                            value={formSubmodulo.txtIdModulo}
                            disabled
                        />
                    </Grid>
                ) : null}

                {entSubmodulo.iIdSubmodulo > 0 ? (
                    <Grid item xs={12}>
                        <TextField
                            name="txtIdSubmodulo"
                            label="ID de submódulo:"
                            variant="outlined"
                            color="secondary"
                            fullWidth
                            value={formSubmodulo.txtIdSubmodulo}
                            disabled
                        />
                    </Grid>
                ) : null}

                <Grid item xs={12}>
                    <TextField
                        name="txtNombre"
                        label="Nombre de submódulo:"
                        variant="outlined"
                        color="secondary"
                        fullWidth
                        autoFocus
                        value={formSubmodulo.txtNombre}
                        onChange={handleChangeForm}
                    />
                </Grid>
                <Grid item sm={6} xs={12}>
                    <Button variant="contained" color="primary" fullWidth>
                        Guardar
                    </Button>
                </Grid>
                <Grid item sm={6} xs={12}>
                    <Button variant="contained" color="secondary" fullWidth onClick={handleClose}>
                        Cancelar
                    </Button>
                </Grid>
            </Grid>
        </ModalForm>
    );
};

export default FormSubmodulo;
