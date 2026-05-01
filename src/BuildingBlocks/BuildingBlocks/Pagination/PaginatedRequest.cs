using System;
using System.Collections.Generic;
using System.Text;

namespace BuildingBlocks.Pagination
{
    public record PaginatedRequest(int PageIndex = 1, int PageSize = 10);
}
