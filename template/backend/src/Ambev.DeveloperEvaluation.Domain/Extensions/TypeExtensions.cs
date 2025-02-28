using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Domain.Extensions
{
    public static class TypeExtensions
    {
        public static T CreateInstance<T>(this Type type, params object[] args)
        {
            return (T)Activator.CreateInstance(type, args);
        }
    }
}
