import PropTypes from "prop-types";
import React, { Fragment } from "react";
import MeditocHeader1 from "../../../utilidades/MeditocHeader1";
import { Tooltip, IconButton } from "@material-ui/core";
import PersonAddIcon from "@material-ui/icons/PersonAdd";
import EditIcon from "@material-ui/icons/Edit";
import DeleteIcon from "@material-ui/icons/Delete";
import MeditocBody from "../../../utilidades/MeditocBody";
import { useState } from "react";
import MeditocTable from "../../../utilidades/MeditocTable";
import CGUController from "../../../../controllers/CGUController";
import { useEffect } from "react";
import FormUsuario from "./FormUsuario";
import MeditocConfirmacion from "../../../utilidades/MeditocConfirmacion";

/*************************************************************
 * Descripcion: Submódulo para vista principal "USUARIOS" del portal Meditoc
 * Creado: Cristopher Noh
 * Fecha: 26/08/2020
 * Invocado desde: ContentMain
 *************************************************************/
const Usuarios = (props) => {
    const { usuarioSesion, funcLoader, funcAlert } = props;

    //Servicios API
    const cguController = new CGUController();

    //Entidad vacía de un usuario
    const usuarioEntidadVacia = {
        iIdUsuario: 0,
        iIdTipoCuenta: 1,
        iIdPerfil: 0,
        sTipoCuenta: "",
        sPerfil: "",
        sUsuario: "",
        sPassword: "",
        sNombres: "",
        sApellidoPaterno: "",
        sApellidoMaterno: "",
        dtFechaNacimiento: null,
        sTelefono: "",
        sCorreo: "",
        sDomicilio: "",
        iIdUsuarioMod: 0,
        bActivo: false,
        bBaja: false,
    };

    //Columnas de la tabla de usuarios
    const columns = [
        { title: "ID", field: "iIdUsuario", align: "center", hidden: true },
        { title: "Tipo de cuenta", field: "sTipoCuenta", align: "center" },
        { title: "Perfil", field: "sPerfil", align: "center" },
        { title: "Usuario", field: "sUsuario", align: "center" },
        { title: "Nombre", field: "sNombres", align: "center" },
        { title: "Apellido", field: "sApellidoPaterno", align: "center" },
    ];

    //Lista de usuarios activos del portal
    const [listaUsuarios, setListaUsuarios] = useState([]);

    //Lista de perfiles activos del portal
    const [listaPerfiles, setListaPerfiles] = useState([]);

    //Usuario seleccionado de la tabla
    const [usuarioSeleccionado, setUsuarioSeleccionado] = useState(usuarioEntidadVacia);

    //Usuario para enviar el formulario de crear/editar
    const [usuarioParaModalForm, setUsuarioParaModalForm] = useState(usuarioEntidadVacia);

    //State para mostrar/ocultar el formulario para crear/editar usuario
    const [modalFormUsuarioNuevoOpen, setModalFormUsuarioNuevoOpen] = useState(false);

    //State para mostrar/ocultar la alerta de confirmación para dar de baja un usuario
    const [modalFormUsuarioEliminarOpen, setModalFormUsuarioEliminarOpen] = useState(false);

    //Función para abrir el formulario para crear un usuario
    const handleClickNuevoUsuario = () => {
        setUsuarioParaModalForm(usuarioEntidadVacia);
        setModalFormUsuarioNuevoOpen(true);
    };

    //Función para abrir el formulario para editar un usuario
    const handleClickEditarUsuario = () => {
        if (usuarioSeleccionado.iIdUsuario === 0) {
            funcAlert("Seleccione un usuario para continuar");
            return;
        }
        setUsuarioParaModalForm(usuarioSeleccionado);
        setModalFormUsuarioNuevoOpen(true);
    };

    //Función para abrir la alerta de confirmación para dar de baja un usuario
    const handleClickEliminarUsuario = () => {
        if (usuarioSeleccionado.iIdUsuario === 0) {
            funcAlert("Seleccione un usuario para continuar");
            return;
        }
        setModalFormUsuarioEliminarOpen(true);
    };

    //Consumir servicio para obtener los usuarios activos del portal
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

    //Consumir servicio para obtener los perfiles activos del portal
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

    //Consumir servicio para dar de baja un usuario del portal
    const funcEliminarUsuario = async () => {
        const entUsuarioSave = {
            iIdUsuario: usuarioSeleccionado.iIdUsuario,
            iIdTipoCuenta: usuarioSeleccionado.iIdTipoCuenta,
            // iIdPerfil: null,
            // sTipoCuenta: null,
            // sPerfil: null,
            // sUsuario: null,
            // sPassword: null,
            // sNombres: null,
            // sApellidoPaterno: null,
            // sApellidoMaterno: null,
            // dtFechaNacimiento: null,
            // sTelefono: null,
            // sCorreo: null,
            // sDomicilio: null,
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

    //Consultar datos al cargar este componente
    useEffect(() => {
        funcGetUsuarios();
        funcGetPerfiles();

        // eslint-disable-next-line
    }, []);

    return (
        <Fragment>
            <MeditocHeader1 title="USUARIOS">
                <Tooltip title="Nuevo usuario" arrow>
                    <IconButton onClick={handleClickNuevoUsuario}>
                        <PersonAddIcon className="color-0" />
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
            </MeditocHeader1>
            <MeditocBody>
                <MeditocTable
                    columns={columns}
                    data={listaUsuarios}
                    rowSelected={usuarioSeleccionado}
                    setRowSelected={setUsuarioSeleccionado}
                    mainField="iIdUsuario"
                    doubleClick={handleClickEditarUsuario}
                />
            </MeditocBody>
            <FormUsuario
                entUsuario={usuarioParaModalForm}
                listaPerfiles={listaPerfiles}
                open={modalFormUsuarioNuevoOpen}
                setUsuarioSeleccionado={setUsuarioSeleccionado}
                setOpen={setModalFormUsuarioNuevoOpen}
                funcGetUsuarios={funcGetUsuarios}
                usuarioSesion={usuarioSesion}
                funcLoader={funcLoader}
                funcAlert={funcAlert}
            />
            <MeditocConfirmacion
                title="Eliminar usuario"
                open={modalFormUsuarioEliminarOpen}
                setOpen={setModalFormUsuarioEliminarOpen}
                okFunc={funcEliminarUsuario}
            >
                ¿Desea eliminar el usuario {usuarioSeleccionado.sUsuario}?
            </MeditocConfirmacion>
        </Fragment>
    );
};

Usuarios.propTypes = {
    funcAlert: PropTypes.func,
    funcLoader: PropTypes.func,
    usuarioSesion: PropTypes.shape({
        iIdUsuario: PropTypes.number,
    }),
};

export default Usuarios;
