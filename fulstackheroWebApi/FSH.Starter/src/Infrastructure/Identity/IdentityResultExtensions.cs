﻿using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace FSH.Starter.Infrastructure.Identity;

internal static class IdentityResultExtensions
{
    public static List<string> GetErrors(this IdentityResult result, IStringLocalizer localizer) =>
        result.Errors.Select(e => localizer[e.Description].ToString()).ToList();
}