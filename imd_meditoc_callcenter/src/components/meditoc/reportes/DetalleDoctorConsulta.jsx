import { Grid } from "@material-ui/core";
import React, { useEffect } from "react";
import { useState } from "react";
import CallCenterController from "../../../controllers/CallCenterController";
import MeditocModal from "../../utilidades/MeditocModal";

const DetalleDoctorConsulta = (props) => {
    const { iIdConsulta, open, setOpen, funcLoader, funcAlert } = props;

    const callcenterController = new CallCenterController();

    const [entCallCenter, setEntCallCenter] = useState(null);

    const funcGetDetalle = async () => {
        funcLoader(true, "Consultando detalles...");

        const response = await callcenterController.funcGetConsulta(iIdConsulta);

        if (response.Code === 0) {
            setEntCallCenter(response.Result);
        } else {
            funcAlert(response.Message);
        }

        funcLoader();
    };

    useEffect(() => {
        if (iIdConsulta !== 0) {
            funcGetDetalle();
        }
        // eslint-disable-next-line
    }, [iIdConsulta]);

    return (
        <MeditocModal title="Detalle de consulta" size="normal" open={open} setOpen={setOpen}>
            {entCallCenter !== null && (
                <Grid container spacing={3}>
                    <Grid item xs={12}>
                        HOLA
                    </Grid>
                </Grid>
            )}
        </MeditocModal>
    );
};

export default DetalleDoctorConsulta;
