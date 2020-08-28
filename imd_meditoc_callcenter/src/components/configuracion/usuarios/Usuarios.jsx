import React, { Fragment } from "react";
import SubmoduloBarra from "../../SubmoduloBarra";
import { Tooltip, IconButton } from "@material-ui/core";
import InsertDriveFileIcon from "@material-ui/icons/InsertDriveFile";
import EditIcon from "@material-ui/icons/Edit";
import DeleteIcon from "@material-ui/icons/Delete";
import SubmoduloContenido from "../../SubmoduloContenido";
import { useState } from "react";
import MeditocTable from "../../MeditocTable";
import CGUController from "../../../controllers/CGUController";
import { useEffect } from "react";
import FormUsuario from "./FormUsuario";
import Confirmacion from "../../Confirmacion";

const Usuarios = (props) => {
    const { usuarioSesion, funcLoader, funcAlert } = props;

    const cguController = new CGUController();

    const usuarioEntidadVacia = {
        iIdUsuario: 0,
        iIdTipoCuenta: 1,
        iIdPerfil: 0,
        sUsuario: "",
        sPassword: "",
        sNombres: "",
        sApellidoPaterno: "",
        sApellidoMaterno: "",
        dtFechaNacimiento: "",
        sTelefono: "",
        sCorreo: "",
        sDomicilio: "",
        iIdUsuarioMod: 0,
        bActivo: false,
        bBaja: false,
    };

    const columns = [
        { title: "ID", field: "iIdUsuario", align: "center" },
        { title: "Tipo", field: "iIdTipoCuenta", align: "center" },
        { title: "Perfil", field: "iIdPerfil", align: "center" },
        { title: "Usuario", field: "sUsuario", align: "center" },
        { title: "Nombre", field: "sNombres", align: "center" },
        { title: "Apellido", field: "sApellidoPaterno", align: "center" },
    ];

    const [listaUsuarios, setListaUsuarios] = useState([]);
    const [listaPerfiles, setListaPerfiles] = useState([]);
    const [usuarioSeleccionado, setUsuarioSeleccionado] = useState(usuarioEntidadVacia);

    const [modalFormUsuarioNuevoOpen, setModalFormUsuarioNuevoOpen] = useState(false);
    const [modalFormUsuarioEditarOpen, setModalFormUsuarioEditarOpen] = useState(false);
    const [modalFormUsuarioEliminarOpen, setModalFormUsuarioEliminarOpen] = useState(true);

    const handleClickNuevoUsuario = () => {
        setModalFormUsuarioNuevoOpen(true);
    };

    const handleClickEditarUsuario = () => {
        if (usuarioSeleccionado.iIdUsuario === 0) {
            funcAlert("Seleccione un usuario para continuar", "warning");
            return;
        }
        setModalFormUsuarioEditarOpen(true);
    };

    const handleClickEliminarUsuario = () => {
        if (usuarioSeleccionado.iIdUsuario === 0) {
            funcAlert("Seleccione un usuario para continuar", "warning");
            return;
        }
        setModalFormUsuarioEliminarOpen(true);
    };

    const funcGetUsuarios = async () => {
        funcLoader(true, "Consultado usuarios del Meditoc...");
        const response = await cguController.funcGetUsuarios();
        funcLoader();

        if (response.Code !== 0) {
            funcAlert(response.Message);
            return;
        }

        setListaUsuarios(response.Result);
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

    const funcEliminarUsuario = async () => {
        const entUsuarioSave = {
            iIdUsuario: usuarioSeleccionado.iIdUsuario,
            iIdTipoCuenta: usuarioSeleccionado.iIdTipoCuenta,
            iIdPerfil: null,
            sUsuario: null,
            sPassword: null,
            sNombres: null,
            sApellidoPaterno: null,
            sApellidoMaterno: null,
            dtFechaNacimiento: null,
            sTelefono: null,
            sCorreo: null,
            sDomicilio: null,
            iIdUsuarioMod: usuarioSesion.iIdUsuario,
            bActivo: false,
            bBaja: true,
        };

        funcLoader(true, "Eliminando usuario...");

        const response = await cguController.funcSaveUsuario(entUsuarioSave);
        if (response.Code !== 0) {
            funcAlert(response.Message);
        } else {
            setModalFormUsuarioEliminarOpen(false);
            funcAlert(response.Message, "success");
            funcGetUsuarios();
        }

        funcLoader();
    };

    useEffect(() => {
        funcGetUsuarios();
        funcGetPerfiles();
    }, []);

    return (
        <Fragment>
            <SubmoduloBarra title="USUARIOS">
                <Tooltip title="Nuevo usuario" arrow>
                    <IconButton onClick={handleClickNuevoUsuario}>
                        <InsertDriveFileIcon className="color-0" />
                    </IconButton>
                </Tooltip>
                <Tooltip title="Editar usuario" arrow>
                    <IconButton onClick={handleClickEditarUsuario}>
                        <EditIcon className="color-0" />
                    </IconButton>
                </Tooltip>
                <Tooltip title="Eliminar usuario" arrow>
                    <IconButton onClick={handleClickEliminarUsuario}>
                        <DeleteIcon className="color-0" />
                    </IconButton>
                </Tooltip>
            </SubmoduloBarra>
            <SubmoduloContenido>
                <MeditocTable
                    columns={columns}
                    data={listaUsuarios}
                    rowSelected={usuarioSeleccionado}
                    setRowSelected={setUsuarioSeleccionado}
                    mainField="iIdUsuario"
                    isLoading={false}
                />
            </SubmoduloContenido>
            <FormUsuario
                entUsuario={usuarioEntidadVacia}
                listaPerfiles={listaPerfiles}
                open={modalFormUsuarioNuevoOpen}
                setOpen={setModalFormUsuarioNuevoOpen}
                funcGetUsuarios={funcGetUsuarios}
                usuarioSesion={usuarioSesion}
                funcLoader={funcLoader}
                funcAlert={funcAlert}
            />
            <FormUsuario
                entUsuario={usuarioSeleccionado}
                listaPerfiles={listaPerfiles}
                open={modalFormUsuarioEditarOpen}
                setOpen={setModalFormUsuarioEditarOpen}
                funcGetUsuarios={funcGetUsuarios}
                usuarioSesion={usuarioSesion}
                funcLoader={funcLoader}
                funcAlert={funcAlert}
            />
            <Confirmacion
                title="Eliminar usuario"
                open={modalFormUsuarioEliminarOpen}
                setOpen={setModalFormUsuarioEliminarOpen}
                okFunc={funcEliminarUsuario}
            >
                ¿Desea eliminar el usuario {usuarioSeleccionado.sUsuario}?
            </Confirmacion>
        </Fragment>
    );
};

export default Usuarios;
