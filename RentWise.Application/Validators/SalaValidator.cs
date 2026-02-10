using FluentValidation;
using RentWise.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentWise.Application.Validators
{
    public class SalaValidator : AbstractValidator<SalaRequest>
    {
        public SalaValidator() 
        {
            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("O nome da sala é obrigatório.")
                .MaximumLength(100).WithMessage("O nome deve ter no máximo 100 caracteres.");

            RuleFor(x => x.Descricao)
                .NotEmpty().WithMessage("A descrição é obrigatória.")
                .MinimumLength(10).WithMessage("Dê uma descrição mais detalhada (mínimo 10 caracteres).");

            RuleFor(x => x.PrecoPorHora)
                .GreaterThan(0).WithMessage("O preço por hora deve ser maior que zero.");
        }
    }
}
