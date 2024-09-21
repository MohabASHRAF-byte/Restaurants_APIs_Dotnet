namespace Restuarants.infrastructure.Authorization;

public static class PolicyNames
{
    public const string HasNationality = "HasNationality";
    public const string AtLeast20 = "AtLeast20";
    public const string AtLeast2Restaurant = "AtLeast2Restaurant";
}

public static class ClaimsTypes
{
    public const string Nationality = "Nationality";
    public const string BirthDay = "BirthDay";
}