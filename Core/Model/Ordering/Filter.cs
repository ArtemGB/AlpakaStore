using System;

namespace Core.Model.Ordering
{
    public class Filter
    {
        public int Id { get; set; }

        /// <summary>
        /// Хэш теги, указываются в одну строку для товара через #.
        /// </summary>
        public string TagsString { get; private set; }

        public Filter()
        {
            TagsString = "";
        }

        /// <summary>
        /// Добавляет новый тэг в строку фильтров.
        /// </summary>
        /// <param name="tag"></param>
        public void AddTag(string tag)
        {
            if (string.IsNullOrEmpty(tag) || string.IsNullOrWhiteSpace(tag))
                throw new ArgumentNullException(nameof(tag));
            if (tag.Contains("#"))
                throw new ArgumentException("Filter can't contains symbol #.");
            TagsString += tag.ToLower() + "#";
        }

        /// <summary>
        /// Удаление тега из строки фильтров.
        /// </summary>
        /// <param name="tag"></param>
        public void RemoveTag(string tag)
        {
            if (string.IsNullOrEmpty(tag) || string.IsNullOrWhiteSpace(tag))
                throw new ArgumentNullException(nameof(tag));
            if (tag.Contains("#"))
                throw new ArgumentException("Filter can't contains symbol #.");
            if (TagsString.Contains(tag))
                TagsString = TagsString.Replace(tag + "#", "");
            else
                throw new ArgumentException($"Filters doesn't contains tag {tag}.");
        }
    }
}
