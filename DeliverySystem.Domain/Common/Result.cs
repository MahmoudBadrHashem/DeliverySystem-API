namespace DeliverySystem.Domain.Common;

public class Result
{

    public bool IsSuccess { get; set; }
    public IEnumerable<Errors?> Errors { get; set; }

    public Result(bool isSuccess)
    {
        IsSuccess = isSuccess;
        Errors = new List<Errors?>();
    }

    public Result(bool isSuccess, IEnumerable<Errors?> errors)
    {
        IsSuccess = isSuccess;
        Errors = errors;
    }
}