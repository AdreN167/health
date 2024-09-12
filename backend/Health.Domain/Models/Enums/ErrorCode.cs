namespace Health.Domain.Models.Enums;

public enum ErrorCode
{
    // For all
    InternalServerError = 500,
    NotFound = 404,

    // Common
    InvalidRequest = 1,

    // Dish
    DishNotFound = 11,

    // Product
    ProductNotFound = 21,
}

