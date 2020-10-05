import React, { useEffect } from "react";

import PropTypes from "prop-types";
import { useSnackbar } from "notistack";

/*************************************************************
 * Descripcion: Contiene el método y la vista para desplegar una alerta genérica
 * Creado: Cristopher Noh
 * Fecha: 26/08/2020
 * Invocado desde: App
 *************************************************************/
const MeditocAlert = (props) => {
    const { entAlert } = props;

    //Instancia para desplegar alertas
    const { enqueueSnackbar } = useSnackbar();

    //Funcion para mostrar una alerta
    const handleShowAlert = () => {
        enqueueSnackbar(entAlert.message, { variant: entAlert.variant });
    };

    //Desplegar alertas
    useEffect(() => {
        if (entAlert.message !== "") {
            handleShowAlert();
        }
        // eslint-disable-next-line
    }, [entAlert]);

    return <div></div>;
};

MeditocAlert.propTypes = {
    entAlert: PropTypes.shape({
        message: PropTypes.string,
        variant: PropTypes.string,
    }),
};

export default MeditocAlert;
