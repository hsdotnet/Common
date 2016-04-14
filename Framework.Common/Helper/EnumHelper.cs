using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Framework.Common.Helper
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class EnumHelper
    {
        /// <summary>
        /// 
        /// </summary>
        private static readonly ConcurrentDictionary<Type, List<EnumDTO>> _dict = new ConcurrentDictionary<Type, List<EnumDTO>>();

        /// <summary>
        /// 获取枚举信息
        /// </summary>
        /// <param name="enumType">枚举</param>
        /// <returns></returns>
        public static IEnumerable<EnumDTO> GetEnumVO(Type enumType)
        {
            List<EnumDTO> list;

            if (_dict.TryGetValue(enumType, out list)) { return list; }

            list = new List<EnumDTO>();

            var fields = enumType.GetFields();

            foreach (var field in fields)
            {
                if (field.IsSpecialName) continue;

                DescriptionAttribute attribute = field.GetCustomAttribute(typeof(DescriptionAttribute), true) as DescriptionAttribute;

                EnumDTO dto = new EnumDTO();

                dto.Id = (int)field.GetRawConstantValue();

                dto.Description = attribute == null ? field.Name : attribute.Description;

                list.Add(dto);
            }

            _dict.TryAdd(enumType, list);

            return list;
        }

        /// <summary>
        /// 获取枚举描叙
        /// </summary>
        /// <param name="value">枚举</param>
        /// <returns>描叙信息</returns>
        public static string GetEnumDescription(Enum value)
        {
            Type enumType = value.GetType();

            List<EnumDTO> list;

            if (!_dict.TryGetValue(enumType, out list))
            {
                FieldInfo field = value.GetType().GetField(value.ToString());

                DescriptionAttribute attribute = field.GetCustomAttribute(typeof(DescriptionAttribute), true) as DescriptionAttribute;

                return attribute == null ? value.ToString() : attribute.Description;
            }
            else
                return list.FirstOrDefault(m => m.Description == value.ToString()).Description;
        }
    }

    /// <summary>
    /// 枚举DTO
    /// </summary>
    public class EnumDTO
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Title
        /// </summary>
        public string Description { get; set; }
    }
}