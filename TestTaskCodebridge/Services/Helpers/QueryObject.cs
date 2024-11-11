namespace TestTaskCodebridge.Services.Helpers;

public class QueryObject
{
    public string? Name { get; set; } = null;
    public string? Color { get; set; } = null;

    public string? SortBy { get; set; } = null;
    public bool IsDescending { get; set; } = false;
}