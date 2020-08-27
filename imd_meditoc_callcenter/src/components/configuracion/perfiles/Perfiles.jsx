import React, { Fragment, useState } from "react";
import SubmoduloBarra from "../../SubmoduloBarra";
import { Tooltip, IconButton, Paper } from "@material-ui/core";
import AddIcon from "@material-ui/icons/Add";
import SubmoduloContenido from "../../SubmoduloContenido";
import MeditocTable from "../../MeditocTable";

/*************************************************************
 * Descripcion: Submódulo para la vista principal "PERFILES" del portal Meditoc
 * Creado: Cristopher Noh
 * Fecha: 26/08/2020
 * Invocado desde: ContentMain
 *************************************************************/
const Perfiles = () => {
    const columns = [
        { title: "ID Perfil", field: "iIdPerfil", align: "center" },
        { title: "Descripción", field: "sNombre", align: "center" },
        { title: "Fecha de registro", field: "dtFechaCreacion", align: "center" },
    ];

    const data = [
        { iIdPerfil: 1, sNombre: "Superadministrador", dtFechaCreacion: "26-08-2020" },
        { iIdPerfil: 2, sNombre: "Administrador", dtFechaCreacion: "26-08-2020" },
        { iIdPerfil: 3, sNombre: "Doctor", dtFechaCreacion: "26-08-2020" },
        { iIdPerfil: 4, sNombre: "Especialista", dtFechaCreacion: "26-08-2020" },
    ];

    const [perfilSeleccionado, setPerfilSeleccionado] = useState({
        iIdPerfil: 0,
        sNombre: "",
        dtFechaCreacion: "",
    });

    return (
        <Fragment>
            <SubmoduloBarra title="PERFILES">
                <Tooltip title="Nuevo perfil" arrow>
                    <IconButton>
                        <AddIcon className="color-0" />
                    </IconButton>
                </Tooltip>
            </SubmoduloBarra>
            <SubmoduloContenido>
                <MeditocTable
                    columns={columns}
                    data={data}
                    rowSelected={perfilSeleccionado}
                    setRowSelected={setPerfilSeleccionado}
                    mainField="iIdPerfil"
                    isLoading={false}
                />
            </SubmoduloContenido>
        </Fragment>
    );
};

export default Perfiles;
