import { IconButton, Tooltip } from "@material-ui/core";
import React, { Fragment } from "react";
import MeditocHeader1 from "../../utilidades/MeditocHeader1";
import CloudUploadIcon from "@material-ui/icons/CloudUpload";
import MeditocBody from "../../utilidades/MeditocBody";
import { useState } from "react";
import FolioController from "../../../controllers/FolioController";
import { useEffect } from "react";
import MeditocTable from "../../utilidades/MeditocTable";
import FolioCargarArchivo from "./FolioCargarArchivo";
import GetAppIcon from "@material-ui/icons/GetApp";
import PublishIcon from "@material-ui/icons/Publish";
import CreateNewFolderIcon from "@material-ui/icons/CreateNewFolder";

const Folios = (props) => {
    const { usuarioSesion, funcLoader, funcAlert, title } = props;

    const foliosController = new FolioController();

    const columns = [
        { title: "Folio", field: "sFolio", align: "center" },
        { title: "Origen", field: "sOrigen", align: "center" },
        { title: "Empresa", field: "sFolioEmpresa", align: "center" },
        { title: "Paciente", field: "sNombrePaciente", align: "center" },
        { title: "Correo", field: "sCorreoPaciente", align: "center" },
        { title: "Creado", field: "sFechaCreacion", align: "center" },
        { title: "Vencimiento", field: "sFechaVencimiento", align: "center" },
    ];

    const [listaFolios, setListaFolios] = useState([]);
    const [folioSeleccionado, setFolioSeleccionado] = useState({});

    const [formSubirArchivoOpen, setFormSubirArchivoOpen] = useState(false);

    const handleClickSubirArchivo = () => {
        setFormSubirArchivoOpen(true);
    };

    const funcGetFolios = async () => {
        funcLoader(true, "Consultando folios...");

        const response = await foliosController.funcGetFolios(null, null, null, null, "", "", null, "", "");

        if (response.Code === 0) {
            setListaFolios(response.Result);
        } else {
            funcAlert(response.Message);
        }

        funcLoader();
    };

    const handleClickDescargarPlantilla = async () => {
        funcLoader(true, "Descargando plantilla...");

        const response = await foliosController.funcDescargarPlantilla();

        if (response.Code === 0) {
            funcAlert(response.Message, "success");
        } else {
            funcAlert(response.Message);
        }
        funcLoader();
    };

    useEffect(() => {
        funcGetFolios();
    }, []);

    return (
        <Fragment>
            <MeditocHeader1 title={title}>
                <Tooltip title="Cargar folios de Venta Calle desde archivo" arrow>
                    <IconButton onClick={handleClickSubirArchivo}>
                        <CreateNewFolderIcon className="color-0" />
                    </IconButton>
                </Tooltip>
                <Tooltip title="Descargar plantilla para cargar folios de Venta Calle" arrow>
                    <IconButton onClick={handleClickDescargarPlantilla}>
                        <GetAppIcon className="color-0" />
                    </IconButton>
                </Tooltip>
            </MeditocHeader1>
            <MeditocBody>
                <MeditocTable
                    columns={columns}
                    data={listaFolios}
                    rowSelected={folioSeleccionado}
                    setRowSelected={setFolioSeleccionado}
                    mainField="sFolio"
                />
            </MeditocBody>
            <FolioCargarArchivo
                open={formSubirArchivoOpen}
                setOpen={setFormSubirArchivoOpen}
                funcGetFolios={funcGetFolios}
                usuarioSesion={usuarioSesion}
                funcLoader={funcLoader}
                funcAlert={funcAlert}
            />
        </Fragment>
    );
};

export default Folios;
