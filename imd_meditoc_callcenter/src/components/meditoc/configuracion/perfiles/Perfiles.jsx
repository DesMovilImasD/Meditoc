import { IconButton, Tooltip } from "@material-ui/core";
import React, { Fragment, useEffect, useState } from "react";

import AddRoundedIcon from "@material-ui/icons/AddRounded";
import CGUController from "../../../../controllers/CGUController";
import DeleteIcon from "@material-ui/icons/Delete";
import EditIcon from "@material-ui/icons/Edit";
import { EnumPerfilesPrincipales } from "../../../../configurations/enumConfig";
import FormPerfil from "./FormPerfil";
import MeditocBody from "../../../utilidades/MeditocBody";
import MeditocConfirmacion from "../../../utilidades/MeditocConfirmacion";
import MeditocHeader1 from "../../../utilidades/MeditocHeader1";
import MeditocTable from "../../../utilidades/MeditocTable";
import Permisos from "./Permisos";
import PropTypes from "prop-types";
import ReplayIcon from "@material-ui/icons/Replay";
import VerifiedUserIcon from "@material-ui/icons/VerifiedUser";
import { emptyFunc } from "../../../../configurations/preventConfig";

/*************************************************************
 * Descripcion: Submódulo para la vista principal "PERFILES" del portal Meditoc
 * Creado: Cristopher Noh
 * Fecha: 26/08/2020
 * Invocado desde: ContentMain
 *************************************************************/
const Perfiles = (props) => {
    const { usuarioSesion, permisos, funcLoader, funcAlert } = props;

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
            funcAlert("Seleccione un perfil de la tabla para continuar");
            return;
        }
        setPerfilForModalForm(perfilSeleccionado);
        setModalFormPerfilOpen(true);
    };

    //Función para abrir la alerta de confirmación para borrar un perfil
    const handleClickEliminarPerfil = () => {
        if (perfilSeleccionado.iIdPerfil === 0) {
            funcAlert("Seleccione un perfil de la tabla para continuar");
            return;
        }

        const perfilesNoEliminar = [
            EnumPerfilesPrincipales.Superadministrador,
            EnumPerfilesPrincipales.Administrador,
            EnumPerfilesPrincipales.DoctorCallCenter,
            EnumPerfilesPrincipales.DoctorEspecialista,
            EnumPerfilesPrincipales.AdministradorEspecialiesta,
        ];
        if (perfilesNoEliminar.includes(perfilSeleccionado.iIdPerfil)) {
            funcAlert("Este perfil de sistema no se puede eliminar", "info");
            return;
        }
        setModalFormEliminarPerfilOpen(true);
    };

    //Función para abrir la ventana de administración de permisos para el perfil seleccionado
    const handleClickPermisosPerfil = () => {
        if (perfilSeleccionado.iIdPerfil === 0) {
            funcAlert("Seleccione un perfil de la tabla para continuar");
            return;
        }
        setPerfilForModalForm(perfilSeleccionado);
        setModalFormPermisosOpen(true);
    };

    //Consumir servicio para obtener todos los elementos del sistema (módulos, submódulos y botones)
    const funcGetPermisosXPerfil = async () => {
        funcLoader(true, "Consultado elementos del sistema...");

        const response = await cguController.funcGetPermisosXPeril();

        if (response.Code === 0) {
            setListaSistema(response.Result);
        } else {
            funcAlert(response.Message);
        }

        funcLoader();
    };

    //Consumir servicio para obtener todos los perfiles activos del sistema
    const funcGetPerfiles = async () => {
        funcLoader(true, "Consultado perfiles del sistema...");

        const response = await cguController.funcGetPerfiles();

        if (response.Code === 0) {
            setListaPerfiles(response.Result);
        } else {
            funcAlert(response.Message);
        }

        funcLoader();
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
        if (response.Code === 0) {
            setModalFormEliminarPerfilOpen(false);
            setPerfilSeleccionado({ iIdPerfil: 0, sNombre: "" });

            await funcGetPerfiles();

            funcAlert(response.Message, "success");
        } else {
            funcAlert(response.Message);
        }

        funcLoader();
    };

    const getData = async () => {
        await funcGetPermisosXPerfil();
        await funcGetPerfiles();
    };

    //Consultar datos al cargar el componente
    useEffect(() => {
        getData();

        // eslint-disable-next-line
    }, []);

    return (
        <Fragment>
            <MeditocHeader1 title={permisos.Nombre}>
                {permisos.Botones["1"] !== undefined && ( //Nuevo perfil
                    <Tooltip title={permisos.Botones["1"].Nombre} arrow>
                        <IconButton onClick={handleClickNuevoPerfil}>
                            <AddRoundedIcon className="color-0" />
                        </IconButton>
                    </Tooltip>
                )}
                {permisos.Botones["2"] !== undefined && ( //Editar perfil
                    <Tooltip title={permisos.Botones["2"].Nombre} arrow>
                        <IconButton onClick={handleClickEditarPerfil}>
                            <EditIcon className="color-0" />
                        </IconButton>
                    </Tooltip>
                )}
                {permisos.Botones["3"] !== undefined && ( //Eliminar perfil
                    <Tooltip title={permisos.Botones["3"].Nombre} arrow>
                        <IconButton onClick={handleClickEliminarPerfil}>
                            <DeleteIcon className="color-0" />
                        </IconButton>
                    </Tooltip>
                )}
                {permisos.Botones["4"] !== undefined && ( //Administrar permisos del perfil
                    <Tooltip title={permisos.Botones["4"].Nombre} arrow>
                        <IconButton onClick={handleClickPermisosPerfil}>
                            <VerifiedUserIcon className="color-0" />
                        </IconButton>
                    </Tooltip>
                )}
                {permisos.Botones["5"] !== undefined && ( //Actualizar tabla
                    <Tooltip title={permisos.Botones["5"].Nombre} arrow>
                        <IconButton onClick={getData}>
                            <ReplayIcon className="color-0" />
                        </IconButton>
                    </Tooltip>
                )}
            </MeditocHeader1>
            <MeditocBody>
                <MeditocTable
                    columns={columns}
                    data={listaPerfiles}
                    rowSelected={perfilSeleccionado}
                    setRowSelected={setPerfilSeleccionado}
                    mainField="iIdPerfil"
                    isLoading={false}
                    doubleClick={permisos.Botones["4"] !== undefined ? handleClickPermisosPerfil : emptyFunc}
                />
            </MeditocBody>
            <FormPerfil
                entPerfil={perfilForModalForm}
                open={modalFormPerfilOpen}
                setOpen={setModalFormPerfilOpen}
                funcGetPerfiles={funcGetPerfiles}
                usuarioSesion={usuarioSesion}
                funcLoader={funcLoader}
                funcAlert={funcAlert}
            />
            <MeditocConfirmacion
                title="Eliminar perfil"
                open={modalFormEliminarPerfilOpen}
                setOpen={setModalFormEliminarPerfilOpen}
                okFunc={funcEliminarPerfil}
            >
                ¿Desea eliminar el perfil {perfilSeleccionado.sNombre} y todos sus permisos?
            </MeditocConfirmacion>
            <Permisos
                entPerfil={perfilForModalForm}
                listaSistema={listaSistema}
                open={modalFormPermisosOpen}
                setOpen={setModalFormPermisosOpen}
                usuarioSesion={usuarioSesion}
                permisos={permisos}
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
