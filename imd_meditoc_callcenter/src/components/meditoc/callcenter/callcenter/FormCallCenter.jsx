import React, { Fragment } from "react";
import { useState } from "react";
import MeditocTabBody from "../../../utilidades/MeditocTabBody";
import MeditocTabHeader from "../../../utilidades/MeditocTabHeader";
import MeditocTabPanel from "../../../utilidades/MeditocTabPanel";

const FormCallCenter = (props) => {
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
                    Paciente
                </MeditocTabPanel>
                <MeditocTabPanel id={1} index={tabIndex}>
                    Diagnostico
                </MeditocTabPanel>
                <MeditocTabPanel id={2} index={tabIndex}>
                    Historila
                </MeditocTabPanel>
            </MeditocTabBody>
        </Fragment>
    );
};

export default FormCallCenter;
