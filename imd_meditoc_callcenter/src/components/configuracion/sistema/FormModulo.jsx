import React, { useState } from "react";
import ModalForm from "../../ModalForm";
import { Grid, TextField, Button } from "@material-ui/core";
import CGUController from "../../../controllers/CGUController";

/*************************************************************
 * Descripcion: Modal del formulario para Agregar/Modificar un módulo
 * Creado: Cristopher Noh
 * Fecha: 26/08/2020
 * Invocado desde: SistemaModulo (Modificar), Sistema (Agregar)
 *************************************************************/
const FormModulo = (props) => {
    const { entModulo, open, setOpen, usuarioSesion, funcGetPermisosXPerfil, funcLoader, funcAlert } = props;

    const cguController = new CGUController();

    const [formModulo, setFormModulo] = useState({
        txtIdModulo: entModulo.iIdModulo,
        txtNombre: entModulo.sNombre,
    });

    const funcSaveModulo = async () => {
        funcLoader(true, "Guardando módulo...");

        const entSaveModulo = {
            iIdModulo: entModulo.iIdModulo,
            iIdUsuarioMod: usuarioSesion.iIdUsuario,
            sNombre: formModulo.txtNombre,
            bActivo: true,
            bBaja: false,
        };

        const response = await cguController.funcSaveModulo(entSaveModulo);
        if (response.Code !== 0) {
            funcAlert(response.Message);
        } else {
            setOpen(false);
            funcAlert(response.Message, "success");
            //setFormModulo({ ...formModulo, txtNombre: "" });
            funcGetPermisosXPerfil();
        }

        funcLoader();
    };

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
                    <Button variant="contained" color="primary" fullWidth onClick={funcSaveModulo}>
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
