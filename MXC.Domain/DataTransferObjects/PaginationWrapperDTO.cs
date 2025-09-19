namespace MXC.Domain.DataTransferObjects;

public class PaginationWrapperDTO<T>
{
    public ICollection<T> Items { get; set; } = [];
    public int ItemCount { get; set; }
    public int ItemsOnPage { get; set; }
    public int PageNumber { get; set; }
}