using WTS.Enums;

namespace WTS.Models
{
    public class FoodSchedule
    {
        private readonly List<string> _foodList = new();
        public EaterType EaterType { get; set; }
        public int Count => _foodList.Count;

        public void Add(string item) => _foodList.Add(item);

        public bool ChangeAt(int index, string item)
        {
            if (!IsIndexInRange(index))
            {
                return false;
            }

            _foodList[index] = item;
            return true;
        }

        public bool DeleteAt(int index)
        {
            if (!IsIndexInRange(index)) { return false; }

            _foodList.RemoveAt(index);
            return true;
        }

        public string[] GetFoodListInfoStrings() => _foodList.ToArray();

        public string Title() => $"Food Schedule for {EaterType}";

        public override string ToString() => string.Join("\n", _foodList);

        public bool IsIndexInRange(int index) => index >= 0 && index < _foodList.Count;
    }
}