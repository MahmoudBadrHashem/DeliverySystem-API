namespace DeliverySystem.Domain.Common;

public class Errors
{
    public Errors(string? code, string? description)
    {
        Code = code;
        Description = description;
    }

    public string? Code { get; set; } = default!;
    public string? Description { get; set; } = default!;

}