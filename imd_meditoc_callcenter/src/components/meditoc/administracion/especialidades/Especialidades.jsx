import React, { Fragment } from "react";
import MeditocHeader1 from "../../../utilidades/MeditocHeader1";
import { Tooltip, IconButton } from "@material-ui/core";
import AddRoundedIcon from "@material-ui/icons/AddRounded";
import EditIcon from "@material-ui/icons/Edit";
import DeleteIcon from "@material-ui/icons/Delete";
import MeditocBody from "../../../utilidades/MeditocBody";
import { useState } from "react";
import EspecialidadController from "../../../../controllers/EspecialidadController";
import { useEffect } from "react";
import MeditocTable from "../../../utilidades/MeditocTable";
import FormEspecialidad from "./FormEspecialidad";
import MeditocConfirmacion from "../../../utilidades/MeditocConfirmacion";

const Especialidades = (props) => {
    const { usuarioSesion, funcLoader, funcAlert } = props;
    const especialidadController = new EspecialidadController();
    const especialidadEntidadVacia = { iIdEspecialidad: 0, sNombre: "" };

    const columns = [
        { title: "ID", field: "iIdEspecialidad", align: "center", hidden: true },
        { title: "Descripción", field: "sNombre", align: "center" },
        { title: "Creado", field: "sFechaCreacion", align: "center" },
    ];

    const [listaEspecialidades, setListaEspecialidades] = useState([]);
    const [especialidadSeleccionada, setEspecialidadSeleccionada] = useState(especialidadEntidadVacia);
    const [especialidadParaFormulario, setEspecialidadParaFormulario] = useState(especialidadEntidadVacia);

    const [modalFormEspecialidadOpen, setModalFormEspecialidadOpen] = useState(false);
    const [modalEliminarEspecialidadOpen, setModalEliminarEspecialidadOpen] = useState(false);

    const handleClickNuevaEspecialidad = () => {
        setEspecialidadParaFormulario(especialidadEntidadVacia);
        setModalFormEspecialidadOpen(true);
    };

    const handleClickEditarEspecialidad = () => {
        if (especialidadSeleccionada.iIdEspecialidad === 0) {
            funcAlert("Seleccione una especialidad de la tabla para continuar");
            return;
        }
        setEspecialidadParaFormulario(especialidadSeleccionada);
        setModalFormEspecialidadOpen(true);
    };

    const handleClickEliminarEspecialidad = () => {
        if (especialidadSeleccionada.iIdEspecialidad === 0) {
            funcAlert("Seleccione una especialidad de la tabla para continuar");
            return;
        }
        if (especialidadSeleccionada.iIdEspecialidad === 1) {
            funcAlert("Esta especialidad no se puede eliminar", "info");
            return;
        }
        setModalEliminarEspecialidadOpen(true);
    };

    const funcGetEspecialidades = async () => {
        funcLoader(true, "Consultando especialidades médicas...");

        const response = await especialidadController.funcGetEspecialidad();

        if (response.Code === 0) {
            setListaEspecialidades(response.Result);
        } else {
            funcAlert(response.Message);
        }

        funcLoader();
    };

    const funcEliminarEspecialidad = async () => {
        funcLoader(true, "Eliminando especialidad...");

        const entEspecialidadSubmit = {
            iIdEspecialidad: especialidadSeleccionada.iIdEspecialidad,
            iIdUsuarioMod: usuarioSesion.iIdUsuario,
            bActivo: false,
            bBaja: true,
        };

        const response = await especialidadController.funcSaveEspecialidad(entEspecialidadSubmit);

        if (response.Code === 0) {
            setModalEliminarEspecialidadOpen(false);
            await funcGetEspecialidades();
            funcAlert(response.Message, "success");
        } else {
            funcAlert(response.Message);
        }

        funcLoader();
    };

    useEffect(() => {
        funcGetEspecialidades();
    }, []);
    return (
        <Fragment>
            <MeditocHeader1 title="ESPECIALIDADES">
                <Tooltip title="Añadir nueva especialidad" arrow>
                    <IconButton onClick={handleClickNuevaEspecialidad}>
                        <AddRoundedIcon className="color-0" />
                    </IconButton>
                </Tooltip>
                <Tooltip title="Editar especialidad" arrow>
                    <IconButton onClick={handleClickEditarEspecialidad}>
                        <EditIcon className="color-0" />
                    </IconButton>
                </Tooltip>
                <Tooltip title="Eliminar especialidad" arrow>
                    <IconButton onClick={handleClickEliminarEspecialidad}>
                        <DeleteIcon className="color-0" />
                    </IconButton>
                </Tooltip>
            </MeditocHeader1>
            <MeditocBody>
                <MeditocTable
                    columns={columns}
                    data={listaEspecialidades}
                    rowSelected={especialidadSeleccionada}
                    setRowSelected={setEspecialidadSeleccionada}
                    mainField="iIdEspecialidad"
                />
            </MeditocBody>
            <FormEspecialidad
                entEspecialidad={especialidadParaFormulario}
                open={modalFormEspecialidadOpen}
                setOpen={setModalFormEspecialidadOpen}
                funcGetEspecialidades={funcGetEspecialidades}
                usuarioSesion={usuarioSesion}
                funcLoader={funcLoader}
                funcAlert={funcAlert}
            />
            <MeditocConfirmacion
                title="Eliminar especialidad"
                open={modalEliminarEspecialidadOpen}
                setOpen={setModalEliminarEspecialidadOpen}
                okFunc={funcEliminarEspecialidad}
            >
                ¿Desea eliminar la especialidad "{especialidadSeleccionada.sNombre}"?
            </MeditocConfirmacion>
        </Fragment>
    );
};

export default Especialidades;
