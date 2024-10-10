using AutoMapper;
using AutoMapper.QueryableExtensions;
using Health.Core.Features.DietEventJournal.Dtos;
using Health.Core.Features.Diets.Dtos;
using Health.Core.Features.Dishes.Dto;
using Health.Core.Features.Exercises.Dtos;
using Health.Core.Features.Goals.Dtos;
using Health.Core.Features.Products.Dto;
using Health.Core.Features.Trainers.Dtos;
using Health.Core.Features.WorkoutEventJournal.Dtos;
using Health.Core.Features.Workouts.Dtos;
using Health.Domain.Models.Common;
using Health.Domain.Models.Entities;

namespace Health.Core.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Dish, DishDto>()
            .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.Products!.Select(p => p.Name)))
            .ForMember(
                dest => dest.ImageUrl,
                opt => opt.MapFrom(
                    src => string.IsNullOrWhiteSpace(src.FileName)
                                ? ""
                                : $@"/uploads/dishes/{src.FileName}"));

        CreateMap<Dish, ExtendedDishDto>()
            .ForMember(dest => dest.Fats, 
                opt => opt.MapFrom(src => src.DishProducts.Sum(
                    dp => dp.Product.Fats * dp.Weight / Constants.COMMON_WEIGHT)))
            .ForMember(dest => dest.Proteins, 
                opt => opt.MapFrom(src => src.DishProducts.Sum(
                    dp => dp.Product.Proteins * dp.Weight / Constants.COMMON_WEIGHT)))
            .ForMember(dest => dest.Calories, 
                opt => opt.MapFrom(src => src.DishProducts.Sum(
                    dp => dp.Product.Calories * dp.Weight / Constants.COMMON_WEIGHT)))
            .ForMember(dest => dest.Carbohydrates, 
                opt => opt.MapFrom(src => src.DishProducts.Sum(
                    dp => dp.Product.Carbohydrates * dp.Weight / Constants.COMMON_WEIGHT)))
            .ForMember(
                dest => dest.ImageUrl,
                opt => opt.MapFrom(
                    src => string.IsNullOrWhiteSpace(src.FileName)
                                ? ""
                                : $@"/uploads/dishes/{src.FileName}"))
            .ForMember(dest => dest.Products,
                opt => opt.MapFrom(src => src.DishProducts.Select(p => new CutedProductDto
                {
                    Id = p.Product.Id,
                    Name = p.Product.Name,
                    Weight = p.Weight,
                    ImageUrl = string.IsNullOrWhiteSpace(p.Product.FileName)
                                ? ""
                                : $@"/uploads/dishes/{p.Product.FileName}"

                }).ToList()));

        CreateMap<Product, ProductDto>()
            .ForMember(
                dest => dest.ImageUrl, 
                opt => opt.MapFrom(
                    src => string.IsNullOrWhiteSpace(src.FileName) 
                                ? "" 
                                : $@"/uploads/products/{src.FileName}"));

        CreateMap<Trainer, TrainerDto>()
            .ForMember(
                dest => dest.ImageUrl,
                opt => opt.MapFrom(
                    src => string.IsNullOrWhiteSpace(src.FileName)
                                ? ""
                                : $@"/uploads/trainers/{src.FileName}"));

        CreateMap<Exercise, ExerciseDto>();

        CreateMap<Goal, GoalDto>();

        CreateMap<Goal, ExtendedGoalDto>();

        CreateMap<Workout, WorkoutDto>();

        CreateMap<Workout, ExtendedWorkoutDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(
                dest => dest.Exercises,
                opt => opt.MapFrom(
                    src => src.WorkoutExercise
                            .Select(we => new WorkoutExerciseDto 
                                { 
                                    Exercise = new ExerciseDto
                                    {
                                        Name = we.Exercise.Name,
                                        Description = we.Exercise.Description,
                                        CaloriesBurned = we.Exercise.CaloriesBurned,
                                        Id = we.Exercise.Id,
                                        Trainer = we.Exercise.Trainer != null 
                                            ? new TrainerDto 
                                                {
                                                    Id = we.Exercise.Trainer.Id, 
                                                    Name = we.Exercise.Trainer.Name,
                                                    ImageUrl = string.IsNullOrWhiteSpace(we.Exercise.Trainer.FileName)
                                                                    ? ""
                                                                    : $@"/uploads/products/{we.Exercise.Trainer.FileName}"
                                                }
                                            : null
                                    }, 
                                    Repetitions = we.Repetitions
                                }) ?? null));

        CreateMap<Diet, DietDto>();

        CreateMap<Diet, ExtendedDietDto>();

        CreateMap<WorkoutEvent, WorkoutEventDto>();

        CreateMap<DietEvent, DietEventDto>();
    }
}

