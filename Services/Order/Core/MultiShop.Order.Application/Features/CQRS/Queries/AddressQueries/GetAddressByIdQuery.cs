using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Order.Application.Features.CQRS.Queries.AddressQueries
{
    public class GetAddressByIdQuery
    {
        // listeleme işlemlerimizde parametre tutuyor queries kısımları genelde.

        public int Id { get; set; }

        //constructer geçmemiz lazım
        public GetAddressByIdQuery(int id)
        {
            Id = id;
        }
    }
}
