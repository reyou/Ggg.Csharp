using Microsoft.AspNetCore.Razor.TagHelpers;
using System;

namespace intro1
{
    public class CountryListTagHelper : TagHelper
    {
        private readonly ICountryService _countryService;

        public CountryListTagHelper(ICountryService countryService)
        {
            _countryService = countryService;
        }

        public string SelectedValue { get; set; }


        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "select";
            output.Content.Clear();
            foreach (var country in _countryService.All())
            {
                var seleted = "";
                if (SelectedValue != null && SelectedValue.Equals(country.Code, StringComparison.CurrentCultureIgnoreCase))
                {
                    seleted = " selected=\"selected\"";
                }
                var listItem = $"<option value=\"{country.Code}\"{seleted}>{country.Name}</option>";
                output.Content.AppendHtml(listItem);
            }
        }
    }

}
