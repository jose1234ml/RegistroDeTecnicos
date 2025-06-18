public class Paginacion<T>
{
    public List<T> Items { get; private set; }
    public int PaginaActual { get; private set; }
    public int TotalPaginas { get; private set; }
    public int ElementosPorPagina { get; private set; }
    public int TotalElementos { get; private set; }

    public Paginacion(List<T> items, int paginaActual, int elementosPorPagina)
    {
        TotalElementos = items.Count;
        ElementosPorPagina = elementosPorPagina;
        PaginaActual = paginaActual;
        TotalPaginas = (int)Math.Ceiling(items.Count / (double)elementosPorPagina);

        Items = items
            .Skip((PaginaActual - 1) * ElementosPorPagina)
            .Take(ElementosPorPagina)
            .ToList();
    }
    public bool TienePaginaAnterior => PaginaActual > 1;
    public bool TienePaginaSiguiente => PaginaActual < TotalPaginas;
    public void IrPaginaSiguiente()
    {
        if (TienePaginaSiguiente)
        {
            PaginaActual++;
        }
    }
    public void IrPaginaAnterior()
    {
        if (TienePaginaAnterior)
        {
            PaginaActual--;
        }
    }

    public Paginacion()
    {
        Items = new List<T>();
        PaginaActual = 1;
        TotalPaginas = 1;
    }
}
