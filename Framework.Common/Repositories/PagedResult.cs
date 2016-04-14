using System.Collections.Generic;

namespace Framework.Common.Repositories
{
    public class PagedResult<T>
    {
        /// <summary>
        /// 总记录数。
        /// </summary>
        public int TotalItems { get; set; }

        /// <summary>
        /// 总页数。
        /// </summary>
        public int TotalPages { get; set; }

        /// <summary>
        /// 每页记录数。
        /// </summary>
        public int ItemsPerPage { get; set; }

        /// <summary>
        /// 当前页。
        /// </summary>
        public int CurrentPage { get; set; }

        /// <summary>
        /// 当页记录。
        /// </summary>
        public IEnumerable<T> Items { get; set; }
    }
}