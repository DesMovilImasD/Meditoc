import PropTypes from "prop-types";
import React, { Fragment, useState, useEffect } from "react";
import SubmoduloBarra from "../../SubmoduloBarra";
import { Tooltip, IconButton } from "@material-ui/core";
import InsertDriveFileIcon from "@material-ui/icons/InsertDriveFile";
import EditIcon from "@material-ui/icons/Edit";
import DeleteIcon from "@material-ui/icons/Delete";
import VerifiedUserIcon from "@material-ui/icons/VerifiedUser";
import SubmoduloContenido from "../../SubmoduloContenido";
import MeditocTable from "../../MeditocTable";
import CGUController from "../../../controllers/CGUController";
import FormPerfil from "./FormPerfil";
import Permisos from "./Permisos";
import Confirmacion from "../../Confirmacion";

/*************************************************************
 * Descripcion: Submódulo para la vista principal "PERFILES" del portal Meditoc
 * Creado: Cristopher Noh
 * Fecha: 26/08/2020
 * Invocado desde: ContentMain
 *************************************************************/
const Perfiles = (props) => {
    const { usuarioSesion, funcLoader, funcAlert } = props;

    //Servicios API
    const cguController = new CGUController();

    //Entidad vacía de un perfil
    const perfilEntidadVacia = { iIdPerfil: 0, sNombre: "" };

    //Columnas a mostrar en la tabla
    const columns = [
        { title: "ID Perfil", field: "iIdPerfil", align: "center" },
        { title: "Descripción", field: "sNombre", align: "center" },
    ];

    //Lista de todos los elementos del sistema (módulos, submódulos y botones)
    const [listaSistema, setListaSistema] = useState([]);

    //Lista de los perfiles activos del sistema
    const [listaPerfiles, setListaPerfiles] = useState([]);

    //State para mostrar/ocultar el formulario para crear/editar un perfil
    const [modalFormPerfilOpen, setModalFormPerfilOpen] = useState(false);

    //State para mostrar/ocultar la alerta de confirmación para borrar un perfil
    const [modalFormEliminarPerfilOpen, setModalFormEliminarPerfilOpen] = useState(false);

    //State para mostrar/ocultar la ventana de administración de permisos para el perfil seleccionado
    const [modalFormPermisosOpen, setModalFormPermisosOpen] = useState(false);

    //Objeto con los datos del tipo de entidad (nuevo o existente) para mostrar en el formulario de crear/editar perfil
    const [perfilForModalForm, setPerfilForModalForm] = useState(perfilEntidadVacia);

    //Objeto con los datos de la entidad perteneciente a la fila seleccionada
    const [perfilSeleccionado, setPerfilSeleccionado] = useState(perfilEntidadVacia);

    //Funcion para abrir el modal para crear un perfil
    const handleClickNuevoPerfil = () => {
        setPerfilForModalForm(perfilEntidadVacia);
        setModalFormPerfilOpen(true);
    };

    //Funcion para abrir el modal para editar un perfil
    const handleClickEditarPerfil = () => {
        if (perfilSeleccionado.iIdPerfil === 0) {
            funcAlert("Seleccione un perfil de la tabla para continuar", "warning");
            return;
        }
        setPerfilForModalForm(perfilSeleccionado);
        setModalFormPerfilOpen(true);
    };

    //Función para abrir la alerta de confirmación para borrar un perfil
    const handleClickEliminarPerfil = () => {
        if (perfilSeleccionado.iIdPerfil === 0) {
            funcAlert("Seleccione un perfil de la tabla para continuar", "warning");
            return;
        }
        setModalFormEliminarPerfilOpen(true);
    };

    //Función para abrir la ventana de administración de permisos para el perfil seleccionado
    const handleClickPermisosPerfil = () => {
        if (perfilSeleccionado.iIdPerfil === 0) {
            funcAlert("Seleccione un perfil de la tabla para continuar", "warning");
            return;
        }
        setPerfilForModalForm(perfilSeleccionado);
        setModalFormPermisosOpen(true);
    };

    //Consumir servicio para obtener todos los elementos del sistema (módulos, submódulos y botones)
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

    //Consumir servicio para obtener todos los perfiles activos del sistema
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

    //Consumir servicio para borrar un perfil en la base
    const funcEliminarPerfil = async () => {
        funcLoader(true, "Eliminando perfil...");

        const entSavePerfil = {
            iIdPerfil: perfilSeleccionado.iIdPerfil,
            iIdUsuarioMod: usuarioSesion.iIdUsuario,
            sNombre: null,
            bActivo: false,
            bBaja: true,
        };

        const response = await cguController.funcSavePerfil(entSavePerfil);
        if (response.Code !== 0) {
            funcAlert(response.Message);
        } else {
            setModalFormEliminarPerfilOpen(false);
            setPerfilSeleccionado({ iIdPerfil: 0, sNombre: "" });
            funcAlert(response.Message, "success");
            funcGetPerfiles();
        }

        funcLoader();
    };

    //Consultar datos al cargar el componente
    useEffect(() => {
        funcGetPermisosXPerfil();
        funcGetPerfiles();

        // eslint-disable-next-line
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
                    doubleClick={handleClickEditarPerfil}
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
            <Confirmacion
                title="Eliminar perfil"
                open={modalFormEliminarPerfilOpen}
                setOpen={setModalFormEliminarPerfilOpen}
                okFunc={funcEliminarPerfil}
            >
                ¿Desea eliminar el perfil {perfilSeleccionado.sNombre} y todos sus permisos?
            </Confirmacion>
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

Perfiles.propTypes = {
    funcAlert: PropTypes.func,
    funcLoader: PropTypes.func,
    usuarioSesion: PropTypes.shape({
        iIdUsuario: PropTypes.number,
    }),
};

export default Perfiles;
