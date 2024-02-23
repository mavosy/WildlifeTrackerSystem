using WTS.Enums;

namespace WTS.Models.AnimalBase
{
    /// <summary>
    /// Base class for all animal models
    /// </summary>
    /// <param name="id"></param>
    /// <param name="age"></param>
    /// <param name="categoryType"></param>
    /// <param name="genderType"></param>
    /// <param name="name"></param>
    internal abstract class Animal(string id, int age, CategoryType categoryType, GenderType genderType = GenderType.Unknown, string? name = "No name")
    {
        private string? _name = name;
        private string _id = id;
        private int _age = age;
        private CategoryType _category = categoryType;
        private GenderType _gender = genderType;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public int Age
        {
            get { return _age; }
            set { _age = value; }
        }

        public GenderType Gender
        {
            get { return _gender; }
            set { _gender = value; }
        }

        public CategoryType Category
        {
            get { return _category; }
            set { _category = value; }
        }
    }
}