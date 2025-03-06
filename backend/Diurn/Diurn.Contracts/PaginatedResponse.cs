namespace Diurn.Contracts;

public class PaginatedResponse<T>
{
    public required int PageNo { get; set; }
    public required int PageSize { get; set; }
    public required int ItemCount { get; set; }
    
    public required IEnumerable<T> Values { get; set; }
}