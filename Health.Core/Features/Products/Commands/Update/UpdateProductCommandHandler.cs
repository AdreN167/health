﻿using AutoMapper;
using Health.Core.Features.Products.Dto;
using Health.Core.Resources;
using Health.DAL;
using Health.Domain.Models.Entities;
using Health.Domain.Models.Enums;
using Health.Domain.Models.Response;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Health.Core.Features.Products.Commands.Update;

public class UpdateProductCommandHandler(ApplicationDbContext context, IMapper mapper)
    : IRequestHandler<UpdateProductCommand, BaseResponse<ProductDto>>
{
    public async Task<BaseResponse<ProductDto>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var product = await context.Products.FindAsync([request.Id], cancellationToken);

            if (product == null)
            {
                return new BaseResponse<ProductDto>
                {
                    ErrorCode = (int)ErrorCode.ProductNotFound,
                    ErrorMessage = ErrorMessages.ProductNotFound
                };
            }

            if (string.IsNullOrWhiteSpace(request.Name) 
                || request.Proteins <= 0
                || request.Carbohydrates <= 0
                || request.Calories <= 0
                || request.Fats <= 0)
            {
                return new BaseResponse<ProductDto>
                {
                    ErrorCode = (int)ErrorCode.InvalidRequest,
                    ErrorMessage = ErrorMessages.InvalidRequest
                };
            }

            product.Name = request.Name;
            product.Proteins = request.Proteins;
            product.Carbohydrates = request.Carbohydrates;
            product.Calories = request.Calories;
            product.Fats = request.Fats;

            await context.SaveChangesAsync(cancellationToken);

            return new BaseResponse<ProductDto>
            {
                Data = mapper.Map<ProductDto>(product)
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<ProductDto>
            {
                ErrorCode = (int)ErrorCode.InternalServerError,
                ErrorMessage = ErrorMessages.InternalServerError + " -> " + ex.Message
            };
        }
    }
}

