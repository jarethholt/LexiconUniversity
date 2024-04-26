using LexiconUniversity.Core.Entities;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace LexiconUniversity.Web.TagHelpers;

public class AddressTagHelper : TagHelper
{
    public Address Address { get; set; } = default!;

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "p";
        output.Attributes.SetAttribute("style", "white-space:pre-line");
        output.Content.SetContent($"{Address.Street}\n{Address.ZipCode} {Address.City}");
    }
}
