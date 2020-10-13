import { Grid, IconButton, MenuItem, TextField, Tooltip, makeStyles } from "@material-ui/core";
import React, { Fragment } from "react";
import { green, red } from "@material-ui/core/colors";

import DetalleFolio from "./DetalleFolio";
import FolioController from "../../../controllers/FolioController";
import MeditocBody from "../../utilidades/MeditocBody";
import MeditocHeader1 from "../../utilidades/MeditocHeader1";
import MeditocHelper from "../../utilidades/MeditocHelper";
import MeditocModalBotones from "../../utilidades/MeditocModalBotones";
import MeditocSubtitulo from "../../utilidades/MeditocSubtitulo";
import MeditocTable from "../../utilidades/MeditocTable";
import ProductoController from "../../../controllers/ProductoController";
import PropTypes from "prop-types";
import ReplayIcon from "@material-ui/icons/Replay";
import StopIcon from "@material-ui/icons/Stop";
import VisibilityIcon from "@material-ui/icons/Visibility";
import { cellProps } from "../../../configurations/dataTableIconsConfig";
import { emptyFunc } from "../../../configurations/preventConfig";
import theme from "../../../configurations/themeConfig";
import { useEffect } from "react";
import { useState } from "react";

const useStyles = makeStyles({
    root: {
        backgroundColor: theme.palette.primary.main,
        color: "#fff",
    },
});

const Folios = (props) => {
    const { permisos, entCatalogos, funcLoader, funcAlert } = props;

    const classes = useStyles();

    const foliosController = new FolioController();
    const productoController = new ProductoController();

    const columns = [
        { title: "", field: "sStatus", ...cellProps, sorting: false },
        { title: "Folio", field: "sFolio", ...cellProps },
        { title: "Origen", field: "sOrigen", ...cellProps },
        { title: "Empresa", field: "sFolioEmpresa", ...cellProps },
        { title: "Paciente", field: "sNombrePaciente", ...cellProps },
        { title: "Correo", field: "sCorreoPaciente", ...cellProps },
        { title: "Creado", field: "sFechaCreacion", ...cellProps },
        { title: "Vencimiento", field: "sFechaVencimiento", ...cellProps },
    ];

    const filtrosVacios = {
        txtOrigen: "",
        txtProducto: "",
        txtVigente: "",
    };

    const [filtroFolios, setFiltroFolios] = useState(filtrosVacios);

    const handleChangeFiltroFolio = (e) => {
        setFiltroFolios({
            ...filtroFolios,
            [e.target.name]: e.target.value,
        });
    };

    const [listaFolios, setListaFolios] = useState([]);
    const [listaProductos, setListaProductos] = useState([]);
    const [folioSeleccionado, setFolioSeleccionado] = useState({ iIdFolio: 0 });

    const [modalDetalleFolioOpen, setModalDetalleFolioOpen] = useState(false);

    const handleClickDetalleFolio = () => {
        if (folioSeleccionado.iIdFolio === 0) {
            funcAlert("Seleccione un folio para continuar");
            return;
        }
        setModalDetalleFolioOpen(true);
    };

    const funcGetFolios = async (clean = false) => {
        funcLoader(true, "Consultando folios...");

        let response;
        if (clean === false) {
            response = await foliosController.funcGetFolios(
                null,
                null,
                filtroFolios.txtProducto,
                filtroFolios.txtOrigen,
                "",
                "",
                null,
                "",
                "",
                filtroFolios.txtVigente
            );
        } else {
            response = await foliosController.funcGetFolios();
            setFiltroFolios(filtrosVacios);
        }

        if (response.Code === 0) {
            setListaFolios(
                response.Result.map((folio) => ({
                    ...folio,
                    sStatus: (
                        <span>
                            <i
                                className="icon size-20"
                                style={{
                                    color: folio.bVigente === true ? green[500] : red[500],
                                    verticalAlign: "middle",
                                }}
                                dangerouslySetInnerHTML={{ __html: `&#x${folio.sIcon};&nbsp;&nbsp;&nbsp;` }}
                            />
                        </span>
                    ),
                }))
            );
        } else {
            funcAlert(response.Message);
        }

        funcLoader();
    };

    const funcGetProductos = async () => {
        funcLoader(true, "Consultando productos...");

        const response = await productoController.funcGetProductos();

        if (response.Code === 0) {
            setListaProductos(response.Result);
        } else {
            funcAlert(response.Message);
        }
        funcLoader();
    };

    const getData = async () => {
        await funcGetFolios(true);
        await funcGetProductos();
    };

    useEffect(() => {
        if (filtroFolios !== filtrosVacios) {
            funcGetFolios(false);
        }
        // eslint-disable-next-line
    }, [filtroFolios]);

    useEffect(() => {
        getData();
        // eslint-disable-next-line
    }, []);

    return (
        <Fragment>
            <MeditocHeader1 title={permisos.Nombre}>
                {permisos.Botones["1"] !== undefined && ( //Detalle de folio
                    <Tooltip title={permisos.Botones["1"].Nombre} arrow>
                        <IconButton onClick={handleClickDetalleFolio}>
                            <VisibilityIcon className="color-0" />
                        </IconButton>
                    </Tooltip>
                )}
                {permisos.Botones["2"] !== undefined && ( //Actualizar tabla
                    <Tooltip title={permisos.Botones["2"].Nombre} arrow>
                        <IconButton onClick={getData}>
                            <ReplayIcon className="color-0" />
                        </IconButton>
                    </Tooltip>
                )}
            </MeditocHeader1>
            <MeditocBody>
                <Grid container spacing={3}>
                    <Grid item xs={12}>
                        <MeditocSubtitulo title="FILTROS" />
                    </Grid>
                    <Grid item sm={4} xs={12}>
                        <TextField
                            name="txtOrigen"
                            label="Origen de folio:"
                            variant="outlined"
                            select
                            fullWidth
                            value={filtroFolios.txtOrigen}
                            onChange={handleChangeFiltroFolio}
                        >
                            <MenuItem value="">Todos</MenuItem>
                            {entCatalogos.catOrigen.map((origen) => (
                                <MenuItem value={origen.fiId.toString()}>{origen.fsDescripcion}</MenuItem>
                            ))}
                        </TextField>
                    </Grid>
                    <Grid item sm={4} xs={12}>
                        <TextField
                            name="txtProducto"
                            label="Producto:"
                            variant="outlined"
                            select
                            SelectProps={{
                                MenuProps: {
                                    classes: {
                                        paper: classes.root,
                                    },
                                    PaperProps: {
                                        style: { maxHeight: 400 },
                                    },
                                },
                            }}
                            fullWidth
                            value={filtroFolios.txtProducto}
                            onChange={handleChangeFiltroFolio}
                        >
                            <MenuItem value="">Todos los productos</MenuItem>
                            {listaProductos.map((producto) => (
                                <MenuItem key={producto.iIdProducto} value={producto.iIdProducto.toString()}>
                                    <i
                                        className="icon size-20"
                                        dangerouslySetInnerHTML={{ __html: `&#x${producto.sIcon};&nbsp;&nbsp;&nbsp;` }}
                                    />
                                    {"   "}
                                    {producto.sNombre}
                                </MenuItem>
                            ))}
                        </TextField>
                    </Grid>
                    <Grid item sm={4} xs={12}>
                        <TextField
                            name="txtVigente"
                            label="Estatus de folio:"
                            variant="outlined"
                            select
                            fullWidth
                            value={filtroFolios.txtVigente}
                            onChange={handleChangeFiltroFolio}
                        >
                            <MenuItem value="">Todos los folios</MenuItem>
                            <MenuItem value="true">Folio activo</MenuItem>
                            <MenuItem value="false">Folio inactivo/vencido</MenuItem>
                        </TextField>
                    </Grid>
                    <MeditocModalBotones okMessage="LIMPIAR FILTRO" hideCancel okFunc={getData} />
                    <Grid item xs={12}>
                        <MeditocTable
                            columns={columns}
                            data={listaFolios}
                            rowSelected={folioSeleccionado}
                            setRowSelected={setFolioSeleccionado}
                            mainField="sFolio"
                            doubleClick={permisos.Botones["1"] !== undefined ? handleClickDetalleFolio : emptyFunc}
                        />
                    </Grid>
                </Grid>
            </MeditocBody>
            <DetalleFolio
                entFolio={folioSeleccionado}
                open={modalDetalleFolioOpen}
                setOpen={setModalDetalleFolioOpen}
            />
            <MeditocHelper title="Estatus de folio:">
                <div>
                    <StopIcon style={{ color: green[500], verticalAlign: "middle" }} />
                    {"  "}
                    Folio activo
                </div>
                <div>
                    <StopIcon style={{ color: red[500], verticalAlign: "middle" }} />
                    {"  "}
                    Folio inactivo
                </div>
            </MeditocHelper>
        </Fragment>
    );
};

Folios.propTypes = {
    funcAlert: PropTypes.func,
    funcLoader: PropTypes.func,
    title: PropTypes.any,
};

export default Folios;
