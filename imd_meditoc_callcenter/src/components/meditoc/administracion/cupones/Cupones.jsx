import { Grid, IconButton, MenuItem, TextField, Tooltip } from "@material-ui/core";
import React, { Fragment } from "react";
import { green, red } from "@material-ui/core/colors";

import AddRoundedIcon from "@material-ui/icons/AddRounded";
import DeleteIcon from "@material-ui/icons/Delete";
import DetalleCupon from "./DetalleCupon";
import { EnumCuponCategoria } from "../../../../configurations/enumConfig";
import FormCupon from "./FormCupon";
import MeditocBody from "../../../utilidades/MeditocBody";
import MeditocConfirmacion from "../../../utilidades/MeditocConfirmacion";
import MeditocHeader1 from "../../../utilidades/MeditocHeader1";
import MeditocHelper from "../../../utilidades/MeditocHelper";
import MeditocModalBotones from "../../../utilidades/MeditocModalBotones";
import MeditocSubtitulo from "../../../utilidades/MeditocSubtitulo";
import MeditocTable from "../../../utilidades/MeditocTable";
import PromocionesController from "../../../../controllers/PromocionesController";
import PropTypes from "prop-types";
import RedeemIcon from "@material-ui/icons/Redeem";
import ReplayIcon from "@material-ui/icons/Replay";
import VisibilityIcon from "@material-ui/icons/Visibility";
import { cellProps } from "../../../../configurations/dataTableIconsConfig";
import { emptyFunc } from "../../../../configurations/preventConfig";
import { useEffect } from "react";
import { useState } from "react";

/*************************************************************
 * Descripcion: Contenido de la vista principal de Cupones
 * Creado: Cristopher Noh
 * Fecha: 26/08/2020
 * Invocado desde: ContentMain
 *************************************************************/
const Cupones = (props) => {
    const { usuarioSesion, permisos, entCatalogos, funcLoader, funcAlert } = props;

    //CONSTANTES
    const columns = [
        { title: "ID", field: "fiIdCupon", ...cellProps, hidden: true },
        { title: "", field: "sStatus", ...cellProps, sorting: false },
        { title: "Código", field: "fsCodigo", ...cellProps },
        { title: "Monto descuento", field: "sMontoDescuento", ...cellProps },
        { title: "Porcentaje descuento", field: "sPorcentajeDescuento", ...cellProps },
        { title: "Total", field: "fiTotalLanzamiento", ...cellProps },
        { title: "Canjeado", field: "fiTotalCanjeado", ...cellProps },
        { title: "Creado", field: "sFechaCreacion", ...cellProps },
        { title: "Vencimiento", field: "sFechaVencimiento", ...cellProps },
    ];

    const cuponEntidadVacia = {
        fiIdCupon: 0,
        fiIdCuponCategoria: EnumCuponCategoria.DescuentoMonto,
        fsDescripcion: "",
        fsCodigo: "",
        fiLongitudCodigo: 0,
        fnMontoDescuento: 0,
        fnPorcentajeDescuento: 0,
        fiTotalLanzamiento: 0,
        fiDiasActivo: 0,
    };

    const filtrosVacios = {
        txtTipoCupon: "",
        txtVigente: "",
    };

    //CONTROLLERS
    const promocionesController = new PromocionesController();

    //STATES
    const [formFiltroCupon, setFormFiltroCupon] = useState(filtrosVacios);

    const [listaCupones, setListaCupones] = useState([]);
    const [cuponSeleccionado, setCuponSeleccionado] = useState(cuponEntidadVacia);
    const [cuponCrearEditar, setCuponEditarCrear] = useState(cuponEntidadVacia);

    const [openFormCupon, setOpenFormCupon] = useState(false);
    const [openEliminarCupon, setOpenEliminarCupon] = useState(false);
    const [openDetalleCupon, setOpenDetalleCupon] = useState(false);

    //HANDLERS
    //Capturar cuandoun filtro es modificado
    const handleChangeFiltroCupon = (e) => {
        setFormFiltroCupon({
            ...formFiltroCupon,
            [e.target.name]: e.target.value,
        });
    };

    //Crear un cupon nuevo
    const handleClickCrearCupon = () => {
        setCuponEditarCrear(cuponEntidadVacia);
        setOpenFormCupon(true);
    };
    const handleClickDetalleCupon = () => {
        if (cuponSeleccionado.fiIdCupon === 0) {
            funcAlert("Seleccione un cupón para continuar");
            return;
        }
        setOpenDetalleCupon(true);
    };

    //Eliminar un cupon seleccionado
    const handleClickEliminarCupon = () => {
        if (cuponSeleccionado.fiIdCupon === 0) {
            funcAlert("Seleccione un cupón para continuar");
            return;
        }
        setOpenEliminarCupon(true);
    };

    //Actualizar los datos de la tabla
    const handleClickActualizarTabla = () => {
        fnObtenerCupones(true);
    };

    //FUNCIONES
    //Consumir API para obtener la lista de cupones
    const fnObtenerCupones = async (clean = false) => {
        funcLoader(true, "Consultando cupones");

        let response;
        if (clean === false) {
            response = await promocionesController.funcObtenerCupones(
                "",
                formFiltroCupon.txtTipoCupon,
                "",
                "",
                "",
                "",
                "",
                "",
                true,
                false,
                formFiltroCupon.txtVigente
            );
        } else {
            response = await promocionesController.funcObtenerCupones();
            setFormFiltroCupon(filtrosVacios);
        }

        if (response.Code === 0) {
            let resListaCupones = response.Result.map((cupon) => ({
                ...cupon,
                sStatus: (
                    <span>
                        <RedeemIcon
                            style={{ color: cupon.fbVigente === true ? green[500] : red[500], verticalAlign: "middle" }}
                        />
                    </span>
                ),
            }));
            setListaCupones(resListaCupones);
            //setListaCuponesFilter(resListaCupones);
        } else {
            funcAlert(response.Message);
        }

        funcLoader();
    };

    //Consumir API para eliminar un cupon
    const fnEliminarCupon = async () => {
        funcLoader(true, "Desactivando cupón..");

        const response = await promocionesController.funcDesactivarCupon(
            cuponSeleccionado.fiIdCupon,
            usuarioSesion.iIdUsuario
        );

        if (response.Code === 0) {
            setOpenEliminarCupon(false);
            await fnObtenerCupones();
            setCuponSeleccionado(cuponEntidadVacia);
            funcAlert(response.Message, "success");
        } else {
            funcAlert(response.Message);
        }

        funcLoader();
    };

    //EFFECTS

    //OBtener los cupones al cargar la vista
    useEffect(() => {
        fnObtenerCupones();
        // eslint-disable-next-line
    }, [formFiltroCupon]);

    return (
        <Fragment>
            <MeditocHeader1 title={permisos.Nombre}>
                {permisos.Botones["1"] !== undefined && ( //Crear un cupón
                    <Tooltip title={permisos.Botones["1"].Nombre} arrow>
                        <IconButton onClick={handleClickCrearCupon}>
                            <AddRoundedIcon className="color-0" />
                        </IconButton>
                    </Tooltip>
                )}
                {permisos.Botones["2"] !== undefined && ( //Detalle de cupón
                    <Tooltip title={permisos.Botones["2"].Nombre} arrow>
                        <IconButton onClick={handleClickDetalleCupon}>
                            <VisibilityIcon className="color-0" />
                        </IconButton>
                    </Tooltip>
                )}
                {permisos.Botones["3"] !== undefined && ( //Eliminar cupón
                    <Tooltip title={permisos.Botones["3"].Nombre} arrow>
                        <IconButton onClick={handleClickEliminarCupon}>
                            <DeleteIcon className="color-0" />
                        </IconButton>
                    </Tooltip>
                )}
                {permisos.Botones["4"] !== undefined && ( //Actualizar tabla
                    <Tooltip title={permisos.Botones["4"].Nombre} arrow>
                        <IconButton onClick={handleClickActualizarTabla}>
                            <ReplayIcon className="color-0" />
                        </IconButton>
                    </Tooltip>
                )}
            </MeditocHeader1>
            <MeditocBody>
                <Grid container spacing={3}>
                    <Grid item xs={12}>
                        <MeditocSubtitulo title="FILTRAR CUPONES" />
                    </Grid>
                    <Grid item sm={6} xs={12}>
                        <TextField
                            name="txtTipoCupon"
                            label="Tipo de descuento del cupón:"
                            variant="outlined"
                            select
                            fullWidth
                            value={formFiltroCupon.txtTipoCupon}
                            onChange={handleChangeFiltroCupon}
                        >
                            <MenuItem value="">Todos los cupones</MenuItem>
                            {entCatalogos.catCuponCategoria.map((cupon) => (
                                <MenuItem key={cupon.fiId} value={cupon.fiId}>
                                    {cupon.fsDescripcion}
                                </MenuItem>
                            ))}
                        </TextField>
                    </Grid>
                    <Grid item sm={6} xs={12}>
                        <TextField
                            name="txtVigente"
                            label="Estatus del cupón:"
                            variant="outlined"
                            select
                            fullWidth
                            value={formFiltroCupon.txtVigente}
                            onChange={handleChangeFiltroCupon}
                        >
                            <MenuItem value="">Todos los cupones</MenuItem>
                            <MenuItem value="true">Cupón activo</MenuItem>
                            <MenuItem value="false">Cupón inactivo/vencido</MenuItem>
                        </TextField>
                    </Grid>
                    <MeditocModalBotones okMessage="LIMPIAR FILTRO" hideCancel okFunc={handleClickActualizarTabla} />
                    <Grid item xs={12}>
                        <MeditocTable
                            columns={columns}
                            data={listaCupones}
                            rowSelected={cuponSeleccionado}
                            setRowSelected={setCuponSeleccionado}
                            mainField="fiIdCupon"
                            doubleClick={permisos.Botones["2"] !== undefined ? handleClickDetalleCupon : emptyFunc}
                        />
                    </Grid>
                </Grid>
            </MeditocBody>
            <FormCupon
                entCupon={cuponCrearEditar}
                open={openFormCupon}
                setOpen={setOpenFormCupon}
                funcObtenerCupones={fnObtenerCupones}
                usuarioSesion={usuarioSesion}
                funcLoader={funcLoader}
                funcAlert={funcAlert}
            />
            <DetalleCupon entCupon={cuponSeleccionado} open={openDetalleCupon} setOpen={setOpenDetalleCupon} />
            <MeditocConfirmacion
                title="Eliminar cupón"
                open={openEliminarCupon}
                setOpen={setOpenEliminarCupon}
                okFunc={fnEliminarCupon}
            >
                ¿Desea eliminar el cupón con código {cuponSeleccionado.fsCodigo}?
            </MeditocConfirmacion>
            <MeditocHelper title="Estatus de cupón:">
                <div>
                    <RedeemIcon style={{ color: green[500], verticalAlign: "middle" }} />
                    {"  "}
                    Cupón activo
                </div>
                <div>
                    <RedeemIcon style={{ color: red[500], verticalAlign: "middle" }} />
                    {"  "}
                    Cupón inactivo
                </div>
            </MeditocHelper>
        </Fragment>
    );
};

Cupones.propTypes = {
    entCatalogos: PropTypes.shape({
        catCuponCategoria: PropTypes.array,
    }),
    funcAlert: PropTypes.func,
    funcLoader: PropTypes.func,
    permisos: PropTypes.shape({
        Botones: PropTypes.object,
        Nombre: PropTypes.string,
    }),
    usuarioSesion: PropTypes.shape({
        iIdUsuario: PropTypes.number,
    }),
};

export default Cupones;
