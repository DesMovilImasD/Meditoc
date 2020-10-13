import { Grid, IconButton, MenuItem, TextField, Tooltip, makeStyles } from "@material-ui/core";
import { green, red } from "@material-ui/core/colors";

import CrearFolios from "./CrearFolios";
import CreateNewFolderIcon from "@material-ui/icons/CreateNewFolder";
import DeleteIcon from "@material-ui/icons/Delete";
import EventAvailableRoundedIcon from "@material-ui/icons/EventAvailableRounded";
import FolioController from "../../../../controllers/FolioController";
import FormCargarArchivo from "./FormCargarArchivo";
import GetAppIcon from "@material-ui/icons/GetApp";
import MeditocBody from "../../../utilidades/MeditocBody";
import MeditocConfirmacion from "../../../utilidades/MeditocConfirmacion";
import MeditocFullModal from "../../../utilidades/MeditocFullModal";
import MeditocHeader2 from "../../../utilidades/MeditocHeader2";
import MeditocHeader3 from "../../../utilidades/MeditocHeader3";
import MeditocHelper from "../../../utilidades/MeditocHelper";
import MeditocModalBotones from "../../../utilidades/MeditocModalBotones";
import MeditocTable from "../../../utilidades/MeditocTable";
import ModificarVigencia from "./ModificarVigencia";
import NoteAddRoundedIcon from "@material-ui/icons/NoteAddRounded";
import PropTypes from "prop-types";
import React from "react";
import ReplayIcon from "@material-ui/icons/Replay";
import StopIcon from "@material-ui/icons/Stop";
import { cellProps } from "../../../../configurations/dataTableIconsConfig";
import theme from "../../../../configurations/themeConfig";
import { useEffect } from "react";
import { useState } from "react";

const useStyles = makeStyles({
    root: {
        backgroundColor: theme.palette.primary.main,
        color: "#fff",
    },
});

const AdministrarFolios = (props) => {
    const {
        entEmpresa,
        open,
        setOpen,
        listaProductos,
        entCatalogos,
        usuarioSesion,
        permisos,
        funcLoader,
        funcAlert,
    } = props;

    const classes = useStyles();

    const folioController = new FolioController();

    const columns = [
        { title: "ID", field: "iIdFolio", ...cellProps, hidden: true },
        { title: "", field: "sStatus", ...cellProps, sorting: false },
        { title: "Folio", field: "sFolio", ...cellProps },
        { title: "Producto", field: "sNombreProducto", ...cellProps },
        { title: "Origen", field: "sOrigen", ...cellProps },
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

    const [listaFoliosEmpresa, setListaFoliosEmpresa] = useState([]);
    const [foliosEmpresaSeleccionado, setFoliosEmpresaSeleccionado] = useState([]);

    const [modalAgregarFoliosOpen, setModalAgregarFoliosOpen] = useState(false);
    const [modalAgregarVigenciaOpen, setModalAgregarVigenciaOpen] = useState(false);
    const [modalEliminarFoliosOpen, setModalEliminarFoliosOpen] = useState(false);

    const [formSubirArchivoOpen, setFormSubirArchivoOpen] = useState(false);
    const handleClickSubirArchivo = () => {
        setFormSubirArchivoOpen(true);
    };

    const handleClickAgregarFolios = () => {
        setModalAgregarFoliosOpen(true);
    };

    const handleClickModificarVigencia = () => {
        if (foliosEmpresaSeleccionado.length < 1) {
            funcAlert("Seleccione al menos un folio de la tabla para continuar");
            return;
        }
        setModalAgregarVigenciaOpen(true);
    };

    const handleClickEliminarFolios = () => {
        if (foliosEmpresaSeleccionado.length < 1) {
            funcAlert("Seleccione al menos un folio de la tabla para continuar");
            return;
        }
        setModalEliminarFoliosOpen(true);
    };

    const funcGetFoliosEmpresa = async (clean = false) => {
        funcLoader(true, "Consultando folios de empresa...");
        let response;
        if (clean === false) {
            response = await folioController.funcGetFolios(
                null,
                entEmpresa.iIdEmpresa,
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
            response = await folioController.funcGetFolios(null, entEmpresa.iIdEmpresa);
            setFiltroFolios(filtrosVacios);
        }

        if (response.Code === 0) {
            setListaFoliosEmpresa(
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
                                dangerouslySetInnerHTML={{ __html: `&#x${folio.sIcon}` }}
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

    const funcEliminarFolios = async () => {
        funcLoader(true, "Eliminando folios...");

        const entFolioFVSubmit = {
            iIdEmpresa: entEmpresa.iIdEmpresa,
            iIdUsuario: usuarioSesion.iIdUsuario,
            lstFolios: foliosEmpresaSeleccionado.map((folio) => ({ iIdFolio: folio.iIdFolio })),
        };

        const response = await folioController.funcEliminarFoliosEmpresa(entFolioFVSubmit);

        if (response.Code === 0) {
            setFoliosEmpresaSeleccionado([]);
            setModalEliminarFoliosOpen(false);
            funcAlert(response.Message, "success");
            await funcGetFoliosEmpresa(true);
        } else {
            funcAlert(response.Message);
        }
        funcLoader();
    };

    const handleClickDescargarPlantilla = async () => {
        funcLoader(true, "Descargando plantilla...");

        const response = await folioController.funcDescargarPlantilla();

        if (response.Code === 0) {
            funcAlert(response.Message, "success");
        } else {
            funcAlert(response.Message);
        }
        funcLoader();
    };

    const handleClickActualizarTabla = () => {
        funcGetFoliosEmpresa(true);
    };

    useEffect(() => {
        if (filtroFolios !== filtrosVacios) {
            funcGetFoliosEmpresa(false);
        }
        // eslint-disable-next-line
    }, [filtroFolios]);

    useEffect(() => {
        if (open === true) {
            funcGetFoliosEmpresa(true);
        }
        // eslint-disable-next-line
    }, [entEmpresa]);

    return (
        <MeditocFullModal open={open} setOpen={setOpen}>
            <div>
                <MeditocHeader2
                    title={`Administrar folios de ${entEmpresa.sNombre} (${entEmpresa.sFolioEmpresa})`}
                    setOpen={setOpen}
                />
                <MeditocBody>
                    <Grid container spacing={3}>
                        <Grid item xs={12}>
                            <MeditocHeader3 title={`Folios generados: ${listaFoliosEmpresa.length}`}>
                                {permisos.Botones["5"] !== undefined && ( //Crear folios a empresa
                                    <Tooltip title={permisos.Botones["5"].Nombre} arrow>
                                        <IconButton onClick={handleClickAgregarFolios}>
                                            <NoteAddRoundedIcon className="color-1" />
                                        </IconButton>
                                    </Tooltip>
                                )}
                                {permisos.Botones["6"] !== undefined && ( //Modificar vigencia a folios seleccionados
                                    <Tooltip title={permisos.Botones["6"].Nombre} arrow>
                                        <IconButton onClick={handleClickModificarVigencia}>
                                            <EventAvailableRoundedIcon className="color-1" />
                                        </IconButton>
                                    </Tooltip>
                                )}
                                {permisos.Botones["7"] !== undefined && ( //Cargar folios desde archivo
                                    <Tooltip title={permisos.Botones["7"].Nombre} arrow>
                                        <IconButton onClick={handleClickSubirArchivo}>
                                            <CreateNewFolderIcon className="color-1" />
                                        </IconButton>
                                    </Tooltip>
                                )}
                                {permisos.Botones["8"] !== undefined && ( //Descargar plantilla para cargar folios desde archivo
                                    <Tooltip title={permisos.Botones["8"].Nombre} arrow>
                                        <IconButton onClick={handleClickDescargarPlantilla}>
                                            <GetAppIcon className="color-1" />
                                        </IconButton>
                                    </Tooltip>
                                )}
                                {permisos.Botones["9"] !== undefined && ( //Eliminar folios seleccionados
                                    <Tooltip title={permisos.Botones["9"].Nombre} arrow>
                                        <IconButton onClick={handleClickEliminarFolios}>
                                            <DeleteIcon className="color-1" />
                                        </IconButton>
                                    </Tooltip>
                                )}
                                {permisos.Botones["10"] !== undefined && ( //Actualizar tabla de folios
                                    <Tooltip title={permisos.Botones["10"].Nombre} arrow>
                                        <IconButton onClick={handleClickActualizarTabla}>
                                            <ReplayIcon className="color-1" />
                                        </IconButton>
                                    </Tooltip>
                                )}
                            </MeditocHeader3>
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
                                            dangerouslySetInnerHTML={{
                                                __html: `&#x${producto.sIcon};&nbsp;&nbsp;&nbsp;`,
                                            }}
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
                        <MeditocModalBotones
                            okMessage="LIMPIAR FILTRO"
                            hideCancel
                            okFunc={handleClickActualizarTabla}
                        />
                        <Grid item xs={12}>
                            <MeditocTable
                                columns={columns}
                                data={listaFoliosEmpresa}
                                setData={setListaFoliosEmpresa}
                                rowsSelected={foliosEmpresaSeleccionado}
                                setRowsSelected={setFoliosEmpresaSeleccionado}
                                selection={true}
                                mainField="iIdFolio"
                            />
                        </Grid>
                    </Grid>
                </MeditocBody>
            </div>
            <CrearFolios
                entEmpresa={entEmpresa}
                open={modalAgregarFoliosOpen}
                setOpen={setModalAgregarFoliosOpen}
                listaProductos={listaProductos}
                funcGetFoliosEmpresa={funcGetFoliosEmpresa}
                usuarioSesion={usuarioSesion}
                funcLoader={funcLoader}
                funcAlert={funcAlert}
            />
            <ModificarVigencia
                entEmpresa={entEmpresa}
                open={modalAgregarVigenciaOpen}
                setOpen={setModalAgregarVigenciaOpen}
                foliosEmpresaSeleccionado={foliosEmpresaSeleccionado}
                funcGetFoliosEmpresa={funcGetFoliosEmpresa}
                usuarioSesion={usuarioSesion}
                funcLoader={funcLoader}
                funcAlert={funcAlert}
            />
            <FormCargarArchivo
                entEmpresa={entEmpresa}
                listaProductos={listaProductos}
                open={formSubirArchivoOpen}
                setOpen={setFormSubirArchivoOpen}
                funcGetFoliosEmpresa={funcGetFoliosEmpresa}
                usuarioSesion={usuarioSesion}
                funcLoader={funcLoader}
                funcAlert={funcAlert}
            />
            <MeditocConfirmacion
                title="Eliminar folios"
                open={modalEliminarFoliosOpen}
                setOpen={setModalEliminarFoliosOpen}
                okFunc={funcEliminarFolios}
            >
                ¿Desea eliminar los folios seleccionados?
            </MeditocConfirmacion>
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
        </MeditocFullModal>
    );
};

AdministrarFolios.propTypes = {
    entEmpresa: PropTypes.shape({
        iIdEmpresa: PropTypes.any,
        sFolioEmpresa: PropTypes.any,
        sNombre: PropTypes.any,
    }),
    funcAlert: PropTypes.func,
    funcLoader: PropTypes.func,
    listaProductos: PropTypes.any,
    open: PropTypes.bool,
    setOpen: PropTypes.any,
    usuarioSesion: PropTypes.shape({
        iIdUsuario: PropTypes.any,
    }),
};

export default AdministrarFolios;
