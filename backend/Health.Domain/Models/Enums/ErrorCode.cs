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

    // User
    UserNotFound = 31,
    WrongPassword = 32,
    PasswordsAreNotEqual = 33,
    UserAlreadyExists = 34,

    // Trainer
    TrainerNotFound = 41,

    // Exercise
    ExerciseNotFound = 51,
}

