namespace CalcCal.API.Extensions;

public static class RateLimiterPolicies
{
    public static readonly string FixedLoose = "fixed-loose";
    public static readonly string FixedStrict = "fixed-strict";
    public static readonly string FixedStandard = "fixed-standard";
    public static readonly string UserOnePerMinute = "fixed-oneper5minutes";
    public static readonly string UserOnePerHour = "user-one-per-hour";
}