﻿using FluentValidation;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Validators
{
    public class UpdateRegionRequestValidator : AbstractValidator<Models.DTO.UpdateRegionRequest>
    {
        public UpdateRegionRequestValidator()
        {
            RuleFor(x => x.Code).NotEmpty();
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Area).GreaterThan(0);
            RuleFor(x => x.Lat).GreaterThan(0);
            RuleFor(x => x.Long).GreaterThan(0);
            RuleFor(x => x.Area).GreaterThanOrEqualTo(0);
        }
    }
}