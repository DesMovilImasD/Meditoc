import { IconButton, Tooltip } from "@material-ui/core";
import React, { Fragment, useEffect } from "react";
import MeditocHeader1 from "../../utilidades/MeditocHeader1";
import ListAltIcon from "@material-ui/icons/ListAlt";
import ReportesController from "../../../controllers/ReportesController";
import { useState } from "react";
import MeditocBody from "../../utilidades/MeditocBody";
import MeditocTable from "../../utilidades/MeditocTable";
import DetalleDoctor from "./DetalleDoctor";

const ReportesDoctores = (props) => {
    const { usuarioSesion, funcLoader, funcAlert } = props;

    const reportesController = new ReportesController();

    const columnas = [
        { title: "ID", field: "iIdDoctor", align: "center" },
        { title: "Nombre", field: "sNombre", align: "center" },
        { title: "Tipo de doctor", field: "sTipoDoctor", align: "center" },
        { title: "Especialidad", field: "sEspecialidad", align: "center" },
        { title: "Total consultas", field: "iTotalConsultas", align: "center" },
    ];

    const [entDoctores, setEntDoctores] = useState({});

    const [doctorSeleccionado, setDoctorSeleccionado] = useState({ iIdDoctor: 0, lstConsultas: [] });

    const [modalDetalleDoctorOpen, setModalDetalleDoctorOpen] = useState(false);

    const funcGetDoctores = async () => {
        funcLoader(true, "Obteniendo lista de doctores...");

        const response = await reportesController.funcObtenerReporteDoctores();

        if (response.Code === 0) {
            setEntDoctores(response.Result);
        } else {
            funcAlert(response.Message);
        }

        funcLoader();
    };

    const funcAbrirDetalle = () => {
        if (doctorSeleccionado.iIdDoctor === 0) {
            funcAlert("Seleccione un doctor de la tabla para ver el detalle", "warning");
            return;
        }
        setModalDetalleDoctorOpen(true);
    };

    useEffect(() => {
        funcGetDoctores();
    }, []);

    return (
        <Fragment>
            <MeditocHeader1 title="REPORTES DOCTORES">
                <Tooltip title="Ver detalle">
                    <IconButton onClick={funcAbrirDetalle}>
                        <ListAltIcon className="color-0" />
                    </IconButton>
                </Tooltip>
            </MeditocHeader1>
            <MeditocBody>
                <MeditocTable
                    columns={columnas}
                    data={entDoctores.lstDoctores}
                    rowSelected={doctorSeleccionado}
                    setRowSelected={setDoctorSeleccionado}
                    mainField="iIdDoctor"
                />
            </MeditocBody>
            <DetalleDoctor
                entDoctor={doctorSeleccionado}
                open={modalDetalleDoctorOpen}
                setOpen={setModalDetalleDoctorOpen}
            />
        </Fragment>
    );
};

export default ReportesDoctores;
