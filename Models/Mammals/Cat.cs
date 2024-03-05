﻿using System.ComponentModel.DataAnnotations;
using WTS.Enums;

namespace WTS.Models.Mammals
{
    internal class Cat : Mammal
    {
        public Cat(string id, string? name, int? age, GenderType gender, int numberOfLegs, string breed) 
            : base(id, name, age, gender, numberOfLegs)
        {
            Breed = breed;
        }

        [Required(ErrorMessage = "This information is required")]
        [StringLength(50,ErrorMessage = "Breed must be 50 characters or less")]
        public string Breed { get; set; }

        public override string ToString()
        {
            return $"{base.ToString()}, Breed: {Breed}";
        }
    }
}