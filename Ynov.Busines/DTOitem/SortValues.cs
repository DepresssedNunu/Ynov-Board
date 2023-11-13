namespace Ynov.Business.DTOitem;

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