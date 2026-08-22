using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SentinelSystemApi.Api.Data;
using SentinelSystemApi.Api.DTOs;

namespace SentinelSystemApi.Api.Validators;

public class RegisterRequestValidator : AbstractValidator<RegisterRequestDto>
{
      private readonly AppDbContext _context;

      public RegisterRequestValidator(AppDbContext context)
      {
            _context = context;

            RuleFor(x => x.Username)
            .NotEmpty().WithMessage("Username cannot be empty.")
            .MaximumLength(100).WithMessage("Maximum username length is 100 characters.")
            .MustAsync(BeUniqueUserName).WithMessage("Username already taken");
      }
      private async Task<bool> BeUniqueUserName(string username, CancellationToken cancellationToken)
      {
            return !await _context.Users.AnyAsync(u => u.Username == username, cancellationToken);
      }
}