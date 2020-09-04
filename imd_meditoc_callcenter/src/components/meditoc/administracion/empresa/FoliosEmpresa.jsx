import React from "react";
import MeditocFullModal from "../../../utilidades/MeditocFullModal";
import { Tooltip, Button, IconButton, Typography } from "@material-ui/core";
import NoteAddRoundedIcon from "@material-ui/icons/NoteAddRounded";
import CloseRoundedIcon from "@material-ui/icons/CloseRounded";
import EventAvailableRoundedIcon from "@material-ui/icons/EventAvailableRounded";
import DeleteIcon from "@material-ui/icons/Delete";
import { useState } from "react";
import MeditocBody from "../../../utilidades/MeditocBody";
import MeditocTable from "../../../utilidades/MeditocTable";
import AgregarFolios from "./AgregarFolios";
import AgregarVigencia from "./AgregarVigencia";
import MeditocConfirmacion from "../../../utilidades/MeditocConfirmacion";
import MeditocHeader2 from "../../../utilidades/MeditocHeader2";
import MeditocHeader3 from "../../../utilidades/MeditocHeader3";

const FoliosEmpresa = (props) => {
    const { entEmpresa, open, setOpen, funcAlert } = props;

    const columns = [
        { title: "ID", field: "iIdFolio", align: "center" },
        { title: "Folio", field: "sFolio", align: "center" },
        { title: "Origen", field: "sOrigen", align: "center" },
        { title: "Fecha de Vencimiento", field: "dtFechaVencimiento", align: "center" },
    ];

    const data = [
        { iIdFolio: 1, sFolio: "VS0023456", sOrigen: "Empresas", dtFechaVencimiento: "03/09/2020" },
        { iIdFolio: 2, sFolio: "VS0023457", sOrigen: "Empresas", dtFechaVencimiento: "03/09/2020" },
        { iIdFolio: 3, sFolio: "VS0023458", sOrigen: "Empresas", dtFechaVencimiento: "03/09/2020" },
        { iIdFolio: 4, sFolio: "VS0023459", sOrigen: "Empresas", dtFechaVencimiento: "03/09/2020" },
    ];

    const [listaFoliosEmpresa, setListaFoliosEmpresa] = useState(data);

    const [foliosEmpresaSeleccionado, setFoliosEmpresaSeleccionado] = useState([]);

    const [modalAgregarFoliosOpen, setModalAgregarFoliosOpen] = useState(false);
    const [modalAgregarVigenciaOpen, setModalAgregarVigenciaOpen] = useState(false);
    const [modalEliminarFoliosOpen, setModalEliminarFoliosOpen] = useState(false);

    const handleClickAgregarFolios = () => {
        setModalAgregarFoliosOpen(true);
    };

    const handleClickAgregarVigencia = () => {
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

    //Funcion para cerrar este modal
    const handleClose = () => {
        setOpen(false);
    };

    return (
        <MeditocFullModal open={open} setOpen={setOpen}>
            <div>
                <MeditocHeader2 title={`Administrar folios de ${entEmpresa.sNombre} (${entEmpresa.sFolioEmpresa})`}>
                    <Tooltip title="Cerrar ventana" arrow>
                        <IconButton onClick={handleClose}>
                            <CloseRoundedIcon className="color-0" />
                        </IconButton>
                    </Tooltip>
                </MeditocHeader2>
                <MeditocBody>
                    <MeditocHeader3 title={`Folios generados: ${listaFoliosEmpresa.length}`}>
                        <Tooltip title="Crear folios a empresa" arrow>
                            <IconButton onClick={handleClickAgregarFolios}>
                                <NoteAddRoundedIcon className="color-1" />
                            </IconButton>
                        </Tooltip>
                        <Tooltip title="Modificar vigencia a folios seleccionados" arrow>
                            <IconButton onClick={handleClickAgregarVigencia}>
                                <EventAvailableRoundedIcon className="color-1" />
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
            <AgregarFolios entEmpresa={entEmpresa} open={modalAgregarFoliosOpen} setOpen={setModalAgregarFoliosOpen} />
            <AgregarVigencia
                entEmpresa={entEmpresa}
                open={modalAgregarVigenciaOpen}
                setOpen={setModalAgregarVigenciaOpen}
            />
            <MeditocConfirmacion
                title="Eliminar folios"
                open={modalEliminarFoliosOpen}
                setOpen={setModalEliminarFoliosOpen}
            >
                ¿Desea eliminar los folios seleccionados?
            </MeditocConfirmacion>
        </MeditocFullModal>
    );
};

export default FoliosEmpresa;
