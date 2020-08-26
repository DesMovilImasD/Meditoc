import React from "react";
import MaterialTable from "material-table";
import tableIcons from "../configurations/dataTableIconsConfig";
import theme from "../configurations/themeConfig";

const MeditocTable = (props) => {
    const { columns, data, rowSelected, setRowSelected, mainField, isLoading } = props;
    return (
        <MaterialTable
            columns={columns}
            data={data}
            icons={tableIcons}
            title=""
            onRowClick={(e, rowData) => setRowSelected(rowData)}
            components={{ Container: (props) => <div {...props}></div> }}
            localization={{
                body: {
                    emptyDataSourceMessage: "Sin datos para mostrar",
                },
                grouping: {
                    placeholder: "Arrastra los Headers de la tabla aquí para agrupar los datos",
                    groupedBy: "Agrupado por",
                },

                pagination: {
                    labelDisplayedRows: "{from}-{to} de {count}",
                    labelRowsSelect: "filas",
                    labelRowsPerPage: "Filas por página",
                    firstAriaLabel: "Primera página",
                    firstTooltip: "Primera página",
                    previousAriaLabel: "Previo",
                    previousTooltip: "Previo",
                    nextAriaLabel: "Siguiente",
                    nextTooltip: "Siguiente",
                    lastAriaLabel: "Última página",
                    lastTooltip: "Última página",
                },
                toolbar: {
                    addRemoveColumns: "Mostrar u ocultar columnas",
                    showColumnsAriaLabel: "Mostrar columnas",
                    showColumnsTitle: "Mostrar columnas",
                    searchPlaceholder: "Buscar en la tabla",
                    searchAriaLabel: "Buscar en la tabla",
                },
            }}
            options={{
                rowStyle: (rowData) => ({
                    backgroundColor: rowData[mainField] === rowSelected[mainField] ? theme.palette.grey[400] : "#fff",
                    color: rowData[mainField] === rowSelected[mainField] ? "#fff" : theme.palette.secondary.main,
                }),
                searchFieldVariant: "outlined",
                columnsButton: true,
                padding: "dense",
                headerStyle: { color: theme.palette.primary.main, fontSize: 19 },
                sorting: true,
                showTextRowsSelected: true,
                showSelectAllCheckbox: false,
                tableLayout: "auto",
                grouping: false,
            }}
            isLoading={isLoading}
        />
    );
};

export default MeditocTable;
