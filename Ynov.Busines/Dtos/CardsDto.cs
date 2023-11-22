using Ynov.Business.Models;

namespace Ynov.Business.Dtos;

public record CreateCardDto
(
    string Name,
    string Description,
    long BoardId,
    Priority Priority
);

public record ModifyCardDto
( 
    string Name,
    string Description
);

public record ModifyCardDescriptionDto
( 
    string Description
);

public record ModifyCardNameDto
( 
    string Name
);