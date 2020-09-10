import React, { Fragment } from "react";
import MeditocHeader1 from "../../../utilidades/MeditocHeader1";
import MeditocBody from "../../../utilidades/MeditocBody";
import { Tooltip, IconButton } from "@material-ui/core";
import AddRoundedIcon from "@material-ui/icons/AddRounded";
import EditIcon from "@material-ui/icons/Edit";
import DeleteIcon from "@material-ui/icons/Delete";
import { useState } from "react";
import FormColaborador from "./FormColaborador";
import PersonAddIcon from "@material-ui/icons/PersonAdd";
import GroupAddIcon from "@material-ui/icons/GroupAdd";
import EspecialidadController from "../../../../controllers/EspecialidadController";
import { useEffect } from "react";

const Colaboradores = (props) => {
    const { usuarioSesion, funcLoader, funcAlert } = props;

    const especialidadController = new EspecialidadController();

    const colaboradorCallCenterEntidadVacia = {
        iIdColaborador: 0,
        iIdTipoDoctor: 1,
    };

    const colaboradorEspecialistaEntidadVacia = {
        iIdColaborador: 0,
        iIdTipoDoctor: 2,
    };

    const [modalNuevoColaboradorOpen, setModalNuevoColaboradorOpen] = useState(false);

    const [listaEspecialidades, setListaEspecialidades] = useState([]);

    const [colaboradorSeleccionado, setColaboradorSeleccionado] = useState(colaboradorCallCenterEntidadVacia);
    const [colaboradorParaModal, setColaboradorParaModal] = useState(colaboradorCallCenterEntidadVacia);

    const handleClickNuevoColaboradorCallCenter = () => {
        setColaboradorParaModal(colaboradorCallCenterEntidadVacia);
        setModalNuevoColaboradorOpen(true);
    };

    const handleClickNuevoColaboradorEspecialista = () => {
        setColaboradorParaModal(colaboradorEspecialistaEntidadVacia);
        setModalNuevoColaboradorOpen(true);
    };

    const funcGetEspecialidades = async () => {
        funcLoader(true, "Consultando especialidades...");

        const response = await especialidadController.funcGetEspecialidad();

        if (response.Code === 0) {
            setListaEspecialidades(response.Result);
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
            <MeditocHeader1 title="COLABORADORES">
                <Tooltip title="Nuevo Colaborador CallCenter" arrow>
                    <IconButton onClick={handleClickNuevoColaboradorCallCenter}>
                        <PersonAddIcon className="color-0" />
                    </IconButton>
                </Tooltip>
                <Tooltip title="Nuevo Colaborador Especialista" arrow>
                    <IconButton onClick={handleClickNuevoColaboradorEspecialista}>
                        <GroupAddIcon className="color-0" />
                    </IconButton>
                </Tooltip>
                <Tooltip title="Editar datos de colaborador" arrow>
                    <IconButton>
                        <EditIcon className="color-0" />
                    </IconButton>
                </Tooltip>
                <Tooltip title="Eliminar de colaborador" arrow>
                    <IconButton>
                        <DeleteIcon className="color-0" />
                    </IconButton>
                </Tooltip>
            </MeditocHeader1>
            <FormColaborador
                entColaborador={colaboradorParaModal}
                open={modalNuevoColaboradorOpen}
                setOpen={setModalNuevoColaboradorOpen}
                listaEspecialidades={listaEspecialidades}
            />
        </Fragment>
    );
};

export default Colaboradores;
