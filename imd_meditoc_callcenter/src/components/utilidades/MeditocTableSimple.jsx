import PropTypes from "prop-types";
import MaterialTable from "material-table";
import React from "react";
import tableIcons from "../../configurations/dataTableIconsConfig";
import theme from "../../configurations/themeConfig";

const MeditocTableSimple = (props) => {
    const { columns, data } = props;

    return (
        <MaterialTable
            columns={columns}
            data={data}
            icons={tableIcons}
            title=""
            components={{
                Container: (props) => <div {...props}></div>,
            }}
            localization={{
                body: {
                    emptyDataSourceMessage: "Sin datos para mostrar",
                    filterRow: {
                        filterTooltip: "Filtrar",
                        filterPlaceHolder: "Buscar en columna",
                    },
                    editRow: {
                        saveTooltip: "Aceptar",
                        cancelTooltip: "Cancelar",
                        deleteText: "¿Desea eliminar este elemento?",
                    },
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
                    nRowsSelected: "Elementos seleccionados: {0}",
                },
            }}
            options={{
                selection: false,
                search: false,
                columnsButton: false,
                paginationType: "normal",
                pageSizeOptions: [25, 50, 100],
                pageSize: 25,
                filtering: false,
                emptyRowsWhenPaging: false,
                padding: "dense",
                headerStyle: { color: theme.palette.primary.main, fontSize: 18 },
                sorting: true,
                showTextRowsSelected: false,
                tableLayout: "auto",
                grouping: false,
                columnResizable: false,
                draggable: false,
                toolbar: false,
            }}
        />
    );
};

MeditocTableSimple.propTypes = {
    columns: PropTypes.any,
    data: PropTypes.any,
};

export default MeditocTableSimple;
