using System.ComponentModel.DataAnnotations;

namespace LexiconUniversity.Core.Entities;

public class Address
{
    [StringLength(35, MinimumLength = 1)]
    public string Street { get; set; } = string.Empty;

    [RegularExpression(@"[\d]{3}[-\w]?[\d]{2}")]
    public string ZipCode { get; set; } = string.Empty;

    [StringLength(35, MinimumLength = 1)]
    public string City { get; set; } = string.Empty;
}
