import React from "react";
import ModalForm from "../../ModalForm";
import { Grid, Button } from "@material-ui/core";
import CGUController from "../../../controllers/CGUController";

const EliminarPermisoBoton = (props) => {
    const { entPerfil, entBoton, funcGetPermisosXPerfil, open, setOpen, usuarioSesion, funcLoader, funcAlert } = props;

    const cguController = new CGUController();

    const funcSavePermisoBoton = async () => {
        const listaPermisosParaGuardar = [
            {
                iIdPerfil: entPerfil.iIdPerfil,
                iIdModulo: entBoton.iIdModulo,
                iIdSubModulo: entBoton.iIdSubModulo,
                iIdBoton: entBoton.iIdBoton,
                iIdUsuarioMod: usuarioSesion.iIdUsuario,
                bActivo: false,
                bBaja: true,
            },
        ];

        funcLoader(true, "Removiendo permisos de botón...");

        const response = await cguController.funcSavePermiso(listaPermisosParaGuardar);
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
        <ModalForm title="Quitar permiso de botón" size="small" open={open} setOpen={setOpen}>
            <Grid container spacing={3}>
                <Grid item xs={12} className="center">
                    ¿Desea remover el permiso para el boton {entBoton.sNombre}?
                </Grid>
                <Grid item sm={6} xs={12}>
                    <Button variant="contained" color="primary" fullWidth onClick={funcSavePermisoBoton}>
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

export default EliminarPermisoBoton;
