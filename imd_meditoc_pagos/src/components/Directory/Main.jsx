import React, { Fragment, useEffect } from "react";
import Menu from "../Menu";
import Footer from "../Footer";
import DirectoryHeader from "./DirectoryHeader";
import Content from "./Content";

const Main = (props) => {
    const { appInfo, funcLoader } = props;

    //Scrollear al inicio cuando se cargue el componente
    useEffect(() => {
        window.scrollTo(0, 0);

        // eslint-disable-next-line
    }, []);

    return (
        <Fragment>
            <Menu />
            <DirectoryHeader />
            <Content funcLoader={funcLoader} />
            <Footer appInfo={appInfo} />
        </Fragment>
    );
};

export default Main;
