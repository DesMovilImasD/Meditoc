import React, { useState, Fragment, useEffect } from "react";
import { IconButton, Tooltip } from "@material-ui/core";
import AddIcon from "@material-ui/icons/Add";
import SistemaModulo from "./SistemaModulo";
import FormModulo from "./FormModulo";
import Simbologia from "./Simbologia";
import SubmoduloBarra from "../../SubmoduloBarra";
import SubmoduloContenido from "../../SubmoduloContenido";
import { lstSistemaTemp } from "../../../configurations/systemConfigTemp";

const Sistema = (props) => {
    const { listaSistema, setListaSistema } = props;

    const [modalAgregarModuloOpen, setModalAgregarModuloOpen] = useState(false);

    const handleAgregarModuloOpen = () => {
        setModalAgregarModuloOpen(true);
    };

    const entModuloNuevo = {
        iIdModulo: 0,
        sNombre: "",
    };

    useEffect(() => {
        setListaSistema(lstSistemaTemp);
    }, []);

    return (
        <Fragment>
            <SubmoduloBarra title="SISTEMA">
                <Tooltip title="Nuevo módulo" arrow>
                    <IconButton onClick={handleAgregarModuloOpen}>
                        <AddIcon className="color-0" />
                    </IconButton>
                </Tooltip>
            </SubmoduloBarra>

            <SubmoduloContenido>
                {listaSistema.map((modulo) => (
                    <SistemaModulo key={modulo.iIdModulo} modulo={modulo} />
                ))}
            </SubmoduloContenido>

            <Simbologia />

            <FormModulo entModulo={entModuloNuevo} open={modalAgregarModuloOpen} setOpen={setModalAgregarModuloOpen} />
        </Fragment>
    );
};

export default Sistema;
