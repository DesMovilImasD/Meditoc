import { IconButton, Tooltip } from "@material-ui/core";
import React, { Fragment } from "react";

import AddRoundedIcon from "@material-ui/icons/AddRounded";
import DeleteIcon from "@material-ui/icons/Delete";
import EditIcon from "@material-ui/icons/Edit";
import { EnumEspecialidadPrincipal } from "../../../../configurations/enumConfig";
import EspecialidadController from "../../../../controllers/EspecialidadController";
import FormEspecialidad from "./FormEspecialidad";
import MeditocBody from "../../../utilidades/MeditocBody";
import MeditocConfirmacion from "../../../utilidades/MeditocConfirmacion";
import MeditocHeader1 from "../../../utilidades/MeditocHeader1";
import MeditocTable from "../../../utilidades/MeditocTable";
import PropTypes from "prop-types";
import ReplayIcon from "@material-ui/icons/Replay";
import { cellProps } from "../../../../configurations/dataTableIconsConfig";
import { emptyFunc } from "../../../../configurations/preventConfig";
import { useEffect } from "react";
import { useState } from "react";

const Especialidades = (props) => {
    const { usuarioSesion, permisos, funcLoader, funcAlert } = props;

    const especialidadController = new EspecialidadController();
    const especialidadEntidadVacia = { iIdEspecialidad: 0, sNombre: "" };

    const columns = [
        { title: "ID", field: "iIdEspecialidad", ...cellProps, hidden: true },
        { title: "Descripción", field: "sNombre", ...cellProps },
        { title: "Creado", field: "sFechaCreacion", ...cellProps },
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
        if (especialidadSeleccionada.iIdEspecialidad === EnumEspecialidadPrincipal.MedicinaGeneral) {
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
            setEspecialidadSeleccionada(especialidadEntidadVacia);
            await funcGetEspecialidades();
            funcAlert(response.Message, "success");
        } else {
            funcAlert(response.Message);
        }

        funcLoader();
    };

    useEffect(() => {
        funcGetEspecialidades();
        // eslint-disable-next-line
    }, []);
    return (
        <Fragment>
            <MeditocHeader1 title={permisos.Nombre}>
                {permisos.Botones["1"] !== undefined && ( //Añadir nueva especialidad
                    <Tooltip title={permisos.Botones["1"].Nombre} arrow>
                        <IconButton onClick={handleClickNuevaEspecialidad}>
                            <AddRoundedIcon className="color-0" />
                        </IconButton>
                    </Tooltip>
                )}
                {permisos.Botones["2"] !== undefined && ( //Editar especialidad
                    <Tooltip title={permisos.Botones["2"].Nombre} arrow>
                        <IconButton onClick={handleClickEditarEspecialidad}>
                            <EditIcon className="color-0" />
                        </IconButton>
                    </Tooltip>
                )}
                {permisos.Botones["3"] !== undefined && ( //Eliminar especialidad
                    <Tooltip title={permisos.Botones["3"].Nombre} arrow>
                        <IconButton onClick={handleClickEliminarEspecialidad}>
                            <DeleteIcon className="color-0" />
                        </IconButton>
                    </Tooltip>
                )}
                {permisos.Botones["4"] !== undefined && ( //Actualizar tabla
                    <Tooltip title={permisos.Botones["4"].Nombre} arrow>
                        <IconButton onClick={funcGetEspecialidades}>
                            <ReplayIcon className="color-0" />
                        </IconButton>
                    </Tooltip>
                )}
            </MeditocHeader1>
            <MeditocBody>
                <MeditocTable
                    columns={columns}
                    data={listaEspecialidades}
                    rowSelected={especialidadSeleccionada}
                    setRowSelected={setEspecialidadSeleccionada}
                    mainField="iIdEspecialidad"
                    doubleClick={permisos.Botones["2"] !== undefined ? handleClickEditarEspecialidad : emptyFunc}
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

Especialidades.propTypes = {
    funcAlert: PropTypes.func,
    funcLoader: PropTypes.func,
    title: PropTypes.any,
    usuarioSesion: PropTypes.shape({
        iIdUsuario: PropTypes.any,
    }),
};

export default Especialidades;
