using MediatR;
using MultiShop.Order.Application.Features.Mediator.Results.OrderingResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Order.Application.Features.Mediator.Queries.OrderingQueries
{
    public class GetOrderingQuery:IRequest<List<GetOrderingQueryResult>>
    {
        // mediatr'da bir merkezi sistem var bu merkezi sistem sayesinde kaotik bağımlılık ortadan kalkıyor.
        // Burada GetOrderingQuery çağırdığımız zaman bize geriye GetOrderingQueryResult'ı dönecek.


    }
}
