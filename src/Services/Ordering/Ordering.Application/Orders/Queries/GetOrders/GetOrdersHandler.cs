using Microsoft.EntityFrameworkCore;
using Ordering.Application.Extensions;
using Ordering.Application.Data;
using BuildingBlocks.Pagination;
using Ordering.Application.Dtos;
using BuildingBlocks.CQRS;

namespace Ordering.Application.Orders.Queries.GetOrders
{
    public class GetOrdersHandler
        (IApplicationDbContext context)
        : IQueryHandler<GetOrdersQuery, GetOrdersResult>
    {
        public async Task<GetOrdersResult> Handle(GetOrdersQuery query, CancellationToken cancellationToken)
        {
            var pageIndex = query.PaginationRequest.PageIndex;
            var pageSize = query.PaginationRequest.PageSize;
            var totalCount = await context.Orders.CountAsync();
            var orders = await context.Orders.
                Include(o => o.OrderItems)
                .AsNoTracking()
                .Skip(pageSize * pageIndex)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return new GetOrdersResult(new PaginatedResult<OrderDto>(
                pageIndex, pageSize, totalCount, orders.ToOrderDtoList()));
        }
    }
}