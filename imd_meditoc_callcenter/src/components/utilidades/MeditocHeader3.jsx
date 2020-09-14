import React from "react";
import { Paper } from "@material-ui/core";

const MeditocHeader3 = (props) => {
    const { title, children } = props;
    return (
        <div className="bar-main-3">
            <div className="rob-nor size-25 align-self-center flx-grw-1">{title}</div>
            <div>{children}</div>
        </div>
    );
};

export default MeditocHeader3;
