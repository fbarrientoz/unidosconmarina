function openNewEncuesta(id, url) {
    var modal = $('#modalAddEncuesta');
    modal.find('form')[0].reset();
    modal.find('input#EVALUADO_ID').val(id);
    $('#VENTA_ID').empty();
    modal.modal('show');
}


function openNewDocumento(id, url) {
    var modal = $('#modalAddDocumentacion');
    modal.find('form')[0].reset();
    modal.find('input#EVALUADO_ID').val(id);
    modal.find('input#EVALUACION_ID').val(id);
    modal.modal('show');
}

function checkVenta(url) {
    //Verificamos si aun tiene disponible encuestas
    Swal.fire({
        title: 'Verificando información...',
        allowOutsideClick: false,
        html: '',
        onBeforeOpen: () => {
            Swal.showLoading();
        }
    });
    $.ajax({
        url: url,
        type: "POST",
        contentType: "application/json",
        dataType: "JSON",
        success: function (response) {
            Swal.close();
            var data = response;
            if (data.accion === true) {
                if (data.venta.CANTIDAD > data.venta.UTILIZADAS) {
                    $("#btnGuardarProgramacion").show();
                    $('#modalAddEncuesta form#frmEvaluacion select#AREA_EVALUACION_ID').val(data.venta.AREA_EVALUACION_ID);
                }
                else {
                    Swal.fire('Error!', 'No tiene evaluaciones disponibles', 'error');
                    $("#btnGuardarProgramacion").hide();
                }
            }
            else {
                Swal.fire('Error!', data.Msg, 'error');
            }
        },
        error: function (response) {
            Swal.close();
            Swal.fire('Error!', 'Hubo un problema al procesar la solicitud', 'error');
        }
    });
}

function loadVentas(url) {

}

function showCampos(tipo) {
    switch (parseInt(tipo)) {
        case 2:
            $('#divSiniestro').show();
            $('#divSiniestro1').show();
            $('#divSiniestro2').show();
            $('#divSiniestro3').show();
            $('#divSiniestro4').show();
            break;

        case 1:
            $('#divSiniestro').hide();
            $('#divSiniestro1').hide();
            $('#divSiniestro2').hide();
            $('#divSiniestro3').hide();
            $('#divSiniestro4').hide();
            break;
    }
}

$("select#RoleName").change(function () {
    var valor = $(this).val();
    showDatosEvaluador(valor);
});


function showDatosEvaluador(tipo) {
    switch (tipo) {
        case "Evaluador":
            $('#datosEva').show();
     
            break;

        case "Administrador":
            $('#datosEva').hide();   
            break;
        case "Cliente":
            $('#datosEva').hide();
            break;
        case "Demo":
            $('#datosEva').hide();
            break;
        case "Facturacion":
            $('#datosEva').hide();
            break;
        case "Programador":
            $('#datosEva').hide();
            break;
        case "Venta":
            $('#datosEva').hide();
            break;
    }
}

function SaveDocumento(url) {
    Swal.fire({
        title: 'Procesando!',
        html: '',
        onBeforeOpen: () => {
            Swal.showLoading()
            return new Promise((resolve) => {
                var formData = new FormData($('form#frmDocumentacion')[0]);

                $.ajax({
                    type: "POST",
                    url: url,
                    data: formData,
                    dataType: 'json',
                    contentType: false,// "multipart/form-data",
                    processData: false,
                    success: function (result) {
                        if (result.accion) {
                            Swal.fire({
                                title: 'Realizado!',
                                text: 'Se ha agregado correctamente',
                                type: 'success',
                                onClose: () => {
                                    window.location.reload();
                                }
                            });
                        }
                        else
                            Swal.fire('Ops!', result.Msg, 'error');
                    },
                    error: function (error) {
                        Swal.fire('Ops!', 'No se pudo completar la operación.', 'error');
                    }
                });

            })
        },
    }).then((result) => {

        if (result.value) {
            Swal.fire({
                title: 'Realizado!',
                text: 'Se ha agregado correctamente',
                type: 'success',
                onClose: () => {
                    window.location.reload();
                }
            });
        }
    });
}


function save(url) {
    Swal.fire({
        title: 'Procesando!',
        html: '',
        onBeforeOpen: () => {
            Swal.showLoading()
            return new Promise((resolve) => {
                var formData = new FormData($('form#frmEvaluacion')[0]);

                $.ajax({
                    type: "POST",
                    url: url,
                    data: formData,
                    dataType: 'json',
                    contentType: false,// "multipart/form-data",
                    processData: false,
                    success: function (result) {
                        if (result.accion) {
                            Swal.fire({
                                title: 'Realizado!',
                                text: 'Se ha agregado correctamente',
                                type: 'success',
                                onClose: () => {
                                    window.location.reload();
                                }
                            });
                        } 
                        else
                            Swal.fire('Ops!', result.Msg, 'error');
                    },
                    error: function (error) {
                        Swal.fire('Ops!', 'No se pudo completar la operación.', 'error');
                    }
                });

            })
        },
    }).then((result) => {
      
        if (result.value) {
            Swal.fire({
                title: 'Realizado!',
                text: 'Se ha agregado correctamente',
                type: 'success',
                onClose: () => {
                    window.location.reload();
                }
            });
        }
    });
}

$(document).ready(function () {
    $('select#EmpresaID').change(function () {
        $('#VENTA_ID').empty();
        $('#VENTA_ID').append('<option selected="selected" value="">Cargando...</option>');

        var url = "/Empresa/GetVentas/" + $(this).val();

        $.getJSON(url, function (data) {
            $('#VENTA_ID').empty();
            $('#VENTA_ID').append('<option selected="selected" value=""></option>');
            $.each(data, function (i, d) {
                $('#VENTA_ID').append('<option value=' + d.ID + '>' + d.FOLIO + '</option>');
            });
        });
    });

    $("select#TIPO_EVALUACION_ID").change(function () {
        var valor = $(this).val();
        showCampos(valor);
    });

 

    $("select#VENTA_ID").change(function () {
        checkVenta('/Programacion/CheckEvaliacionesDisponibles/' + $(this).val());
    });

});