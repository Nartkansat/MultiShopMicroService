using MultiShop.Cargo.DataAccessLayer.Abstract;
using MultiShop.Cargo.DataAccessLayer.Concrete;
using MultiShop.Cargo.DataAccessLayer.Repositories;
using MultiShop.Cargo.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Cargo.DataAccessLayer.EntityFramework
{
    public class EfCargoCompanyDal : GenericRepository<CargoCompany>, ICargoCompanyDal
    {
        //GenericRepository içerisinde kullandığımız constructorımız GenericRepository başka bir classa inherit(Miras) verildiği zaman
        // o constructor bizden bir adet CargoContext türünde değer bekliyor.
        // base ile miras aldığımız sınıfa bu context değerini iletip constructor metodun işlevini yerine getirmesini sağlıyoruz.
        //kullanmazsak constructor metodu kullanılamaz dolayısıyla GenericRepository'de kullanılamaz.
        public EfCargoCompanyDal(CargoContext context) : base(context)
        {
            
        }
    }
}
