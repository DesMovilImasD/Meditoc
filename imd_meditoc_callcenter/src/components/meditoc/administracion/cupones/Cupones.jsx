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

const Cupones = (props) => {
    const { funcAlert } = props;

    const columns = [
        { title: "ID", field: "iIdCupon", align: "center" },
        { title: "Código", field: "sCodigo", align: "center" },
        { title: "Monto descuento", field: "nMontoDescuento", align: "center" },
        { title: "Porcentaje descuento", field: "nPorcentajeDescuento", align: "center" },
        { title: "Total", field: "iTotalLanzamiento", align: "center" },
        { title: "Canjeado", field: "iTotalCanjeado", align: "center" },
        { title: "Vencimiento", field: "dtFechaVencimiento", align: "center" },
    ];

    const cuponEntidadVacia = {
        iIdCupon: 0,
        iIdCuponCategoria: 0,
        sDescripcion: "",
        sCodigo: "",
        nMontoDescuento: 0,
        nPorcentajeDescuento: 0,
        iTotalLanzamiento: 0,
        iTotalCanjeado: 0,
        dtFechaVencimiento: "",
    };

    const data = [
        {
            iIdCupon: 1,
            iIdCuponCategoria: 1,
            sDescripcion: "a",
            sCodigo: "a",
            nMontoDescuento: 1,
            nPorcentajeDescuento: 1,
            iTotalLanzamiento: 1,
            iTotalCanjeado: 1,
            dtFechaVencimiento: "",
        },
    ];

    const [listaCupones, setListaCupones] = useState(data);
    const [cuponSeleccionado, setCuponSeleccionado] = useState(cuponEntidadVacia);

    const [modalFormCuponOpen, setModalFormCuponOpen] = useState(false);
    const [modalEliminarCuponOpen, setModalEliminarCuponOpen] = useState(false);

    const handleClickCrearCupon = () => {
        setModalFormCuponOpen(true);
    };

    const handleClickEliminarCupon = () => {
        if (cuponSeleccionado.iIdCupon === 0) {
            funcAlert("Seleccione un cupón para continuar");
            return;
        }
        setModalEliminarCuponOpen(true);
    };

    return (
        <Fragment>
            <MeditocHeader1 title="CUPONES">
                <Tooltip title="Crear un cupón" arrow>
                    <IconButton onClick={handleClickCrearCupon}>
                        <AddRoundedIcon className="color-0" />
                    </IconButton>
                </Tooltip>
                {/* <Tooltip title="Editar cupón" arrow>
                    <IconButton>
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
                    mainField="iIdCupon"
                />
            </MeditocBody>
            <FormCupon open={modalFormCuponOpen} setOpen={setModalFormCuponOpen} />
            <MeditocConfirmacion
                title="Eliminar cupón"
                open={modalEliminarCuponOpen}
                setOpen={setModalEliminarCuponOpen}
            >
                ¿Desea eliminar el cupón con código {cuponSeleccionado.sCodigo}?
            </MeditocConfirmacion>
        </Fragment>
    );
};

export default Cupones;
