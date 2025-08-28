namespace ProductCatalog.Application.Common.Models;

public class Result
{
    public bool IsSuccess { get; set; }
    public string[] Errors { get; set; } = Array.Empty<string>();

    public static Result Success() => new Result { IsSuccess = true };
    public static Result Failure(params string[] errors) => new Result { Errors = errors };
}

public class Result<T> : Result
{
    public T? Data { get; set; }

    public static Result<T> Success(T data) => new Result<T> { IsSuccess = true, Data = data };
    public static new Result<T> Failure(params string[] errors) => new Result<T> { Errors = errors };
}

public class PagedResult<T>
{
    public IReadOnlyList<T> Items { get; set; } = new List<T>();
    public int TotalCount { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
    public bool HasPreviousPage => PageNumber > 1;
    public bool HasNextPage => PageNumber < TotalPages;
}

public class PaginationQuery
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}
