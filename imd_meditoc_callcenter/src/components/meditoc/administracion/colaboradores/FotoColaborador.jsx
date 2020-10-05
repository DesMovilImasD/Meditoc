import { Grid, IconButton, Tooltip } from "@material-ui/core";
import React, { Fragment } from "react";

import BrokenImageIcon from "@material-ui/icons/BrokenImage";
import ColaboradorController from "../../../../controllers/ColaboradorController";
import DeleteIcon from "@material-ui/icons/Delete";
import InfoIcon from "@material-ui/icons/Info";
import InsertPhotoIcon from "@material-ui/icons/InsertPhoto";
import MeditocConfirmacion from "../../../utilidades/MeditocConfirmacion";
import MeditocHeader3 from "../../../utilidades/MeditocHeader3";
import MeditocModal from "../../../utilidades/MeditocModal";
import PropTypes from "prop-types";
import { useEffect } from "react";
import { useState } from "react";

const FotoColaborador = (props) => {
    const { entColaborador, open, setOpen, usuarioSesion, funcLoader, funcAlert } = props;

    const colaboradorController = new ColaboradorController();

    const [fotoColaboradorB64, setFotoColaboradorB64] = useState("");
    const [modalEliminarFotoOpen, setModalEliminarFotoOpen] = useState(false);

    const handleClickLEiminarFoto = () => {
        setModalEliminarFotoOpen(true);
    };

    const funcSaveColaboradorFoto = () => {
        let input = document.createElement("input");
        input.type = "file";
        input.value = "";
        input.addEventListener("change", async () => {
            let archivo = input.files[0];
            if (!archivo) {
                funcAlert("No se cargó ningún archivo");
                return;
            }

            if (archivo.type !== "image/jpeg" && archivo.type !== "image/png") {
                funcAlert("El formato de la imagen debe ser .jpg o .png");
                return;
            }

            if (parseInt(archivo.size) > 2097152) {
                funcAlert("El tamaño de la imagen no debe exceder los 2MB");
                return;
            }

            let img = new Image();
            img.onload = async function () {
                const imgW = this.width.toFixed(0);
                const imgH = this.height.toFixed(0);
                console.log(imgW, imgH);

                //proporcion ideal = 9 / 16
                const proporcionMAX = 9 / 17;
                const proporcionMIN = 9 / 12;
                const proporcionImg = imgW / imgH;

                if (proporcionImg < proporcionMAX || proporcionImg > proporcionMIN) {
                    funcAlert(
                        "Las proporciones de la imagen deben ser entre 9:12 (ancho:altura) y 9:16 (ancho:altura). Ejemplo: 600px x 900px (ancho x altura)."
                    );
                    return;
                }

                funcLoader(true, "Guardando foto de colaborador...");

                const response = await colaboradorController.funcSaveColaboradorFoto(
                    entColaborador.iIdColaborador,
                    usuarioSesion.iIdUsuario,
                    archivo
                );

                if (response.Code === 0) {
                    await funcGetColaboradorFoto();
                    funcAlert(response.Message, "success");
                } else {
                    funcAlert(response.Message);
                }
                funcLoader();
            };
            img.src = URL.createObjectURL(archivo);
        });
        input.click();
        input.remove();
    };

    const funcGetColaboradorFoto = async () => {
        funcLoader(true, "Consultando foto de colaborador...");

        const response = await colaboradorController.funcGetColaboradorFoto(entColaborador.iIdColaborador);

        if (response.Code === 0) {
            setFotoColaboradorB64(response.Result);
        } else {
            funcAlert(response.Message);
        }
        funcLoader();
    };

    const funcDeleteColaboradorFoto = async () => {
        funcLoader(true, "Eliminando foto de colaborador...");

        const response = await colaboradorController.funcDeleteColaboradorFoto(
            entColaborador.iIdColaborador,
            usuarioSesion.iIdUsuario
        );

        if (response.Code === 0) {
            setFotoColaboradorB64("");
            setModalEliminarFotoOpen(false);
            funcAlert(response.Message, "success");
        } else {
            funcAlert(response.Message);
        }
        funcLoader();
    };

    useEffect(() => {
        setFotoColaboradorB64("");
        if (open === true) {
            funcGetColaboradorFoto();
        }
        // eslint-disable-next-line
    }, [entColaborador]);

    return (
        <Fragment>
            <MeditocModal title="Foto de colaborador" size="small" open={open} setOpen={setOpen}>
                <MeditocHeader3 title={entColaborador.sNombreDirectorio}>
                    <Tooltip
                        arrow
                        title="Las proporciones de la imagen permitidas se encuentran entre 9:12 (ancho:altura) y 9:16 (ancho:altura). Ejemplo: 600px(ancho) por 900px(altura)."
                    >
                        <IconButton>
                            <InfoIcon className="color-1" />
                        </IconButton>
                    </Tooltip>
                    <Tooltip arrow title="Agregar o cambiar foto">
                        <IconButton onClick={funcSaveColaboradorFoto}>
                            <InsertPhotoIcon className="color-1" />
                        </IconButton>
                    </Tooltip>
                    <Tooltip arrow title="Eliminar foto">
                        <IconButton onClick={handleClickLEiminarFoto}>
                            <DeleteIcon className="color-1" />
                        </IconButton>
                    </Tooltip>
                </MeditocHeader3>
                <Grid container spacing={3}>
                    <Grid item xs={12} className="center">
                        {fotoColaboradorB64 !== "" ? (
                            <img
                                alt="FotoColaborador"
                                src={`data:image/png;base64,${fotoColaboradorB64}`}
                                className="foto-colaborador"
                            />
                        ) : (
                            <BrokenImageIcon style={{ fontSize: 180, color: "e6e6e6" }} />
                        )}
                    </Grid>
                </Grid>
            </MeditocModal>
            <MeditocConfirmacion
                title="Eliminar foto"
                open={modalEliminarFotoOpen}
                setOpen={setModalEliminarFotoOpen}
                okFunc={funcDeleteColaboradorFoto}
            >
                ¿Desea eliminar la foto del colaborador?
            </MeditocConfirmacion>
        </Fragment>
    );
};

FotoColaborador.propTypes = {
    entColaborador: PropTypes.shape({
        iIdColaborador: PropTypes.any,
        sNombreDirectorio: PropTypes.any,
    }),
    funcAlert: PropTypes.func,
    funcLoader: PropTypes.func,
    open: PropTypes.bool,
    setOpen: PropTypes.any,
    usuarioSesion: PropTypes.shape({
        iIdUsuario: PropTypes.any,
    }),
};

export default FotoColaborador;
