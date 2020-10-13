import { IconButton, Tooltip, Typography } from "@material-ui/core";
import React, { Fragment } from "react";

import CGUController from "../../../../controllers/CGUController";
import ColaboradorController from "../../../../controllers/ColaboradorController";
import DeleteIcon from "@material-ui/icons/Delete";
import DetalleColaborador from "./DetalleColaborador";
import EditIcon from "@material-ui/icons/Edit";
import EmailIcon from "@material-ui/icons/Email";
import { EnumTipoDoctor } from "../../../../configurations/enumConfig";
import EspecialidadController from "../../../../controllers/EspecialidadController";
import FormColaborador from "./FormColaborador";
import FotoColaborador from "./FotoColaborador";
import GroupAddIcon from "@material-ui/icons/GroupAdd";
import InsertPhotoIcon from "@material-ui/icons/InsertPhoto";
import MeditocBody from "../../../utilidades/MeditocBody";
import MeditocConfirmacion from "../../../utilidades/MeditocConfirmacion";
import MeditocHeader1 from "../../../utilidades/MeditocHeader1";
import MeditocTable from "../../../utilidades/MeditocTable";
import PersonAddIcon from "@material-ui/icons/PersonAdd";
import PropTypes from "prop-types";
import ReplayIcon from "@material-ui/icons/Replay";
import VisibilityIcon from "@material-ui/icons/Visibility";
import { cellProps } from "../../../../configurations/dataTableIconsConfig";
import { emptyFunc } from "../../../../configurations/preventConfig";
import { useEffect } from "react";
import { useState } from "react";

/*************************************************************
 * Descripcion: Contenido de la vista principal de Colaboradores
 * Creado: Cristopher Noh
 * Fecha: 26/08/2020
 * Invocado desde: ContentMain
 *************************************************************/
const Colaboradores = (props) => {
    //PROPS
    const { usuarioSesion, permisos, funcLoader, funcAlert } = props;

    //CONTROLLERS
    const especialidadController = new EspecialidadController();
    const colaboradorController = new ColaboradorController();

    //CONSTANTES
    const columns = [
        { title: "ID", field: "iIdColaborador", hidden: true, ...cellProps },
        { title: "Tipo de doctor", field: "sTipoDoctor", ...cellProps },
        { title: "Especialidad", field: "sEspecialidad", ...cellProps },
        { title: "Nombre", field: "sNombreDirectorio", ...cellProps },
        { title: "Usuario", field: "sUsuarioTitular", ...cellProps },
        { title: "Sala", field: "iNumSala", ...cellProps },
        { title: "Acceso", field: "sAcceso", ...cellProps },
    ];

    const colaboradorCallCenterEntidadVacia = {
        iIdColaborador: 0,
        iIdTipoDoctor: EnumTipoDoctor.CallCenter,
        sNombreDirectorio: "",
        sNombreConsultorio: "",
        sCedulaProfecional: "",
        sRFC: "",
        sTelefonoDirectorio: "",
        sCorreoDirectorio: "",
        sWhatsApp: "",
        iIdEspecialidad: "",
        iNumSala: "",
        sDireccionConsultorio: "",
        sURL: "",
        sMaps: "",
        sNombresDoctor: "",
        sApellidoPaternoDoctor: "",
        sApellidoMaternoDoctor: "",
        dtFechaNacimientoDoctor: null,
        sTelefonoDoctor: "",
        sCorreoDoctor: "",
        sDomicilioDoctor: "",
        sUsuarioTitular: "",
        sUsuarioAdministrativo: "",
        bAdministrador: false,
        bAcceso: false,
    };

    const colaboradorEspecialistaEntidadVacia = {
        iIdColaborador: 0,
        iIdTipoDoctor: EnumTipoDoctor.Especialista,
        sNombreDirectorio: "",
        sNombreConsultorio: "",
        sCedulaProfecional: "",
        sRFC: "",
        sTelefonoDirectorio: "",
        sWhatsApp: "",
        sCorreoDirectorio: "",
        iIdEspecialidad: "",
        iNumSala: "",
        sDireccionConsultorio: "",
        sURL: "",
        sMaps: "",
        sNombresDoctor: "",
        sApellidoPaternoDoctor: "",
        sApellidoMaternoDoctor: "",
        dtFechaNacimientoDoctor: null,
        sTelefonoDoctor: "",
        sCorreoDoctor: "",
        sDomicilioDoctor: "",
        sUsuarioTitular: "",
        sUsuarioAdministrativo: "",
        bAdministrador: false,
        bAcceso: false,
    };

    //STATES
    const [openNuevoColaborador, setOpenNuevoColaborador] = useState(false);
    const [openDetalleColaborador, setOpenDetalleColaborador] = useState(false);
    const [openEliminarColaborador, setOpenEliminarColaborador] = useState(false);
    const [openFotoColaborador, setOpenFotoColaborador] = useState(false);

    const [listaEspecialidades, setListaEspecialidades] = useState([]);
    const [listaColaboradores, setListaColaboradores] = useState([]);

    const [colaboradorSeleccionado, setColaboradorSeleccionado] = useState(colaboradorCallCenterEntidadVacia);
    const [colaboradorCrearEditar, setColaboradorCrearEditar] = useState(colaboradorCallCenterEntidadVacia);

    //HANDLERS
    //Nuevo colaborador callcenter
    const handleClickNuevoColaboradorCallCenter = () => {
        setColaboradorCrearEditar(colaboradorCallCenterEntidadVacia);
        setOpenNuevoColaborador(true);
    };

    //Nuevo colaborador especialista
    const handleClickNuevoColaboradorEspecialista = () => {
        setColaboradorCrearEditar(colaboradorEspecialistaEntidadVacia);
        setOpenNuevoColaborador(true);
    };

    //Ver deralle del colaborador
    const handleClickDetalleColaborador = () => {
        if (colaboradorSeleccionado.iIdColaborador === 0) {
            funcAlert("Seleccione un colaborador de la tabla para continuar");
            return;
        }
        setOpenDetalleColaborador(true);
    };

    //Editar los datos del colaborador
    const handleClickEditarColaborador = () => {
        if (colaboradorSeleccionado.iIdColaborador === 0) {
            funcAlert("Seleccione un colaborador de la tabla para continuar");
            return;
        }
        setColaboradorCrearEditar(colaboradorSeleccionado);
        setOpenNuevoColaborador(true);
    };

    //Abrir administrador de foto del colaborador
    const handleClickFotoColaborador = () => {
        if (colaboradorSeleccionado.iIdColaborador === 0) {
            funcAlert("Seleccione un colaborador de la tabla para continuar");
            return;
        }
        setOpenFotoColaborador(true);
        setColaboradorCrearEditar(colaboradorSeleccionado);
    };

    //Eliminar un colaborador
    const handleClickEliminarColaborador = () => {
        if (colaboradorSeleccionado.iIdColaborador === 0) {
            funcAlert("Seleccione un colaborador de la tabla para continuar");
            return;
        }
        setOpenEliminarColaborador(true);
    };

    //Reenviar las credenciales del colaborador
    const handleClickReenviarCredenciales = async () => {
        if (colaboradorSeleccionado.iIdColaborador === 0) {
            funcAlert("Seleccione un colaborador de la tabla para continuar");
            return;
        }

        funcLoader(true, "Reenviando credenciales...");

        const entUsuarioReenviar = {
            sCorreo: colaboradorSeleccionado.sCorreoDoctor,
        };

        const cguController = new CGUController();
        const response = await cguController.funcRecuperarPassword(entUsuarioReenviar);
        if (response.Code === 0) {
            funcAlert(response.Message, "success");
        } else {
            funcAlert(response.Message);
        }
        funcLoader();
    };

    //Obtener el registro de colaboradores y especialidades
    const handleClickGetData = async () => {
        await fnObtenerColaboradores();
        await fnObtenerEspecialidades();
    };

    //FUNCIONES
    //Consumir API para consultar las especialidades activas
    const fnObtenerEspecialidades = async () => {
        funcLoader(true, "Consultando especialidades...");

        const response = await especialidadController.funcGetEspecialidad();

        if (response.Code === 0) {
            // let especialidades = [...response.Result];
            // especialidades.sor
            setListaEspecialidades(response.Result);
        } else {
            funcAlert(response.Message);
        }

        funcLoader();
    };

    //Consumir API para obtener la lista de colaboradores
    const fnObtenerColaboradores = async () => {
        funcLoader(true, "Consultando colaboradores...");

        const response = await colaboradorController.funcGetColaboradores();
        if (response.Code === 0) {
            setListaColaboradores(response.Result);
        } else {
            funcAlert(response.Message);
        }
        funcLoader();
    };

    //Consumir API para eliminar la foto del colaborador
    const fnEliminarColaborador = async () => {
        funcLoader(true, "Eliminando colaboradores...");

        const response = await colaboradorController.funcSaveColaborador({
            iIdColaborador: colaboradorSeleccionado.iIdColaborador,
            iIdTipoDoctor: colaboradorSeleccionado.iIdTipoDoctor,
            iIdUsuarioCGU: colaboradorSeleccionado.iIdUsuarioCGU,
            iIdUsuarioMod: usuarioSesion.iIdUsuario,
            bActivo: false,
            bBaja: true,
        });

        if (response.Code === 0) {
            setOpenEliminarColaborador(false);
            setColaboradorSeleccionado(colaboradorCallCenterEntidadVacia);
            await fnObtenerColaboradores();
            funcAlert(response.Message, "success");
        } else {
            funcAlert(response.Message);
        }
        funcLoader();
    };

    //EFFECTS
    //Consultar información al cargar esta vista
    useEffect(() => {
        handleClickGetData();
        // eslint-disable-next-line
    }, []);

    return (
        <Fragment>
            <MeditocHeader1 title={permisos.Nombre}>
                {permisos.Botones["1"] !== undefined && ( //Nuevo Colaborador CallCenter
                    <Tooltip title={permisos.Botones["1"].Nombre} arrow>
                        <IconButton onClick={handleClickNuevoColaboradorCallCenter}>
                            <PersonAddIcon className="color-0" />
                        </IconButton>
                    </Tooltip>
                )}
                {permisos.Botones["2"] !== undefined && ( //Nuevo Colaborador Especialista
                    <Tooltip title={permisos.Botones["2"].Nombre} arrow>
                        <IconButton onClick={handleClickNuevoColaboradorEspecialista}>
                            <GroupAddIcon className="color-0" />
                        </IconButton>
                    </Tooltip>
                )}
                {permisos.Botones["3"] !== undefined && ( //Ver detalles de colaborador
                    <Tooltip title={permisos.Botones["3"].Nombre} arrow>
                        <IconButton onClick={handleClickDetalleColaborador}>
                            <VisibilityIcon className="color-0" />
                        </IconButton>
                    </Tooltip>
                )}
                {permisos.Botones["4"] !== undefined && ( //Editar datos de colaborador
                    <Tooltip title={permisos.Botones["4"].Nombre} arrow>
                        <IconButton onClick={handleClickEditarColaborador}>
                            <EditIcon className="color-0" />
                        </IconButton>
                    </Tooltip>
                )}
                {permisos.Botones["5"] !== undefined && ( //Foto del colaborador
                    <Tooltip title={permisos.Botones["5"].Nombre} arrow>
                        <IconButton onClick={handleClickFotoColaborador}>
                            <InsertPhotoIcon className="color-0" />
                        </IconButton>
                    </Tooltip>
                )}
                {permisos.Botones["6"] !== undefined && ( //Eliminar de colaborador
                    <Tooltip title={permisos.Botones["6"].Nombre} arrow>
                        <IconButton onClick={handleClickEliminarColaborador}>
                            <DeleteIcon className="color-0" />
                        </IconButton>
                    </Tooltip>
                )}
                {permisos.Botones["7"] !== undefined && ( //Actualizar tabla
                    <Tooltip title={permisos.Botones["7"].Nombre} arrow>
                        <IconButton onClick={handleClickGetData}>
                            <ReplayIcon className="color-0" />
                        </IconButton>
                    </Tooltip>
                )}
                {permisos.Botones["10"] !== undefined && ( //Reenviar credenciales
                    <Tooltip title={permisos.Botones["10"].Nombre} arrow>
                        <IconButton onClick={handleClickReenviarCredenciales}>
                            <EmailIcon className="color-0" />
                        </IconButton>
                    </Tooltip>
                )}
            </MeditocHeader1>
            <MeditocBody>
                <MeditocTable
                    columns={columns}
                    data={listaColaboradores}
                    rowSelected={colaboradorSeleccionado}
                    setRowSelected={setColaboradorSeleccionado}
                    mainField="iIdColaborador"
                    doubleClick={permisos.Botones["3"] !== undefined ? handleClickDetalleColaborador : emptyFunc}
                />
            </MeditocBody>
            <FormColaborador
                entColaborador={colaboradorCrearEditar}
                open={openNuevoColaborador}
                setOpen={setOpenNuevoColaborador}
                listaEspecialidades={listaEspecialidades}
                funcGetColaboradores={fnObtenerColaboradores}
                usuarioSesion={usuarioSesion}
                funcLoader={funcLoader}
                funcAlert={funcAlert}
            />
            <DetalleColaborador
                entColaborador={colaboradorSeleccionado}
                open={openDetalleColaborador}
                setOpen={setOpenDetalleColaborador}
            />
            <FotoColaborador
                entColaborador={colaboradorCrearEditar}
                open={openFotoColaborador}
                setOpen={setOpenFotoColaborador}
                usuarioSesion={usuarioSesion}
                permisos={permisos}
                funcLoader={funcLoader}
                funcAlert={funcAlert}
            />
            <MeditocConfirmacion
                title="Eliminar colaborador"
                open={openEliminarColaborador}
                setOpen={setOpenEliminarColaborador}
                okFunc={fnEliminarColaborador}
            >
                ¿Desea eliminar el colaborador seleccionado?
                <br />
                <br />
                <Typography variant="body2">El colaborador perderá el acceso al portal de Meditoc</Typography>
            </MeditocConfirmacion>
        </Fragment>
    );
};

Colaboradores.propTypes = {
    funcAlert: PropTypes.func,
    funcLoader: PropTypes.func,
    permisos: PropTypes.shape({
        Botones: PropTypes.object,
        Nombre: PropTypes.string,
    }),
    usuarioSesion: PropTypes.shape({
        iIdUsuario: PropTypes.number,
    }),
};

export default Colaboradores;
