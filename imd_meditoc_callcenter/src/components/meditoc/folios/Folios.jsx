import { IconButton, Tooltip } from "@material-ui/core";
import React, { Fragment } from "react";
import MeditocHeader1 from "../../utilidades/MeditocHeader1";
import AddRoundedIcon from "@material-ui/icons/AddRounded";
import MeditocBody from "../../utilidades/MeditocBody";
import { useState } from "react";
import FolioController from "../../../controllers/FolioController";
import { useEffect } from "react";
import MeditocTable from "../../utilidades/MeditocTable";

const Folios = (props) => {
    const { entUsuario, funcLoader, funcAlert } = props;

    const foliosController = new FolioController();

    const columns = [
        { title: "Folio", field: "sFolio", align: "center" },
        { title: "Origen", field: "sOrigen", align: "center" },
        { title: "Empresa", field: "sFolioEmpresa", align: "center" },
        { title: "Paciente", field: "sNombrePaciente", align: "center" },
        { title: "Creado", field: "sFechaCreacion", align: "center" },
        { title: "Vencimiento", field: "sFechaVencimiento", align: "center" },
    ];

    const [listaFolios, setListaFolios] = useState([]);
    const [folioSeleccionado, setFolioSeleccionado] = useState({});

    const funcGetFolios = async () => {
        funcLoader(true, "Consultando folios...");

        const response = await foliosController.funcGetFolios(null, null, null, null, "", "", null, null, null);

        if (response.Code === 0) {
            setListaFolios(response.Result);
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
            <MeditocHeader1 title="FOLIOS">
                <Tooltip title="Crear folio vacío" arrow>
                    <IconButton>
                        <AddRoundedIcon className="color-0" />
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
        </Fragment>
    );
};

export default Folios;
