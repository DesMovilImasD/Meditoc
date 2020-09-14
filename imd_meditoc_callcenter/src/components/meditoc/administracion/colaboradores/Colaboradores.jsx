import React, { Fragment } from "react";
import MeditocHeader1 from "../../../utilidades/MeditocHeader1";
import MeditocBody from "../../../utilidades/MeditocBody";
import { Tooltip, IconButton, Typography } from "@material-ui/core";
import AddRoundedIcon from "@material-ui/icons/AddRounded";
import EditIcon from "@material-ui/icons/Edit";
import DeleteIcon from "@material-ui/icons/Delete";
import { useState } from "react";
import FormColaborador from "./FormColaborador";
import PersonAddIcon from "@material-ui/icons/PersonAdd";
import GroupAddIcon from "@material-ui/icons/GroupAdd";
import EspecialidadController from "../../../../controllers/EspecialidadController";
import { useEffect } from "react";
import ColaboradorController from "../../../../controllers/ColaboradorController";
import MeditocTable from "../../../utilidades/MeditocTable";
import MeditocConfirmacion from "../../../utilidades/MeditocConfirmacion";
import InsertPhotoIcon from "@material-ui/icons/InsertPhoto";
import FotoColaborador from "./FotoColaborador";

const Colaboradores = (props) => {
    const { usuarioSesion, funcLoader, funcAlert } = props;

    const especialidadController = new EspecialidadController();
    const colaboradorController = new ColaboradorController();

    const columns = [
        { title: "ID", field: "iIdColaborador", align: "center", hidden: true },
        { title: "Tipo de doctor", field: "sTipoDoctor", align: "center" },
        { title: "Especialidad", field: "sEspecialidad", align: "center" },
        { title: "Nombre", field: "sNombreDirectorio", align: "center" },
        { title: "Usuario", field: "sUsuarioTitular", align: "center" },
        { title: "Sala", field: "iNumSala", align: "center" },
    ];

    const colaboradorCallCenterEntidadVacia = {
        iIdColaborador: 0,
        iIdTipoDoctor: 1,
    };

    const colaboradorEspecialistaEntidadVacia = {
        iIdColaborador: 0,
        iIdTipoDoctor: 2,
    };

    const [modalNuevoColaboradorOpen, setModalNuevoColaboradorOpen] = useState(false);
    const [modalEliminarColaboradorOpen, setModalEliminarColaboradorOpen] = useState(false);
    const [modalFotoColaboradorOpen, setModalFotoColaboradorOpen] = useState(false);

    const [listaEspecialidades, setListaEspecialidades] = useState([]);
    const [listaColaboradores, setListaColaboradores] = useState([]);

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

    const handleClickEditarColaborador = () => {
        if (colaboradorSeleccionado.iIdColaborador === 0) {
            funcAlert("Seleccione un colaborador de la tabla para continuar");
            return;
        }
        setColaboradorParaModal(colaboradorSeleccionado);
        setModalNuevoColaboradorOpen(true);
    };

    const handleClickFotoColaborador = () => {
        if (colaboradorSeleccionado.iIdColaborador === 0) {
            funcAlert("Seleccione un colaborador de la tabla para continuar");
            return;
        }
        setModalFotoColaboradorOpen(true);
        setColaboradorParaModal(colaboradorSeleccionado);
    };

    const handleClickEliminarColaborador = () => {
        if (colaboradorSeleccionado.iIdColaborador === 0) {
            funcAlert("Seleccione un colaborador de la tabla para continuar");
            return;
        }
        setModalEliminarColaboradorOpen(true);
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

    const funcGetColaboradores = async () => {
        funcLoader(true, "Consultando colaboradores...");

        const response = await colaboradorController.funcGetColaboradores();
        if (response.Code === 0) {
            setListaColaboradores(response.Result);
        } else {
            funcAlert(response.Message);
        }
        funcLoader();
    };

    const funcEliminarColaborador = async () => {
        funcLoader(true, "Eliminando colaboradores...");

        const response = await colaboradorController.funcSaveColaborador({
            iIdColaborador: colaboradorSeleccionado.iIdColaborador,
            iIdTipoDoctor: colaboradorSeleccionado.iIdTipoDoctor,
            iIdUsuarioCGU: colaboradorSeleccionado.iIdUsuarioCGU,
            iIdUsuarioMod: usuarioSesion.iIdUsuario,
            bActivo: false,
            bBaja: true,
        });

        if (response.Code === 0) {
            setModalEliminarColaboradorOpen(false);
            await funcGetColaboradores();
            funcAlert(response.Message, "success");
        } else {
            funcAlert(response.Message);
        }
        funcLoader();
    };

    const funcGetData = async () => {
        await funcGetColaboradores();
        await funcGetEspecialidades();
    };

    useEffect(() => {
        funcGetData();
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
                    <IconButton onClick={handleClickEditarColaborador}>
                        <EditIcon className="color-0" />
                    </IconButton>
                </Tooltip>
                <Tooltip title="Foto del colaborador" arrow>
                    <IconButton onClick={handleClickFotoColaborador}>
                        <InsertPhotoIcon className="color-0" />
                    </IconButton>
                </Tooltip>
                <Tooltip title="Eliminar de colaborador" arrow>
                    <IconButton onClick={handleClickEliminarColaborador}>
                        <DeleteIcon className="color-0" />
                    </IconButton>
                </Tooltip>
            </MeditocHeader1>
            <MeditocBody>
                <MeditocTable
                    columns={columns}
                    data={listaColaboradores}
                    rowSelected={colaboradorSeleccionado}
                    setRowSelected={setColaboradorSeleccionado}
                    mainField="iIdColaborador"
                />
            </MeditocBody>
            <FormColaborador
                entColaborador={colaboradorParaModal}
                open={modalNuevoColaboradorOpen}
                setOpen={setModalNuevoColaboradorOpen}
                listaEspecialidades={listaEspecialidades}
                funcGetColaboradores={funcGetColaboradores}
                usuarioSesion={usuarioSesion}
                funcLoader={funcLoader}
                funcAlert={funcAlert}
            />
            <FotoColaborador
                entColaborador={colaboradorParaModal}
                open={modalFotoColaboradorOpen}
                setOpen={setModalFotoColaboradorOpen}
                usuarioSesion={usuarioSesion}
                funcLoader={funcLoader}
                funcAlert={funcAlert}
            />
            <MeditocConfirmacion
                title="Eliminar colaborador"
                open={modalEliminarColaboradorOpen}
                setOpen={setModalEliminarColaboradorOpen}
                okFunc={funcEliminarColaborador}
            >
                ¿Desea eliminar el colaborador seleccionado?
                <br />
                <br />
                <Typography variant="body2">El colaborador perderá el acceso al portal de Meditoc</Typography>
            </MeditocConfirmacion>
        </Fragment>
    );
};

export default Colaboradores;
