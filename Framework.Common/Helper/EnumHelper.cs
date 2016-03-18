using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Framework.Common.Helper
{
    public sealed class EnumHelper
    {
        /// <summary>
        /// 
        /// </summary>
        private static readonly ConcurrentDictionary<Type, List<EnumDTO>> _enumDict = new ConcurrentDictionary<Type, List<EnumDTO>>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="enumType"></param>
        /// <returns></returns>
        public static IEnumerable<EnumDTO> GetEnumVO(Type enumType)
        {
            List<EnumDTO> list;

            if (_enumDict.TryGetValue(enumType, out list)) { return list; }

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

            _enumDict.TryAdd(enumType, list);

            return list;
        }

        public static string GetEnumDescription(Enum value)
        {
            Type enumType = value.GetType();

            List<EnumDTO> list;

            if (!_enumDict.TryGetValue(enumType, out list))
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
    /// 
    /// </summary>
    public class EnumDTO
    {
        public int Id { get; set; }

        public string Description { get; set; }
    }
}