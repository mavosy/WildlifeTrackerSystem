namespace WTS.ViewModels.Comparers
{
    /// <summary>
    /// Compares two instances of AnimalListItemViewModel by the given property.
    /// If the property values are equal or both null, returns the comparison result of their IDs instead.
    /// </summary>
    public class AnimalNameComparer : IComparer<AnimalListItemViewModel>
    {
        public int Compare(AnimalListItemViewModel? x, AnimalListItemViewModel? y)
        {
            int idCompare = string.Compare(x.Id, y.Id);

            if (x?.Name == null && y?.Name == null) return idCompare;
            if (x?.Name == null) return -1;
            if (y?.Name == null) return 1;

            int nameCompare = string.Compare(x?.Name, y?.Name);

            if (nameCompare == 0)
            {
                return idCompare;
            }

            return nameCompare;
        }
    }
}