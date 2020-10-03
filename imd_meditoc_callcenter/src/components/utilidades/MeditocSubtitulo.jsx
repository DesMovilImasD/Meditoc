import PropTypes from "prop-types";
import { Divider } from "@material-ui/core";
import React, { Fragment } from "react";

const MeditocSubtitulo = (props) => {
    const { title } = props;

    return (
        <Fragment>
            <span className="rob-nor bold size-20 color-4">{title}</span>
            <Divider />
        </Fragment>
    );
};

MeditocSubtitulo.propTypes = {
    title: PropTypes.any,
};

export default MeditocSubtitulo;
