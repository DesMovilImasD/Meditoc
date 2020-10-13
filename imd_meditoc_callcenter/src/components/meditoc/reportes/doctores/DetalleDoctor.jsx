import { Button, Grid } from "@material-ui/core";
import React, { useEffect, useState } from "react";

import DetalleConsulta from "./DetalleConsulta";
import MeditocInfoField from "../../../utilidades/MeditocInfoField";
import MeditocModal from "../../../utilidades/MeditocModal";
import MeditocModalBotones from "../../../utilidades/MeditocModalBotones";
import MeditocTable from "../../../utilidades/MeditocTable";
import PropTypes from "prop-types";
import { cellProps } from "../../../../configurations/dataTableIconsConfig";

const DetalleDoctor = (props) => {
    const { entDoctor, open, setOpen, funcLoader, funcAlert } = props;

    const columnas = [
        { title: "ID", field: "iIdConsulta", ...cellProps },
        { title: "Consulta inicio", field: "sFechaConsultaInicio", ...cellProps },
        { title: "Consulta fin", field: "sFechaConsultaFin", ...cellProps },
        { title: "Nombre", field: "name", ...cellProps },
        { title: "Estatus de consulta", field: "sEstatusConsulta", ...cellProps },
        { title: "Detalle", field: "sDetalle", ...cellProps },
    ];

    const [modalDetalleConsultaOpen, setModalDetalleConsultaOpen] = useState(false);
    const [consultaSeleccionada, setConsultaSeleccionada] = useState({ iIdConsulta: 0 });

    const [listaConsultas, setListaConsultas] = useState([]);

    // eslint-disable-next-line
    const hadleClickVerDetalle = () => {
        if (consultaSeleccionada.iIdConsulta === 0) {
            funcAlert("Seleccione una consulta para continuar");
            return;
        }
        setModalDetalleConsultaOpen(true);
    };

    const handleClickDetalleConsulta = () => {
        //setIIdConsulta(id);
        setModalDetalleConsultaOpen(true);
    };

    useEffect(() => {
        setListaConsultas(
            entDoctor.lstConsultas.map((consulta) => ({
                ...consulta,
                name: consulta.entPaciente.name,
                sFechaConsultaInicio:
                    consulta.sEstatusConsulta === "Creado/Programado" || consulta.sEstatusConsulta === "Cancelado"
                        ? consulta.sFechaProgramadaInicio
                        : consulta.sFechaConsultaInicio,
                sFechaConsultaFin:
                    consulta.sEstatusConsulta === "Creado/Programado" || consulta.sEstatusConsulta === "Cancelado"
                        ? consulta.sFechaProgramadaFin
                        : consulta.sEstatusConsulta === "En consulta"
                        ? "Consultando"
                        : consulta.sFechaConsultaFin,
                sDetalle: (
                    <Button variant="contained" color="primary" onClick={handleClickDetalleConsulta}>
                        DETALLE
                    </Button>
                ),
            }))
        );
    }, [entDoctor]);

    return (
        <MeditocModal title="Detalle de doctor" size="large" open={open} setOpen={setOpen}>
            <Grid container spacing={3}>
                <Grid item sm={4} xs={12}>
                    <MeditocInfoField label="Nombre del Doctor:" value={entDoctor.sNombre} />
                </Grid>
                <Grid item sm={4} xs={12}>
                    <MeditocInfoField label="Tipo de Doctor:" value={entDoctor.sTipoDoctor} />
                </Grid>
                <Grid item sm={4} xs={12}>
                    <MeditocInfoField label="Especialidad:" value={entDoctor.sEspecialidad} />
                </Grid>
                <Grid item sm={4} xs={12}>
                    <MeditocInfoField label="Teléfono:" value={entDoctor.sTelefono} />
                </Grid>
                <Grid item sm={4} xs={12}>
                    <MeditocInfoField label="Correo Electrónico:" value={entDoctor.sCorreo} />
                </Grid>
                <Grid item sm={4} xs={12}>
                    <MeditocInfoField label="Dirección de Consultorio:" value={entDoctor.sDireccionConsultorio} />
                </Grid>
                <Grid item sm={4} xs={12}>
                    <MeditocInfoField label="Número de Sala Icelink:" value={entDoctor.iNumSala} />
                </Grid>
                <Grid item sm={4} xs={12}>
                    <MeditocInfoField label="Total de consultas:" value={entDoctor.iTotalConsultas} />
                </Grid>
                <Grid item xs={12}>
                    <MeditocTable
                        columns={columnas}
                        data={listaConsultas}
                        rowSelected={consultaSeleccionada}
                        setRowSelected={setConsultaSeleccionada}
                        mainField="iIdConsulta"
                        doubleClick={handleClickDetalleConsulta}
                    />
                </Grid>
                <MeditocModalBotones cancelMessage="Cerrar detalle de doctor" setOpen={setOpen} hideOk />
            </Grid>
            <DetalleConsulta
                entConsultaSeleccionada={consultaSeleccionada}
                open={modalDetalleConsultaOpen}
                setOpen={setModalDetalleConsultaOpen}
                funcLoader={funcLoader}
                funcAlert={funcAlert}
            />
        </MeditocModal>
    );
};

DetalleDoctor.propTypes = {
    entDoctor: PropTypes.shape({
        iNumSala: PropTypes.any,
        iTotalConsultas: PropTypes.any,
        lstConsultas: PropTypes.shape({
            map: PropTypes.func,
        }),
        sCorreo: PropTypes.any,
        sDireccionConsultorio: PropTypes.any,
        sEspecialidad: PropTypes.any,
        sNombre: PropTypes.any,
        sTelefono: PropTypes.any,
        sTipoDoctor: PropTypes.any,
    }),
    funcAlert: PropTypes.any,
    funcLoader: PropTypes.any,
    open: PropTypes.any,
    setOpen: PropTypes.any,
};

export default DetalleDoctor;
