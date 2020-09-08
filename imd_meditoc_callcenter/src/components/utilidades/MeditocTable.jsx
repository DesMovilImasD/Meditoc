import React, { useEffect } from "react";
import MaterialTable, { MTableBodyRow } from "material-table";
import tableIcons from "../../configurations/dataTableIconsConfig";
import theme from "../../configurations/themeConfig";
import { TextField, useForkRef, Paper } from "@material-ui/core";
import { useState } from "react";

/*************************************************************
 * Descripcion: Contiene la estructura de una tabla para desplegar datos en las secciones del portal que lo requiera
 * Creado: Cristopher Noh
 * Fecha: 26/08/2020
 * Invocado desde: -
 *************************************************************/
const MeditocTable = (props) => {
    const {
        columns,
        data,
        rowSelected,
        setRowSelected,
        mainField,
        doubleClick,
        selection,
        rowsSelected,
        setRowsSelected,
        cellEditable,
        onCellEditable,
    } = props;

    //Funcion a ejecutar al dar doble click sobre una fila
    const funcDoubleClick = doubleClick === undefined ? () => {} : doubleClick;

    const mtSelection = selection === undefined ? false : selection;

    const mtRowSelected = mtSelection === false ? rowSelected : null;

    const mtSetRowSelected = mtSelection === false ? setRowSelected : (prop) => {};

    const mtRowsSelected = mtSelection === true ? rowsSelected : null;

    const mtSetRowsSelected = mtSelection === true ? setRowsSelected : (prop) => {};

    const mtCellEditable = cellEditable === undefined ? false : cellEditable;

    const mtOnCellEditable = onCellEditable === undefined ? (newValue, oldValue, row, column) => {} : onCellEditable;

    return (
        <MaterialTable
            columns={columns}
            data={data}
            icons={tableIcons}
            title=""
            onRowClick={
                mtSelection === false
                    ? (e, rowData) => {
                          mtSetRowSelected(rowData);
                      }
                    : () => {}
            }
            onSelectionChange={
                mtSelection === true
                    ? (rows) => {
                          mtSetRowsSelected(rows);
                      }
                    : () => {}
            }
            // onFilterChange={(filter) => {
            //     if (filter.length > 0) {
            //         filter.forEach((f) => {
            //             console.log(f);
            //             // let columnsCopy = [...columns];
            //             // columnsCopy[
            //             //     columnsCopy.indexOf(columnsCopy.find((c) => (c.field = f.column.field)))
            //             // ].defaultFilter = f.value;

            //             let columnsCopy = [...columns];
            //             columnsCopy[f.column.tableData.id].defaultFilter = f.value;
            //             setColumnas(columnsCopy);
            //         });
            //     }
            // }}
            components={{
                //Container: (props) => <Paper {...props} elevation={0} />,
                //Container: (props) => <div {...props}></div>,
                // FilterRow: (props) => (
                //     <tr>
                //         {mtSelection === true ? <td></td> : null}
                //         {props.columns.map((column, index) => (
                //             <td key={index}>
                //                 <TextField
                //                     variant="outlined"
                //                     onChange={(e) => {
                //                         props.onFilterChanged(columns.indexOf(column), e.target.value);
                //                     }}
                //                     style={{
                //                         margin: 10,
                //                         width: "90%",
                //                     }}
                //                     placeholder={`Buscar en ${column.title}...`}
                //                     inputProps={{
                //                         style: {
                //                             textAlign: column.align,
                //                         },
                //                     }}
                //                 />
                //             </td>
                //         ))}
                //     </tr>
                // ),
                Row: (props) => (
                    <MTableBodyRow
                        {...props}
                        onDoubleClick={
                            mtSelection === false
                                ? (e) => {
                                      mtSetRowSelected(props.data);
                                      funcDoubleClick();
                                  }
                                : () => {}
                        }
                    />
                ),
            }}
            cellEditable={
                mtCellEditable === true
                    ? {
                          onCellEditApproved: (newValue, oldValue, rowData, columnDef) => {
                              return new Promise((resolve, reject) => {
                                  mtOnCellEditable(newValue, oldValue, rowData, columnDef.field);
                                  resolve(() => {});
                              });
                          },
                      }
                    : null
            }
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
                rowStyle:
                    mtSelection === false
                        ? (rowData) => ({
                              //linear-gradient(0deg, rgb(17 92 138 / 76%) 0%, rgba(18,182,203,1) 100%); op.1
                              //linear-gradient(239deg, rgba(17,92,138,1) 0%, rgba(18,182,203,1) 100%); op.2

                              //linear-gradient(0deg, rgba(17,92,138,1) 0%, rgba(18,182,203,1) 100%)

                              background: rowData[mainField] === mtRowSelected[mainField] ? `#bbb` : "#fff",
                              color:
                                  rowData[mainField] === mtRowSelected[mainField]
                                      ? "#fff"
                                      : theme.palette.secondary.main,
                          })
                        : null,
                selection: mtSelection,
                search: true,
                // searchFieldVariant: "outlined",
                // searchFieldAlignment: "left",
                //searchFieldStyle: { width: "80%" },
                searchAutoFocus: true,
                columnsButton: true,
                paginationType: "normal",
                pageSizeOptions: [25, 50, 100],
                pageSize: 25,
                filtering: false,
                filterCellStyle: { textAlign: "center" },
                hideFilterIcons: true,
                emptyRowsWhenPaging: false,
                padding: "default",
                headerStyle: { color: theme.palette.primary.main, fontSize: 18 },
                sorting: true,
                showTextRowsSelected: false,
                showSelectAllCheckbox: true,
                tableLayout: "auto",
                grouping: false,
                columnResizable: true,
                draggable: false,
            }}
        />
    );
};

export default MeditocTable;
