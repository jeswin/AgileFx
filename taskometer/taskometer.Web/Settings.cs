using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace taskometer.Web
{
    public static class Settings
    {
        public const int ITEMS_PER_PAGE = 10;

        public static int GetPageStart(int? page, int max)
        {
            // handle all
            if (page.HasValue && page.Value == 0)
            {
                return 0;
            }
            else
            {
                return max * (page.GetValueOrDefault(1) - 1) + 1;
            }
        }
    }
}
