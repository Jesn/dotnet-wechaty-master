using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    /// <summary>
    /// 多例模式
    /// </summary>
    public class PersonFactory
    {
        private static readonly Dictionary<string, PersonFactory> _instancesDict = new Dictionary<string, PersonFactory>();
        public string name;


        private PersonFactory()
        {

        }

        public static PersonFactory GetInstance(string name)
        {

            if (_instancesDict?.Count(x => x.Key == name) == 0)
            {
                var aa = new PersonFactory
                {
                    name = name
                };
                _instancesDict.TryAdd(name, aa);
                return aa;
            }
            else
            {
                var aa = _instancesDict[name];
                return aa;
            }
        }

        public static int GetInstaceCount()
        {
            return _instancesDict.Count();
        }

        /// <summary>
        /// 根据名称释放实例
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static bool IDisposable(string name)
        {
            if (_instancesDict?.Count(x => x.Key == name) > 0)
            {
                return _instancesDict.Remove(name);
            }
            return false;
        }

        /// <summary>
        /// 释放所有实例
        /// </summary>
        /// <returns></returns>
        public static void IDisposable()
        {
            var list_name = _instancesDict?.Keys.ToList();

            //foreach (var name in list_name)
            //{
            //    _instancesDict.Remove(name);
            //}
            _instancesDict.Clear();
        }

        public static void remove()
        {
            
            
        }
    }
}
