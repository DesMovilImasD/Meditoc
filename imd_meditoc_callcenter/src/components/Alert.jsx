import { useEffect } from "react";
import { useSnackbar } from "notistack";

/*************************************************************
 * Descripcion: Contiene el método y la vista para desplegar una alerta genérica
 * Creado: Cristopher Noh
 * Fecha: 26/08/2020
 * Invocado desde: App
 *************************************************************/
const Alert = (props) => {
    const { entAlert } = props;

    const { enqueueSnackbar } = useSnackbar();

    const handleShowAlert = () => {
        enqueueSnackbar(entAlert.message, { variant: entAlert.variant });
    };

    useEffect(() => {
        if (entAlert.message !== "") {
            handleShowAlert();
        }
        // eslint-disable-next-line
    }, [entAlert]);

    return "";
};

export default Alert;
