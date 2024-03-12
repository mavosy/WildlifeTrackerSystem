namespace WTS.ViewModels.Comparers
{
    /// <summary>
    /// Compares two instances of AnimalListItemViewModel by the given property.
    /// If the property values are equal or both null, returns the comparison result of their IDs instead.
    /// </summary>
    public class AnimalCategoryComparer : IComparer<AnimalListItemViewModel>
    {
        public int Compare(AnimalListItemViewModel x, AnimalListItemViewModel y)
        {
            int categoryCompare = string.Compare(x.Category, y.Category);
            int idCompare = string.Compare(x.Id, y.Id);

            if (categoryCompare == 0)
            {
                return idCompare;
            }
            return categoryCompare;
        }
    }
}