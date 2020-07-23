import React, { Fragment } from "react";
import Header from "./Header";
import Contenido from "./Contenido";
import Footer from "../Precios/Footer";

const Principal = (props) => {
    const { funcLoader } = props;
    return (
        <Fragment>
            <Header />
            <Contenido funcLoader={funcLoader} />
            <Footer />
        </Fragment>
    );
};

export default Principal;
