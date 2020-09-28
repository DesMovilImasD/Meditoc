import React from "react";

const ResumeInfo = (props) => {
    const { label, value } = props;

    return (
        <div className="reporte-resumen">
            <div className="flx-grw-1">
                <span className="rob-nor bold size-20 color-2">{label}</span>
            </div>
            <div>
                <span className="rob-nor size-20 color-2">{value}</span>
            </div>
        </div>
    );
};

export default ResumeInfo;
