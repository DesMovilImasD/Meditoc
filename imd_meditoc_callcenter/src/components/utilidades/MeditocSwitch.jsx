import React from "react";
import { withStyles } from "@material-ui/core/styles";
import { Switch } from "@material-ui/core";
import { green, red } from "@material-ui/core/colors";

const MeditocSwitch = withStyles(() => ({
    root: {
        width: 55,
        height: 35,
        padding: 4,
        margin: 5,
    },
    switchBase: {
        padding: 7,
        "&$checked": {
            transform: "translateX(16px)",
            color: "#fff",
            "& + $track": {
                backgroundColor: green[500],
                opacity: 1,
                border: `2px solid #fff`,
            },
        },
        "&$focusVisible $thumb": {
            border: "2px solid #fff",
        },
    },
    thumb: {
        width: 25,
        height: 25,
    },
    track: {
        borderRadius: 36 / 2,
        border: `2px solid #fff`,
        backgroundColor: red[500],
        opacity: 1,
    },
    checked: {},
    focusVisible: {},
}))(({ classes, ...props }) => {
    return (
        <Switch
            focusVisibleClassName={classes.focusVisible}
            disableRipple
            classes={{
                root: classes.root,
                switchBase: classes.switchBase,
                thumb: classes.thumb,
                track: classes.track,
                checked: classes.checked,
            }}
            {...props}
        />
    );
});
export default MeditocSwitch;
