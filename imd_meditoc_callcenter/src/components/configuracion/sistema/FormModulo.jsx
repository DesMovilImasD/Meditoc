import React, { useState } from "react";
import ModalForm from "../../ModalForm";
import { Grid, TextField, Button, Typography } from "@material-ui/core";

const FormModulo = (props) => {
    const { entModulo, open, setOpen } = props;

    const [formModulo, setFormModulo] = useState({
        txtIdModulo: entModulo.iIdModulo,
        txtNombre: entModulo.sNombre,
    });

    const handleClose = () => {
        setOpen(false);
    };

    const handleChangeForm = (e) => {
        setFormModulo({
            ...formModulo,
            [e.target.name]: e.target.value,
        });
    };

    return (
        <ModalForm
            title={entModulo.iIdModulo == 0 ? "Nuevo módulo" : "Editar módulo"}
            size="small"
            open={open}
            setOpen={setOpen}
        >
            <Grid container spacing={3}>
                {entModulo.iIdModulo > 0 ? (
                    <Grid item xs={12}>
                        <TextField
                            name="txtIdModulo"
                            label="ID de módulo:"
                            variant="outlined"
                            color="secondary"
                            fullWidth
                            value={formModulo.txtIdModulo}
                            disabled
                        />
                    </Grid>
                ) : null}

                <Grid item xs={12}>
                    <TextField
                        name="txtNombre"
                        label="Nombre de módulo:"
                        variant="outlined"
                        color="secondary"
                        fullWidth
                        autoFocus
                        value={formModulo.txtNombre}
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

export default FormModulo;
