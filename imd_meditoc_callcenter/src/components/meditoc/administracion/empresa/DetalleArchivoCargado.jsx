import { Grid, Typography } from "@material-ui/core";
import React, { useState } from "react";

import MeditocModal from "../../../utilidades/MeditocModal";
import MeditocModalBotones from "../../../utilidades/MeditocModalBotones";
import MeditocTable from "../../../utilidades/MeditocTable";
import PropTypes from "prop-types";

/*************************************************************
 * Descripcion: Modal para ver el detalle de los archivos cargados
 * Creado: Cristopher Noh
 * Fecha: 26/08/2020
 * Invocado desde: FormCargarArchivo
 *************************************************************/
const DetalleArchivoCargado = (props) => {
    //=================================PROPS=================================
    const { entArchivoVerificado, open, setOpen } = props;

    //=================================VARIABLES=================================
    const columns = [
        { title: "Folio", field: "sFolio", align: "center" },
        { title: "Contraseña", field: "sPassword", align: "center" },
    ];

    //=================================STATES=================================
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

DetalleArchivoCargado.propTypes = {
    entArchivoVerificado: PropTypes.shape({
        entProducto: PropTypes.shape({
            sNombre: PropTypes.string,
        }),
        lstFolios: PropTypes.array,
        totalFolios: PropTypes.number,
    }),
    open: PropTypes.bool,
    setOpen: PropTypes.func,
};

export default DetalleArchivoCargado;
