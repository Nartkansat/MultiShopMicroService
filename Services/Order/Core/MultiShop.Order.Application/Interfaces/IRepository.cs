using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Order.Application.Interfaces
{
    public interface IRepository<T> where T : class
    {
        // generic bir interface tanımladık, bir T değeri verdik ve bu T değeri mutlaka bir class olmalı

        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task CreateAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);

        // Func-> bir giriş ve bir çıkış değeri istiyor.
        // giriş değerimiz T, çıkış değerimiz ise true ya da false
        // filter ise gönderilen parametreyi tutacak.
        Task<T> GetByFilterAsync(Expression<Func<T,bool>> filter);
    }
}
