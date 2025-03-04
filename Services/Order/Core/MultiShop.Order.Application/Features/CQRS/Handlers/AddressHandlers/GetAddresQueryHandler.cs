﻿using MultiShop.Order.Application.Features.CQRS.Results.AddressResults;
using MultiShop.Order.Application.Interfaces;
using MultiShop.Order.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Order.Application.Features.CQRS.Handlers.AddressHandlers
{
    public class GetAddresQueryHandler
    {
        private readonly IRepository<Address> _repository;

        public GetAddresQueryHandler(IRepository<Address> repository)
        {
            _repository = repository;
        }

        public async Task<List<GetAddressQueryResult>> Handle()
        {
            var values = await _repository.GetAllAsync();
            return values.Select(x => new GetAddressQueryResult
            {
                AddressId = x.AddressId,
                City = x.City,
                Detail1 = x.Detail1,
                District = x.District,
                UserId = x.UserId,
                ZipCode = x.ZipCode,
                Phone = x.Phone,
                Surname = x.Surname,
                Name = x.Name,
                Email = x.Email,
                Detail2 = x.Detail2,
                Description = x.Description,
                Country = x.Country
            }).ToList();
        }
    }
}
