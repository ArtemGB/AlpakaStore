using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DbControl;
using Core.Model.Ordering;

namespace Core.Model.Managing
{
    public class FilterManager
    {
        private StoreDbContext _dbContext;

        /// <summary>
        /// Фильтры
        /// </summary>
        public List<Filter> Filters => _dbContext.Filters.ToList();

        public FilterManager()
        {
            _dbContext = new StoreDbContext();
        }

        public Filter AddFilter(string filterString)
        {
            Filter newFilter = new Filter();
            newFilter.AddFilter(filterString);
            _dbContext.Filters.Add(newFilter);
            return newFilter;
        }
    }
}
