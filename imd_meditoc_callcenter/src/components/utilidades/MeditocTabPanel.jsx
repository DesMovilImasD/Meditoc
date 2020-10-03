import PropTypes from "prop-types";
import React from "react";
const MeditocTabPanel = (props) => {
    const { id, index, children } = props;

    return (
        <div hidden={id !== index} style={{ padding: 12, marginTop: 10 }}>
            {id === index && children}
        </div>
    );
};

MeditocTabPanel.propTypes = {
    children: PropTypes.any,
    id: PropTypes.any,
    index: PropTypes.any,
};

export default MeditocTabPanel;
