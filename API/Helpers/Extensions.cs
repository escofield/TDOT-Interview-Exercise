using IO.Swagger.Models;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
namespace IO.Swagger.Helpers
{
    /// extension methods
    public static class Helpers{
        
        /// returns null if string is null or empty
        public static string NullIf(this string s)
        {
            if(string.IsNullOrEmpty(s)) return null;
            return s;
        }
    }
}