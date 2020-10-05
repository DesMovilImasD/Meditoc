import React, { Fragment } from "react";

import { Divider } from "@material-ui/core";
import PropTypes from "prop-types";

const MeditocSubtitulo = (props) => {
    const { title } = props;

    return (
        <Fragment>
            <span className="rob-nor bold size-20 color-2">{title}</span>
            <Divider />
        </Fragment>
    );
};

MeditocSubtitulo.propTypes = {
    title: PropTypes.any,
};

export default MeditocSubtitulo;
