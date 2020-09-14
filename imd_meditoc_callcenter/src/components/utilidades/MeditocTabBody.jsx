import React from "react";
import SwipeableViews from "react-swipeable-views";
import theme from "../../configurations/themeConfig";

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

export default MeditocTabBody;
