using WTS.Enums;

namespace WTS.Models.AnimalBase
{
    internal abstract class Animal
    {
        private string? _name;
        private int _id;
        private int _age;
        private CategoryType _category;
        private GenderType _gender;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public int Id
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

        public Animal(int id, int age, CategoryType categoryType, GenderType genderType = GenderType.Unknown, string? name = "No name")
        {
            _id = id;
            _age = age;
            _category = categoryType;
            _gender = genderType;
            _name = name;
        }
    }
}