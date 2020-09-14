import React from "react";
import MeditocModal from "../../../utilidades/MeditocModal";
import { Grid, TextField } from "@material-ui/core";
import MeditocModalBotones from "../../../utilidades/MeditocModalBotones";
import { useState } from "react";
import EspecialidadController from "../../../../controllers/EspecialidadController";
import { useEffect } from "react";

const FormEspecialidad = (props) => {
    const { entEspecialidad, open, setOpen, funcGetEspecialidades, usuarioSesion, funcLoader, funcAlert } = props;

    const especialidadController = new EspecialidadController();

    const [formEspecialidad, setFormEspecialidad] = useState({
        txtNombreEspecialidad: "",
    });

    useEffect(() => {
        setFormEspecialidad({
            txtNombreEspecialidad: entEspecialidad.sNombre,
        });
    }, [entEspecialidad]);

    const [formEspecialidadOK, setFormEspecialidadOK] = useState({
        txtNombreEspecialidad: true,
    });

    const handleChangeFormEspecialidad = (e) => {
        const nombreCampo = e.target.name;
        const valorCampo = e.target.value;

        switch (nombreCampo) {
            case "txtNombreEspecialidad":
                if (formEspecialidadOK.txtNombreEspecialidad === false) {
                    if (valorCampo !== "") {
                        setFormEspecialidadOK({
                            ...formEspecialidadOK,
                            [nombreCampo]: true,
                        });
                    }
                }
                break;

            default:
                break;
        }

        setFormEspecialidad({
            ...formEspecialidad,
            [nombreCampo]: valorCampo,
        });
    };

    const handleClickSaveEspecialidad = async () => {
        let formEspecialidadOKValidacion = {
            txtNombreEspecialidad: true,
        };

        let formError = false;

        if (formEspecialidad.txtNombreEspecialidad === "") {
            formEspecialidadOKValidacion.txtNombreEspecialidad = false;
            formError = true;
        }

        setFormEspecialidadOK(formEspecialidadOKValidacion);

        if (formError === true) {
            return;
        }

        funcLoader(true, "Guardando especialidad");

        const entEspecialidadSubmit = {
            iIdEspecialidad: entEspecialidad.iIdEspecialidad,
            sNombre: formEspecialidad.txtNombreEspecialidad,
            iIdUsuarioMod: usuarioSesion.iIdUsuario,
            bActivo: true,
            bBaja: false,
        };

        const response = await especialidadController.funcSaveEspecialidad(entEspecialidadSubmit);

        if (response.Code === 0) {
            setOpen(false);
            await funcGetEspecialidades();
            funcAlert(response.Message, "success");
        } else {
            funcAlert(response.Message);
        }
        funcLoader();
    };

    return (
        <MeditocModal
            title={entEspecialidad.iIdEspecialidad === 0 ? "Crear especialidad" : "Editar especialidad"}
            size="small"
            open={open}
            setOpen={setOpen}
        >
            <Grid container spacing={3}>
                <Grid item xs={12}>
                    <TextField
                        name="txtNombreEspecialidad"
                        label="Nombre de especialidad:"
                        variant="outlined"
                        fullWidth
                        required
                        autoFocus
                        value={formEspecialidad.txtNombreEspecialidad}
                        onChange={handleChangeFormEspecialidad}
                        error={formEspecialidadOK.txtNombreEspecialidad === false}
                        helperText={
                            formEspecialidadOK.txtNombreEspecialidad === true
                                ? ""
                                : "El nombre de la especialidad es requerido"
                        }
                    />
                </Grid>
                <MeditocModalBotones
                    okMessage="Guardar"
                    okFunc={handleClickSaveEspecialidad}
                    open={open}
                    setOpen={setOpen}
                />
            </Grid>
        </MeditocModal>
    );
};

export default FormEspecialidad;
