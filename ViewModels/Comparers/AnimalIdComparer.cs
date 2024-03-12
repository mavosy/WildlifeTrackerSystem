namespace WTS.ViewModels.Comparers
{
    /// <summary>
    /// Compares two instances of AnimalListItemViewModel by the given property.
    /// </summary>
    public class AnimalIdComparer : IComparer<AnimalListItemViewModel>
    {
        public int Compare(AnimalListItemViewModel x, AnimalListItemViewModel y)
        {
            return string.Compare(x.Id, y.Id);
        }
    }
}