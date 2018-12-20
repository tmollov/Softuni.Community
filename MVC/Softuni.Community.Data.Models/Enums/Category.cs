using System.ComponentModel.DataAnnotations;
using Softuni.Community.Data.Common;

namespace Softuni.Community.Data.Models.Enums
{
    public enum Category
    {
        [Display(Name = DataEnumConstants.Other)]
        Other = 0,

        [Display(Name = DataEnumConstants.WebDevelopment)]
        WebDevelopment = 1,

        [Display(Name = DataEnumConstants.AndroidDevelopment)]
        AndroidDevelopment = 2,

        [Display(Name = DataEnumConstants.IosDevelopment)]
        IosDevelopment = 3,

        [Display(Name = DataEnumConstants.DesktopDevelopment)]
        DesktopDevelopment = 4,

        [Display(Name = DataEnumConstants.CSharp)]
        CSharp = 5,

        [Display(Name = DataEnumConstants.C)]
        C = 6,

        [Display(Name = DataEnumConstants.Java)]
        Java = 7,

        [Display(Name = DataEnumConstants.JavaScript)]
        JavaScript = 8,

        [Display(Name = DataEnumConstants.PHP)]
        PHP = 9,

        [Display(Name = DataEnumConstants.Python)]
        Python = 10,
    }
}
