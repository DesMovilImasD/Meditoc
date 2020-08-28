import React, { Fragment, useState, useEffect } from "react";
import SubmoduloBarra from "../../SubmoduloBarra";
import { Tooltip, IconButton, Paper } from "@material-ui/core";
import AddIcon from "@material-ui/icons/Add";
import InsertDriveFileIcon from "@material-ui/icons/InsertDriveFile";
import EditIcon from "@material-ui/icons/Edit";
import DeleteIcon from "@material-ui/icons/Delete";
import VerifiedUserIcon from "@material-ui/icons/VerifiedUser";
import SubmoduloContenido from "../../SubmoduloContenido";
import MeditocTable from "../../MeditocTable";
import CGUController from "../../../controllers/CGUController";
import FormPerfil from "./FormPerfil";
import EliminarPerfil from "./EliminarPerfil";
import Permisos from "./Permisos";

/*************************************************************
 * Descripcion: Submódulo para la vista principal "PERFILES" del portal Meditoc
 * Creado: Cristopher Noh
 * Fecha: 26/08/2020
 * Invocado desde: ContentMain
 *************************************************************/
const Perfiles = (props) => {
    const { usuarioSesion, funcLoader, funcAlert } = props;

    const cguController = new CGUController();

    const columns = [
        { title: "ID Perfil", field: "iIdPerfil", align: "center" },
        { title: "Descripción", field: "sNombre", align: "center" },
    ];

    const [listaSistema, setListaSistema] = useState([]);
    const [listaPerfiles, setListaPerfiles] = useState([]);

    const [modalFormPerfilOpen, setModalFormPerfilOpen] = useState(false);
    const [modalFormEliminarPerfilOpen, setModalFormEliminarPerfilOpen] = useState(false);
    const [modalFormPermisosOpen, setModalFormPermisosOpen] = useState(false);

    const [perfilForModalForm, setPerfilForModalForm] = useState({ iIdPerfil: 0, sNombre: "" });
    const [perfilSeleccionado, setPerfilSeleccionado] = useState({ iIdPerfil: 0, sNombre: "" });

    //Obtener todos los componenetes del sistema sin importar el perfil
    const funcGetPermisosXPerfil = async () => {
        funcLoader(true, "Consultado elementos del sistema...");
        const response = await cguController.funcGetPermisosXPeril();
        funcLoader();

        if (response.Code !== 0) {
            funcAlert(response.Message);
            return;
        }

        setListaSistema(response.Result);
    };

    const funcGetPerfiles = async () => {
        funcLoader(true, "Consultado perfiles del sistema...");
        const response = await cguController.funcGetPerfiles();
        funcLoader();

        if (response.Code !== 0) {
            funcAlert(response.Message);
            return;
        }

        setListaPerfiles(response.Result);
    };

    //Nuevo perfil
    const handleClickNuevoPerfil = () => {
        setPerfilForModalForm({ iIdPerfil: 0, sNombre: "" });
        setModalFormPerfilOpen(true);
    };

    //Editar perfil
    const handleClickEditarPerfil = () => {
        if (perfilSeleccionado.iIdPerfil === 0) {
            funcAlert("Seleccione un perfil de la tabla para continuar", "warning");
            return;
        }
        setPerfilForModalForm(perfilSeleccionado);
        setModalFormPerfilOpen(true);
    };

    //Eliminar perfil
    const handleClickEliminarPerfil = () => {
        if (perfilSeleccionado.iIdPerfil === 0) {
            funcAlert("Seleccione un perfil de la tabla para continuar", "warning");
            return;
        }
        setPerfilForModalForm(perfilSeleccionado);
        setModalFormEliminarPerfilOpen(true);
    };

    //Administrar permisos del perfil
    const handleClickPermisosPerfil = () => {
        if (perfilSeleccionado.iIdPerfil === 0) {
            funcAlert("Seleccione un perfil de la tabla para continuar", "warning");
            return;
        }
        setPerfilForModalForm(perfilSeleccionado);
        setModalFormPermisosOpen(true);
    };

    //Consultar datos al cargar el componente
    useEffect(() => {
        // eslint-disable-next-line
        funcGetPermisosXPerfil();
        funcGetPerfiles();
    }, []);

    return (
        <Fragment>
            <SubmoduloBarra title="PERFILES">
                <Tooltip title="Nuevo perfil" arrow>
                    <IconButton onClick={handleClickNuevoPerfil}>
                        <InsertDriveFileIcon className="color-0" />
                    </IconButton>
                </Tooltip>
                <Tooltip title="Editar perfil" arrow>
                    <IconButton onClick={handleClickEditarPerfil}>
                        <EditIcon className="color-0" />
                    </IconButton>
                </Tooltip>
                <Tooltip title="Eliminar perfil" arrow>
                    <IconButton onClick={handleClickEliminarPerfil}>
                        <DeleteIcon className="color-0" />
                    </IconButton>
                </Tooltip>
                <Tooltip title="Administrar permisos del perfil" arrow>
                    <IconButton onClick={handleClickPermisosPerfil}>
                        <VerifiedUserIcon className="color-0" />
                    </IconButton>
                </Tooltip>
            </SubmoduloBarra>
            <SubmoduloContenido>
                <MeditocTable
                    columns={columns}
                    data={listaPerfiles}
                    rowSelected={perfilSeleccionado}
                    setRowSelected={setPerfilSeleccionado}
                    mainField="iIdPerfil"
                    isLoading={false}
                />
            </SubmoduloContenido>
            <FormPerfil
                entPerfil={perfilForModalForm}
                open={modalFormPerfilOpen}
                setOpen={setModalFormPerfilOpen}
                funcGetPerfiles={funcGetPerfiles}
                usuarioSesion={usuarioSesion}
                funcLoader={funcLoader}
                funcAlert={funcAlert}
            />
            <EliminarPerfil
                entPerfil={perfilForModalForm}
                setPerfilSeleccionado={setPerfilSeleccionado}
                open={modalFormEliminarPerfilOpen}
                setOpen={setModalFormEliminarPerfilOpen}
                funcGetPerfiles={funcGetPerfiles}
                usuarioSesion={usuarioSesion}
                funcLoader={funcLoader}
                funcAlert={funcAlert}
            />
            <Permisos
                entPerfil={perfilForModalForm}
                listaSistema={listaSistema}
                open={modalFormPermisosOpen}
                setOpen={setModalFormPermisosOpen}
                usuarioSesion={usuarioSesion}
                funcLoader={funcLoader}
                funcAlert={funcAlert}
            />
        </Fragment>
    );
};

export default Perfiles;
