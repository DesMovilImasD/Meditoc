import { IconButton, Tooltip } from "@material-ui/core";
import React from "react";
import { useState } from "react";
import { useEffect } from "react";
import { Fragment } from "react";
import AddIcon from "@material-ui/icons/Add";
import EditIcon from "@material-ui/icons/Edit";
import DeleteIcon from "@material-ui/icons/Delete";
import ColaboradorController from "../../../../controllers/ColaboradorController";
import MeditocHeader1 from "../../../utilidades/MeditocHeader1";
import CallCenterController from "../../../../controllers/CallCenterController";
import MeditocBody from "../../../utilidades/MeditocBody";
import MeditocTable from "../../../utilidades/MeditocTable";
import FormConsulta from "./FormConsulta";

const Administrador = (props) => {
    const { usuarioSesion, funcLoader, funcAlert } = props;

    const colaboradorController = new ColaboradorController();
    const callcenterController = new CallCenterController();

    const columns = [
        { title: "ID", field: "iIdConsulta", align: "center", hidden: true },
        { title: "Inicio", field: "sFechaProgramadaInicio", align: "center" },
        { title: "Fin", field: "sFechaProgramadaFin", align: "center" },
        { title: "Paciente", field: "sNombrePaciente", align: "center" },
        { title: "Folio", field: "sFolio", align: "center" },
        { title: "Estatus", field: "sEstatusConsulta", align: "center" },
        { title: "Creado", field: "sFechaCreacion", align: "center" },
    ];

    const consultaEntidadVacia = {
        iIdConsulta: 0,
    };

    const [usuarioColaborador, setUsuarioColaborador] = useState(null);
    const [listaConsultas, setListaConsultas] = useState([]);
    const [consultaSeleccionada, setConsultaSeleccionada] = useState(consultaEntidadVacia);
    const [consultaParaModal, setConsultaParaModal] = useState(consultaEntidadVacia);

    const [modalFormConsultaOpen, setModalFormConsultaOpen] = useState(false);

    const funcGetColaboradorUser = async () => {
        funcLoader(true, "Obteniendo usuario administrativo");

        const response = await colaboradorController.funcGetColaboradores(null, null, null, usuarioSesion.iIdUsuario);

        if (response.Code === 0) {
            if (response.Result.length > 0) {
                setUsuarioColaborador(response.Result[0]);
                return;
            } else {
                funcAlert("No se ha ingresado con una cuenta de colaborador");
            }
        } else {
            funcAlert(response.Message);
        }
        funcLoader();
    };

    useEffect(() => {
        funcGetColaboradorUser();
    }, []);

    const funcGetConsultas = async () => {
        funcLoader(true, "Obteniendo consultas");

        const response = await callcenterController.funcGetConsulta(null, null, usuarioColaborador.iIdColaborador);

        if (response.Code === 0) {
            setListaConsultas(response.Result);
        } else {
            funcAlert(response.Message);
        }
        funcLoader();
    };

    useEffect(() => {
        if (usuarioColaborador !== null) {
            funcGetConsultas();
        }
    }, [usuarioColaborador]);

    const handleClickNuevaConsulta = () => {
        setConsultaParaModal(consultaEntidadVacia);
        setModalFormConsultaOpen(true);
    };

    const handleEditarConsulta = () => {
        if (consultaSeleccionada.iIdConsulta === 0) {
            funcAlert("Seleccione una consulta de la tabla para editar", "warning");
            return;
        }
        setConsultaParaModal(consultaSeleccionada);
        setModalFormConsultaOpen(true);
    };

    return (
        <Fragment>
            <MeditocHeader1 title="ADMINISTRADOR CONSULTAS">
                <Tooltip title="Nueva consulta" arrow>
                    <IconButton onClick={handleClickNuevaConsulta}>
                        <AddIcon className="color-0" />
                    </IconButton>
                </Tooltip>
                <Tooltip title="Editar consulta" arrow>
                    <IconButton onClick={handleEditarConsulta}>
                        <EditIcon className="color-0" />
                    </IconButton>
                </Tooltip>
                <Tooltip title="Cancelar consulta" arrow>
                    <IconButton>
                        <DeleteIcon className="color-0" />
                    </IconButton>
                </Tooltip>
            </MeditocHeader1>
            <MeditocBody>
                <MeditocTable
                    columns={columns}
                    data={listaConsultas}
                    rowSelected={consultaSeleccionada}
                    setRowSelected={setConsultaSeleccionada}
                    mainField="iIdConsulta"
                />
            </MeditocBody>
            <FormConsulta
                entConsulta={consultaParaModal}
                open={modalFormConsultaOpen}
                setOpen={setModalFormConsultaOpen}
                funcGetConsultas={funcGetConsultas}
                consultaEntidadVacia={consultaEntidadVacia}
                setConsultaSeleccionada={setConsultaSeleccionada}
                usuarioSesion={usuarioSesion}
                funcLoader={funcLoader}
                funcAlert={funcAlert}
                usuarioColaborador={usuarioColaborador}
            />
        </Fragment>
    );
};

export default Administrador;
