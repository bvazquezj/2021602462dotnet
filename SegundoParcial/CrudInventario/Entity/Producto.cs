namespace CrudInventario.Entity;

public class Producto
{
    public required int Id { get; set; }
    public required string Nombre { get; set; }
    public required int Stock { get; set; }
    public decimal Precio { get; set; }
    public string? Descripcion { get; set; }
}