
function cerrarModalesAbiertos() {
    document.querySelectorAll('.modal.show').forEach(function (modalElement) {
        const modalInstance = bootstrap.Modal.getInstance(modalElement);
        if (modalInstance) {
            modalInstance.hide();
        }
    });
}




function mostrarModalEditarPersona(id) {
    cerrarModalesAbiertos();
    fetch(`/Persona/ModalEditarPersona?id=${id}`)
        .then(response => response.text())
        .then(html => {
            document.getElementById('contenedor-modal-editar-persona').innerHTML = html;
            const modalElement = document.getElementById('editarPersonaModal');
            const modal = new bootstrap.Modal(modalElement);
            modal.show();
        })
        .catch(error => console.error('Error al cargar el modal:', error));
}


function mostrarModalEliminarPersona(id) {
    cerrarModalesAbiertos();
    fetch(`/Persona/ModalEliminarPersona?id=${id}`)
        .then(response => response.text())
        .then(html => {
            document.getElementById('contenedor-modal-eliminar-persona').innerHTML = html;
            const modalElement = document.getElementById('eliminarPersonaModal');
            const modal = new bootstrap.Modal(modalElement);
            modal.show();
        })
        .catch(error => console.error('Error al cargar el modal:', error));
}



function mostrarModalEditarOrden(id, vistaOrigen) {
    cerrarModalesAbiertos();
    fetch(`/Ordenes/ModalEditarOrden?id=${id}&vistaOrigen=${vistaOrigen}`)
        .then(response => response.text())
        .then(html => {
            document.getElementById('contenedor-modal-editar-orden').innerHTML = html;
            const modalElement = document.getElementById('editarOrdenModal');
            const modal = new bootstrap.Modal(modalElement);
            modal.show();
        })
        .catch(error => console.error('Error al cargar el modal:', error));
}


function mostrarModalAsignarOrden(id, vistaOrigen) {
    cerrarModalesAbiertos();
    fetch(`/Ordenes/ModalAsignarOrden?id=${id}&vistaOrigen=${vistaOrigen}`)
        .then(response => response.text())
        .then(html => {
            document.getElementById('contenedor-modal-asignar-orden').innerHTML = html;
            const modalElement = document.getElementById('asignarOrdenModal');
            const modal = new bootstrap.Modal(modalElement);
            modal.show();
        })
        .catch(error => console.error('Error al cargar el modal:', error));
}



/*


Funciones para los modales de email



*/
function mostrarModalEmail(tipo, id, idPersona = null, origenInput) {
    cerrarModalesAbiertos();

    let url = "";
    let contenedor = "";
    let modalId = "";

    switch (tipo) {
        case "editar":
            url = `/Email/ModalEditarEmail?id=${id}`;
            contenedor = "contenedor-modal-editar-email";
            modalId = "editarEmailModal";
            break;
        case "eliminar":
            url = `/Email/ModalEliminarEmail?id=${id}`;
            contenedor = "contenedor-modal-eliminar-email";
            modalId = "eliminarEmailModal";
            break;
        case "agregar":
            url = `/Email/ModalAgregarEmail?idPersona=${idPersona}`;
            contenedor = "contenedor-modal-agregar-email";
            modalId = "agregarEmailModal";
            break;
        default:
            console.error("Tipo de modal no válido");
            return;
    }

    if (origenInput) {
        url += `&origen=${origenInput}`;
    }

    fetch(url)
        .then(response => response.text())
        .then(html => {
            document.getElementById(contenedor).innerHTML = html;
            const modalElement = document.getElementById(modalId);
            const modal = new bootstrap.Modal(modalElement);
            modal.show();

            const origenInput = modalElement.querySelector('input[name="origen"]');
            if (origenInput && origen) {
                origenInput.value = origen;
            }
        })
        .catch(error => console.error('Error al cargar el modal:', error));
}


document.addEventListener("submit", function (e) {
    const forms = {
        formEditarEmail: {
            url: '/Email/EditarEmail',
            modalId: 'editarEmailModal'
        },
        formEliminarEmail: {
            url: '/Email/EliminarEmail',
            modalId: 'eliminarEmailModal'
        },
        formAgregarEmail: {
            url: '/Email/AgregarEmail',
            modalId: 'agregarEmailModal'
        }
    };

    const formConfig = forms[e.target.id];

    if (formConfig) {
        e.preventDefault();

        const form = e.target;
        const formData = new FormData(form);

        fetch(formConfig.url, {
            method: 'POST',
            body: formData,
            headers: { 'X-Requested-With': 'XMLHttpRequest' }
        })
            .then(r => r.json())
            .then(json => {
                if (json.success) {
                    const modalEl = document.getElementById(formConfig.modalId);
                    bootstrap.Modal.getInstance(modalEl).hide();
                    window.location.href = json.redirectUrl;
                } else {
                    alert("Ocurrió un error en la operación.");
                    console.error(json.errors || json);
                }
            })
            .catch(console.error);
    }
});



/*


Funciones para los modales de telefono



*/
function mostrarModalTelefono(tipo, id, idPersona = null, origenInput) {
    cerrarModalesAbiertos();

    let url = "";
    let contenedor = "";
    let modalId = "";

    switch (tipo) {
        case "editar":
            url = `/Telefono/ModalEditarTelefono?id=${id}`;
            contenedor = "contenedor-modal-editar-telefono";
            modalId = "editarTelefonoModal";
            break;
        case "eliminar":
            url = `/Telefono/ModalEliminarTelefono?id=${id}`;
            contenedor = "contenedor-modal-eliminar-telefono";
            modalId = "eliminarTelefonoModal";
            break;
        case "agregar":
            url = `/Telefono/ModalAgregarTelefono?idPersona=${idPersona}`;
            contenedor = "contenedor-modal-agregar-telefono";
            modalId = "agregarTelefonoModal";
            break;
        default:
            console.error("Tipo de modal no válido");
            return;
    }

    if (origenInput) {
        url += `&origen=${origenInput}`;
    }

    fetch(url)
        .then(response => response.text())
        .then(html => {
            document.getElementById(contenedor).innerHTML = html;
            const modalElement = document.getElementById(modalId);
            const modal = new bootstrap.Modal(modalElement);
            modal.show();

            const origenInput = modalElement.querySelector('input[name="origen"]');
            if (origenInput && origen) {
                origenInput.value = origen;
            }

        })
        .catch(error => console.error('Error al cargar el modal:', error));
}

document.addEventListener("submit", function (e) {
    const forms = {
        formEditarTelefono: {
            url: '/Telefono/EditarTelefono',
            modalId: 'editarTelefonoModal'
        },
        formEliminarTelefono: {
            url: '/Telefono/EliminarTelefono',
            modalId: 'eliminarTelefonoModal'
        },
        formAgregarTelefono: {
            url: '/Telefono/AgregarTelefono',
            modalId: 'agregarTelefonoModal'
        }
    };

    const formConfig = forms[e.target.id];

    if (formConfig) {
        e.preventDefault();

        const form = e.target;
        const formData = new FormData(form);

        fetch(formConfig.url, {
            method: 'POST',
            body: formData,
            headers: { 'X-Requested-With': 'XMLHttpRequest' }
        })
            .then(r => r.json())
            .then(json => {
                if (json.success) {
                    const modalEl = document.getElementById(formConfig.modalId);
                    bootstrap.Modal.getInstance(modalEl).hide();
                    window.location.href = json.redirectUrl;
                } else {
                    alert("Ocurrió un error en la operación.");
                    console.error(json.errors || json);
                }
            })
            .catch(console.error);
    }
});


/*


Funciones para los modales de direccion


*/
function mostrarModalDireccion(tipo, id, idPersona = null, origenInput) {
    cerrarModalesAbiertos();

    let url = "";
    let contenedor = "";
    let modalId = "";

    switch (tipo) {
        case "editar":
            url = `/Direccion/ModalEditarDireccion?id=${id}`;
            contenedor = "contenedor-modal-editar-direccion";
            modalId = "editarDireccionModal";
            break;
        case "eliminar":
            url = `/Direccion/ModalEliminarDireccion?id=${id}`;
            contenedor = "contenedor-modal-eliminar-direccion";
            modalId = "eliminarDireccionModal";
            break;
        case "agregar":
            url = `/Direccion/ModalAgregarDireccion?idPersona=${idPersona}`;
            contenedor = "contenedor-modal-agregar-direccion";
            modalId = "agregarDireccionModal";
            break;
        default:
            console.error("Tipo de modal no válido");
            return;
    }

    if (origenInput) {
        url += `&origen=${origenInput}`;
    }

    fetch(url)
        .then(response => response.text())
        .then(html => {
            document.getElementById(contenedor).innerHTML = html;
            const modalElement = document.getElementById(modalId);
            const modal = new bootstrap.Modal(modalElement);
            modal.show();

            const origenInput = modalElement.querySelector('input[name="origen"]');
            if (origenInput && origen) {
                origenInput.value = origen;
            }
        })
        .catch(error => console.error('Error al cargar el modal:', error));
}

document.addEventListener("submit", function (e) {
    const forms = {
        formEditarDireccion: {
            url: '/Direccion/EditarDireccion',
            modalId: 'editarDireccionModal'
        },
        formEliminarDireccion: {
            url: '/Direccion/EliminarDireccion',
            modalId: 'eliminarDireccionModal'
        },
        formAgregarDireccion: {
            url: '/Direccion/AgregarDireccion',
            modalId: 'agregarDireccionModal'
        }
    };

    const formConfig = forms[e.target.id];

    if (formConfig) {
        e.preventDefault();

        const form = e.target;
        const formData = new FormData(form);

        fetch(formConfig.url, {
            method: 'POST',
            body: formData,
            headers: { 'X-Requested-With': 'XMLHttpRequest' }
        })
            .then(r => r.json())
            .then(json => {
                if (json.success) {
                    const modalEl = document.getElementById(formConfig.modalId);
                    bootstrap.Modal.getInstance(modalEl).hide();
                    window.location.href = json.redirectUrl;
                } else {
                    alert("Ocurrió un error en la operación.");
                    console.error(json.errors || json);
                }
            })
            .catch(console.error);
    }
});

document.addEventListener("change", function (e) {
    if (e.target && e.target.id === "ddlProvincia") {
        const idProvincia = e.target.value;
        if (!idProvincia) return;

        fetch(`/Direccion/ObtenerCantonesPorProvincia?idProvincia=${idProvincia}`)
            .then(r => r.json())
            .then(data => {
                const ddlCanton = document.getElementById("ddlCanton");
                ddlCanton.innerHTML = '<option value="">Seleccione un cantón --</option>';
                data.forEach(c => {
                    ddlCanton.innerHTML += `<option value="${c.value}">${c.text}</option>`;
                });

                // Limpiar distritos también
                const ddlDistrito = document.getElementById("ddlDistrito");
                ddlDistrito.innerHTML = '<option value="">Seleccione un distrito --</option>';
            });
    }

    if (e.target && e.target.id === "ddlCanton") {
        const idCanton = e.target.value;
        if (!idCanton) return;

        fetch(`/Direccion/ObtenerDistritosPorCanton?idCanton=${idCanton}`)
            .then(r => r.json())
            .then(data => {
                const ddlDistrito = document.getElementById("ddlDistrito");
                ddlDistrito.innerHTML = '<option value="">Seleccione un distrito --</option>';
                data.forEach(d => {
                    ddlDistrito.innerHTML += `<option value="${d.value}">${d.text}</option>`;
                });
            });
    }
});


/*------------------Módales de colaborador-------------------*/

function mostrarModalColaborador(tipo, id) {
    cerrarModalesAbiertos();

    let url = "";
    let contenedor = "";
    let modalId = "";

    switch (tipo) {
        case "editar":
            url = `/Colaborador/ModalEditarColaborador?id=${id}`;
            contenedor = "contenedor-modal-editar-colaborador";
            modalId = "editarColaboradorModal";
            break;
        case "eliminar":
            url = `/Colaborador/ModalEliminarColaborador?id=${id}`;
            contenedor = "contenedor-modal-eliminar-colaborador";
            modalId = "eliminarColaboradorModal";
            break;
        default:
            console.error("Tipo de modal no válido");
            return;
    }

    fetch(url)
        .then(response => response.text())
        .then(html => {
            document.getElementById(contenedor).innerHTML = html;
            const modalElement = document.getElementById(modalId);
            const modal = new bootstrap.Modal(modalElement);
            modal.show();
        })
        .catch(error => console.error('Error al cargar el modal:', error));
}

document.addEventListener("submit", function (e) {
    const forms = {
        formEditarColaborador: {
            url: '/Colaborador/EditarColaborador',
            modalId: 'editarColaboradorModal'
        },
        formEliminarColaborador: {
            url: '/Colaborador/EliminarColaborador',
            modalId: 'eliminarColaboradorModal'
        }

    };

    const formConfig = forms[e.target.id];

    if (formConfig) {
        e.preventDefault();

        const form = e.target;
        const formData = new FormData(form);

        fetch(formConfig.url, {
            method: 'POST',
            body: formData,
            headers: { 'X-Requested-With': 'XMLHttpRequest' }
        })
            .then(r => r.json())
            .then(json => {
                if (json.success) {
                    const modalEl = document.getElementById(formConfig.modalId);
                    bootstrap.Modal.getInstance(modalEl).hide();
                    window.location.href = json.redirectUrl;
                } else {
                    alert("Ocurrió un error en la operación.");
                    console.error(json.errors || json);
                }
            })
            .catch(console.error);
    }
});



document.addEventListener("change", function (e) {
    if (e.target && e.target.id === "ddlTipoPuesto") {
        const puestoId = e.target.value;
        if (!puestoId) return;

        fetch(`/Ordenes/ObtenerColaboradoresPorPuesto?puestoId=${puestoId}`)
            .then(r => r.json())
            .then(data => {
                console.log(data);
                const ddlColaborador = document.getElementById("ddlColaborador");
                ddlColaborador.innerHTML = '<option value="">Seleccione un colaborador --</option>';
                data.forEach(c => {
                    ddlColaborador.innerHTML += `<option value="${c.value}">${c.text}</option>`;
                });
            })
            .catch(error => console.error('Error al obtener los colaboradores:', error));
    }
});





