using CommunityToolkit.Mvvm.Messaging;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using WTS.Commands;
using WTS.Messages;
using WTS.Models;

namespace WTS.ViewModels
{
    public class FoodItemViewModel : BaseViewModel
    {
        private readonly IMessenger _messenger;

        public FoodItemViewModel(IMessenger messenger)
        {
            _messenger = messenger ?? throw new ArgumentNullException(nameof(messenger));

            FoodItem = new FoodItem();
            IngredientsListInput = new ObservableCollection<string>();
            InitializeCommands();
        }

        public FoodItem FoodItem { get; set; }
        public string? NameInput { get; set; }
        public string? IngredientInput { get; set; }
        public ObservableCollection<string> IngredientsListInput { get; set; }
        public string? SelectedIngredient { get; set; }
        public bool? DialogResult { get; set; }

        public ICommand AddIngredientCommand { get; private set; }
        public ICommand UpdateIngredientCommand { get; private set; }
        public ICommand DeleteIngredientCommand { get; private set; }
        public ICommand AddAsFoodItemCommand { get; private set; }
        public ICommand CancelDialogCommand { get; private set; }
        private void InitializeCommands()
        {
            AddIngredientCommand = new RelayCommand(_ => AddIngredient());
            UpdateIngredientCommand = new RelayCommand(_ => UpdateIngredient());
            DeleteIngredientCommand = new RelayCommand(_ => DeleteIngredient());
            AddAsFoodItemCommand = new RelayCommand(_ => AddAsFoodItem());
            CancelDialogCommand = new RelayCommand(_ => DialogResult = true);
        }

        private void AddIngredient()
        {
            if (!string.IsNullOrWhiteSpace(IngredientInput))
            {
                FoodItem.Ingredients.Add(IngredientInput);
                LoadIngredientsList();
                IngredientInput = string.Empty;
            }
        }

        private void UpdateIngredient()
        {
            if (SelectedIngredient is not null && !string.IsNullOrWhiteSpace(IngredientInput))
            {

                int selectedIngredientIndex = IngredientsListInput.IndexOf(SelectedIngredient);
                FoodItem.Ingredients.Replace(selectedIngredientIndex, IngredientInput);
                LoadIngredientsList();
                SelectedIngredient = IngredientInput;
                IngredientInput = string.Empty;

            }
            else
            {
                MessageBox.Show("There are some errors in your input. Please correct them before proceeding.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void DeleteIngredient()
        {
            if (SelectedIngredient is not null)
            {
                int selectedIngredientIndex = IngredientsListInput.IndexOf(SelectedIngredient);
                FoodItem.Ingredients.Delete(SelectedIngredient);
                LoadIngredientsList();

                SelectedIngredient = FoodItem.Ingredients.Count > 0
                    ? IngredientsListInput[Math.Min(selectedIngredientIndex, IngredientsListInput.Count - 1)]
                    : null;

                IngredientInput = string.Empty;
            }
        }

        private void LoadIngredientsList()
        {
            IngredientsListInput.Clear();
            int foodItemsListLength = FoodItem.Ingredients.Count;
            for (int i = 0; i < foodItemsListLength; i++)
            {
                IngredientsListInput.Add(FoodItem.Ingredients.GetItemAt(i));
            }
        }

        private void AddAsFoodItem()
        {
            if (FoodItem is not null && !string.IsNullOrEmpty(NameInput) && FoodItem.Ingredients.Count > 0)
            {
                FoodItem.Name = NameInput;
                SendFoodItem(FoodItem);
                DialogResult = true;
            }
            else
            {
                MessageBox.Show("There are some errors in your input. Please correct them according to the instructions before proceeding.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        public void SendFoodItem(FoodItem foodItem)
        {
            _messenger.Send(new FoodItemMessage(foodItem));
        }
    }
}