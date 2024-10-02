namespace Ordering.Domain.Exceptions;

public static class ExceptionMessageExtension
{
    public static string GetEmptyMessageByNameEntity(string nameEntity) => $"{nameEntity} cannot be empty.";
}
