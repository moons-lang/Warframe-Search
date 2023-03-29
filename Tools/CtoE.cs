using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WarframeSearch.Tools;

namespace WindowsFormsApp3
{
    internal class CtoE
    {
      //查询英文名称
      public static string selurlname(string searchName,string language) 
       {
            string lang = StatementClump.SelectLanguage(3, language);
            string lan = StatementClump.SelectLanguage(2, lang);
            DataRow row;
            if (language.Equals("繁體中文") || language.Equals("简体中文") || language.Equals("한국어") || language.Equals("Русский"))
            {
                row = StatementClump.SelectRow(language, "urlname", "rivenName", lan, searchName);
            }
            else
            {
                TextInfo myTI = new CultureInfo("en-US", false).TextInfo;
                string s = myTI.ToTitleCase(searchName);
                string ns = s.Replace("_"," ");
                row = StatementClump.SelectRow(language, "urlname", "rivenName", lan, ns);
            }
        
            if (row != null)
            {
                return row.ItemArray[0].ToString();
            }
            else
            {
                return null;
            }
        }

        // 获取最新数据并更新表
        public static void updataData(string language) {
            WebHeaderCollection webHeader = new WebHeaderCollection();
            webHeader.Add("Language", language);
            string nameres = HttpUitls.Get(Config.RIVEN_NAME, webHeader);
            //更新名称表
            if (nameres != null)
            {
                string lang= StatementClump.SelectLanguage(2,language);
                JObject jsonchat = JObject.Parse(nameres);
                IList<JToken> tokens = jsonchat["payload"]["items"].Children().ToList();
                foreach (var item in tokens)
                {
                    // 查询是否已写入数据
                    string urlName = item["url_name"].ToString();
                    string itemName = item["item_name"].ToString();
                    DataTable dt =  StatementClump.SelectTable("cname", "rivenName", "urlname", urlName);
                    //无此数据 则新增
                    if (dt.Rows.Count == 0)
                    {
                        List<string> table = new List<string>();
                        table.Add(lang);
                        table.Add("urlname");
                        List<string> value = new List<string>();
                        value.Add(itemName);
                        value.Add(urlName);
                        StatementClump.NoSearch(1, "rivenName", table, value, null, null);
                    }
                    else //有则更新 
                    {
                        List<string> uptitle = new List<string>();
                        List<string> upvalue = new List<string>();
                        uptitle.Add(lang);
                        upvalue.Add(itemName);
                        StatementClump.NoSearch(2, "rivenName", uptitle, upvalue, "urlname", urlName);
                    }
                }
            }
            //更新属性表
            string attres = HttpUitls.Get(Config.RIVEN_ATTRIBUTES, webHeader);
            if (attres != null)
            {
                string lang = StatementClump.SelectLanguage(1, language);
                JObject jsonchat = JObject.Parse(attres);
                IList<JToken> tokens = jsonchat["payload"]["attributes"].Children().ToList();
                foreach (var item in tokens)
                {
                    // 查询是否已写入数据
                    string urlName = item["url_name"].ToString();
                    string effect = item["effect"].ToString();
                    DataTable dt = StatementClump.SelectTable("attcname", "attName", "atturlname", urlName);
                    // 无此数据 则新增
                    if (dt.Rows.Count == 0)
                    {
                        List<string> table = new List<string>();
                        table.Add(lang);
                        table.Add("atturlname");
                        List<string> value = new List<string>();
                        value.Add(effect);
                        value.Add(urlName);
                        StatementClump.NoSearch(1,"attName", table, value,null,null);
                    }
                    else //有则更新 
                    {
                        List<string> uptitle = new List<string>();
                        List<string> upvalue = new List<string>();
                        uptitle.Add(lang);
                        upvalue.Add(effect);
                        StatementClump.NoSearch(2, "attName", uptitle, upvalue, "atturlname", urlName);
                    }
                }
            }
            MessageBox.Show("更新完成", "提示");
        }
    }
}
