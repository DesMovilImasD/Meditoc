import React from "react";

const SubmoduloContenido = (props) => {
    const { children } = props;
    return <div className="bar-content">{children}</div>;
};

export default SubmoduloContenido;
