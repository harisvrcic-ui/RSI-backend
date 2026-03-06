namespace eParking.Helper;

/// <summary>
/// Centralized API route path constants for consistency and easier refactoring.
/// </summary>
public static class ApiRouteConstants
{
    /// <summary>Route segment for filtered/paginated list endpoints.</summary>
    public const string Filter = "filter";

    /// <summary>Route template for resource-by-id endpoints.</summary>
    public const string Id = "{id}";

    // Resource base paths
    public const string Auth = "auth";
    public const string Login = "login";
    public const string Logout = "logout";

    public const string Cities = "cities";
    public const string Countries = "countries";
    public const string Brands = "brands";
    public const string Users = "users";
    public const string Genders = "genders";
    public const string Cars = "Cars";
    public const string Reservations = "Reservations";
    public const string Reviews = "Reviews";
    public const string ParkingZones = "ParkingZones";
    public const string ParkingSpots = "ParkingSpots";
    public const string ParkingSpotTypes = "ParkingSpotTypes";
    public const string Colors = "Colors";
    public const string ReservationTypes = "ReservationTypes";

    public const string DataSeed = "data-seed";
    public const string GenerateImages = "generate-images";
}
