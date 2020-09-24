import React, { Fragment } from "react";
import MeditocHeader1 from "../../../utilidades/MeditocHeader1";
import MeditocBody from "../../../utilidades/MeditocBody";
import { Tooltip, IconButton } from "@material-ui/core";
import AddRoundedIcon from "@material-ui/icons/AddRounded";
import EditIcon from "@material-ui/icons/Edit";
import DeleteIcon from "@material-ui/icons/Delete";
import { useState } from "react";
import MeditocTable from "../../../utilidades/MeditocTable";
import FormCupon from "./FormCupon";
import MeditocConfirmacion from "../../../utilidades/MeditocConfirmacion";
import PromocionesController from "../../../../controllers/PromocionesController";
import { useEffect } from "react";
import { EnumCuponCategoria } from "../../../../configurations/enumConfig";

const Cupones = (props) => {
    const { usuarioSesion, funcLoader, funcAlert } = props;

    const promocionesController = new PromocionesController();

    const columns = [
        { title: "ID", field: "fiIdCupon", align: "center", hidden: true },
        { title: "Código", field: "fsCodigo", align: "center" },
        { title: "Monto descuento", field: "sMontoDescuento", align: "center" },
        { title: "Porcentaje descuento", field: "sPorcentajeDescuento", align: "center" },
        { title: "Total", field: "fiTotalLanzamiento", align: "center" },
        { title: "Canjeado", field: "fiTotalCanjeado", align: "center" },
        { title: "Creado", field: "sFechaCreacion", align: "center" },
        { title: "Vencimiento", field: "sFechaVencimiento", align: "center" },
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

    const [listaCupones, setListaCupones] = useState([]);
    const [cuponSeleccionado, setCuponSeleccionado] = useState(cuponEntidadVacia);
    const [cuponParaModalForm, setCuponParaModalForm] = useState(cuponEntidadVacia);

    const [modalFormCuponOpen, setModalFormCuponOpen] = useState(false);
    const [modalEliminarCuponOpen, setModalEliminarCuponOpen] = useState(false);

    const handleClickCrearCupon = () => {
        setCuponParaModalForm(cuponEntidadVacia);
        setModalFormCuponOpen(true);
    };

    const handleEditarCupon = () => {
        if (cuponSeleccionado.fiIdCupon === 0) {
            funcAlert("Seleccione un cupón para continuar");
            return;
        }
        setCuponParaModalForm(cuponSeleccionado);
        setModalFormCuponOpen(true);
    };

    const handleClickEliminarCupon = () => {
        if (cuponSeleccionado.fiIdCupon === 0) {
            funcAlert("Seleccione un cupón para continuar");
            return;
        }
        setModalEliminarCuponOpen(true);
    };

    const funcObtenerCupones = async () => {
        funcLoader(true, "Consultando cupones");

        const response = await promocionesController.funcObtenerCupones();

        if (response.Code === 0) {
            setListaCupones(response.Result);
        } else {
            funcAlert(response.Message);
        }

        funcLoader();
    };

    const funcDesactivarCupon = async () => {
        funcLoader(true, "Desactivando cupón..");

        const response = await promocionesController.funcDesactivarCupon(
            cuponSeleccionado.fiIdCupon,
            usuarioSesion.iIdUsuario
        );

        if (response.Code === 0) {
            setModalEliminarCuponOpen(false);
            await funcObtenerCupones();
            setCuponSeleccionado(cuponEntidadVacia);
            funcAlert(response.Message, "success");
        } else {
            funcAlert(response.Message);
        }

        funcLoader();
    };

    useEffect(() => {
        funcObtenerCupones();
    }, []);

    return (
        <Fragment>
            <MeditocHeader1 title="CUPONES">
                <Tooltip title="Crear un cupón" arrow>
                    <IconButton onClick={handleClickCrearCupon}>
                        <AddRoundedIcon className="color-0" />
                    </IconButton>
                </Tooltip>
                {/* <Tooltip title="Editar cupón" arrow>
                    <IconButton onClick={handleEditarCupon}>
                        <EditIcon className="color-0" />
                    </IconButton>
                </Tooltip> */}
                <Tooltip title="Eliminar cupón" arrow>
                    <IconButton onClick={handleClickEliminarCupon}>
                        <DeleteIcon className="color-0" />
                    </IconButton>
                </Tooltip>
            </MeditocHeader1>
            <MeditocBody>
                <MeditocTable
                    columns={columns}
                    data={listaCupones}
                    rowSelected={cuponSeleccionado}
                    setRowSelected={setCuponSeleccionado}
                    mainField="fiIdCupon"
                />
            </MeditocBody>
            <FormCupon
                entCupon={cuponParaModalForm}
                open={modalFormCuponOpen}
                setOpen={setModalFormCuponOpen}
                funcObtenerCupones={funcObtenerCupones}
                usuarioSesion={usuarioSesion}
                funcLoader={funcLoader}
                funcAlert={funcAlert}
            />
            <MeditocConfirmacion
                title="Eliminar cupón"
                open={modalEliminarCuponOpen}
                setOpen={setModalEliminarCuponOpen}
                okFunc={funcDesactivarCupon}
            >
                ¿Desea eliminar el cupón con código {cuponSeleccionado.fsCodigo}?
            </MeditocConfirmacion>
        </Fragment>
    );
};

export default Cupones;
