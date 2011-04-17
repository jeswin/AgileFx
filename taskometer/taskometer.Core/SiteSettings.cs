using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace taskometer.Core
{
    public class SubscriptionPlan
    {
        public string Name { get; set; }
        public float MonthlyFee { get; set; }
        public string keyword { get; set; }
    }

    public static class SiteSettings
    {
        public const string DEFAULT_LOGO = "/images/default-logo.png";
        public static List<SubscriptionPlan> ActivePlans = new List<SubscriptionPlan>();

        public static int HTTP_PORT { get; set; }
        public static int HTTPS_PORT { get; set; }
        public static string Domain { get; set; }

        static SiteSettings()
        {
            HTTP_PORT = 80;
            HTTPS_PORT = 443;

            ActivePlans.Add(new SubscriptionPlan() { Name = "Premium", keyword = "premium", MonthlyFee = 100 });
            ActivePlans.Add(new SubscriptionPlan() { Name = "Pro", keyword = "pro", MonthlyFee = 50 });
            ActivePlans.Add(new SubscriptionPlan() { Name = "Standard", keyword = "standard", MonthlyFee = 25 });
            ActivePlans.Add(new SubscriptionPlan() { Name = "Free", keyword = "free", MonthlyFee = 0 });
        }
    }
}
