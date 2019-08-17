using FoodService.DAO.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodService.DAO.DataAccess
{
    public interface IBaseDao<T>
    {
        void Insert(IList<T> dtos);
        void Insert(T dto);
        void Delete(T dto);
        void Delete(IList<T> dtos);
        void Update(T dto);
        void Update(IList<T> dtos);
        T SelectById(T dto);
        //IList<T> Select(IList<T> dtos);
    };
}
