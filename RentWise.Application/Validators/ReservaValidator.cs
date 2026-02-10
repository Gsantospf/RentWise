using FluentValidation;
using RentWise.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentWise.Application.Validators
{
    public class ReservaValidaor : AbstractValidator<ReservaRequest>
    {
        public ReservaValidaor()
        {
            RuleFor(x => x.SalaId)
                .GreaterThan(0).WithMessage("ID da sala inválido.");

            RuleFor(x => x.UsuarioId)
                .GreaterThan(0).WithMessage("ID do usuário inválido.");

            RuleFor(x => x.DataInicio)
                .NotEmpty().WithMessage("A data de início é obrigatória.")
                .GreaterThanOrEqualTo(DateTime.Now).WithMessage("A reserva não pode ser no passado.");

            RuleFor(x => x.DataFim)
                .NotEmpty().WithMessage("A data de término é obrigatória.")
                .GreaterThan(x => x.DataInicio)
                .WithMessage("A data de término deve ser posterior à data de início.");
        }
    }
}
