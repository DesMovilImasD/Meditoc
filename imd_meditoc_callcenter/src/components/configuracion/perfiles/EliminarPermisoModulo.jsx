import React from "react";
import ModalForm from "../../ModalForm";
import CGUController from "../../../controllers/CGUController";
import { Grid, Button } from "@material-ui/core";

const EliminarPermisoModulo = (props) => {
    const { entModulo, entPerfil, funcGetPermisosXPerfil, open, setOpen, usuarioSesion, funcLoader, funcAlert } = props;

    const cguController = new CGUController();

    const funcSavePermisoModulo = async () => {
        const listaPermisosParaGuardar = [
            {
                iIdPerfil: entPerfil.iIdPerfil,
                iIdModulo: entModulo.iIdModulo,
                iIdSubModulo: 0,
                iIdBoton: 0,
                iIdUsuarioMod: usuarioSesion.iIdUsuario,
                bActivo: false,
                bBaja: true,
            },
        ];

        funcLoader(true, "Removiendo permisos de módulo...");

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
        <ModalForm title="Quitar permiso para módulo" size="small" open={open} setOpen={setOpen}>
            <Grid container spacing={3}>
                <Grid item xs={12} className="center">
                    ¿Desea remover el permiso para el módulo {entModulo.sNombre} y todos sus submódulos y botones?
                </Grid>
                <Grid item sm={6} xs={12}>
                    <Button variant="contained" color="primary" fullWidth onClick={funcSavePermisoModulo}>
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

export default EliminarPermisoModulo;
