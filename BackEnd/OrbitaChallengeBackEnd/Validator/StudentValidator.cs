using FluentValidation;
using OrbitaChallengeBackEnd.Models;


namespace OrbitaChallengeBackEnd.Validator;

public class StudentValidator : AbstractValidator<Student>
{
   public StudentValidator()
   {
       RuleFor(x => x.Name).NotEmpty().WithMessage("O nome é obrigatório.");
       RuleFor(x => x.Email).NotEmpty().WithMessage("O Email é obrigatório.");
       RuleFor(x => x.RA).NotEmpty().WithMessage("O RA é obrigatório.");
       RuleFor(x => x.CPF)
             .NotEmpty()
             .WithMessage("O CPF é obrigatório.")
             .Custom((cpf, context) =>
             {
                 // Remove qualquer caractere não numérico do CPF
                 var cleanedCpf = new string(cpf.Where(char.IsDigit).ToArray());

                 // Substitui o CPF original pelo CPF limpo
                 context.InstanceToValidate.CPF = cleanedCpf;

                 // Valida o comprimento do CPF (depois de limpar os caracteres não numéricos)
                 if (cleanedCpf.Length != 11)
                 {
                     context.AddFailure("O CPF deve conter 11 dígitos.");
                 }

                 // Verifica se o CPF tem apenas números
                 if (!cleanedCpf.All(char.IsDigit))
                 {
                     context.AddFailure("O CPF deve conter apenas números.");
                 }
             });
    }
}

