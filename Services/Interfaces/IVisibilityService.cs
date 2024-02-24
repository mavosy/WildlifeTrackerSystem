using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WTS.Services.Interfaces
{
    internal interface IVisibilityService
    {
        public void UpdateSpeciesInputVisibility();
        public void UpdateCategoryInputVisibility();
        public void ResetCategoryInputVisibility();
        public void ResetSpeciesInputVisibility();
        public Dictionary<string, Action> InitializeSpeciesVisibilityMap();
    }
}