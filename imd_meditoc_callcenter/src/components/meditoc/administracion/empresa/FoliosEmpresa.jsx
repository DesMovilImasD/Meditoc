import React from "react";
import MeditocFullModal from "../../../utilidades/MeditocFullModal";
import { Tooltip, IconButton } from "@material-ui/core";
import NoteAddRoundedIcon from "@material-ui/icons/NoteAddRounded";
import CloseRoundedIcon from "@material-ui/icons/CloseRounded";
import EventAvailableRoundedIcon from "@material-ui/icons/EventAvailableRounded";
import DeleteIcon from "@material-ui/icons/Delete";
import { useState } from "react";
import MeditocBody from "../../../utilidades/MeditocBody";
import MeditocTable from "../../../utilidades/MeditocTable";
import CrearFolios from "./CrearFolios";
import ModificarVigencia from "./ModificarVigencia";
import MeditocConfirmacion from "../../../utilidades/MeditocConfirmacion";
import MeditocHeader2 from "../../../utilidades/MeditocHeader2";
import MeditocHeader3 from "../../../utilidades/MeditocHeader3";
import FolioController from "../../../../controllers/FolioController";
import CreateNewFolderIcon from "@material-ui/icons/CreateNewFolder";
import { useEffect } from "react";
import FormCargarArchivo from "./FormCargarArchivo";
import GetAppIcon from "@material-ui/icons/GetApp";

const FoliosEmpresa = (props) => {
    const { entEmpresa, open, setOpen, listaProductos, usuarioSesion, funcLoader, funcAlert } = props;

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
                        <Tooltip title="Crear folios a empresa" arrow>
                            <IconButton onClick={handleClickAgregarFolios}>
                                <NoteAddRoundedIcon className="color-1" />
                            </IconButton>
                        </Tooltip>
                        <Tooltip title="Modificar vigencia a folios seleccionados" arrow>
                            <IconButton onClick={handleClickModificarVigencia}>
                                <EventAvailableRoundedIcon className="color-1" />
                            </IconButton>
                        </Tooltip>
                        <Tooltip title="Cargar folios desde archivo" arrow>
                            <IconButton onClick={handleClickSubirArchivo}>
                                <CreateNewFolderIcon className="color-1" />
                            </IconButton>
                        </Tooltip>
                        <Tooltip title="Descargar plantilla para cargar folios desde archivo" arrow>
                            <IconButton onClick={handleClickDescargarPlantilla}>
                                <GetAppIcon className="color-1" />
                            </IconButton>
                        </Tooltip>
                        <Tooltip title="Eliminar folios seleccionados" arrow>
                            <IconButton onClick={handleClickEliminarFolios}>
                                <DeleteIcon className="color-1" />
                            </IconButton>
                        </Tooltip>
                    </MeditocHeader3>
                    <MeditocTable
                        columns={columns}
                        data={listaFoliosEmpresa}
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

export default FoliosEmpresa;
