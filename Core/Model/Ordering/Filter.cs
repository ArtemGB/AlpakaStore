using System;

namespace Core.Model.Ordering
{
    public class Filter
    {
        public int Id { get; set; }

        /// <summary>
        /// Хэш теги, указываются в одну строку для товара через #.
        /// </summary>
        private string _value;

        public string Value
        {
            get => _value;
        }

        public Filter()
        {
            _value = "";
        }

        /// <summary>
        /// Добавляет новый фильтр в строку фильтров.
        /// </summary>
        /// <param name="filter"></param>
        void AddFilter(string filter)
        {
            if (filter.Contains("#"))
                throw new ArgumentException("Filter can't contains symbol #.");
            _value += filter.ToLower() + "#";
        }

        void RemoveFilter(string filter)
        {
            if (filter.Contains("#"))
                throw new ArgumentException("Filter can't contains symbol #.");
            if (_value.Contains(filter))
                _value = _value.Replace(filter + "#", "");
            else
                throw new ArgumentException($"Filters doesn't contains filter {filter}.");
        }
    }
}
