using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using WindowsFormsApp3;

namespace WarframeSearch.Tools
{
    internal class StatementClump
    {
        // mod 1 确定条件查询 2 like查询
        public static DataTable SelectTable(int mod,string selName,string tablurlname,string term,string selterm,string like)
        {
            string exepath = Application.ExecutablePath;
            string exedic = Path.GetDirectoryName(exepath);
            SQLiteHelper war = new SQLiteHelper(exedic + "\\" + Config.SQLLITE_PATH);
            StringBuilder selsb = new StringBuilder();
            selsb.Append("SELECT ");
            selsb.Append(selName);
            selsb.Append(" FROM ");
            selsb.Append(tablurlname);
            if (mod == 1 && term != null && selterm != null && !term.Equals("") && !selterm.Equals(""))
            {
                selsb.Append(" WHERE ");
                selsb.Append(term);
                selsb.Append("=");
                selsb.Append("'");
                selsb.Append(selterm);
                selsb.Append("'");
            } else if (mod == 2 && term != null && selterm != null && !term.Equals("") && !selterm.Equals("")) 
            {
                selsb.Append(" WHERE ");
                selsb.Append(term);
                selsb.Append(" like ");
                selsb.Append("'");
                selsb.Append(selterm);
                selsb.Append(like);
                selsb.Append("'");
            }
            return war.ExecuteDataTable(selsb.ToString());
        }

        public static DataRow SelectRow(string selName, string tablurlname, string term, string selterm)
        {

            
            string exepath = Application.ExecutablePath;
            string exedic = Path.GetDirectoryName(exepath);
            SQLiteHelper war = new SQLiteHelper(exedic + "\\" + Config.SQLLITE_PATH);
            StringBuilder selsb = new StringBuilder();
            selsb.Append("SELECT ");
            selsb.Append(selName);
            selsb.Append(" FROM ");
            selsb.Append(tablurlname);
            if (term != null && selterm != null && !term.Equals("") && !selterm.Equals(""))
            {
                selsb.Append(" WHERE ");
                selsb.Append(term);
                selsb.Append("=");
                selsb.Append("'");
                selsb.Append(selterm);
                selsb.Append("'");
            }
            return war.ExecuteDataRow(selsb.ToString());
        }

        //mod 模式 1 插入  2 更新 3 删除 factor where查询条件 searchvalue where查询值
        public static void NoSearch(int mod, string tablurlname, List<string> title , List<string> value ,string factor,string searchvalue)
        {
            string exepath = Application.ExecutablePath;
            string exedic = Path.GetDirectoryName(exepath);
            SQLiteHelper war = new SQLiteHelper(exedic + "\\" + Config.SQLLITE_PATH);
            StringBuilder inssb = new StringBuilder();
            if (mod == 1)
            {
                inssb.Append("INSERT INTO ");
                inssb.Append(tablurlname);
                inssb.Append(" (");
                foreach (var item in title)
                {
                    inssb.Append(item);
                    inssb.Append(",");
                }
                inssb.Remove(inssb.Length - 1, 1);
                inssb.Append(")");
                inssb.Append(" VALUES ");
                inssb.Append("(");
                foreach (var item in value)
                {
                    inssb.Append("'");
                    inssb.Append(item);
                    inssb.Append("'");
                    inssb.Append(",");
                }
                inssb.Remove(inssb.Length - 1, 1);
                inssb.Append(")");
            }
            else if (mod == 2)
            {
                int i = 0;
                inssb.Append("UPDATE ");
                inssb.Append(tablurlname);
                inssb.Append(" SET ");
                foreach (var t in title)
                {
                    inssb.Append(t);
                    inssb.Append("=");
                    for (int a=0; a < value.Count;) {
                        inssb.Append("'");
                        inssb.Append(value[i]);
                        inssb.Append("'");
                        inssb.Append(",");
                        break;
                    }
                    i++;
                }
                inssb.Remove(inssb.Length - 1, 1);
                inssb.Append(" WHERE ");
                inssb.Append(factor);
                inssb.Append("=");
                inssb.Append("'");
                inssb.Append(searchvalue);
                inssb.Append("'");

            }
            war.ExecuteNonQuery(inssb.ToString());
        }

        //1 属性表 2 名称表 3 转换请求
        public static string SelectLanguage(int arr,string language) {
            string lang;
            if (arr == 1)
            {
                switch (language)
                
                {
                    case "zh-hant":
                        lang = "attctname";
                        break;
                    case "en":
                        lang = "attename";
                        break;
                    case "ru":
                        lang = "attruname";
                        break;
                    case "pl":
                        lang = "attplname";
                        break;
                    case "ko":
                        lang = "attkoname";
                        break;
                    case "de":
                        lang = "attdurlname";
                        break;
                    case "fr":
                        lang = "attfrname";
                        break;
                    case "pt":
                        lang = "attptname";
                        break;
                    case "es":
                        lang = "attesname";
                        break;
                    case "it":
                        lang = "attitname";
                        break;
                    default:
                        lang = "attcname";
                        break;
                }
                return lang;
            }
            else if (arr == 2)
            {
                switch (language)
                {
                    case "zh-hant":
                        lang = "ctname";
                        break;
                    case "ru":
                        lang = "runame";
                        break;
                    case "en":
                        lang = "ename";
                        break;
                    case "pl":
                        lang = "plname";
                        break;
                    case "ko":
                        lang = "koname";
                        break;
                    case "de":
                        lang = "durlname";
                        break;
                    case "fr":
                        lang = "frname";
                        break;
                    case "pt":
                        lang = "ptname";
                        break;
                    case "es":
                        lang = "esname";
                        break;
                    case "it":
                        lang = "itname";
                        break;
                    default:
                        lang = "cname";
                        break;
                }
                return lang;
            }else if (arr == 3)
            {
                switch (language)
                {
                    case "繁體中文":
                        lang = "zh-hant";
                        break;
                    case "English":
                        lang = "en";
                        break;
                    case "Русский":
                        lang = "ru";
                        break;
                    case "Polski":
                        lang = "pl";
                        break;
                    case "한국어":
                        lang = "ko";
                        break;
                    case "Deutsch":
                        lang = "de";
                        break;
                    case "Français":
                        lang = "fr";
                        break;
                    case "Português":
                        lang = "pt";
                        break;
                    case "Español":
                        lang = "es";
                        break;
                    case "Svenska":
                        lang = "it";
                        break;
                    default:
                        lang = "zh-hans";
                        break;
                }
                return lang;
            }
            MessageBox.Show("错误","错误");
            return null;

        }



    }
}
