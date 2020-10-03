import PropTypes from "prop-types";
import {
    Button,
    Grid,
    IconButton,
    InputAdornment,
    Table,
    TableBody,
    TableCell,
    TableContainer,
    TableHead,
    TableRow,
    TextField,
    Tooltip,
} from "@material-ui/core";
import React, { Fragment, useState } from "react";
import MeditocModal from "../../utilidades/MeditocModal";
import AttachFileIcon from "@material-ui/icons/AttachFile";
import DeleteIcon from "@material-ui/icons/Delete";
import MeditocModalBotones from "../../utilidades/MeditocModalBotones";
import FolioController from "../../../controllers/FolioController";
import AssignmentTurnedInIcon from "@material-ui/icons/AssignmentTurnedIn";
import FolioValidadoDetalle from "./FolioValidadoDetalle";

const FolioCargarArchivo = (props) => {
    const { open, setOpen, funcGetFolios, usuarioSesion, funcLoader, funcAlert } = props;

    const folioController = new FolioController();

    const [archivoCargado, setArchivoCargado] = useState(null);
    const [formCargarFolio, setFormCargarFolio] = useState({
        txtFolioEmpresa: "",
        txtArchivoFolios: "",
    });

    const validacionFormulario = {
        txtFolioEmpresa: true,
        txtArchivoFolios: true,
    };

    const [formCargarFolioOK, setFormCargarFolioOK] = useState(validacionFormulario);

    const [archivoVerificado, setArchivoVerificado] = useState(null);
    const archivoVeridicadoEntidadVacia = {
        producto: "",
        totalFolios: 0,
        lstFolios: [],
    };
    const [archivoVerificadoDetalleFolios, setArchivoVerificadoDetalleFolios] = useState(archivoVeridicadoEntidadVacia);

    const [modalDetalleFolioValidadoOpen, setModalDetalleFolioValidadoOpen] = useState(false);

    const handleClickCargarArchivo = () => {
        let input = document.createElement("input");
        input.type = "file";
        input.value = "";
        input.addEventListener("change", async () => {
            let archivo = input.files[0];
            if (!archivo) {
                funcAlert("No se cargó ningún archivo");
                return;
            }

            if (
                archivo.type !== "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" &&
                archivo.type !== "application/vnd.ms-excel"
            ) {
                funcAlert("El formato del archivo debe ser .xls o .xlsx");
                return;
            }

            setArchivoCargado(archivo);
            setArchivoVerificado(null);
            setArchivoVerificadoDetalleFolios(archivoVeridicadoEntidadVacia);
            setFormCargarFolio({ ...formCargarFolio, txtArchivoFolios: archivo.name });
            setFormCargarFolioOK({ ...formCargarFolioOK, txtArchivoFolios: true });
        });
        input.click();
        input.remove();
    };

    const handleClickLimpiarArchivo = () => {
        setArchivoCargado(null);
        setArchivoVerificado(null);
        setArchivoVerificadoDetalleFolios(archivoVeridicadoEntidadVacia);
        setFormCargarFolio({ ...formCargarFolio, txtArchivoFolios: "" });
    };

    const handleChangeFolioEmpresa = (e) => {
        const valorCampo = e.target.value;
        setFormCargarFolio({ ...formCargarFolio, txtFolioEmpresa: valorCampo });
        if (valorCampo !== "") {
            setFormCargarFolioOK({ ...formCargarFolioOK, txtFolioEmpresa: true });
        }
    };

    const handleClickVerificarArchivo = async () => {
        if (formCargarFolio.txtArchivoFolios === "" || archivoCargado === null) {
            return;
        }

        funcLoader(true, "Verificando folios...");

        const response = await folioController.funcVerificarVentaCalle(archivoCargado);

        if (response.Code === 0) {
            setArchivoVerificado(response.Result);
            funcAlert(response.Message, "success");
        } else {
            funcAlert(response.Message);
        }
        funcLoader();
    };

    const handleClickVerDetalleFoliosVerificados = (producto) => {
        setArchivoVerificadoDetalleFolios(producto);
        setModalDetalleFolioValidadoOpen(true);
    };

    const handleClickSubirArchivo = async () => {
        let formCargarFolioOKValidacion = { ...validacionFormulario };

        let formError = false;

        if (formCargarFolio.txtFolioEmpresa === "") {
            formCargarFolioOKValidacion.txtFolioEmpresa = false;
            formError = true;
        }

        if (formCargarFolio.txtArchivoFolios === "" || archivoCargado === null) {
            formCargarFolioOKValidacion.txtArchivoFolios = false;
            formError = true;
        }

        setFormCargarFolioOK(formCargarFolioOKValidacion);
        if (formError) {
            return;
        }

        funcLoader(true, "Guardando folios...");

        const response = await folioController.funcSaveVentaCalle(
            usuarioSesion.iIdUsuario,
            formCargarFolio.txtFolioEmpresa,
            archivoCargado
        );

        if (response.Code === 0) {
            setFormCargarFolio({
                txtFolioEmpresa: "",
                txtArchivoFolios: "",
            });
            setArchivoCargado(null);
            setArchivoVerificado(null);
            setArchivoVerificadoDetalleFolios(archivoVeridicadoEntidadVacia);
            setOpen(false);
            await funcGetFolios();
            funcAlert(response.Message, "success");
        } else {
            funcAlert(response.Message);
        }
        funcLoader();
    };

    return (
        <MeditocModal size="small" title="Generar folios desde archivo" open={open} setOpen={setOpen}>
            <Grid container spacing={3}>
                <Grid item xs={12}>
                    Seleccione el archivo .xls o .xlsx que contiene los nuevos folios y verifique el archivo cargado
                </Grid>
                <Grid item xs={12}>
                    <TextField
                        name="txtFolioEmpresa"
                        label="Folio de empresa contenedora:"
                        variant="outlined"
                        fullWidth
                        required
                        value={formCargarFolio.txtFolioEmpresa}
                        onChange={handleChangeFolioEmpresa}
                        error={!formCargarFolioOK.txtFolioEmpresa}
                        helperText={!formCargarFolioOK.txtFolioEmpresa ? "El folio de la empresa es requerido" : ""}
                    />
                </Grid>
                <Grid item xs={12}>
                    <TextField
                        name="txtArchivoFolios"
                        label="Archivo:"
                        variant="outlined"
                        fullWidth
                        InputProps={{
                            readOnly: true,
                            endAdornment: (
                                <InputAdornment position="end">
                                    <Tooltip title="Seleccionar archivo" arrow placement="top">
                                        <IconButton onClick={handleClickCargarArchivo}>
                                            <AttachFileIcon />
                                        </IconButton>
                                    </Tooltip>
                                    {archivoCargado !== null && (
                                        <Fragment>
                                            <Tooltip title="Verificar folios del archivo" arrow placement="top">
                                                <IconButton onClick={handleClickVerificarArchivo}>
                                                    <AssignmentTurnedInIcon className="color-1" />
                                                </IconButton>
                                            </Tooltip>
                                            <Tooltip title="Limpiar campo" arrow placement="top">
                                                <IconButton onClick={handleClickLimpiarArchivo}>
                                                    <DeleteIcon />
                                                </IconButton>
                                            </Tooltip>
                                        </Fragment>
                                    )}
                                </InputAdornment>
                            ),
                        }}
                        required
                        value={formCargarFolio.txtArchivoFolios}
                        error={!formCargarFolioOK.txtArchivoFolios}
                        helperText={!formCargarFolioOK.txtArchivoFolios ? "No se ha seleccionado ningún archivo" : ""}
                    />
                </Grid>
                {archivoVerificado !== null && (
                    <Grid item xs={12}>
                        <TableContainer>
                            <Table>
                                <TableHead>
                                    <TableRow>
                                        <TableCell>Producto</TableCell>
                                        <TableCell align="center">Folios</TableCell>
                                        <TableCell align="center">Detalle</TableCell>
                                    </TableRow>
                                </TableHead>
                                <TableBody>
                                    {archivoVerificado.map((producto) => (
                                        <TableRow key={producto.producto}>
                                            <TableCell>{producto.producto}</TableCell>
                                            <TableCell align="center">{producto.totalFolios}</TableCell>
                                            <TableCell align="right">
                                                <Button
                                                    variant="contained"
                                                    color="primary"
                                                    onClick={() => {
                                                        handleClickVerDetalleFoliosVerificados(producto);
                                                    }}
                                                >
                                                    Detalle
                                                </Button>
                                            </TableCell>
                                        </TableRow>
                                    ))}
                                </TableBody>
                            </Table>
                        </TableContainer>
                    </Grid>
                )}

                <MeditocModalBotones
                    okMessage="Cargar folios"
                    okFunc={handleClickSubirArchivo}
                    okDisabled={archivoVerificado === null}
                    setOpen={setOpen}
                />
            </Grid>
            <FolioValidadoDetalle
                entVentaCalle={archivoVerificadoDetalleFolios}
                open={modalDetalleFolioValidadoOpen}
                setOpen={setModalDetalleFolioValidadoOpen}
            />
        </MeditocModal>
    );
};

FolioCargarArchivo.propTypes = {
    funcAlert: PropTypes.func,
    funcGetFolios: PropTypes.func,
    funcLoader: PropTypes.func,
    open: PropTypes.any,
    setOpen: PropTypes.func,
    usuarioSesion: PropTypes.shape({
        iIdUsuario: PropTypes.any,
    }),
};

export default FolioCargarArchivo;
