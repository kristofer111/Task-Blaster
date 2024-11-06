namespace TaskBlaster.TaskManagement.Models;

public class Envelope<T> where T : class
{
    public Envelope(int pageNumber, int pageSize, IEnumerable<T> items)
    {
        Items = items.Skip((pageNumber - 1) * pageSize).Take(pageSize);
        MaxCount = (int)Math.Ceiling((decimal)items.Count() / pageSize);
        PageNumber = pageNumber;
        PageSize = pageSize;
    }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int MaxCount { get; set; }
    public IEnumerable<T> Items { get; set; } = null!;
}