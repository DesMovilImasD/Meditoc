import { useEffect } from "react";
import { useSnackbar } from "notistack";

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
