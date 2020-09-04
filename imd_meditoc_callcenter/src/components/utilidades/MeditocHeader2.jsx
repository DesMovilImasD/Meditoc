import React from "react";
import { Paper } from "@material-ui/core";

const MeditocHeader2 = (props) => {
    const { children, title } = props;

    return (
        <Paper elevation={3}>
            <div className="bar-main">
                <div className="ops-nor bold size-25 align-self-center flx-grw-1">{title}</div>
                <div>{children}</div>
            </div>
        </Paper>
    );
};

export default MeditocHeader2;
