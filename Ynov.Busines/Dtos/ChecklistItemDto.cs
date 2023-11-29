namespace Ynov.Business.Dtos;

public record CreateChecklistItemDto
(
    string Name,
    long ChecklistId
);

public record EditChecklistItemDto(
    string Name
);