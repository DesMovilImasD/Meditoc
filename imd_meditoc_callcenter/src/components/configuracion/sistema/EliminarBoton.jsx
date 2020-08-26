import React from "react";
import ModalForm from "../../ModalForm";
import { Grid, Button } from "@material-ui/core";

const EliminarBoton = (props) => {
    const { entBoton, open, setOpen } = props;

    const handleClose = () => {
        setOpen(false);
    };

    return (
        <ModalForm title="Eliminar botón" size="small" open={open} setOpen={setOpen}>
            <Grid container spacing={3}>
                <Grid item xs={12} className="center">
                    ¿Desea eliminar el botón {entBoton.sNombre}?
                </Grid>
                <Grid item sm={6} xs={12}>
                    <Button variant="contained" color="primary" fullWidth>
                        Eliminar
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

export default EliminarBoton;
