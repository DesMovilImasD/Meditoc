import { Grid } from "@material-ui/core";
import React from "react"; //imr
import { useState } from "react";
import MeditocModal from "../../utilidades/MeditocModal";
import MeditocTable from "../../utilidades/MeditocTable";
import InfoField from "../../utilidades/InfoField";

const DetalleDoctor = (props) => {
  //sfc
  const { entDoctor, open, setOpen } = props;

  const columnas = [
    { title: "ID", field: "iIdConsulta", align: "center" },
    {
      title: "Consulta inicio",
      field: "sFechaConsultaInicio",
      align: "center",
    },
    { title: "Consulta fin", field: "sFechaConsultaFin", align: "center" },
    { title: "Nombre", field: "name", align: "center" },
    {
      title: "Estatus de consulta",
      field: "sEstatusConsulta",
      align: "center",
    },
  ];

  const [consultaSeleccionada, setConsultaSeleccionada] = useState({});

  return (
    <MeditocModal
      title="Detalle de doctor"
      size="large"
      open={open}
      setOpen={setOpen}
    >
      <Grid container spacing={3}>
        <Grid item sm={4} xs={12}>
          <InfoField label="Nombre del Doctor:" value={entDoctor.sNombre} />
        </Grid>
        <Grid item sm={4} xs={12}>
          <InfoField label="Tipo de Doctor:" value={entDoctor.sTipoDoctor} />
        </Grid>
        <Grid item sm={4} xs={12}>
          <InfoField label="Especialidad:" value={entDoctor.sEspecialidad} />
        </Grid>
        <Grid item sm={4} xs={12}>
          <InfoField label="Teléfono:" value={entDoctor.sTelefono} />
        </Grid>
        <Grid item sm={4} xs={12}>
          <InfoField label="Correo Electrónico:" value={entDoctor.sCorreo} />
        </Grid>
        <Grid item sm={4} xs={12}>
          <InfoField
            label="Dirección de Consultorio:"
            value={entDoctor.sDireccionConsultorio}
          />
        </Grid>
        <Grid item sm={4} xs={12}>
          <InfoField
            label="Número de Sala Icelink:"
            value={entDoctor.iNumSala}
          />
        </Grid>
        <Grid item sm={4} xs={12}>
          <InfoField
            label="Total de consultas:"
            value={entDoctor.iTotalConsultas}
          />
        </Grid>
        <Grid item xs={12}>
          <MeditocTable
            columns={columnas}
            data={entDoctor.lstConsultas.map((consulta) => ({
              ...consulta,
              name: consulta.entPaciente.name,
              sFechaConsultaInicio:
                consulta.sEstatusConsulta === "Creado/Programado" ||
                consulta.sEstatusConsulta === "Cancelado"
                  ? consulta.sFechaProgramadaInicio
                  : consulta.sFechaConsultaInicio,
              sFechaConsultaFin:
                consulta.sEstatusConsulta === "Creado/Programado" ||
                consulta.sEstatusConsulta === "Cancelado"
                  ? consulta.sFechaProgramadaFin
                  : consulta.sEstatusConsulta === "En consulta"
                  ? "Consultando"
                  : consulta.sFechaConsultaFin,
            }))}
            rowSelected={consultaSeleccionada}
            setRowSelected={setConsultaSeleccionada}
            mainField="iIdConsulta"
          />
        </Grid>
      </Grid>
    </MeditocModal>
  );
};

export default DetalleDoctor;
