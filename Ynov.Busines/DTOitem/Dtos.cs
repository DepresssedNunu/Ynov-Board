using System.ComponentModel.DataAnnotations;
using Ynov.Business.Models;

namespace Ynov.Business.Dtos;

public record BoardDto
(
    List<Card> CardList,
    string Name,
    long Id
);

public record CreateBoardDto
(
    string Name
);

public record CreateCardDto
(
    string Name,
    string Description,
    [Required] long BoardId
);