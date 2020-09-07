import React from "react";
import { Grid } from "@material-ui/core";

const MeditocTabPanel = (props) => {
    const { id, index, children } = props;

    return (
        <div hidden={id !== index} style={{ padding: 12, marginTop: 10 }}>
            {id === index && children}
        </div>
    );
};

export default MeditocTabPanel;
