using BuildingBlocks.Core.Exceptions;
using Ordering.Domain.Models;

namespace Ordering.Application.Exceptions;

public class OrderNotFoundException(Guid orderKey) 
    : NotFoundException(nameof(Order), orderKey.ToString())
{
}
