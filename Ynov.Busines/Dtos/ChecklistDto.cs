namespace Ynov.Business.Dtos;

public record CreateChecklistDto
(
    string Name,
    long CardId
);

public record EditChecklistDto(
    string Name
);