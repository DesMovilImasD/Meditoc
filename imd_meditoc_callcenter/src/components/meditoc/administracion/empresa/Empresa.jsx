import React, { Fragment, useState } from "react";
import SubmoduloBarra from "../../../utilidades/SubmoduloBarra";
import { Tooltip, IconButton } from "@material-ui/core";
import EditIcon from "@material-ui/icons/Edit";
import DeleteIcon from "@material-ui/icons/Delete";
import FormatListBulletedIcon from "@material-ui/icons/FormatListBulleted";
import ListAltOutlinedIcon from "@material-ui/icons/ListAltOutlined";
import NoteAddRoundedIcon from "@material-ui/icons/NoteAddRounded";
import EventAvailableRoundedIcon from "@material-ui/icons/EventAvailableRounded";
import SubmoduloContenido from "../../../utilidades/SubmoduloContenido";
import MeditocTable from "../../../utilidades/MeditocTable";

const Empresa = (props) => {
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

    return (
        <Fragment>
            <SubmoduloBarra title="EMPRESA">
                <Tooltip title="Detalle empresa" arrow>
                    <IconButton>
                        <FormatListBulletedIcon className="color-0" />
                    </IconButton>
                </Tooltip>
                <Tooltip title="Editar datos de empresa" arrow>
                    <IconButton>
                        <EditIcon className="color-0" />
                    </IconButton>
                </Tooltip>
                <Tooltip title="Ver folios de empresa" arrow>
                    <IconButton>
                        <ListAltOutlinedIcon className="color-0" />
                    </IconButton>
                </Tooltip>
                <Tooltip title="Agregar folios a empresa" arrow>
                    <IconButton>
                        <NoteAddRoundedIcon className="color-0" />
                    </IconButton>
                </Tooltip>
                <Tooltip title="Agregar vigencia a folios de empresa" arrow>
                    <IconButton>
                        <EventAvailableRoundedIcon className="color-0" />
                    </IconButton>
                </Tooltip>
            </SubmoduloBarra>
            <SubmoduloContenido>
                <MeditocTable
                    columns={columns}
                    data={listaEmpresas}
                    rowSelected={empresaSeleccionada}
                    setRowSelected={setEmpresaSeleccionada}
                    mainField="iIdEmpresa"
                />
            </SubmoduloContenido>
        </Fragment>
    );
};

export default Empresa;
