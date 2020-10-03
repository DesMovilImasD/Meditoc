import PropTypes from "prop-types";
import React, { Fragment } from "react";
import { useState } from "react";
import MeditocTabBody from "../../../utilidades/MeditocTabBody";
import MeditocTabHeader from "../../../utilidades/MeditocTabHeader";
import MeditocTabPanel from "../../../utilidades/MeditocTabPanel";
import FormPaciente from "./FormPaciente";
import FormDiagnosticoTratamiento from "./FormDiagnosticoTratamiento";
import HistorialClinico from "./HistorialClinico";

const FormCallCenter = (props) => {
    const {
        usuarioSesion,
        funcLoader,
        funcAlert,
        usuarioColaborador,
        entCallCenter,
        setEntCallCenter,
        formDiagnosticoTratamiento,
        setFormDiagnosticoTratamiento,
    } = props;

    const [tabIndex, setTabIndex] = useState(0);

    return (
        <Fragment>
            <MeditocTabHeader
                tabs={["PACIENTE", "DIAGNÓSTICO Y TRATAMIENTO", "HISTORIAL"]}
                index={tabIndex}
                setIndex={setTabIndex}
            />
            <MeditocTabBody index={tabIndex} setIndex={setTabIndex}>
                <MeditocTabPanel id={0} index={tabIndex}>
                    <FormPaciente
                        funcLoader={funcLoader}
                        funcAlert={funcAlert}
                        usuarioSesion={usuarioSesion}
                        usuarioColaborador={usuarioColaborador}
                        entCallCenter={entCallCenter}
                        setEntCallCenter={setEntCallCenter}
                    />
                </MeditocTabPanel>
                <MeditocTabPanel id={1} index={tabIndex}>
                    <FormDiagnosticoTratamiento
                        formDiagnosticoTratamiento={formDiagnosticoTratamiento}
                        setFormDiagnosticoTratamiento={setFormDiagnosticoTratamiento}
                    />
                </MeditocTabPanel>
                <MeditocTabPanel id={2} index={tabIndex}>
                    <HistorialClinico entCallCenter={entCallCenter} />
                </MeditocTabPanel>
            </MeditocTabBody>
        </Fragment>
    );
};

FormCallCenter.propTypes = {
    entCallCenter: PropTypes.any,
    formDiagnosticoTratamiento: PropTypes.any,
    funcAlert: PropTypes.any,
    funcLoader: PropTypes.any,
    setEntCallCenter: PropTypes.any,
    setFormDiagnosticoTratamiento: PropTypes.any,
    usuarioColaborador: PropTypes.any,
    usuarioSesion: PropTypes.any,
};

export default FormCallCenter;
