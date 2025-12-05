namespace Common.SharedClasses.Exceptions
{
    public class ForbiddenException(string action) : Exception($"{action} is forbidden")
    {
    }
}
