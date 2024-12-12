namespace ShoppingTask.Core.DTOs;

public record OrderItemDTO(int Id,decimal Quantity , decimal Price, int ProductId);
