import PropTypes from "prop-types";
import React from "react";

const MeditocHeader3 = (props) => {
    const { title, children } = props;
    return (
        <div className="bar-main-3">
            <div className="rob-nor size-25 align-self-center flx-grw-1">{title}</div>
            <div>{children}</div>
        </div>
    );
};

MeditocHeader3.propTypes = {
    children: PropTypes.any,
    title: PropTypes.any,
};

export default MeditocHeader3;
