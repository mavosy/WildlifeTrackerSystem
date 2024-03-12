namespace WTS.ViewModels.Comparers
{
    /// <summary>
    /// Compares two instances of AnimalListItemViewModel by the given property.
    /// If the property values are equal or both null, returns the comparison result of their IDs instead.
    /// </summary>
    public class AnimalAgeComparer : IComparer<AnimalListItemViewModel>
    {
        public int Compare(AnimalListItemViewModel? x, AnimalListItemViewModel? y)
        {
            int idCompare = string.Compare(x.Id, y.Id);

            if (x?.Age == null && y?.Age == null) return idCompare;
            if (x?.Age == null) return 1;
            if (y?.Age == null) return -1;

            int ageCompare = x.Age.Value.CompareTo(y.Age.Value);

            if (ageCompare == 0)
            {
                return idCompare;
            }

            return ageCompare;
        }
    }
}