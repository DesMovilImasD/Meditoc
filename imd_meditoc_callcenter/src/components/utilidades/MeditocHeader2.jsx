import React from "react";

const MeditocHeader2 = (props) => {
    const { children, title } = props;

    return (
        <div className="bar-main">
            <div className="ops-nor bold size-25 align-self-center flx-grw-1">{title}</div>
            <div>{children}</div>
        </div>
    );
};

export default MeditocHeader2;
