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
        private static readonly ConcurrentDictionary<Type, List<EnumDTO>> dic = new ConcurrentDictionary<Type, List<EnumDTO>>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="enumType"></param>
        /// <returns></returns>
        public static IEnumerable<EnumDTO> GetEnumDTO(Type enumType)
        {
            List<EnumDTO> list;

            if (dic.TryGetValue(enumType, out list)) { return list; }

            list = new List<EnumDTO>();

            var fields = enumType.GetFields();

            foreach (var field in fields)
            {
                if (field.IsSpecialName) continue;

                DescriptionAttribute attribute = field.GetCustomAttribute(typeof(DescriptionAttribute), true) as DescriptionAttribute;

                EnumDTO vo = new EnumDTO();

                vo.Id = (int)field.GetRawConstantValue();

                vo.Description = attribute == null ? field.Name : attribute.Description;

                list.Add(vo);
            }

            dic.TryAdd(enumType, list);

            return list;
        }

        public static string GetEnumDescription(Enum value)
        {
            Type enumType = value.GetType();

            List<EnumDTO> list;

            if (!dic.TryGetValue(enumType, out list))
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