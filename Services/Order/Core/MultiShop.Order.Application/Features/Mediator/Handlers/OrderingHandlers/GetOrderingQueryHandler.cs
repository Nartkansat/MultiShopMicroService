using MediatR;
using MultiShop.Order.Application.Features.Mediator.Queries.OrderingQueries;
using MultiShop.Order.Application.Features.Mediator.Results.OrderingResults;
using MultiShop.Order.Application.Interfaces;
using MultiShop.Order.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Order.Application.Features.Mediator.Handlers.OrderingHandlers
{
    public class GetOrderingQueryHandler : IRequestHandler<GetOrderingQuery, List<GetOrderingQueryResult>>
    {
        //IRequestHandler'te istek yapılan yer GetOrderingQueryResult, yanıtı ise GetOrderingQueryResult buradan yapılacak.

        private readonly IRepository<Ordering> _repository;

        public GetOrderingQueryHandler(IRepository<Ordering> repository)
        {
            _repository = repository;
        }

        public async Task<List<GetOrderingQueryResult>> Handle(GetOrderingQuery request, CancellationToken cancellationToken)
        {
            //CancellationToken sekmede yenilenince vs işlem iptal olsun mu diye kullanılabilir ama burada kullanılmıyor

            var values = await _repository.GetAllAsync();
            return values.Select(x=>new GetOrderingQueryResult
            {
                OrderDate = x.OrderDate,
                OrderingId = x.OrderingId,
                TotalPrice = x.TotalPrice,
                UserId = x.UserId
            }).ToList();
        }
    }
}
