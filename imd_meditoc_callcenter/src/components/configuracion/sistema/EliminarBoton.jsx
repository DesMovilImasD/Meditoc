import React from "react";
import ModalForm from "../../ModalForm";
import { Grid, Button } from "@material-ui/core";
import CGUController from "../../../controllers/CGUController";

/*************************************************************
 * Descripcion: Contenido del diálogo de confirmación para eliminar botón
 * Creado: Cristopher Noh
 * Fecha: 26/08/2020
 * Invocado desde: SistemaBoton
 *************************************************************/
const EliminarBoton = (props) => {
    const { entBoton, open, setOpen, usuarioSesion, funcGetPermisosXPerfil, funcLoader, funcAlert } = props;

    const cguController = new CGUController();

    const funcSaveBoton = async () => {
        funcLoader(true, "Guardando botón...");

        const entSaveBoton = {
            iIdModulo: entBoton.iIdModulo,
            iIdSubModulo: entBoton.iIdSubModulo,
            iIdBoton: entBoton.iIdBoton,
            iIdUsuarioMod: usuarioSesion.iIdUsuario,
            sNombre: null,
            bActivo: false,
            bBaja: true,
        };

        const response = await cguController.funcSaveBoton(entSaveBoton);
        if (response.Code !== 0) {
            funcAlert(response.Message);
        } else {
            setOpen(false);
            funcAlert(response.Message, "success");
            funcGetPermisosXPerfil();
        }

        funcLoader();
    };

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
                    <Button variant="contained" color="primary" fullWidth onClick={funcSaveBoton}>
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
