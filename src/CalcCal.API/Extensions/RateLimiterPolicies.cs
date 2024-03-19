namespace CalcCal.API.Extensions;

public static class RateLimiterPolicies
{
    public static readonly string FixedLoose = "fixed-loose";
    public static readonly string FixedStrict = "fixed-strict";
    public static readonly string FixedStandard = "fixed-standard";
}