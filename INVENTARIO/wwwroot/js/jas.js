<script>
document.addEventListener("DOMContentLoaded", () => {
    const formAgregar = document.getElementById("formAgregar");
    const formEditar = document.getElementById("formEditar");
    const btnEliminar = document.getElementById("btnEliminar");

    // Guardar nuevo producto
    formAgregar.addEventListener("submit", async (e) => {
        e.preventDefault();

    const data = {
        Nombre: document.getElementById("Nombre").value,
    Categoria: document.getElementById("Categoria").value,
    Cantidad: parseInt(document.getElementById("Cantidad").value),
    Precio: parseFloat(document.getElementById("Precio").value),
    Proveedor: document.getElementById("Proveedor").value,
    FechaIngreso: document.getElementById("FechaIngreso").value
        };

    try {
            const response = await fetch("/Inventario/GuardarProducto", {
        method: "POST",
    headers: {"Content-Type": "application/json" },
    body: JSON.stringify(data)
            });

    if (response.ok) {
        alert("Producto guardado correctamente.");
    formAgregar.reset();
    bootstrap.Modal.getInstance(document.getElementById("modalAgregar")).hide();
    location.reload();
            } else {
        alert("Error al guardar el producto.");
            }
        } catch (err) {
        console.error(err);
    alert("Error inesperado.");
        }
    });

    // Cargar datos en modal editar
    document.querySelectorAll(".btn-editar").forEach(btn => {
        btn.addEventListener("click", () => {
            document.getElementById("EditId").value = btn.dataset.id;
            document.getElementById("EditNombre").value = btn.dataset.nombre;
            document.getElementById("EditCategoria").value = btn.dataset.categoria;
            document.getElementById("EditPrecio").value = btn.dataset.precio;
            document.getElementById("EditCantidad").value = btn.dataset.cantidad;
            document.getElementById("EditProveedor").value = btn.dataset.proveedor;
            document.getElementById("EditFechaIngreso").value = btn.dataset.fechaingreso;

            // 👇 Pasamos el id al botón eliminar
            btnEliminar.dataset.id = btn.dataset.id;
        });
    });

    // Editar producto
    formEditar.addEventListener("submit", async (e) => {
        e.preventDefault();

    const data = {
        Id: parseInt(document.getElementById("EditId").value),
    Nombre: document.getElementById("EditNombre").value,
    Categoria: document.getElementById("EditCategoria").value,
    Cantidad: parseInt(document.getElementById("EditCantidad").value),
    Precio: parseFloat(document.getElementById("EditPrecio").value),
    Proveedor: document.getElementById("EditProveedor").value,
    FechaIngreso: document.getElementById("EditFechaIngreso").value
        };

    try {
            const response = await fetch("/Inventario/Edita", {
        method: "POST",
    headers: {"Content-Type": "application/json" },
    body: JSON.stringify(data)
            });

    if (response.ok) {
        alert("Producto actualizado correctamente.");
    bootstrap.Modal.getInstance(document.getElementById("modalEditar")).hide();
    location.reload();
            } else {
        alert("Error al actualizar el producto.");
            }
        } catch (err) {
        console.error(err);
    alert("Error inesperado.");
        }
    });

    // Eliminar producto
    btnEliminar.addEventListener("click", async () => {
        const id = btnEliminar.dataset.id;

    if (!id) {
        alert("No se encontró el producto a eliminar.");
    return;
        }

    if (confirm("¿Estás seguro de que deseas eliminar este producto?")) {
            try {
                const response = await fetch(`/Inventario/EliminarProducto`, {
        method: "POST",
    headers: {"Content-Type": "application/json" },
    body: JSON.stringify({id: parseInt(id) })
                });

    if (response.ok) {
        alert("Producto eliminado correctamente.");
    bootstrap.Modal.getInstance(document.getElementById("modalEditar")).hide();
    location.reload();
                } else {
        alert("Error al eliminar el producto.");
                }
            } catch (err) {
        console.error(err);
    alert("Error inesperado al eliminar.");
            }
        }
    });
});
</script>