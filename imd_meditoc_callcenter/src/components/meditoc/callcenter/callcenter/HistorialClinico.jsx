import PropTypes from "prop-types";
import { Button, Grid } from "@material-ui/core";
import React, { Fragment } from "react";
import { useState } from "react";
import MeditocTable from "../../../utilidades/MeditocTable";
import HistorialClinicoDetalle from "./HistorialClinicoDetalle";

const HistorialClinico = (props) => {
    const { lstHistorialClinico } = props;
    const columns = [
        { title: "ID", field: "iIdHistorialClinico", align: "center", hidden: true },
        { title: "Fecha", field: "sFechaCreacion", align: "center" },
        { title: "Duración", field: "sDuracionConsulta", align: "center" },
        { title: "Detalle", field: "sDetalle", align: "center" },
    ];

    const [historialClinicoDetalle, setHistorialClinicoDetalle] = useState({ iIdHistorialClinico: 0 });

    const [modalHistorialDetalleOpen, setModalHistorialDetalleOpen] = useState(false);

    const handleClickDetalle = (iIdHistorialClinico) => {
        setHistorialClinicoDetalle(lstHistorialClinico.find((x) => x.iIdHistorialClinico === iIdHistorialClinico));
        setModalHistorialDetalleOpen(true);
    };

    return (
        <Fragment>
            <Grid container spacing={3}>
                <Grid item xs={12}>
                    <MeditocTable
                        columns={columns}
                        data={lstHistorialClinico.map((historia) => ({
                            ...historia,
                            sDetalle: (
                                <Button
                                    variant="contained"
                                    color="primary"
                                    onClick={() => handleClickDetalle(historia.iIdHistorialClinico)}
                                >
                                    Ver detalle
                                </Button>
                            ),
                        }))}
                        rowClick={false}
                        mainField="iIdHistorialClinico"
                    />
                </Grid>
            </Grid>
            <HistorialClinicoDetalle
                open={modalHistorialDetalleOpen}
                setOpen={setModalHistorialDetalleOpen}
                historial={historialClinicoDetalle}
            />
        </Fragment>
    );
};

HistorialClinico.propTypes = {
    lstHistorialClinico: PropTypes.shape({
        find: PropTypes.func,
        map: PropTypes.func,
    }),
};

export default HistorialClinico;
