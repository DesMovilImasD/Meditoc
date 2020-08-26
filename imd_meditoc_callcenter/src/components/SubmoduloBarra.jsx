import React from "react";

const SubmoduloBarra = (props) => {
    const { children, title } = props;

    return (
        <div className="bar-main">
            <div className="flx-grw-1">{children}</div>
            <div className="ops-nor bold size-20 align-self-center">{title}</div>
        </div>
    );
};

export default SubmoduloBarra;
