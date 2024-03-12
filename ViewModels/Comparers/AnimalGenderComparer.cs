namespace WTS.ViewModels.Comparers
{
    /// <summary>
    /// Compares two instances of AnimalListItemViewModel by the given property.
    /// If the property values are equal or both null, returns the comparison result of their IDs instead.
    /// </summary>
    public class AnimalGenderComparer : IComparer<AnimalListItemViewModel>
    {
        public int Compare(AnimalListItemViewModel x, AnimalListItemViewModel y)
        {
            int genderCompare = string.Compare(x.Gender, y.Gender);
            int idCompare = string.Compare(x.Id, y.Id);

            if (genderCompare == 0)
            {
                return idCompare;
            }
            return genderCompare;
        }
    }
}