import { IconButton, Paper, Tooltip } from "@material-ui/core";

import CloseRoundedIcon from "@material-ui/icons/CloseRounded";
import PropTypes from "prop-types";
import React from "react";

const MeditocHeader2 = (props) => {
    const { setOpen, title, actions } = props;

    const mtButtons = actions === undefined ? null : actions;

    //Funcion para cerrar este modal
    const handleClose = () => {
        setOpen(false);
    };

    return (
        <Paper elevation={3}>
            <div className="bar-main">
                <div className="ops-nor bold size-20 align-self-center flx-grw-1">{title}</div>
                <div>
                    {mtButtons}
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

MeditocHeader2.propTypes = {
    setOpen: PropTypes.func,
    title: PropTypes.any,
};

export default MeditocHeader2;
