namespace MongoDbExercise.Core.Domain.Exceptions;

public abstract class CustomException(string error) : Exception(error);