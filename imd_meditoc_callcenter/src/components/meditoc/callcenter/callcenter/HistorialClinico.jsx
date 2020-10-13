import { Button, Grid } from "@material-ui/core";
import React, { Fragment } from "react";

import DetalleHistorialClinico from "./DetalleHistorialClinico";
import MeditocTable from "../../../utilidades/MeditocTable";
import PropTypes from "prop-types";
import { cellProps } from "../../../../configurations/dataTableIconsConfig";
import { useState } from "react";

const HistorialClinico = (props) => {
    const { lstHistorialClinico } = props;
    const columns = [
        { title: "ID", field: "iIdHistorialClinico", ...cellProps, hidden: true },
        { title: "Fecha", field: "sFechaCreacion", ...cellProps },
        { title: "Duración", field: "sDuracionConsulta", ...cellProps },
        { title: "Detalle", field: "sDetalle", ...cellProps },
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
            <DetalleHistorialClinico
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
