import React, { Fragment } from "react";
import Menu from "../Menu";
import Footer from "../Footer";
import DirectoryHeader from "./DirectoryHeader";
import Content from "./Content";

const Main = (props) => {
    const { appInfo, funcLoader } = props;
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
