import PropTypes from "prop-types";
import React from "react";
import { AppBar, Tabs, Tab } from "@material-ui/core";

const MeditocTabHeader = (props) => {
    const { index, setIndex, tabs } = props;

    const mtTabs = tabs === undefined ? [] : tabs;

    const handleChangeTab = (e, value) => {
        setIndex(value);
    };

    return (
        <AppBar
            position="relative"
            color="inherit"
            elevation={0}
            //style={{ borderBottom: `4px solid ${theme.palette.primary.main}` }}
        >
            <Tabs
                value={index}
                onChange={handleChangeTab}
                variant="fullWidth"
                textColor="primary"
                indicatorColor="primary"
            >
                {mtTabs.map((tab) => (
                    <Tab key={tab} label={tab} />
                ))}
            </Tabs>
        </AppBar>
    );
};

MeditocTabHeader.propTypes = {
    index: PropTypes.any,
    setIndex: PropTypes.func,
    tabs: PropTypes.any,
};

export default MeditocTabHeader;
