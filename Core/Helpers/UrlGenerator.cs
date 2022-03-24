using System;
using System.Linq;

namespace Core.Helpers
{
    public static class UrlGenerator
    {
        public static string Generate(string title)
        {
            return $"{title.Replace(' ', '-')}-{Guid.NewGuid()}".ToLower();
        }
    }
}