namespace ShoppingTask.Core.DTOs;

public record PaginationResponseDTO<T>(List<T>? Values, int? Pages = 0);
