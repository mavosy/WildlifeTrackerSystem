using FluentValidation;
using WTS.ViewModels;

namespace WTS.Validators
{
    /// <summary>
    /// Provides validation rules for various properties of the WTSViewModel related to different animal types.
    /// </summary>
    public class GeneralAnimalValidator : AbstractValidator<WTSViewModel>
    {
        /// <summary>
        /// Initializes validation rules for properties.
        /// Includes rules for common animal properties and specific types like Amphibians, Arachnids, Birds, Fish, Insects, Mammals, and Reptiles.
        /// </summary>
        public GeneralAnimalValidator()
        {
            // Rules for Animal inputs
            RuleFor(wtsViewModel => wtsViewModel.Name).MustBeValidNullableString(wtsViewModel => false);
            RuleFor(wtsViewModel => wtsViewModel.Age).MustBeValidNullableInteger(wtsViewModel => false);

            // Rules for Amphibian inputs
            RuleFor(wtsViewModel => wtsViewModel.Color).MustBeValidNullableString(wtsViewModel => false);
            RuleFor(wtsViewModel => wtsViewModel.RegenerationRate).MustBeValidNullableDouble(wtsViewModel => false);

            // Rules for Arachnid inputs
            // [PLACEHOLDER]

            // Rules for Bird inputs
            RuleFor(wtsViewModel => wtsViewModel.DivingSpeed).MustBeValidNullableInteger(wtsViewModel => false);

            // Rules for Fish inputs
            RuleFor(wtsViewModel => wtsViewModel.NumberOfGills).MustBeValidNullableInteger(wtsViewModel => false);

            // Rules for Insect inputs
            RuleFor(wtsViewModel => wtsViewModel.NumberOfSpots).MustBeValidNullableInteger(wtsViewModel => false);

            // Rules for Mammal inputs
            RuleFor(wtsViewModel => wtsViewModel.NumberOfLegs).MustBeValidNullableInteger(wtsViewModel => false);
            RuleFor(wtsViewModel => wtsViewModel.Breed).MustBeValidNullableString(wtsViewModel => false);
            RuleFor(wtsViewModel => wtsViewModel.TrunkLength).MustBeValidNullableInteger(wtsViewModel => false);

            // Rules for Reptiles inputs
            RuleFor(wtsViewModel => wtsViewModel.MaxAgeInYears).MustBeValidNullableInteger(wtsViewModel => false);
        }
    }
}