import React from "react";
import MeditocModal from "../../../utilidades/MeditocModal";
import { Grid, TextField, Button } from "@material-ui/core";
import { useState } from "react";
import { useEffect } from "react";

const FormEmpresa = (props) => {
    const { entEmpresa, open, setOpen } = props;

    const [formEmpresa, setFormEmpresa] = useState({
        txtFolioEmpresa: "",
        txtNombreEmpresa: "",
        txtCorreoEmpresa: "",
    });

    useEffect(() => {
        setFormEmpresa({
            txtFolioEmpresa: entEmpresa.sFolioEmpresa,
            txtNombreEmpresa: entEmpresa.sNombre,
            txtCorreoEmpresa: entEmpresa.sCorreo,
        });
    }, [entEmpresa]);

    const handleChangeFormEmpresa = (e) => {
        setFormEmpresa({
            ...formEmpresa,
            [e.target.name]: e.target.value,
        });
    };

    //Funcion para cerrar este modal
    const handleClose = () => {
        setOpen(false);
    };

    return (
        <MeditocModal
            title={entEmpresa.iIdEmpresa === 0 ? "Nueva empresa" : "Editar empresa"}
            size="small"
            open={open}
            setOpen={setOpen}
        >
            <Grid container spacing={3}>
                <Grid item xs={12}>
                    {entEmpresa.iIdEmpresa > 0 ? (
                        <TextField
                            name="txtFolioEmpresa"
                            label="Folio de empresa:"
                            variant="outlined"
                            disabled
                            fullWidth
                            value={formEmpresa.txtFolioEmpresa}
                        />
                    ) : null}
                </Grid>
                <Grid item xs={12}>
                    <TextField
                        name="txtNombreEmpresa"
                        label="Nombre de empresa:"
                        autoFocus
                        variant="outlined"
                        fullWidth
                        value={formEmpresa.txtNombreEmpresa}
                        onChange={handleChangeFormEmpresa}
                    />
                </Grid>
                <Grid item xs={12}>
                    <TextField
                        name="txtCorreoEmpresa"
                        label="Correo de empresa:"
                        variant="outlined"
                        fullWidth
                        value={formEmpresa.txtCorreoEmpresa}
                        onChange={handleChangeFormEmpresa}
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
        </MeditocModal>
    );
};

export default FormEmpresa;
