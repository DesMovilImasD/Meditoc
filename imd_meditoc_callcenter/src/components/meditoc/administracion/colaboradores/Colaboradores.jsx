import { IconButton, Tooltip, Typography } from "@material-ui/core";
import React, { Fragment } from "react";

import ColaboradorController from "../../../../controllers/ColaboradorController";
import DeleteIcon from "@material-ui/icons/Delete";
import DetalleColaborador from "./DetalleColaborador";
import EditIcon from "@material-ui/icons/Edit";
import { EnumTipoDoctor } from "../../../../configurations/enumConfig";
import EspecialidadController from "../../../../controllers/EspecialidadController";
import FormColaborador from "./FormColaborador";
import FotoColaborador from "./FotoColaborador";
import GroupAddIcon from "@material-ui/icons/GroupAdd";
import InsertPhotoIcon from "@material-ui/icons/InsertPhoto";
import MeditocBody from "../../../utilidades/MeditocBody";
import MeditocConfirmacion from "../../../utilidades/MeditocConfirmacion";
import MeditocHeader1 from "../../../utilidades/MeditocHeader1";
import MeditocTable from "../../../utilidades/MeditocTable";
import PersonAddIcon from "@material-ui/icons/PersonAdd";
import PropTypes from "prop-types";
import ReplayIcon from "@material-ui/icons/Replay";
import VisibilityIcon from "@material-ui/icons/Visibility";
import { useEffect } from "react";
import { useState } from "react";

const Colaboradores = (props) => {
    const { usuarioSesion, funcLoader, funcAlert, title } = props;

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
        iIdTipoDoctor: EnumTipoDoctor.CallCenter,
        sNombreDirectorio: "",
        sCedulaProfecional: "",
        sRFC: "",
        sTelefonoDirectorio: "",
        sCorreoDirectorio: "",
        sWhatsApp: "",
        iIdEspecialidad: "",
        iNumSala: "",
        sDireccionConsultorio: "",
        sURL: "",
        sMaps: "",
        sNombresDoctor: "",
        sApellidoPaternoDoctor: "",
        sApellidoMaternoDoctor: "",
        dtFechaNacimientoDoctor: null,
        sTelefonoDoctor: "",
        sCorreoDoctor: "",
        sDomicilioDoctor: "",
        sUsuarioTitular: "",
        sUsuarioAdministrativo: "",
    };

    const colaboradorEspecialistaEntidadVacia = {
        iIdColaborador: 0,
        iIdTipoDoctor: EnumTipoDoctor.Especialista,
        sNombreDirectorio: "",
        sCedulaProfecional: "",
        sRFC: "",
        sTelefonoDirectorio: "",
        sWhatsApp: "",
        sCorreoDirectorio: "",
        iIdEspecialidad: "",
        iNumSala: "",
        sDireccionConsultorio: "",
        sURL: "",
        sMaps: "",
        sNombresDoctor: "",
        sApellidoPaternoDoctor: "",
        sApellidoMaternoDoctor: "",
        dtFechaNacimientoDoctor: null,
        sTelefonoDoctor: "",
        sCorreoDoctor: "",
        sDomicilioDoctor: "",
        sUsuarioTitular: "",
        sUsuarioAdministrativo: "",
    };

    const [modalNuevoColaboradorOpen, setModalNuevoColaboradorOpen] = useState(false);
    const [modalDetalleColaboradorOpen, setModalDetalleColaboradorOpen] = useState(false);
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

    const handleClickDetalleColaborador = () => {
        if (colaboradorSeleccionado.iIdColaborador === 0) {
            funcAlert("Seleccione un colaborador de la tabla para continuar");
            return;
        }
        setModalDetalleColaboradorOpen(true);
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
            // let especialidades = [...response.Result];
            // especialidades.sor
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
            setColaboradorSeleccionado(colaboradorCallCenterEntidadVacia);
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
        // eslint-disable-next-line
    }, []);

    return (
        <Fragment>
            <MeditocHeader1 title={title}>
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
                <Tooltip title="Ver detalles de colaborador" arrow>
                    <IconButton onClick={handleClickDetalleColaborador}>
                        <VisibilityIcon className="color-0" />
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
                <Tooltip title="Actualizar tabla" arrow>
                    <IconButton onClick={funcGetData}>
                        <ReplayIcon className="color-0" />
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
                    doubleClick={handleClickDetalleColaborador}
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
            <DetalleColaborador
                entColaborador={colaboradorSeleccionado}
                open={modalDetalleColaboradorOpen}
                setOpen={setModalDetalleColaboradorOpen}
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

Colaboradores.propTypes = {
    funcAlert: PropTypes.func,
    funcLoader: PropTypes.func,
    title: PropTypes.any,
    usuarioSesion: PropTypes.shape({
        iIdUsuario: PropTypes.any,
    }),
};

export default Colaboradores;
