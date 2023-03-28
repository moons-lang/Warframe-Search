﻿using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp3.VO;

namespace WindowsFormsApp3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.platform.SelectedIndex = 0;
            this.search.Text = "沙皇";
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            if (search.Text.Equals(""))
            {
                info.Text = "请输入查询值";

            }
            else {
                info.Text = null;
                SQLiteHelper war = new SQLiteHelper(Config.SQLLITE_PATH);
                StringBuilder selsb = new StringBuilder();
                selsb.Append("SELECT ename FROM rivenName WHERE cname=");
                selsb.Append("'");
                selsb.Append(search.Text);
                selsb.Append("'");
                DataRow row = war.ExecuteDataRow(selsb.ToString());
                if (row != null)
                {
                    info.Text = "搜索中 请稍侯";
                    getOfficialData();
                    getMarketData();
                }
                else 
                {
                    info.Text = "无此武器 请更新数据或检查输入";
                }

            }

        }

        public string getMarketData()
        {
            string url = Config.RIVEN_SEARCH;
            WebHeaderCollection webHeader = new WebHeaderCollection();
            webHeader.Add("platform", platform.Text);
            Hashtable hashtable = new Hashtable();
            hashtable.Add("weapon_url_name", CtoE.selEName(search.Text));
            string resp = HttpUitls.Get(HttpUitls.splicParam(url, hashtable), webHeader);
            if (resp != null)
            {
                SQLiteHelper war = new SQLiteHelper(Config.SQLLITE_PATH);
                JObject jsonchat = JObject.Parse(resp);
                IList<JToken> tokens = jsonchat["payload"]["auctions"].Children().ToList();
                DataTable table = new DataTable();
                table.Columns.Add("属性1");
                table.Columns.Add("属性2");
                table.Columns.Add("属性3");
                table.Columns.Add("属性4");
                table.Columns.Add("属性5");
                foreach (var auctions in tokens)
                {
                    List<string> l = new List<string>(5);
                    List<string> v = new List<string>(5);
                    IList<JToken> attributes = auctions["item"]["attributes"].Children().ToList();
                    string name = auctions["owner"]["ingame_name"].ToString();
                    foreach (var item in attributes)
                    {
                        string attr = item["url_name"].ToString();
                        string value = item["value"].ToString();
                        StringBuilder selsb = new StringBuilder();
                        selsb.Append("SELECT attcname FROM attName WHERE attename=");
                        selsb.Append("'");
                        selsb.Append(attr);
                        selsb.Append("'");
                        DataRow dr = war.ExecuteDataRow(selsb.ToString());
                        l.Add(dr.ItemArray[0].ToString());
                        v.Add(value);
                    }
                    if (v.Count <= 2)
                    {
                        v.Add("");
                        v.Add("");
                    }
                    if (v.Count <= 3)
                    {
                        v.Add("");
                    }
                    v.Add("/w " + name + " Hello");
                    table.Rows.Add(l.Cast<object>().ToArray());
                    table.Rows.Add(v.Cast<object>().ToArray());
                    market.Text = "市场数据";
                    marketData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    marketData.RowHeadersVisible = false;
                    marketData.AllowUserToAddRows = false;
                    marketData.RowsDefaultCellStyle.BackColor = Color.DeepSkyBlue;
                    marketData.AlternatingRowsDefaultCellStyle.BackColor = Color.Gold;
                    marketData.DataSource = table;
                    info.Text = "单击复制表格内容";
                }
                return null;
            }
            return null;
        }

        public void getOfficialData()
        {
            string newpl;
            switch (platform.Text)
            {
                case "xbox":
                    newpl = "xb1";
                    break;
                case "ps4":
                    newpl = "ps4";
                    break;
                case "switch":
                    newpl = "swi";
                    break;
                default:
                    newpl = "pc";
                    break;
            }
            string ename = CtoE.selEName(search.Text);
            string url;
            if (ename != null)
            {
                url = Config.OFFICIAL_RIVEN_PRICE + newpl + "/rivens/search/" + ename.Replace("_", "%20");
                if (search.Text.Equals("暗黑分合剑"))
                {
                    url = Config.OFFICIAL_RIVEN_PRICE + newpl + "/rivens/search/" + "dark%20split-sword";
                }
                string resp = HttpUitls.Get(url);
                if (resp != null && !resp.Equals("{}"))
                {
                    //截取json
                    string json = resp.ToString().Trim();
                    string midchat = json.Remove(0, json.IndexOf(":") + 1);
                    string finchat = midchat.Remove(midchat.Length - 1, 1);
                    JObject jsonchat = JObject.Parse(finchat);
                    //清除所有行
                    officeDataGrid.DataSource = null;
                    //设置左上第一格
                    officeDataGrid.TopLeftHeaderCell.Value = search.Text;
                    officeDataGrid.RowsDefaultCellStyle.BackColor = Color.DeepSkyBlue;
                    officeDataGrid.AlternatingRowsDefaultCellStyle.BackColor = Color.Gold;
                    officeDataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    officeDataGrid.RowHeadersVisible = false;
                    officeDataGrid.AllowUserToAddRows = false;
                    office.Text = "官方数据";
                    DataTable table = new DataTable();
                    table.Columns.Add(search.Text);
                    table.Columns.Add("平均数");
                    table.Columns.Add("最大值");
                    table.Columns.Add("中位数");
                    table.Columns.Add("最小值");
                    table.Columns.Add("偏差值");
                    table.Columns.Add("人数");
                    //取数据
                    if (!jsonchat["rerolled"].ToString().Equals(""))
                    {
                        string reavg = jsonchat["rerolled"]["avg"].ToString();
                        string restddev = jsonchat["rerolled"]["stddev"].ToString();
                        string remin = jsonchat["rerolled"]["min"].ToString();
                        string remax = jsonchat["rerolled"]["max"].ToString();
                        string repop = jsonchat["rerolled"]["pop"].ToString();
                        string remedian = jsonchat["rerolled"]["median"].ToString();
                        table.Rows.Add("已洗", reavg, remax, remedian, remin, restddev, repop);
                        officeDataGrid.DataSource = table;
                    }
                    if (!jsonchat["unrolled"].ToString().Equals(""))
                    {
                        string unavg = jsonchat["unrolled"]["avg"].ToString();
                        string unstddev = jsonchat["unrolled"]["stddev"].ToString();
                        string unmin = jsonchat["unrolled"]["min"].ToString();
                        string unmax = jsonchat["unrolled"]["max"].ToString();
                        string unpop = jsonchat["unrolled"]["pop"].ToString();
                        string unmedian = jsonchat["unrolled"]["median"].ToString();
                        table.Rows.Add("未洗", unavg, unmax, unmedian, unmin, unstddev, unpop);
                        officeDataGrid.DataSource = table;
                    }
                }
                else
                {
                    info.Text = "查询失败";
                }
            }
            else
            {
                info.Text = "无此武器 请更新数据";
            }
        }

        private void upendata_Click(object sender, EventArgs e)
        {
            CtoE.updataData();
        }


        private void office_Click(object sender, EventArgs e)
        {

        }

        private void clear_Click(object sender, EventArgs e)
        {
            info.Text = null;
            office.Text = string.Empty;
            officeDataGrid.DataSource = null;  
            market.Text = string.Empty;
            marketData.DataSource = null;
        }

        private void marketData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            Clipboard.SetText(marketData.CurrentCell.Value.ToString());
        }
    }
}