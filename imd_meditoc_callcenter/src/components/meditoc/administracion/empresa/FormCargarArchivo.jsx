import {
    Button,
    Grid,
    IconButton,
    InputAdornment,
    MenuItem,
    Table,
    TableBody,
    TableCell,
    TableContainer,
    TableHead,
    TableRow,
    TextField,
    Tooltip,
    makeStyles,
} from "@material-ui/core";
import React, { Fragment, useEffect, useState } from "react";

import AssignmentTurnedInIcon from "@material-ui/icons/AssignmentTurnedIn";
import AttachFileIcon from "@material-ui/icons/AttachFile";
import DeleteIcon from "@material-ui/icons/Delete";
import DetalleArchivoCargado from "./DetalleArchivoCargado";
import FolioController from "../../../../controllers/FolioController";
import MeditocModal from "../../../utilidades/MeditocModal";
import MeditocModalBotones from "../../../utilidades/MeditocModalBotones";
import PropTypes from "prop-types";
import theme from "../../../../configurations/themeConfig";

const useStyles = makeStyles({
    root: {
        backgroundColor: theme.palette.primary.main,
        color: "#fff",
    },
});

const FormCargarArchivo = (props) => {
    const {
        entEmpresa,
        listaProductos,
        open,
        setOpen,
        funcGetFoliosEmpresa,
        usuarioSesion,
        funcLoader,
        funcAlert,
    } = props;

    const classes = useStyles();

    const folioController = new FolioController();

    const [archivoCargado, setArchivoCargado] = useState(null);
    const [formCargarArchivo, setFormCargarArchivo] = useState({
        txtProducto: "",
        txtArchivoFolios: "",
    });

    const validacionFormulario = {
        txtProducto: true,
        txtArchivoFolios: true,
    };

    const [formCargarArchivoOK, setFormCargarArchivoOK] = useState(validacionFormulario);

    const [entArchivoVerificado, setEntArchivoVerificado] = useState(null);
    const [modalDetalleArchivoVerificado, setModalDetalleArchivoVerificado] = useState(false);

    const handleChangeProducto = (e) => {
        const valorCampo = e.target.value;
        setFormCargarArchivo({ ...formCargarArchivo, txtProducto: valorCampo });
        if (valorCampo !== "") {
            setFormCargarArchivoOK({ ...formCargarArchivoOK, txtProducto: true });
        }
        setEntArchivoVerificado(null);
    };

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
            setEntArchivoVerificado(null);
            //setArchivoVerificadoDetalleFolios(archivoVeridicadoEntidadVacia);
            setFormCargarArchivo({ ...formCargarArchivo, txtArchivoFolios: archivo.name });
            setFormCargarArchivoOK({ ...formCargarArchivoOK, txtArchivoFolios: true });
        });
        input.click();
        input.remove();
    };

    const handleClickVerDetalleArchivoVerificado = () => {
        setModalDetalleArchivoVerificado(true);
    };

    const handleClickLimpiarArchivo = () => {
        setArchivoCargado(null);
        setEntArchivoVerificado(null);
        //setArchivoVerificadoDetalleFolios(archivoVeridicadoEntidadVacia);
        setFormCargarArchivo({ ...formCargarArchivo, txtArchivoFolios: "" });
    };

    const handleClickVerificarArchivoFolios = async () => {
        let formCargarArchivoOKValidacion = { ...validacionFormulario };

        let formError = false;

        if (formCargarArchivo.txtProducto === "") {
            formCargarArchivoOKValidacion.txtProducto = false;
            formError = true;
        }

        if (formCargarArchivo.txtArchivoFolios === "" || archivoCargado === null) {
            formCargarArchivoOKValidacion.txtArchivoFolios = false;
            formError = true;
        }

        setFormCargarArchivoOK(formCargarArchivoOKValidacion);
        if (formError) {
            return;
        }

        funcLoader(true, "Verificando archivo...");

        const response = await folioController.funcVerificarArchivo(
            entEmpresa.iIdEmpresa,
            formCargarArchivo.txtProducto,
            archivoCargado
        );

        if (response.Code === 0) {
            setEntArchivoVerificado(response.Result);
            funcAlert(response.Message, "success");
        } else {
            funcAlert(response.Message);
        }
        funcLoader();
    };

    const handleClickSubirArchivo = async () => {
        let formCargarArchivoOKValidacion = { ...validacionFormulario };

        let formError = false;

        if (formCargarArchivo.txtProducto === "") {
            formCargarArchivoOKValidacion.txtProducto = false;
            formError = true;
        }

        if (formCargarArchivo.txtArchivoFolios === "" || archivoCargado === null) {
            formCargarArchivoOKValidacion.txtArchivoFolios = false;
            formError = true;
        }

        setFormCargarArchivoOK(formCargarArchivoOKValidacion);
        if (formError) {
            return;
        }

        funcLoader(true, "Cargando folios desde archivo...");

        const response = await folioController.funcGenerarFolioArchivo(
            entEmpresa.iIdEmpresa,
            formCargarArchivo.txtProducto,
            usuarioSesion.iIdUsuario,
            archivoCargado
        );

        if (response.Code === 0) {
            setFormCargarArchivo({
                txtProducto: "",
                txtArchivoFolios: "",
            });
            setArchivoCargado(null);
            setEntArchivoVerificado(null);
            setOpen(false);
            await funcGetFoliosEmpresa();
            funcAlert(response.Message, "success");
        } else {
            funcAlert(response.Message);
        }
        funcLoader();
    };

    useEffect(() => {
        if (open === true) {
            setFormCargarArchivo({
                txtProducto: "",
                txtArchivoFolios: "",
            });
            setArchivoCargado(null);
            setEntArchivoVerificado(null);
        }
        // eslint-disable-next-line
    }, [open]);

    return (
        <MeditocModal size="small" title="Generar folios desde archivo" open={open} setOpen={setOpen}>
            <Grid container spacing={3}>
                <Grid item xs={12}>
                    Seleccione el archivo .xls o .xlsx que contiene los nuevos folios y verifique el archivo cargado
                </Grid>
                <Grid item xs={12}>
                    <TextField
                        name="txtProducto"
                        label="Producto de los folios:"
                        variant="outlined"
                        fullWidth
                        required
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
                        value={formCargarArchivo.txtProducto}
                        onChange={handleChangeProducto}
                        error={!formCargarArchivoOK.txtProducto}
                        helperText={!formCargarArchivoOK.txtProducto ? "Seleccione el producto para continuar" : ""}
                    >
                        <MenuItem value={""} hidden>
                            Seleccionar producto
                        </MenuItem>
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
                                                <IconButton onClick={handleClickVerificarArchivoFolios}>
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
                        value={formCargarArchivo.txtArchivoFolios}
                        error={!formCargarArchivoOK.txtArchivoFolios}
                        helperText={!formCargarArchivoOK.txtArchivoFolios ? "No se ha seleccionado ningún archivo" : ""}
                    />
                </Grid>
                {entArchivoVerificado !== null && (
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
                                    <TableRow>
                                        <TableCell>{entArchivoVerificado.entProducto.sNombre}</TableCell>
                                        <TableCell align="center">{entArchivoVerificado.totalFolios}</TableCell>
                                        <TableCell align="right">
                                            <Button
                                                variant="contained"
                                                color="primary"
                                                onClick={handleClickVerDetalleArchivoVerificado}
                                            >
                                                Detalle
                                            </Button>
                                        </TableCell>
                                    </TableRow>
                                </TableBody>
                            </Table>
                        </TableContainer>
                    </Grid>
                )}

                <MeditocModalBotones
                    okMessage="Cargar folios"
                    okFunc={handleClickSubirArchivo}
                    okDisabled={entArchivoVerificado === null}
                    setOpen={setOpen}
                />
            </Grid>
            {entArchivoVerificado !== null && (
                <DetalleArchivoCargado
                    entArchivoVerificado={entArchivoVerificado}
                    open={modalDetalleArchivoVerificado}
                    setOpen={setModalDetalleArchivoVerificado}
                />
            )}
        </MeditocModal>
    );
};

FormCargarArchivo.propTypes = {
    entEmpresa: PropTypes.shape({
        iIdEmpresa: PropTypes.any,
    }),
    funcAlert: PropTypes.func,
    funcGetFoliosEmpresa: PropTypes.func,
    funcLoader: PropTypes.func,
    listaProductos: PropTypes.shape({
        map: PropTypes.func,
    }),
    open: PropTypes.bool,
    setOpen: PropTypes.func,
    usuarioSesion: PropTypes.shape({
        iIdUsuario: PropTypes.any,
    }),
};

export default FormCargarArchivo;
