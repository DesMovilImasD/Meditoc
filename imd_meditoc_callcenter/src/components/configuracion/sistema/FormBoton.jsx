import React, { useState } from "react";
import ModalForm from "../../ModalForm";
import { Grid, TextField, Button } from "@material-ui/core";

const FormBoton = (props) => {
    const { entBoton, open, setOpen } = props;

    const [formBoton, setFormBoton] = useState({
        txtIdModulo: entBoton.iIdModulo,
        txtIdSubmodulo: entBoton.iIdSubmodulo,
        txtIdBoton: entBoton.iIdBoton,
        txtNombre: entBoton.sNombre,
    });

    const handleClose = () => {
        setOpen(false);
    };

    const handleChangeForm = (e) => {
        setFormBoton({
            ...formBoton,
            [e.target.name]: e.target.value,
        });
    };

    return (
        <ModalForm
            title={entBoton.iIdBoton === 0 ? "Nuevo botón" : "Editar botón"}
            size="small"
            open={open}
            setOpen={setOpen}
        >
            <Grid container spacing={3}>
                {entBoton.iIdModulo > 0 ? (
                    <Grid item xs={12}>
                        <TextField
                            name="txtIdModulo"
                            label="ID de módulo:"
                            variant="outlined"
                            color="secondary"
                            fullWidth
                            value={formBoton.txtIdModulo}
                            disabled
                        />
                    </Grid>
                ) : null}

                {entBoton.iIdSubmodulo > 0 ? (
                    <Grid item xs={12}>
                        <TextField
                            name="txtIdSubmodulo"
                            label="ID de submódulo:"
                            variant="outlined"
                            color="secondary"
                            fullWidth
                            value={formBoton.txtIdSubmodulo}
                            disabled
                        />
                    </Grid>
                ) : null}

                {entBoton.iIdBoton > 0 ? (
                    <Grid item xs={12}>
                        <TextField
                            name="txtIdBoton"
                            label="ID de botón:"
                            variant="outlined"
                            color="secondary"
                            fullWidth
                            value={formBoton.txtIdBoton}
                            disabled
                        />
                    </Grid>
                ) : null}

                <Grid item xs={12}>
                    <TextField
                        name="txtNombre"
                        label="Nombre de botón:"
                        variant="outlined"
                        color="secondary"
                        fullWidth
                        autoFocus
                        value={formBoton.txtNombre}
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

export default FormBoton;
