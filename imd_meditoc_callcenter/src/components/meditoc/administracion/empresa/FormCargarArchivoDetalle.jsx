import PropTypes from "prop-types";
import { Grid, Typography } from "@material-ui/core";
import React, { useState } from "react";
import MeditocModal from "../../../utilidades/MeditocModal";
import MeditocModalBotones from "../../../utilidades/MeditocModalBotones";
import MeditocTable from "../../../utilidades/MeditocTable";

const FormCargarArchivoDetalle = (props) => {
    const { entArchivoVerificado, open, setOpen } = props;

    const columns = [
        { title: "Folio", field: "sFolio", align: "center" },
        { title: "Contraseña", field: "sPassword", align: "center" },
    ];

    const [folioSeleccionado, setFolioSeleccionado] = useState({ sFolio: "" });

    return (
        <MeditocModal
            title={entArchivoVerificado.entProducto.sNombre}
            size="small"
            open={open}
            setOpen={setOpen}
            level={2}
        >
            <Grid container spacing={3}>
                <Grid item xs={12}>
                    <Typography variant="h5" color="primary">
                        Folios encontrados: {entArchivoVerificado.totalFolios}
                    </Typography>
                </Grid>
                <Grid item xs={12}>
                    <MeditocTable
                        columns={columns}
                        data={entArchivoVerificado.lstFolios}
                        rowSelected={folioSeleccionado}
                        setRowSelected={setFolioSeleccionado}
                        mainField="sFolio"
                    />
                </Grid>
                <MeditocModalBotones hideCancel setOpen={setOpen} />
            </Grid>
        </MeditocModal>
    );
};

FormCargarArchivoDetalle.propTypes = {
    entArchivoVerificado: PropTypes.shape({
        entProducto: PropTypes.shape({
            sNombre: PropTypes.any,
        }),
        lstFolios: PropTypes.any,
        totalFolios: PropTypes.any,
    }),
    open: PropTypes.any,
    setOpen: PropTypes.any,
};

export default FormCargarArchivoDetalle;
