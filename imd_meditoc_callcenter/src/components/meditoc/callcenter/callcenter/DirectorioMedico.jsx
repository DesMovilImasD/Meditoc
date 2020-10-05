import { Grid, IconButton, InputAdornment, MenuItem, TextField, Tooltip, makeStyles } from "@material-ui/core";
import React, { useEffect, useState } from "react";

import CloseIcon from "@material-ui/icons/Close";
import ColaboradorController from "../../../../controllers/ColaboradorController";
import DetalleDirectorioMeditoc from "./DetalleDirectorioMeditoc";
import EspecialidadController from "../../../../controllers/EspecialidadController";
import FindInPageIcon from "@material-ui/icons/FindInPage";
import MeditocModal from "../../../utilidades/MeditocModal";
import Pagination from "@material-ui/lab/Pagination";
import PropTypes from "prop-types";
import SearchIcon from "@material-ui/icons/Search";

const useStyles = makeStyles({
    ul: {
        display: "inline-flex",
        flexWrap: "initial",
    },
});

const DirectorioMedico = (props) => {
    const { open, setOpen, funcLoader, funcAlert } = props;

    const classes = useStyles();
    const pageSize = 20;

    const colaboradorController = new ColaboradorController();
    const especialidadController = new EspecialidadController();

    const [paginaSeleccionada, setPaginaSeleccionada] = useState(1);
    const [paginasTotales, setPaginasTotales] = useState(0);
    const [especialidadSeleccionada, setEspecialidadSeleccionada] = useState("");
    const [buscadorIngresada, setBuscadorIngresada] = useState("");
    const [ultimaBusqueda, setUltimaBusqueda] = useState("");

    const [listaColaboradores, setListaColaboradores] = useState([]);
    const [listaEspecialidades, setListaEspecialidades] = useState([]);

    const funcGetDirectorio = async (especialidad = null, buscador = "", page = null) => {
        funcLoader(true, "Consultando en directorio médico...");

        const response = await colaboradorController.funcGetDirectorio(especialidad, buscador, page, pageSize);

        if (response.Code === 0) {
            setPaginasTotales(response.Result.iTotalPaginas);
            setListaColaboradores(response.Result.lstColaboradores);
        } else {
            funcAlert(response.Message);
        }
        funcLoader();
    };

    const funcGetEspecialidades = async () => {
        funcLoader(true, "Consultando especialidades...");

        const response = await especialidadController.funcGetEspecialidad();

        if (response.Code === 0) {
            setListaEspecialidades(response.Result);
        } else {
            funcAlert(response.Message);
        }

        funcLoader();
    };

    const handleChangeEspecialidad = async (e) => {
        await funcGetDirectorio(e.target.value, buscadorIngresada, 1);
        setEspecialidadSeleccionada(e.target.value);
        setPaginaSeleccionada(1);
    };

    const handleChangePage = async (e, page) => {
        await funcGetDirectorio(especialidadSeleccionada, buscadorIngresada, page);
        setPaginaSeleccionada(page);
    };

    const handleChangeBuscador = (e) => {
        setBuscadorIngresada(e.target.value);
    };

    const handleClickBuscar = async () => {
        await funcGetDirectorio(null, buscadorIngresada, 1);
        setUltimaBusqueda(buscadorIngresada);
        setPaginaSeleccionada(1);
    };

    const handleClickLimpiar = async () => {
        await funcGetDirectorio(especialidadSeleccionada, "", 1);
        setBuscadorIngresada("");
        setUltimaBusqueda("");
        setPaginaSeleccionada(1);
    };

    const funcGetData = async () => {
        await funcGetDirectorio(null, "", 1);
        await funcGetEspecialidades();
    };

    useEffect(() => {
        if (open === true) {
            funcGetData();
        }
        // eslint-disable-next-line
    }, [open]);

    return (
        <MeditocModal title="Directorio médico de especialistas" size="large" open={open} setOpen={setOpen}>
            <Grid container spacing={3}>
                <Grid item sm={6} xs={12}>
                    <TextField
                        variant="outlined"
                        fullWidth
                        label="Buscar..."
                        InputProps={{
                            endAdornment: (
                                <InputAdornment edge="start">
                                    {ultimaBusqueda !== "" ? (
                                        <Tooltip title="Limpiar búsqueda" arrow placement="top">
                                            <IconButton onClick={handleClickLimpiar}>
                                                <CloseIcon />
                                            </IconButton>
                                        </Tooltip>
                                    ) : null}
                                    <Tooltip title="Iniciar búsqueda" arrow placement="top">
                                        <IconButton onClick={handleClickBuscar}>
                                            <SearchIcon />
                                        </IconButton>
                                    </Tooltip>
                                </InputAdornment>
                            ),
                        }}
                        value={buscadorIngresada}
                        onChange={handleChangeBuscador}
                    />
                </Grid>
                <Grid item sm={6} xs={12}>
                    <TextField
                        fullWidth
                        variant="outlined"
                        label="Especialidad:"
                        select
                        value={especialidadSeleccionada}
                        onChange={handleChangeEspecialidad}
                        SelectProps={{ MenuProps: { PaperProps: { style: { maxHeight: 300 } } } }}
                    >
                        <MenuItem value="">
                            <em>Todas las especialidades</em>
                        </MenuItem>
                        {listaEspecialidades
                            .sort((a, b) => (a.sNombre > b.sNombre ? 1 : -1))
                            .map((especialidad) =>
                                especialidad.iIdEspecialidad === 1 ? null : (
                                    <MenuItem
                                        key={especialidad.iIdEspecialidad}
                                        value={especialidad.iIdEspecialidad.toString()}
                                    >
                                        {especialidad.sNombre}
                                    </MenuItem>
                                )
                            )}
                    </TextField>
                </Grid>
                <Grid item xs={12}>
                    {listaColaboradores.map((entColaborador) => (
                        <DetalleDirectorioMeditoc key={entColaborador.iIdColaborador} entColaborador={entColaborador} />
                    ))}
                    {paginasTotales > 0 ? (
                        <div className="directory-pagination-container">
                            <Pagination
                                classes={{ ul: classes.ul }}
                                count={paginasTotales}
                                size="large"
                                showFirstButton
                                showLastButton
                                color="primary"
                                page={paginaSeleccionada}
                                onChange={handleChangePage}
                            />
                        </div>
                    ) : (
                        <div className="center">
                            <FindInPageIcon style={{ fontSize: 150, color: "#ccc" }} />
                            <br />
                            <span className="price-content-description-normal">No se encontraron registros</span>
                        </div>
                    )}
                </Grid>
            </Grid>
        </MeditocModal>
    );
};

DirectorioMedico.propTypes = {
    funcAlert: PropTypes.func,
    funcLoader: PropTypes.func,
    open: PropTypes.bool,
    setOpen: PropTypes.any,
};

export default DirectorioMedico;
