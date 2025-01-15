using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.Net.NetworkInformation;

namespace dotnet_weather_app.Shared;

public class Result
{
  public const string GENERIC_ERROR_MESSAGE = "There has been a server error.";
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
  public string GetGenericErrorMessage() => GENERIC_ERROR_MESSAGE;


  public static Result Success() => new(true, Error.None);

  public static Result<TValue> Success<TValue>(TValue value) => new(value, true, Error.None);

  public static Result Failure(Error error) => new(false, error);

  public static Result<TValue> Failure<TValue>(Error error) => new(default, false, error);
  public static Result<TValue> Create<TValue>(TValue? value) => value is not null ? Success(value) : Failure<TValue>(new(System.Net.HttpStatusCode.InternalServerError, GENERIC_ERROR_MESSAGE));
}
