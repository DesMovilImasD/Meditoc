import React, { Fragment } from "react";
import MeditocHeader1 from "../../../utilidades/MeditocHeader1";
import MeditocBody from "../../../utilidades/MeditocBody";
import { Tooltip, IconButton } from "@material-ui/core";
import AddRoundedIcon from "@material-ui/icons/AddRounded";
import EditIcon from "@material-ui/icons/Edit";
import DeleteIcon from "@material-ui/icons/Delete";
import { useState } from "react";
import FormColaborador from "./FormColaborador";

const Colaboradores = (props) => {
    const { funcAlert } = props;

    const [modalNuevoColaboradorOpen, setModalNuevoColaboradorOpen] = useState(true);

    const [colaboradorSeleccionado, setColaboradorSeleccionado] = useState({});
    const [colaboradorParaModal, setColaboradorParaModal] = useState({});

    const handleClickNuevoColaborador = () => {
        setModalNuevoColaboradorOpen(true);
    };
    return (
        <Fragment>
            <MeditocHeader1 title="COLABORADORES">
                <Tooltip title="Nuevo colaborador" arrow>
                    <IconButton onClick={handleClickNuevoColaborador}>
                        <AddRoundedIcon className="color-0" />
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
            />
        </Fragment>
    );
};

export default Colaboradores;
