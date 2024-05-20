namespace BuildingBlocks.Pagination
{
    public class PaginatedResult<TEntity>
        (int pageIndex, int pageSize, long count, IEnumerable<TEntity> data)
    {
        public long Count { get; } = count;
        public int PageSize { get; } = pageSize;
        public int PageIndex { get; } = pageIndex;
        public IEnumerable<TEntity> Data { get; } = data;


    }
}