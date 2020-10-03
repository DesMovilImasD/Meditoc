import PropTypes from "prop-types";
import React from "react";
import SwipeableViews from "react-swipeable-views";

const MeditocTabBody = (props) => {
    const { index, setIndex, children } = props;

    const handleChangeTab = (e, value) => {
        setIndex(value);
    };

    return (
        <SwipeableViews index={index} onChangeIndex={handleChangeTab}>
            {children}
        </SwipeableViews>
    );
};

MeditocTabBody.propTypes = {
    children: PropTypes.any,
    index: PropTypes.any,
    setIndex: PropTypes.func,
};

export default MeditocTabBody;
