using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp3
{
    internal class Config
    {
        public static string RIVEN_SEARCH ="https://api.warframe.market/v1/auctions/search?type=riven";
        public static string RIVEN_NAME = "https://api.warframe.market/v1/riven/items";
        public static string RIVEN_ATTRIBUTES = "https://api.warframe.market/v1/riven/attributes";
        public static string SQLLITE_PATH = "E:\\wf\\WindowsFormsApp3\\warframe.db";
        public static string OFFICIAL_RIVEN_PRICE = "https://api.warframestat.us/";
    }
}
