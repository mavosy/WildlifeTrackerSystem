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
            RuleFor(wtsViewModel => wtsViewModel.Color).MustBeValidString(wtsViewModel => true);
            RuleFor(wtsViewModel => wtsViewModel.RegenerationRate).MustBeValidDouble(wtsViewModel => true);

            // Rules for Arachnid inputs
            // [PLACEHOLDER]

            // Rules for Bird inputs
            RuleFor(wtsViewModel => wtsViewModel.DivingSpeed).MustBeValidInteger(wtsViewModel => true);

            // Rules for Fish inputs
            RuleFor(wtsViewModel => wtsViewModel.NumberOfGills).MustBeValidInteger(wtsViewModel => true);

            // Rules for Insect inputs
            RuleFor(wtsViewModel => wtsViewModel.NumberOfSpots).MustBeValidInteger(wtsViewModel => true);


            // Rules for Mammal inputs
            RuleFor(wtsViewModel => wtsViewModel.NumberOfLegs).MustBeValidInteger(wtsViewModel => true);
            RuleFor(wtsViewModel => wtsViewModel.Breed).MustBeValidString(wtsViewModel => true);
            RuleFor(wtsViewModel => wtsViewModel.TrunkLength).MustBeValidInteger(wtsViewModel => true);

            // Rules for Reptiles inputs
            RuleFor(wtsViewModel => wtsViewModel.MaxAgeInYears).MustBeValidInteger(wtsViewModel => true);
        }
    }
}