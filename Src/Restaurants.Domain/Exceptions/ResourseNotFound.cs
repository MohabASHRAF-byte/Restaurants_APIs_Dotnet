namespace Restaurants.Domain.Exceptions;

public class ResourseNotFound(string type, string id)
    : Exception($"No {type} with Id : {id} exists. ")
{
}