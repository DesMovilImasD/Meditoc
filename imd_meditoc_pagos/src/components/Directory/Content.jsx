import React, { useEffect, useState } from "react";
import {
    Grid,
    TextField,
    InputAdornment,
    IconButton,
    FormControl,
    InputLabel,
    Select,
    MenuItem,
    makeStyles,
    Tooltip,
} from "@material-ui/core";
import { BsSearch } from "react-icons/bs";
import { AiOutlineFileSearch } from "react-icons/ai";
import { MdClose } from "react-icons/md";
import Pagination from "@material-ui/lab/Pagination";
import { logoMeditocDoctorSample } from "../../configuration/imgConfig";
import { serverMain } from "../../configuration/serverConfig";
import { apiGetDirectorio, apiGetEspecialidades } from "../../configuration/apiConfig";
import MedicInfo from "./MedicInfo";

const useStyles = makeStyles({
    ul: {
        display: "inline-flex",
        flexWrap: "initial",
    },
});

const Content = (props) => {
    const { funcLoader } = props;

    const classes = useStyles();
    const pageSize = 20;

    const [paginaSeleccionada, setPaginaSeleccionada] = useState(1);
    const [paginasTotales, setPaginasTotales] = useState(0);
    const [especialidadSeleccionada, setEspecialidadSeleccionada] = useState("");
    const [buscadorIngresada, setBuscadorIngresada] = useState("");
    const [ultimaBusqueda, setUltimaBusqueda] = useState("");

    const [listaColaboradores, setListaColaboradores] = useState([]);
    const [listaEspecialidades, setListaEspecialidades] = useState([]);

    const funcGetDirectorio = async (especialidad = null, buscador = "", page = null) => {
        funcLoader(true, "Consultando en directorio médico...");

        try {
            const apiResponse = await fetch(
                `${serverMain}${apiGetDirectorio}?piIdEspecialidad=${especialidad}&psBuscador=${buscador}&piPage=${page}&piPageSize=${pageSize}`
            );

            const response = await apiResponse.json();

            if (response.Code === 0) {
                setPaginasTotales(response.Result.iTotalPaginas);
                setListaColaboradores(response.Result.lstColaboradores);
            }
        } catch (error) {}

        funcLoader();
    };

    const funcGetEspecialidades = async () => {
        funcLoader(true, "Consultando especialidades...");

        try {
            const apiResponse = await fetch(`${serverMain}${apiGetEspecialidades}`, {
                headers: {
                    "AppKey": "qSVBJIQpOqtp0UfwzwX1ER6fNYR8YiPU/bw5CdEqYqk=",
                    "AppToken": "Xx3ePv63cUTg77QPATmztJ3J8cdO1riA7g+lVRzOzhfnl9FnaVT1O2YIv8YCTVRZ",
                },
            });

            const response = await apiResponse.json();

            if (response.Code === 0) {
                setListaEspecialidades(response.Result);
            }
        } catch (error) {}

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
        funcGetData();
    }, []);

    return (
        <div className="directory-content">
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
                                                <MdClose />
                                            </IconButton>
                                        </Tooltip>
                                    ) : null}
                                    <Tooltip title="Iniciar búsqueda" arrow placement="top">
                                        <IconButton onClick={handleClickBuscar}>
                                            <BsSearch />
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
                    <FormControl fullWidth variant="outlined">
                        <InputLabel id="slcEspecialidad">Especialidad:</InputLabel>
                        <Select
                            labelId="slcEspecialidad"
                            label="Especialidad:"
                            value={especialidadSeleccionada}
                            onChange={handleChangeEspecialidad}
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
                        </Select>
                    </FormControl>
                </Grid>
                <Grid item xs={12}>
                    {listaColaboradores.map((entColaborador) => (
                        <MedicInfo key={entColaborador.iIdColaborador} entColaborador={entColaborador} />
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
                            <AiOutlineFileSearch style={{ fontSize: 150, color: "#ccc" }} />
                            <br />
                            <span className="price-content-description-normal">No se encontraron registros</span>
                        </div>
                    )}
                </Grid>
            </Grid>
        </div>
    );
};

export default Content;
