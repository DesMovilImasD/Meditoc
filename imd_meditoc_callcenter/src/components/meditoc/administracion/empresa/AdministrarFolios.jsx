import { IconButton, Tooltip } from "@material-ui/core";

import CrearFolios from "./CrearFolios";
import CreateNewFolderIcon from "@material-ui/icons/CreateNewFolder";
import DeleteIcon from "@material-ui/icons/Delete";
import EventAvailableRoundedIcon from "@material-ui/icons/EventAvailableRounded";
import FolioController from "../../../../controllers/FolioController";
import FormCargarArchivo from "./FormCargarArchivo";
import GetAppIcon from "@material-ui/icons/GetApp";
import MeditocBody from "../../../utilidades/MeditocBody";
import MeditocConfirmacion from "../../../utilidades/MeditocConfirmacion";
import MeditocFullModal from "../../../utilidades/MeditocFullModal";
import MeditocHeader2 from "../../../utilidades/MeditocHeader2";
import MeditocHeader3 from "../../../utilidades/MeditocHeader3";
import MeditocTable from "../../../utilidades/MeditocTable";
import ModificarVigencia from "./ModificarVigencia";
import NoteAddRoundedIcon from "@material-ui/icons/NoteAddRounded";
import PropTypes from "prop-types";
import React from "react";
import ReplayIcon from "@material-ui/icons/Replay";
import { useEffect } from "react";
import { useState } from "react";

const AdministrarFolios = (props) => {
    const { entEmpresa, open, setOpen, listaProductos, usuarioSesion, permisos, funcLoader, funcAlert } = props;

    const folioController = new FolioController();

    const columns = [
        { title: "ID", field: "iIdFolio", align: "center", hidden: true },
        { title: "Folio", field: "sFolio", align: "center" },
        { title: "Producto", field: "sNombreProducto", align: "center" },
        { title: "Origen", field: "sOrigen", align: "center" },
        { title: "Creado", field: "sFechaCreacion", align: "center" },
        { title: "Vencimiento", field: "sFechaVencimiento", align: "center" },
    ];

    const [listaFoliosEmpresa, setListaFoliosEmpresa] = useState([]);
    const [foliosEmpresaSeleccionado, setFoliosEmpresaSeleccionado] = useState([]);

    const [modalAgregarFoliosOpen, setModalAgregarFoliosOpen] = useState(false);
    const [modalAgregarVigenciaOpen, setModalAgregarVigenciaOpen] = useState(false);
    const [modalEliminarFoliosOpen, setModalEliminarFoliosOpen] = useState(false);

    const [formSubirArchivoOpen, setFormSubirArchivoOpen] = useState(false);
    const handleClickSubirArchivo = () => {
        setFormSubirArchivoOpen(true);
    };

    const handleClickAgregarFolios = () => {
        setModalAgregarFoliosOpen(true);
    };

    const handleClickModificarVigencia = () => {
        if (foliosEmpresaSeleccionado.length < 1) {
            funcAlert("Seleccione al menos un folio de la tabla para continuar");
            return;
        }
        setModalAgregarVigenciaOpen(true);
    };

    const handleClickEliminarFolios = () => {
        if (foliosEmpresaSeleccionado.length < 1) {
            funcAlert("Seleccione al menos un folio de la tabla para continuar");
            return;
        }
        setModalEliminarFoliosOpen(true);
    };

    const funcGetFoliosEmpresa = async () => {
        funcLoader(true, "Consultando folios de empresa...");

        const response = await folioController.funcGetFolios(null, entEmpresa.iIdEmpresa);

        if (response.Code === 0) {
            setListaFoliosEmpresa(response.Result);
        } else {
            funcAlert(response.Message);
        }

        funcLoader();
    };

    const funcEliminarFolios = async () => {
        funcLoader(true, "Eliminando folios...");

        const entFolioFVSubmit = {
            iIdEmpresa: entEmpresa.iIdEmpresa,
            iIdUsuario: usuarioSesion.iIdUsuario,
            lstFolios: foliosEmpresaSeleccionado.map((folio) => ({ iIdFolio: folio.iIdFolio })),
        };

        const response = await folioController.funcEliminarFoliosEmpresa(entFolioFVSubmit);

        if (response.Code === 0) {
            setFoliosEmpresaSeleccionado([]);
            setModalEliminarFoliosOpen(false);
            funcAlert(response.Message, "success");
            await funcGetFoliosEmpresa();
        } else {
            funcAlert(response.Message);
        }
        funcLoader();
    };

    const handleClickDescargarPlantilla = async () => {
        funcLoader(true, "Descargando plantilla...");

        const response = await folioController.funcDescargarPlantilla();

        if (response.Code === 0) {
            funcAlert(response.Message, "success");
        } else {
            funcAlert(response.Message);
        }
        funcLoader();
    };

    useEffect(() => {
        if (open === true) {
            funcGetFoliosEmpresa();
        }
        // eslint-disable-next-line
    }, [entEmpresa]);

    return (
        <MeditocFullModal open={open} setOpen={setOpen}>
            <div>
                <MeditocHeader2
                    title={`Administrar folios de ${entEmpresa.sNombre} (${entEmpresa.sFolioEmpresa})`}
                    setOpen={setOpen}
                />
                <MeditocBody>
                    <MeditocHeader3 title={`Folios generados: ${listaFoliosEmpresa.length}`}>
                        {permisos.Botones["5"] !== undefined && ( //Crear folios a empresa
                            <Tooltip title={permisos.Botones["5"].Nombre} arrow>
                                <IconButton onClick={handleClickAgregarFolios}>
                                    <NoteAddRoundedIcon className="color-1" />
                                </IconButton>
                            </Tooltip>
                        )}
                        {permisos.Botones["6"] !== undefined && ( //Modificar vigencia a folios seleccionados
                            <Tooltip title={permisos.Botones["6"].Nombre} arrow>
                                <IconButton onClick={handleClickModificarVigencia}>
                                    <EventAvailableRoundedIcon className="color-1" />
                                </IconButton>
                            </Tooltip>
                        )}
                        {permisos.Botones["7"] !== undefined && ( //Cargar folios desde archivo
                            <Tooltip title={permisos.Botones["7"].Nombre} arrow>
                                <IconButton onClick={handleClickSubirArchivo}>
                                    <CreateNewFolderIcon className="color-1" />
                                </IconButton>
                            </Tooltip>
                        )}
                        {permisos.Botones["8"] !== undefined && ( //Descargar plantilla para cargar folios desde archivo
                            <Tooltip title={permisos.Botones["8"].Nombre} arrow>
                                <IconButton onClick={handleClickDescargarPlantilla}>
                                    <GetAppIcon className="color-1" />
                                </IconButton>
                            </Tooltip>
                        )}
                        {permisos.Botones["9"] !== undefined && ( //Eliminar folios seleccionados
                            <Tooltip title={permisos.Botones["9"].Nombre} arrow>
                                <IconButton onClick={handleClickEliminarFolios}>
                                    <DeleteIcon className="color-1" />
                                </IconButton>
                            </Tooltip>
                        )}
                        {permisos.Botones["10"] !== undefined && ( //Actualizar tabla de folios
                            <Tooltip title={permisos.Botones["10"].Nombre} arrow>
                                <IconButton onClick={funcGetFoliosEmpresa}>
                                    <ReplayIcon className="color-1" />
                                </IconButton>
                            </Tooltip>
                        )}
                    </MeditocHeader3>
                    <MeditocTable
                        columns={columns}
                        data={listaFoliosEmpresa}
                        setData={setListaFoliosEmpresa}
                        rowsSelected={foliosEmpresaSeleccionado}
                        setRowsSelected={setFoliosEmpresaSeleccionado}
                        selection={true}
                        mainField="iIdFolio"
                    />
                </MeditocBody>
            </div>
            <CrearFolios
                entEmpresa={entEmpresa}
                open={modalAgregarFoliosOpen}
                setOpen={setModalAgregarFoliosOpen}
                listaProductos={listaProductos}
                funcGetFoliosEmpresa={funcGetFoliosEmpresa}
                usuarioSesion={usuarioSesion}
                funcLoader={funcLoader}
                funcAlert={funcAlert}
            />
            <ModificarVigencia
                entEmpresa={entEmpresa}
                open={modalAgregarVigenciaOpen}
                setOpen={setModalAgregarVigenciaOpen}
                foliosEmpresaSeleccionado={foliosEmpresaSeleccionado}
                funcGetFoliosEmpresa={funcGetFoliosEmpresa}
                usuarioSesion={usuarioSesion}
                funcLoader={funcLoader}
                funcAlert={funcAlert}
            />
            <FormCargarArchivo
                entEmpresa={entEmpresa}
                listaProductos={listaProductos}
                open={formSubirArchivoOpen}
                setOpen={setFormSubirArchivoOpen}
                funcGetFoliosEmpresa={funcGetFoliosEmpresa}
                usuarioSesion={usuarioSesion}
                funcLoader={funcLoader}
                funcAlert={funcAlert}
            />
            <MeditocConfirmacion
                title="Eliminar folios"
                open={modalEliminarFoliosOpen}
                setOpen={setModalEliminarFoliosOpen}
                okFunc={funcEliminarFolios}
            >
                ¿Desea eliminar los folios seleccionados?
            </MeditocConfirmacion>
        </MeditocFullModal>
    );
};

AdministrarFolios.propTypes = {
    entEmpresa: PropTypes.shape({
        iIdEmpresa: PropTypes.any,
        sFolioEmpresa: PropTypes.any,
        sNombre: PropTypes.any,
    }),
    funcAlert: PropTypes.func,
    funcLoader: PropTypes.func,
    listaProductos: PropTypes.any,
    open: PropTypes.bool,
    setOpen: PropTypes.any,
    usuarioSesion: PropTypes.shape({
        iIdUsuario: PropTypes.any,
    }),
};

export default AdministrarFolios;
