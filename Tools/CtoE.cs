using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp3
{
    internal class CtoE
    {
      //查询英文名称
      public static string selEName(string chineseName) 
       {
            SQLiteHelper war = new SQLiteHelper(Config.SQLLITE_PATH);
            StringBuilder selsb = new StringBuilder();
            selsb.Append("SELECT ename FROM rivenName WHERE cname=");
            selsb.Append("'");
            selsb.Append(chineseName);
            selsb.Append("'");
            DataRow row = war.ExecuteDataRow(selsb.ToString());
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
        public static void updataData() {
            int val = 0;
            string nameUrl = Config.RIVEN_NAME;
            string attUrl = Config.RIVEN_ATTRIBUTES;
            WebHeaderCollection webHeader = new WebHeaderCollection();
            webHeader.Add("Language", "zh-hans");
            string nameres = HttpUitls.Get(nameUrl, webHeader);
            //更新名称表
            if (nameres != null)
            {
                SQLiteHelper war = new SQLiteHelper(Config.SQLLITE_PATH);
                JObject jsonchat = JObject.Parse(nameres);
                IList<JToken> tokens = jsonchat["payload"]["items"].Children().ToList();
                foreach (var item in tokens)
                {
                    // 查询是否已写入数据
                    string urlName = item["url_name"].ToString();
                    string itemName = item["item_name"].ToString();
                    StringBuilder selsb = new StringBuilder();
                    selsb.Append("SELECT cname FROM rivenName WHERE ename=");
                    selsb.Append("'");
                    selsb.Append(urlName);
                    selsb.Append("'");
                    DataTable dt = war.ExecuteDataTable(selsb.ToString());
                    // 无此数据 则新增
                    if (dt.Rows.Count == 0)
                    {
                        StringBuilder inssb = new StringBuilder();
                        inssb.Append("INSERT INTO rivenName (cname,ename) VALUES ");
                        inssb.Append("(");
                        inssb.Append("'");
                        inssb.Append(itemName);
                        inssb.Append("'");
                        inssb.Append(",");
                        inssb.Append("'");
                        inssb.Append(urlName);
                        inssb.Append("'");
                        inssb.Append(")");
                        war.ExecuteNonQuery(inssb.ToString());
                    }
                }
                MessageBox.Show("更新完成","信息");
            }
            //更新属性表
            string attres = HttpUitls.Get(attUrl,webHeader);
            if (attres != null)
            {
                SQLiteHelper war = new SQLiteHelper(Config.SQLLITE_PATH);
                JObject jsonchat = JObject.Parse(attres);
                IList<JToken> tokens = jsonchat["payload"]["attributes"].Children().ToList();
                foreach (var item in tokens)
                {
                    // 查询是否已写入数据
                    string urlName = item["url_name"].ToString();
                    string effect = item["effect"].ToString();
                    StringBuilder selsb = new StringBuilder();
                    selsb.Append("SELECT attcname FROM attName WHERE attename=");
                    selsb.Append("'");
                    selsb.Append(urlName);
                    selsb.Append("'");
                    DataTable dt = war.ExecuteDataTable(selsb.ToString());
                    // 无此数据 则新增
                    if (dt.Rows.Count == 0)
                    {
                        StringBuilder inssb = new StringBuilder();
                        inssb.Append("INSERT INTO attName (attcname,attename) VALUES ");
                        inssb.Append("(");
                        inssb.Append("'");
                        inssb.Append(effect);
                        inssb.Append("'");
                        inssb.Append(",");
                        inssb.Append("'");
                        inssb.Append(urlName);
                        inssb.Append("'");
                        inssb.Append(")");
                        war.ExecuteNonQuery(inssb.ToString());
                    }
                }
                MessageBox.Show("更新完成", "信息");
            }
        }
    }
}
