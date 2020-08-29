import React from "react";
import MaterialTable, { MTableBodyRow } from "material-table";
import tableIcons from "../configurations/dataTableIconsConfig";
import theme from "../configurations/themeConfig";
import { TextField } from "@material-ui/core";

/*************************************************************
 * Descripcion: Contiene la estructura de una tabla para desplegar datos en las secciones del portal que lo requiera
 * Creado: Cristopher Noh
 * Fecha: 26/08/2020
 * Invocado desde: -
 *************************************************************/
const MeditocTable = (props) => {
    const { columns, data, rowSelected, setRowSelected, mainField, doubleClick } = props;

    //Funcion a ejecutar al dar doble click sobre una fila
    const funcDoubleClick = doubleClick === undefined ? () => {} : doubleClick;

    return (
        <MaterialTable
            columns={columns}
            data={data}
            icons={tableIcons}
            title=""
            onRowClick={(e, rowData) => setRowSelected(rowData)}
            components={{
                Container: (props) => <div {...props}></div>,
                FilterRow: (props) => (
                    <tr>
                        {props.columns.map((column, index) => (
                            <td key={index}>
                                <TextField
                                    variant="outlined"
                                    onChange={(e) => {
                                        props.onFilterChanged(index, e.target.value);
                                    }}
                                    style={{
                                        margin: 10,
                                        width: "90%",
                                    }}
                                    placeholder={`Buscar en ${column.title}...`}
                                    inputProps={{
                                        style: {
                                            textAlign: column.align,
                                        },
                                    }}
                                ></TextField>
                            </td>
                        ))}
                    </tr>
                ),
                Row: (props) => (
                    <MTableBodyRow
                        {...props}
                        onDoubleClick={(e) => {
                            setRowSelected(props.data);
                            funcDoubleClick();
                        }}
                    />
                ),
            }}
            localization={{
                body: {
                    emptyDataSourceMessage: "Sin datos para mostrar",
                    filterRow: {
                        filterTooltip: "Filtrar",
                        filterPlaceHolder: "Buscar en columna",
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
                },
            }}
            options={{
                rowStyle: (rowData) => ({
                    //linear-gradient(0deg, rgb(17 92 138 / 76%) 0%, rgba(18,182,203,1) 100%); op.1
                    //linear-gradient(239deg, rgba(17,92,138,1) 0%, rgba(18,182,203,1) 100%); op.2
                    background:
                        rowData[mainField] === rowSelected[mainField]
                            ? `linear-gradient(0deg, rgba(17,92,138,1) 0%, rgba(18,182,203,1) 100%)`
                            : "#fff",
                    color: rowData[mainField] === rowSelected[mainField] ? "#fff" : theme.palette.secondary.main,
                }),
                selection: false,
                addRowPosition: "first",
                search: false,
                searchFieldVariant: "outlined",
                searchFieldAlignment: "left",
                searchFieldStyle: { maxWidth: "500px" },
                searchAutoFocus: true,
                columnsButton: true,
                paginationType: "normal",
                pageSizeOptions: [10, 20, 50, 100],
                pageSize: 10,
                filtering: true,
                filterCellStyle: { textAlign: "center" },
                hideFilterIcons: true,
                emptyRowsWhenPaging: false,
                padding: "dense",
                headerStyle: { color: theme.palette.primary.main, fontSize: 18 },
                sorting: true,
                showTextRowsSelected: true,
                showSelectAllCheckbox: false,
                tableLayout: "auto",
                grouping: false,
                columnResizable: true,
            }}
        />
    );
};

export default MeditocTable;
