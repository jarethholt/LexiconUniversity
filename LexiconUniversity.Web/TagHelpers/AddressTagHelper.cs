﻿using Microsoft.AspNetCore.Razor.TagHelpers;

namespace LexiconUniversity.Web.TagHelpers;

public class AddressTagHelper : TagHelper
{
    public string Street { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string ZipCode { get; set; } = string.Empty;

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "p";
        output.Content.SetContent($"{Street}\n{ZipCode} {City}");
    }
}
