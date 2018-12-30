using System.ComponentModel.DataAnnotations;
using Softuni.Community.Data.Common;

namespace Softuni.Community.Data.Models.Enums
{
    public enum JokeCategory
    {
        [Display(Name = DataEnumConstants.ChuckNorris)]
        ChuckNorris = 0,
        Animals = 1,
        Computers = 2,
        [Display(Name = DataEnumConstants.DriversAndPilots)]
        DriversAndPilots = 3,
        Ivancho = 4,
        Students = 5,
        Different = 6
    }
}
