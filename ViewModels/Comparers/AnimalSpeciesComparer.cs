namespace WTS.ViewModels.Comparers
{
    /// <summary>
    /// Compares two instances of AnimalListItemViewModel by the given property.
    /// If the property values are equal or both null, returns the comparison result of their IDs instead.
    /// </summary>
    public class AnimalSpeciesComparer : IComparer<AnimalListItemViewModel>
    {
        public int Compare(AnimalListItemViewModel x, AnimalListItemViewModel y)
        {
            int speciesCompare = string.Compare(x.Species, y.Species);
            int idCompare = string.Compare(x.Id, y.Id);

            if (speciesCompare == 0)
            {
                return idCompare;
            }
            return speciesCompare;
        }
    }
}