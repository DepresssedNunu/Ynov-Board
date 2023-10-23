namespace Ynov.API.DTOitem;

public enum SortValues
{
    DateAscending,
    DateDescending,
    TitleAscending,
    TitleDescending,
}

public class BoardSort
{
    public SortValues Query { get; set; }
}