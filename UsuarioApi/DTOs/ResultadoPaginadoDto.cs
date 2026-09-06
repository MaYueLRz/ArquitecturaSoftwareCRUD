namespace UsuarioApi.DTOs;

/// <summary>
/// Envoltura de respuesta para los listados paginados.
/// </summary>
public record ResultadoPaginadoDto<T>(
    List<T> Items,
    int Pagina,
    int TamanoPagina,
    int TotalRegistros,
    int TotalPaginas
);
