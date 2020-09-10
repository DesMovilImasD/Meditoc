import React from "react";
import { AppBar, Tabs, Tab } from "@material-ui/core";
import theme from "../../configurations/themeConfig";

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

export default MeditocTabHeader;
