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

        public Filter AddFilter()
        {
            Filter newFilter = new Filter();
            _dbContext.Filters.Add(newFilter);
            _dbContext.SaveChanges();
            return newFilter;
        }

        public void AddTagToFilter(int filterId, string tag)
        {
            Filter filter = _dbContext.Filters.Find(filterId);
            if (filter == null)
                throw new ArgumentException($"There is no filter with id = {filterId}");
            filter.AddTag(tag);
            _dbContext.SaveChanges();
        }
    }
}
