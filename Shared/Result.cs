using System.Net.NetworkInformation;

namespace dotnet_weather_app.Shared;

public class Result
{
  public const string GENERIC_ERROR_MESSAGE = "";
  protected internal Result(bool isSuccess, Error error)
  {
    if (isSuccess && error != Error.None)
    {
      throw new InvalidOperationException();
    }

    if (!isSuccess && error == Error.None)
    {
      throw new InvalidOperationException();
    }

    IsSuccess = isSuccess;
    Error = error;
  }

  public bool IsSuccess { get; }

  public bool IsFailure => !IsSuccess;

  public Error Error { get; }
  public string GenericErrorMessage() => GENERIC_ERROR_MESSAGE;


  public static Result Success() => new(true, Error.None);

  public static Result<TValue> Success<TValue>(TValue value) => new(value, true, Error.None);

  public static Result Failure(Error error) => new(false, error);

  public static Result<TValue> Failure<TValue>(Error error) => new(default, false, error);
}
