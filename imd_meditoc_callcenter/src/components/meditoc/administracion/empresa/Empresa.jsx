import React, { Fragment, useState } from "react";
import MeditocHeader1 from "../../../utilidades/MeditocHeader1";
import { Tooltip, IconButton } from "@material-ui/core";
import EditIcon from "@material-ui/icons/Edit";
import DeleteIcon from "@material-ui/icons/Delete";
import AddRoundedIcon from "@material-ui/icons/AddRounded";
import FormatListBulletedIcon from "@material-ui/icons/FormatListBulleted";
import ListAltOutlinedIcon from "@material-ui/icons/ListAltOutlined";
import WorkRoundedIcon from "@material-ui/icons/WorkRounded";

import MeditocBody from "../../../utilidades/MeditocBody";
import MeditocTable from "../../../utilidades/MeditocTable";
import FormEmpresa from "./FormEmpresa";
import FoliosEmpresa from "./FoliosEmpresa";

const Empresa = (props) => {
    const { funcAlert } = props;

    const columns = [
        { title: "ID", field: "iIdEmpresa", align: "center" },
        { title: "Nombre", field: "sNombre", align: "center" },
        { title: "Folio", field: "sFolioEmpresa", align: "center" },
        { title: "Correo", field: "sCorreo", align: "center" },
    ];

    const empresaEntidadVacia = {
        iIdEmpresa: 0,
        sNombre: "",
        sFolioEmpresa: "",
        sCorreo: "",
    };

    const data = [
        { iIdEmpresa: 1, sNombre: "Empresa-A", sFolioEmpresa: "EM-0001", sCorreo: "empresaa@gmail.com" },
        { iIdEmpresa: 2, sNombre: "Empresa-B", sFolioEmpresa: "EM-0002", sCorreo: "empresab@gmail.com" },
        { iIdEmpresa: 3, sNombre: "Empresa-C", sFolioEmpresa: "EM-0003", sCorreo: "empresac@gmail.com" },
        { iIdEmpresa: 4, sNombre: "Empresa-D", sFolioEmpresa: "EM-0004", sCorreo: "empresad@gmail.com" },
    ];

    const [listaEmpresas, setListaEmpresas] = useState(data);

    const [empresaSeleccionada, setEmpresaSeleccionada] = useState(empresaEntidadVacia);
    const [empresaParaModalForm, setEmpresaParaModalForm] = useState(empresaEntidadVacia);

    const [modalFormEmpresaOpen, setModalFormEmpresaOpen] = useState(false);
    const [modalFoliosEmpresaOpen, setModalFoliosEmpresaOpen] = useState(false);

    const handleClickNuevaEmpresa = () => {
        setEmpresaParaModalForm(empresaEntidadVacia);
        setModalFormEmpresaOpen(true);
    };

    const handleClickEditarEmpresa = () => {
        if (empresaSeleccionada.iIdEmpresa === 0) {
            funcAlert("Seleccione una empresa de la tabla para continuar");
            return;
        }
        setEmpresaParaModalForm(empresaSeleccionada);
        setModalFormEmpresaOpen(true);
    };

    const handleClickFoliosEmpresa = () => {
        if (empresaSeleccionada.iIdEmpresa === 0) {
            funcAlert("Seleccione una empresa de la tabla para continuar");
            return;
        }
        setEmpresaParaModalForm(empresaSeleccionada);
        setModalFoliosEmpresaOpen(true);
    };

    return (
        <Fragment>
            <MeditocHeader1 title="EMPRESAS">
                <Tooltip title="Nueva empresa" arrow>
                    <IconButton onClick={handleClickNuevaEmpresa}>
                        <AddRoundedIcon className="color-0" />
                    </IconButton>
                </Tooltip>
                {/* <Tooltip title="Detalle empresa" arrow>
                    <IconButton>
                        <FormatListBulletedIcon className="color-0" />
                    </IconButton>
                </Tooltip> */}
                <Tooltip title="Editar datos de empresa" arrow>
                    <IconButton onClick={handleClickEditarEmpresa}>
                        <EditIcon className="color-0" />
                    </IconButton>
                </Tooltip>
                <Tooltip title="Administrar folios de empresa" arrow>
                    <IconButton onClick={handleClickFoliosEmpresa}>
                        <WorkRoundedIcon className="color-0" />
                    </IconButton>
                </Tooltip>
            </MeditocHeader1>
            <MeditocBody>
                <MeditocTable
                    columns={columns}
                    data={listaEmpresas}
                    rowSelected={empresaSeleccionada}
                    setRowSelected={setEmpresaSeleccionada}
                    mainField="iIdEmpresa"
                />
            </MeditocBody>
            <FormEmpresa
                entEmpresa={empresaParaModalForm}
                open={modalFormEmpresaOpen}
                setOpen={setModalFormEmpresaOpen}
            />
            <FoliosEmpresa
                entEmpresa={empresaParaModalForm}
                open={modalFoliosEmpresaOpen}
                setOpen={setModalFoliosEmpresaOpen}
                funcAlert={funcAlert}
            />
        </Fragment>
    );
};

export default Empresa;
