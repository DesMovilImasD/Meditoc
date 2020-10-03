import PropTypes from "prop-types";
import React, { Fragment, useState, useEffect } from "react";
import MeditocHeader1 from "../../../utilidades/MeditocHeader1";
import { Tooltip, IconButton } from "@material-ui/core";
import EditIcon from "@material-ui/icons/Edit";
import AddRoundedIcon from "@material-ui/icons/AddRounded";
import WorkRoundedIcon from "@material-ui/icons/WorkRounded";
import MeditocBody from "../../../utilidades/MeditocBody";
import MeditocTable from "../../../utilidades/MeditocTable";
import FormEmpresa from "./FormEmpresa";
import FoliosEmpresa from "./FoliosEmpresa";
import EmpresaController from "../../../../controllers/EmpresaController";
import ProductoController from "../../../../controllers/ProductoController";

const Empresa = (props) => {
    const { usuarioSesion, funcLoader, funcAlert, title } = props;

    const empresaController = new EmpresaController();
    const productoController = new ProductoController();

    let columns = [
        { title: "ID", field: "iIdEmpresa", align: "center", hidden: true },
        { title: "Nombre", field: "sNombre", align: "center" },
        { title: "Folio", field: "sFolioEmpresa", align: "center" },
        { title: "Correo", field: "sCorreo", align: "center" },
        { title: "Fecha", field: "sFechaCreacion", align: "center" },
    ];

    const empresaEntidadVacia = {
        iIdEmpresa: 0,
        sNombre: "",
        sFolioEmpresa: "",
        sCorreo: "",
    };

    const [listaEmpresas, setListaEmpresas] = useState([]);
    const [listaProductos, setListaProductos] = useState([]);

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

    const funcGetEmpresas = async () => {
        funcLoader(true, "Consultando empresas...");

        const response = await empresaController.funcGetEmpresas();

        if (response.Code === 0) {
            setListaEmpresas(response.Result);
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

    const funcGetData = async () => {
        await funcGetEmpresas();
        await funcGetProductos();
    };

    useEffect(() => {
        funcGetData();
        // eslint-disable-next-line
    }, []);

    return (
        <Fragment>
            <MeditocHeader1 title={title}>
                <Tooltip title="Nueva empresa" arrow>
                    <IconButton onClick={handleClickNuevaEmpresa}>
                        <AddRoundedIcon className="color-0" />
                    </IconButton>
                </Tooltip>
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
                    doubleClick={handleClickFoliosEmpresa}
                />
            </MeditocBody>
            <FormEmpresa
                entEmpresa={empresaParaModalForm}
                open={modalFormEmpresaOpen}
                setOpen={setModalFormEmpresaOpen}
                funcGetEmpresas={funcGetEmpresas}
                usuarioSesion={usuarioSesion}
                funcLoader={funcLoader}
                funcAlert={funcAlert}
            />
            <FoliosEmpresa
                entEmpresa={empresaParaModalForm}
                open={modalFoliosEmpresaOpen}
                setOpen={setModalFoliosEmpresaOpen}
                listaProductos={listaProductos}
                usuarioSesion={usuarioSesion}
                funcLoader={funcLoader}
                funcAlert={funcAlert}
            />
        </Fragment>
    );
};

Empresa.propTypes = {
    funcAlert: PropTypes.func,
    funcLoader: PropTypes.func,
    title: PropTypes.any,
    usuarioSesion: PropTypes.any,
};

export default Empresa;
