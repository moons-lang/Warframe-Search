using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WarframeSearch.Tools;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowsFormsApp3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.platform.SelectedIndex = 0;
            this.point.Text = "输入想要查找的紫卡并点击搜索";
            this.search.Text = "沙皇";
            this.sort.Text = "default";
            this.language.Text = "简体中文";
            this.label3.Text = "请用对应的语言查询 否则会无结果";

        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            info.Text = null;
            office.Text = string.Empty;
            officeDataGrid.DataSource = null;
            market.Text = string.Empty;
            marketData.DataSource = null;
            if (search.Text.Equals(""))
            {
                info.Text = "请输入查询值";

            }
            else
            {
                string lang = StatementClump.SelectLanguage(3, language.Text);
                string lan = StatementClump.SelectLanguage(2, lang);
                DataRow row;
                if (language.Text.Equals("繁體中文") || language.Text.Equals("简体中文") || language.Text.Equals("한국어") || language.Text.Equals("Русский"))
                {
                    row = StatementClump.SelectRow( "urlname", "rivenName", lan, search.Text);
                }
                else
                {
                    TextInfo myTI = new CultureInfo("en-US", false).TextInfo;
                    string s = myTI.ToTitleCase(search.Text);
                    row = StatementClump.SelectRow( "urlname", "rivenName", lan, s);
                }
                if (row != null)
                {
                    info.Text = "搜索中 请稍侯";
                    getOfficialData();
                    getMarketData();
                    point.Text = string.Empty;
                }
                else
                {
                    info.Text = "无结果 请更新数据，检查输入或检查语言设置";
                }


            }

        }

        public string getMarketData()
        {
            string url = Config.RIVEN_SEARCH;
            WebHeaderCollection webHeader = new WebHeaderCollection();
            webHeader.Add("platform", platform.Text);
            Hashtable hashtable = new Hashtable();
            switch (sort.Text)
            {
                case "price asc":
                    hashtable.Add("sort_by", "price_asc");
                    break;
                case "price desc":
                    hashtable.Add("sort_by", "price_desc");
                    break;
            }
            hashtable.Add("weapon_url_name", CtoE.selurlname(search.Text, language.Text));
            string resp = HttpUitls.Get(HttpUitls.splicParam(url, hashtable), webHeader);
            if (resp != null)
            {
                JObject jsonchat = JObject.Parse(resp);
                IList<JToken> tokens = jsonchat["payload"]["auctions"].Children().ToList();
                DataTable table = new DataTable();
                table.Columns.Add("词条1");
                table.Columns.Add("词条2");
                table.Columns.Add("词条3");
                table.Columns.Add("词条4");
                table.Columns.Add("极性");
                table.Columns.Add("紫卡等级");
                table.Columns.Add("洗点次数");
                table.Columns.Add("起拍价");
                table.Columns.Add("买断价");
                table.Columns.Add("状态");
                table.Columns.Add("卖家");
                foreach (var auctions in tokens)
                {
                    List<string> l = new List<string>(5);
                    List<string> v = new List<string>(5);
                    IList<JToken> attributes = auctions["item"]["attributes"].Children().ToList();
                    string name = auctions["owner"]["ingame_name"].ToString();
                    string status = auctions["owner"]["status"].ToString();
                    string polarity = auctions["item"]["polarity"].ToString();
                    string rank = auctions["item"]["mod_rank"].ToString();
                    string rero = auctions["item"]["re_rolls"].ToString();
                    string start = auctions["starting_price"].ToString();
                    string end = auctions["buyout_price"].ToString();
                    foreach (var item in attributes)
                    {
                        string attr = item["url_name"].ToString();
                        string value = item["value"].ToString();
                        string lang = StatementClump.SelectLanguage(3, language.Text);
                        string lan = StatementClump.SelectLanguage(1, lang);
                        DataRow dr = StatementClump.SelectRow( lan, "attName", "atturlname", attr);
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
                    v.Add(polarity);
                    v.Add(rank);
                    v.Add(rero);
                    v.Add(start);
                    v.Add(end);
                    v.Add(status);
                    //if (status.Equals("offline"))
                    //{
                    //    v.Add("离线");
                    //}
                    //else if ((status.Equals("ingame")))
                    //{
                    //    v.Add("游戏中");
                    //}
                    //else 
                    //{
                    //    v.Add("在线");
                    //}
                    v.Add("/w " + name + " Hi!");
                    table.Rows.Add(l.Cast<object>().ToArray());
                    table.Rows.Add(v.Cast<object>().ToArray());
                    market.Text = "market data";
                    marketData.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    marketData.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    marketData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    marketData.RowHeadersVisible = false;
                    marketData.AllowUserToAddRows = false;
                    marketData.RowsDefaultCellStyle.BackColor = Color.DeepSkyBlue;
                    marketData.AlternatingRowsDefaultCellStyle.BackColor = Color.Gold;
                    marketData.DataSource = table;
                    info.Text = "Click to copy table content";
                }
            }
            else
            {
                market.Text = "no data";
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
            string urlname = CtoE.selurlname(search.Text, language.Text);
            string url;
            if (urlname != null)
            {
                url = Config.OFFICIAL_RIVEN_PRICE + newpl + "/rivens/search/" + urlname.Replace("_", "%20");
                if (search.Text.Equals("暗黑分合剑"))
                {
                    url = Config.OFFICIAL_RIVEN_PRICE + newpl + "/rivens/search/" + "dark%20split-sword";
                }
                string resp = HttpUitls.Get(url);
                if (resp != null && !resp.Equals("{}"))
                {
                    //截取json
                    JObject jsonst = JObject.Parse(resp);
                    JToken a = jsonst.Last;
                    string json = a.ToString().Trim();
                    string midchat = json.Remove(0, json.IndexOf(":") + 1);
                    JObject jsonchat = JObject.Parse(midchat);
                    //清除所有行
                    officeDataGrid.DataSource = null;
                    //设置左上第一格
                    officeDataGrid.TopLeftHeaderCell.Value = search.Text;
                    officeDataGrid.RowsDefaultCellStyle.BackColor = Color.DeepSkyBlue;
                    officeDataGrid.AlternatingRowsDefaultCellStyle.BackColor = Color.Gold;
                    officeDataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    officeDataGrid.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    officeDataGrid.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    officeDataGrid.RowHeadersVisible = false;
                    officeDataGrid.AllowUserToAddRows = false;
                    office.Text = "official data";
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
                    office.Text = "no data";
                }
            }
            else
            {
                info.Text = "无此武器 请更新数据";
            }
        }

        private void upendata_Click(object sender, EventArgs e)
        {
            string lang = StatementClump.SelectLanguage(3, language.Text);
            CtoE.updataData(lang);
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
            MessageBox.Show("复制成功", "提示");
        }

        private void search_TextChanged(object sender, EventArgs e)
        {
            var source = new AutoCompleteStringCollection();
            string lang = StatementClump.SelectLanguage(3, language.Text);
            string lan = StatementClump.SelectLanguage(2, lang);
            DataTable dt = StatementClump.SelectTable(2, lan, "rivenName", lan, search.Text, "%");
            foreach (DataRow item in dt.Rows)
            {
                source.Add(item[0].ToString());
            }
            search.AutoCompleteCustomSource = source;
            search.AutoCompleteMode = AutoCompleteMode.Suggest;
            search.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }

        private void label3_Click(object sender, EventArgs e)
        {
            label3.Text = string.Empty;
        }
    }
 }