namespace Diurn.Contracts;

public record ActivityResponse(Guid Id, string Name, ActivityTypeResponse Type);