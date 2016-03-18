using System;
using System.Web;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Collections.Concurrent;
using System.IO;

using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;

namespace Framework.Common.Helper
{
    /// <summary>
    /// NPOI Excel
    /// </summary>
    public class ExcelHelper
    {
        #region Private Fields
        private static readonly ConcurrentDictionary<Type, PropertyInfo[]> _paramCache = new ConcurrentDictionary<Type, PropertyInfo[]>();

        private static HSSFWorkbook _workbook;

        private static ICellStyle _cellHeaderStyle;

        private static ICellStyle _cellBodyStyle;

        private static ICellStyle _cellDateStyle;

        /// <summary>
        /// 默认单元格高度
        /// </summary>
        private static short _defaultHeight = 10;

        /// <summary>
        /// 默认单元格宽度
        /// </summary>
        private static short _defaultWidth = 10;

        /// <summary>
        /// 默认头字体
        /// </summary>
        private static string _defaultHeaderFontName = "微软雅黑";

        /// <summary>
        /// 默认正文字体
        /// </summary>
        private static string _defaultBodyFontName = "微软雅黑";
        #endregion

        #region Public Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="sheetColumns"></param>
        /// <param name="sheetName"></param>
        /// <param name="excelName"></param>
        public static void ListToExcel(IList source, List<SheetColumn> sheetColumns = null, string sheetName = null, string excelName = null)
        {
            if (source == null || source.Count == 0) { throw new ArgumentNullException("未提供数据源"); }

            Initialise();

            FillExcel(source, sheetColumns, sheetName);

            SaveExcel(excelName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="stream"></param>
        /// <param name="sheetColumns"></param>
        /// <param name="isFirstRowColumn"></param>
        /// <returns></returns>
        public static IEnumerable<T> ExcelToList<T>(Stream stream, List<SheetColumn> sheetColumns = null, bool isFirstRowColumn = true) where T : new()
        {
            List<T> list = new List<T>();

            _workbook = new HSSFWorkbook(stream);

            ISheet sheet = _workbook.GetSheetAt(0);

            int startRowNum = isFirstRowColumn ? 1 : 0;

            int rowCount = sheet.LastRowNum;

            if (sheetColumns == null || sheetColumns.Count == 0)
            {
                for (int rowIndex = startRowNum; rowIndex <= rowCount; rowIndex++)
                {
                    T item = new T();

                    PropertyInfo[] properties = GetProperties(item);

                    for (int columnIndex = 0; columnIndex < properties.Count(); columnIndex++)
                    {
                        PropertyInfo propertyInfo = properties[columnIndex];

                        propertyInfo.SetValue(item, GetCellValue(propertyInfo, sheet.GetRow(rowIndex).GetCell(columnIndex)), null);
                    }

                    list.Add(item);
                }
            }
            else
            {
                for (int rowIndex = startRowNum; rowIndex <= rowCount; rowIndex++)
                {
                    T item = new T();

                    PropertyInfo[] properties = GetProperties(item);

                    for (int columnIndex = 0; columnIndex < sheetColumns.Count; columnIndex++)
                    {
                        SheetColumn column = sheetColumns[columnIndex];

                        PropertyInfo propertyInfo = properties.FirstOrDefault(m => m.Name == column.FieldName);

                        if (propertyInfo == null) { throw new ArgumentNullException(string.Format("未找到{0}", column.FieldName)); }

                        propertyInfo.SetValue(item, GetCellValue(propertyInfo, sheet.GetRow(rowIndex).GetCell(columnIndex)), null);
                    }

                    list.Add(item);
                }
            }

            Close();

            return list;
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// 
        /// </summary>
        private static void Initialise()
        {
            _workbook = new HSSFWorkbook();

            SetHeaderStyle();

            SetBodyStyle();

            SetDateStyle();
        }

        /// <summary>
        /// 
        /// </summary>
        private static void Close()
        {
            if (_workbook != null) { _workbook = null; }

            if (_cellHeaderStyle != null) { _cellHeaderStyle = null; }

            if (_cellBodyStyle != null) { _cellBodyStyle = null; }

            if (_cellDateStyle != null) { _cellDateStyle = null; }
        }

        /// <summary>
        /// 表头单元格格式 粗体 文字居中
        /// </summary>
        private static void SetHeaderStyle()
        {
            IFont font = _workbook.CreateFont();
            font.Boldweight = (short)FontBoldWeight.Bold;
            font.FontName = _defaultHeaderFontName;
            _cellHeaderStyle = _workbook.CreateCellStyle();
            _cellHeaderStyle.VerticalAlignment = VerticalAlignment.Center;
            _cellHeaderStyle.Alignment = HorizontalAlignment.Center;
            _cellHeaderStyle.SetFont(font);
        }

        /// <summary>
        /// 单元格格式 文字居中
        /// </summary>
        private static void SetBodyStyle()
        {
            IFont font = _workbook.CreateFont();
            font.FontName = _defaultBodyFontName;
            _cellBodyStyle = _workbook.CreateCellStyle();
            _cellBodyStyle.VerticalAlignment = VerticalAlignment.Center;
            _cellBodyStyle.SetFont(font);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="format"></param>
        /// <returns></returns>
        private static void SetDateStyle(string format = "yyyy-MM-dd")
        {
            IFont font = _workbook.CreateFont();
            font.FontName = _defaultBodyFontName;
            _cellDateStyle = _workbook.CreateCellStyle();
            _cellDateStyle.VerticalAlignment = VerticalAlignment.Center;
            _cellDateStyle.SetFont(font);
            IDataFormat dataFormat = _workbook.CreateDataFormat();
            _cellDateStyle.DataFormat = dataFormat.GetFormat(format);
        }

        /// <summary>
        /// Excel填充数据
        /// </summary>
        /// <param name="source">数据源</param>
        /// <param name="sheetName">Sheet名称</param>
        private static void FillExcel(IList source, List<SheetColumn> sheetColumns = null, string sheetName = null)
        {
            ISheet sheet = string.IsNullOrWhiteSpace(sheetName) ? _workbook.CreateSheet() : _workbook.CreateSheet(sheetName);

            IRow headerRow = sheet.CreateRow(0);

            headerRow.Height = Convert.ToInt16(_defaultHeight * 20);

            if (sheetColumns != null && sheetColumns.Count > 0)
            {
                for (int columnIndex = 0; columnIndex < sheetColumns.Count; columnIndex++)
                {
                    SheetColumn column = sheetColumns[columnIndex];

                    ICell cell = headerRow.CreateCell(columnIndex);

                    cell.CellStyle = _cellHeaderStyle;

                    cell.SetCellValue(column.ColumnName);

                    int width = column.ColumnWidth <= 0 ? _defaultWidth : column.ColumnWidth;

                    sheet.SetColumnWidth(columnIndex, width * 256);
                }

                FillCell(sheet, source, sheetColumns);
            }
            else
            {
                PropertyInfo[] properties = GetProperties(source[0]);

                for (int columnIndex = 0; columnIndex < properties.Count(); columnIndex++)
                {
                    ICell cell = headerRow.CreateCell(columnIndex);

                    cell.CellStyle = _cellHeaderStyle;

                    cell.SetCellValue(properties[columnIndex].Name);

                    sheet.SetColumnWidth(columnIndex, _defaultWidth * 256);
                }

                FillCell(sheet, source);
            }
        }

        /// <summary>
        /// 填充数据
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="source"></param>
        /// <param name="sheetColumns"></param>
        private static void FillCell(ISheet sheet, IList source, List<SheetColumn> sheetColumns)
        {
            for (int rowIndex = 0; rowIndex < source.Count; rowIndex++)
            {
                object @object = source[rowIndex];

                PropertyInfo[] properties = GetProperties(@object);

                IRow row = sheet.CreateRow(rowIndex + 1);

                row.Height = Convert.ToInt16(_defaultHeight * 20);

                for (int columnIndex = 0; columnIndex < sheetColumns.Count; columnIndex++)
                {
                    ICell cell = row.CreateCell(columnIndex);

                    SheetColumn column = sheetColumns[columnIndex];

                    PropertyInfo propertyInfo = properties.FirstOrDefault(m => m.Name == column.FieldName);

                    if (propertyInfo == null) { throw new ArgumentNullException(string.Format("未找到{0}", column.FieldName)); }

                    object value = propertyInfo.GetValue(@object, null);

                    FillCell(cell, propertyInfo, value);
                }
            }
        }

        /// <summary>
        /// 填充数据
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="source"></param>
        private static void FillCell(ISheet sheet, IList source)
        {
            for (int rowIndex = 0; rowIndex < source.Count; rowIndex++)
            {
                object @object = source[rowIndex];

                PropertyInfo[] properties = GetProperties(@object);

                IRow row = sheet.CreateRow(rowIndex + 1);

                row.Height = Convert.ToInt16(_defaultHeight * 20);

                for (int columnIndex = 0; columnIndex < properties.Count(); columnIndex++)
                {
                    ICell cell = row.CreateCell(columnIndex);

                    PropertyInfo propertyInfo = properties[columnIndex];

                    object value = propertyInfo.GetValue(@object, null);

                    FillCell(cell, propertyInfo, value);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cell"></param>
        /// <param name="propertyInfo"></param>
        /// <param name="value"></param>
        private static void FillCell(ICell cell, PropertyInfo propertyInfo, object value)
        {
            switch (propertyInfo.PropertyType.Name)
            {
                case "Boolean":
                    cell.SetCellValue(Convert.ToBoolean(value));
                    cell.CellStyle = _cellBodyStyle;
                    break;
                case "Int16":
                case "Int32":
                case "Int64":
                case "Double":
                case "Decimal":
                    cell.SetCellValue(Convert.ToDouble(value));
                    cell.CellStyle = _cellBodyStyle;
                    break;
                case "DateTime":
                    cell.SetCellValue(Convert.ToDateTime(value));
                    cell.CellStyle = _cellDateStyle;
                    break;
                default:
                    cell.SetCellValue(value == null ? string.Empty : value.ToString());
                    cell.CellStyle = _cellBodyStyle;
                    break;
            }
        }

        private static object GetCellValue(PropertyInfo propertyInfo, ICell cell)
        {
            switch (propertyInfo.PropertyType.Name)
            {
                case "Boolean":
                    return cell.BooleanCellValue;
                case "Int16":
                    return Convert.ToInt16(cell.NumericCellValue);
                case "Int32":
                    return Convert.ToInt32(cell.NumericCellValue);
                case "Int64":
                    return Convert.ToInt64(cell.NumericCellValue);
                case "Double":
                    return cell.NumericCellValue;
                case "Decimal":
                    return Convert.ToDecimal(cell.NumericCellValue);
                case "DateTime":
                    return cell.DateCellValue;
                default:
                    return cell.StringCellValue;
            }
        }

        /// <summary>
        /// 保存Excel
        /// </summary>
        /// <param name="excelName">文件名称</param>
        private static void SaveExcel(string excelName = null)
        {
            if (string.IsNullOrWhiteSpace(excelName)) { excelName = DateTime.Now.ToString("yyyyMMddHHmmss"); }

            HttpContext context = HttpContext.Current;

            if (context != null)
            {
                context.Response.Clear();

                context.Response.ContentType = "application/x-excel";

                context.Response.AddHeader("Content-Disposition", "attachment;filename=" + excelName + ".xls");

                _workbook.Write(context.Response.OutputStream);

                Close();

                context.Response.End();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private static PropertyInfo[] GetProperties(object @object)
        {
            if (@object == null) { return new PropertyInfo[] { }; }

            Type type = @object.GetType();

            PropertyInfo[] properties;

            if (_paramCache.TryGetValue(type, out properties)) { return properties; }

            properties = type.GetProperties();

            _paramCache[type] = properties;

            return properties;
        }
        #endregion
    }

    /// <summary>
    /// 
    /// </summary>
    public class SheetColumn
    {
        /// <summary>
        /// 
        /// </summary>
        public SheetColumn(string fieldName) :
            this(fieldName, null) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="columnName"></param>
        public SheetColumn(string fieldName, string columnName) :
            this(fieldName, columnName, 0) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="columnName"></param>
        /// <param name="columnWidth"></param>
        public SheetColumn(string fieldName, string columnName, int columnWidth)
        {
            this.FieldName = fieldName;
            this.ColumnName = columnName;
            this.ColumnWidth = columnWidth;
        }

        /// <summary>
        /// 字段名字
        /// </summary>
        public string FieldName { get; set; }

        /// <summary>
        /// Excel 列明
        /// </summary>
        public string ColumnName { get; set; }

        /// <summary>
        /// Excel 列宽
        /// </summary>
        public int ColumnWidth { get; set; }
    }
}