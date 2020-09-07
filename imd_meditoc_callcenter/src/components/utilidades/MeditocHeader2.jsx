import React from "react";
import { Paper, Tooltip, IconButton } from "@material-ui/core";
import CloseRoundedIcon from "@material-ui/icons/CloseRounded";

const MeditocHeader2 = (props) => {
    const { setOpen, title } = props;

    //Funcion para cerrar este modal
    const handleClose = () => {
        setOpen(false);
    };

    return (
        <Paper elevation={3}>
            <div className="bar-main">
                <div className="ops-nor bold size-20 align-self-center flx-grw-1">{title}</div>
                <div>
                    <Tooltip title="Cerrar ventana" arrow>
                        <IconButton onClick={handleClose}>
                            <CloseRoundedIcon className="color-0" />
                        </IconButton>
                    </Tooltip>
                </div>
            </div>
        </Paper>
    );
};

export default MeditocHeader2;
