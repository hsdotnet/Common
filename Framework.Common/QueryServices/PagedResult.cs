﻿using System;
using System.Collections.Generic;

namespace Framework.Common.QueryServices
{
    /// <summary>
    /// 分页对象
    /// </summary>
    /// <typeparam name="T">数据对象类型</typeparam>
    public class PagedResult<T>
    {
        /// <summary>
        /// 总记录数。
        /// </summary>
        public int TotalItems { get; set; }

        /// <summary>
        /// 总页数。
        /// </summary>
        public int TotalPages
        {
            get
            {
                return this.TotalItems % this.ItemsPerPage == 0 ? this.TotalItems / this.ItemsPerPage : this.TotalItems / this.ItemsPerPage + 1;
            }
        }

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

        /// <summary>
        /// 构造函数
        /// </summary>
        public PagedResult()
        {
            this.ItemsPerPage = 10;
            this.CurrentPage = 1;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="currentPage">当前页</param>
        /// <param name="itemsPerPage">每页记录数</param>
        public PagedResult(int currentPage, int itemsPerPage)
        {
            if (currentPage <= 0)
                throw new ArgumentOutOfRangeException("CurrentPage", "当前页面不能小于零");
            if (itemsPerPage <= 0)
                throw new ArgumentOutOfRangeException("CurrentPage", "每页记录数不能小于零");

            this.CurrentPage = currentPage;
            this.ItemsPerPage = itemsPerPage;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="currentPage">当前页</param>
        /// <param name="itemsPerPage">每页记录数</param>
        /// <param name="totalItems">总记录数</param>
        /// <param name="items">当页记录</param>
        public PagedResult(int currentPage, int itemsPerPage, int totalItems, IEnumerable<T> items)
            : this(currentPage, itemsPerPage)
        {
            this.TotalItems = totalItems;
            this.Items = items;
        }
    }
}