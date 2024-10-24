using FluentValidation;
using TgInstanceAgent.Infrastructure.Web.Components.Validatiors;
using TgInstanceAgent.Infrastructure.Web.Profile.InputModels;

namespace TgInstanceAgent.Infrastructure.Web.Profile.Validators;

/// <summary>
/// Валидатор для SetPictureProfilePhotoInputModel
/// </summary>
public class SetPictureProfilePhotoInputModelValidator: AbstractValidator<SetPictureProfilePhotoInputModel>
{
    /// <summary>
    /// Конструктор
    /// </summary>
    public SetPictureProfilePhotoInputModelValidator()
    {
        // Включаем правила валидатора в текущий валидатор
        Include(new WithInputFileValidator());
    }
}