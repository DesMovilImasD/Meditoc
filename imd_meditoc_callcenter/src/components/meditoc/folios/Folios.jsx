import { IconButton, Tooltip } from "@material-ui/core";
import React, { Fragment } from "react";

import DetalleFolio from "./DetalleFolio";
import FolioController from "../../../controllers/FolioController";
import MeditocBody from "../../utilidades/MeditocBody";
import MeditocHeader1 from "../../utilidades/MeditocHeader1";
import MeditocTable from "../../utilidades/MeditocTable";
import PropTypes from "prop-types";
import ReplayIcon from "@material-ui/icons/Replay";
import VisibilityIcon from "@material-ui/icons/Visibility";
import { useEffect } from "react";
import { useState } from "react";

const Folios = (props) => {
    const { funcLoader, funcAlert, title } = props;

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
    const [folioSeleccionado, setFolioSeleccionado] = useState({ iIdFolio: 0 });

    const [modalDetalleFolioOpen, setModalDetalleFolioOpen] = useState(false);

    const handleClickDetalleFolio = () => {
        if (folioSeleccionado.iIdFolio === 0) {
            funcAlert("Seleccione un folio para continuar");
            return;
        }
        setModalDetalleFolioOpen(true);
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

    useEffect(() => {
        funcGetFolios();
        // eslint-disable-next-line
    }, []);

    return (
        <Fragment>
            <MeditocHeader1 title={title}>
                <Tooltip title="Detalle de folio" arrow>
                    <IconButton onClick={handleClickDetalleFolio}>
                        <VisibilityIcon className="color-0" />
                    </IconButton>
                </Tooltip>
                <Tooltip title="Actualizar tabla" arrow>
                    <IconButton onClick={funcGetFolios}>
                        <ReplayIcon className="color-0" />
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
                    doubleClick={handleClickDetalleFolio}
                />
            </MeditocBody>
            <DetalleFolio
                entFolio={folioSeleccionado}
                open={modalDetalleFolioOpen}
                setOpen={setModalDetalleFolioOpen}
            />
        </Fragment>
    );
};

Folios.propTypes = {
    funcAlert: PropTypes.func,
    funcLoader: PropTypes.func,
    title: PropTypes.any,
};

export default Folios;
