import PropTypes from "prop-types";
import React from "react";

const MeditocInfoField = (props) => {
    const { label, value } = props;
    return (
        <div>
            <span className="ops-nor bold color-3 size-15">{label}</span>
            <br />
            <span className="ops-nor color-4 size-15">{value}</span>
        </div>
    );
};

MeditocInfoField.propTypes = {
    label: PropTypes.any,
    value: PropTypes.any,
};

export default MeditocInfoField;
