import MaterialTable, { MTableBodyRow } from "material-table";

import PropTypes from "prop-types";
import React from "react";
import { emptyFunc } from "../../configurations/preventConfig";
import tableIcons from "../../configurations/dataTableIconsConfig";
import theme from "../../configurations/themeConfig";

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
        setData,
        rowSelected,
        setRowSelected,
        mainField,
        doubleClick,
        selection,
        rowsSelected,
        setRowsSelected,
        cellEditable,
        onCellEditable,
        search,
        rowClick,
    } = props;

    //Funcion a ejecutar al dar doble click sobre una fila
    const funcDoubleClick = doubleClick === undefined ? () => {} : doubleClick;

    const mtSelection = selection === undefined ? false : selection;

    const mtRowSelected = mtSelection === false ? rowSelected : null;

    const mtSetRowSelected = mtSelection === false ? setRowSelected : () => {};

    const mtSetRowsSelected = mtSelection === true ? setRowsSelected : () => {};

    const mtCellEditable = cellEditable === undefined ? false : cellEditable;

    const mtOnCellEditable = onCellEditable === undefined ? () => {} : onCellEditable;

    const mtSearch = search === undefined ? true : search;

    const mtRowClick = rowClick === undefined ? true : rowClick;

    const handleRowClickNoSelection = (e, row) => {
        mtSetRowSelected(row);
    };

    const handleRowClickSelection = (e, row) => {
        if (rowsSelected !== undefined) {
            const index = rowsSelected.indexOf(row);
            const rowsSelectedCopy = [...rowsSelected];

            const indexData = data.indexOf(row);
            const dataCopy = [...data];

            if (index === -1) {
                rowsSelectedCopy.push(row);
                dataCopy[indexData].tableData.checked = true;
            } else {
                rowsSelectedCopy.splice(index, 1);
                dataCopy[indexData].tableData.checked = false;
            }
            mtSetRowsSelected(rowsSelectedCopy);
            setData(dataCopy);
        }
    };

    const handleRowClickSelectionOnEditable = (row) => {
        if (rowsSelected !== undefined) {
            const index = rowsSelected.indexOf(row);
            const rowsSelectedCopy = [...rowsSelected];

            const indexData = data.indexOf(row);
            const dataCopy = [...data];

            if (index === -1) {
                rowsSelectedCopy.push(row);
                dataCopy[indexData].tableData.checked = true;
            }
            mtSetRowsSelected(rowsSelectedCopy);
            setData(dataCopy);
        }
    };

    const handleSelectionChange = (rows) => {
        mtSetRowsSelected(rows);
    };

    const handleDoubleClick = () => {
        //mtSetRowSelected(props.data);
        funcDoubleClick();
    };

    return (
        <MaterialTable
            columns={columns}
            data={data}
            icons={tableIcons}
            title=""
            //actions={actions}
            onRowClick={
                mtSelection === false && mtRowClick === true ? handleRowClickNoSelection : handleRowClickSelection
            }
            onSelectionChange={mtSelection === true ? handleSelectionChange : emptyFunc}
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
                    <MTableBodyRow {...props} onDoubleClick={mtSelection === false ? handleDoubleClick : emptyFunc} />
                ),
            }}
            cellEditable={
                mtCellEditable === true
                    ? {
                          onCellEditApproved: (newValue, oldValue, rowData, columnDef) => {
                              return new Promise((resolve) => {
                                  mtOnCellEditable(newValue, oldValue, rowData, columnDef.field);
                                  if (mtSelection) {
                                      handleRowClickSelectionOnEditable(rowData);
                                  }
                                  resolve(emptyFunc);
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
                    mtSelection === false && mtRowClick === true
                        ? (rowData) => ({
                              //linear-gradient(0deg, rgb(17 92 138 / 76%) 0%, rgba(18,182,203,1) 100%); op.1
                              //linear-gradient(239deg, rgba(17,92,138,1) 0%, rgba(18,182,203,1) 100%); op.2

                              //linear-gradient(0deg, rgba(17,92,138,1) 0%, rgba(18,182,203,1) 100%)

                              background: rowData[mainField] === mtRowSelected[mainField] ? `#e8e8e8` : "#fff",
                              color: theme.palette.secondary.main,
                              //   color:
                              //       rowData[mainField] === mtRowSelected[mainField]
                              //           ? "#fff"
                              //           : theme.palette.secondary.main,
                          })
                        : null,
                selection: mtSelection,
                search: mtSearch,
                searchFieldVariant: "outlined",
                searchFieldAlignment: "left",
                // searchFieldStyle: { width: "80%" },
                // actionsCellStyle: {
                //     color: theme.palette.secondary.main,
                // },
                //actionsColumnIndex: -1,
                searchAutoFocus: false,
                columnsButton: true,
                paginationType: "normal",
                pageSizeOptions: [20, 50, 100, 500],
                pageSize: 20,
                filtering: false,
                filterCellStyle: { textAlign: "center" },
                hideFilterIcons: true,
                emptyRowsWhenPaging: false,
                padding: "dense",
                headerStyle: { color: theme.palette.primary.main, fontSize: 18, zIndex: 0 },
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

MeditocTable.propTypes = {
    cellEditable: PropTypes.any,
    columns: PropTypes.any,
    data: PropTypes.any,
    doubleClick: PropTypes.any,
    mainField: PropTypes.any,
    onCellEditable: PropTypes.any,
    rowSelected: PropTypes.any,
    search: PropTypes.any,
    selection: PropTypes.any,
    setRowSelected: PropTypes.any,
    setRowsSelected: PropTypes.any,
};

export default MeditocTable;
