using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Routing;
using System.Web;


namespace HuRongClub
{

    public static class Page
    {
        #region 分页标签
        public static string SplitPageControl(int RecordCount, int pageSize, int pageIndex, string posturl, string divid)
        {
            StringBuilder xml = new StringBuilder();
            //总数只有一页不显示翻页
            if (RecordCount / pageSize >= 1 && RecordCount % pageSize > 0)
            {
                if (divid == "jclendcase")
                {
                    xml.Append("<div class=\"splitpagediv\"><ul><li>");
                }
                else
                {
                    xml.Append("<div style=\"padding-right:20px;border-top:1px solid #f2f2f2\"><div class=\"splitpagediv\" ><ul><li>");
                }
                int pcount = RecordCount / pageSize;
                if (RecordCount % pageSize != 0)
                {
                    pcount += 1;
                }

                xml.Append("<span class=\"splitpage_a_total\">共" + pcount + "页</span>");
                int preindex = 1;
                if (pageIndex > 1)
                {
                    preindex = pageIndex - 1;
                    if (divid == "jclendcase")
                    {
                        xml.Append("<a href=\"javascript:void(0)\" class=\"splitpage_a_pre\" onclick=\"splitpage_click1('" + preindex + "','" + pageSize + "','" + posturl + "','" + divid + "','" + Common.Cookies.GetCookie("choice") + "')\">上一页</a>");
                    }
                    else
                    {
                        xml.Append("<a href=\"javascript:void(0)\" class=\"splitpage_a_pre\" onclick=\"splitpage_click('" + preindex + "','" + pageSize + "','" + posturl + "','" + divid + "')\">上一页</a>");
                    }
                }
                else
                {
                    xml.Append("<span href=\"javascript:void(0)\" class=\"splitpage_a_pre\">上一页</span>");
                }


                int a = pageIndex / 5;
                if (pageIndex % 5 == 0)
                {
                    a -= 1;
                }
                for (int i = 5 * a; i < 5 * a + 5; i++)
                {
                    if (Convert.ToInt32(i + 1) > pcount)
                    {
                        break;
                    }
                    if (Convert.ToInt32(i + 1) == pageIndex)
                    {
                        if (divid == "jclendcase")
                        {
                            xml.Append("<a href=\"javascript:void(0)\" class=\"splitpage_a_current\" onclick=\"splitpage_click1('" + Convert.ToInt32(i + 1) + "','" + pageSize + "','" + posturl + "','" + divid + "','" + Common.Cookies.GetCookie("choice") + "')\">" + Convert.ToInt32(i + 1) + "</a>");
                        }
                        else
                        {
                            xml.Append("<a href=\"javascript:void(0)\" class=\"splitpage_a_current\" onclick=\"splitpage_click('" + Convert.ToInt32(i + 1) + "','" + pageSize + "','" + posturl + "','" + divid + "')\">" + Convert.ToInt32(i + 1) + "</a>");
                        }
                    }
                    else
                    {
                        if (divid == "jclendcase")
                        {
                            xml.Append("<a href=\"javascript:void(0)\" class=\"splitpage_a\" onclick=\"splitpage_click1('" + Convert.ToInt32(i + 1) + "','" + pageSize + "','" + posturl + "','" + divid + "','" + Common.Cookies.GetCookie("choice") + "')\">" + Convert.ToInt32(i + 1) + "</a>");

                        }
                        else
                        {
                            xml.Append("<a href=\"javascript:void(0)\" class=\"splitpage_a\" onclick=\"splitpage_click('" + Convert.ToInt32(i + 1) + "','" + pageSize + "','" + posturl + "','" + divid + "')\">" + Convert.ToInt32(i + 1) + "</a>");
                        }
                    }
                }
                int nextindex = 1;
                if (pageIndex < pcount)
                {
                    nextindex = pageIndex + 1;
                    if (divid == "jclendcase")
                    {
                        xml.Append("<a href=\"javascript:void(0)\" class=\"splitpage_a_next\" onclick=\"splitpage_click1('" + nextindex + "','" + pageSize + "','" + posturl + "','" + divid + "','" + Common.Cookies.GetCookie("choice") + "')\">下一页</a>");

                    }
                    else
                    {
                        xml.Append("<a href=\"javascript:void(0)\" class=\"splitpage_a_next\" onclick=\"splitpage_click('" + nextindex + "','" + pageSize + "','" + posturl + "','" + divid + "')\">下一页</a>");
                    }
                }
                else
                {
                    xml.Append("<span href=\"javascript:void(0)\" class=\"splitpage_a_next\">下一页</span>");
                }

                if (divid == "jclendcase")
                {
                    xml.Append("</li></ul></div>");
                }
                else
                {
                    xml.Append("</li></ul></div></div>");
                }
                xml.Append("<input type=\"hidden\" id=\"pageSize\" value=\"4\" />");
                xml.Append("<input type=\"hidden\" id=\"pageIndex\" value=\"" + pageIndex + "\" />");
                xml.Append("<input type=\"hidden\" id=\"pageCount\" value=\"" + pcount + "\" />");
            }



            return xml.ToString();
        }
        #endregion

        #region 分页标签
        public static string SplitPageControl(int RecordCount, int pageSize, int pageIndex)
        {
            StringBuilder xml = new StringBuilder();
            //总数只有一页不显示翻页
            if (RecordCount / pageSize >= 1)
            {
                xml.Append("<div class=\"splitpagediv\"><ul><li>");


                int pcount = RecordCount / pageSize;
                if (RecordCount % pageSize != 0)
                {
                    pcount += 1;
                }

                xml.Append("<span class=\"splitpage_a_total\">共" + pcount + "页</span>");
                int preindex = 1;
                if (pageIndex > 1)
                {
                    preindex = pageIndex - 1;
                    xml.Append("<input type=button onclick=\"document.getElementById ('pageIndex').value='" + (pageIndex - 1) + "';this.form.submit();\" class=\"splitpage_a_pre\" value=\" 上一页\"/>");

                }
                else
                {
                    xml.Append("<input type=button  class=\"splitpage_a_pre\" value=\" 上一页 \"/>");

                }


                int a = pageIndex / 5;
                if (pageIndex % 5 == 0)
                {
                    a -= 1;
                }
                for (int i = 5 * a; i < 5 * a + 5; i++)
                {
                    if (Convert.ToInt32(i + 1) > pcount)
                    {
                        break;
                    }
                    if (Convert.ToInt32(i + 1) == pageIndex)
                    {
                        xml.Append("<input type=button onclick=\"document.getElementById('pageIndex').value='" + Convert.ToInt32(i + 1) + "';this.form.submit();\" class=\"splitpage_a_current\" value=" + Convert.ToInt32(i + 1) + ">");

                    }
                    else
                    {
                        xml.Append("<input type=button onclick=\"document.getElementById('pageIndex').value='" + Convert.ToInt32(i + 1) + "';this.form.submit();\" class=\"splitpage_a\" value=" + Convert.ToInt32(i + 1) + ">");

                    }
                }
                int nextindex = 1;

                if (pageIndex < pcount)
                {
                    nextindex = pageIndex + 1;
                    xml.Append("<input type=button onclick=\"document.getElementById('pageIndex').value='" + (pageIndex + 1) + "';this.form.submit();\" class=\"splitpage_a_next\" value=\"下一页\"/>");
                }
                else
                {
                    xml.Append("<input type=button class=\"splitpage_a_next\" value=\"下一页\"/>");
                }

                xml.Append("</li></ul></div>");
                xml.Append("<input type=\"hidden\" name=\"pageSize\" id=\"pageSize\" value=" + pageSize + " />");
                xml.Append("<input type=\"hidden\"  name=\"pageIndex\" id=\"pageIndex\" value=\"" + pageIndex + "\" />");
                xml.Append("<input type=\"hidden\" name=\"pageCount\" id=\"pageCount\" value=\"" + pcount + "\" />");
            }

            return xml.ToString();
        }

        public static string SplitPageControl(int RecordCount, int pageSize, int pageIndex, string url)
        {
            StringBuilder xml = new StringBuilder();
            //总数只有一页不显示翻页
            if (RecordCount / pageSize >= 1)
            {
                xml.Append("<ul class=\"pagination\">");


                int pcount = RecordCount / pageSize;
                if (RecordCount % pageSize != 0)
                {
                    pcount += 1;
                }


                int preindex = 1;
                if (pageIndex > 1)
                {
                    preindex = pageIndex - 1;
                    xml.Append("<li><a href=\"" + url + "?pageIndex=" + (pageIndex - 1) + "\">&laquo;</a></li>");

                }
                else
                {
                    xml.Append("<li><a href=\"javascript:void(0)\">&laquo;</a></li>");

                }


                int a = pageIndex / 5;
                if (pageIndex % 5 == 0)
                {
                    a -= 1;
                }
                for (int i = 5 * a; i < 5 * a + 5; i++)
                {
                    if (Convert.ToInt32(i + 1) > pcount)
                    {
                        break;
                    }
                    if (Convert.ToInt32(i + 1) == pageIndex)
                    {
                        xml.Append("<li><a href=\"" + url + "?pageIndex=" + Convert.ToInt32(i + 1) + "\">" + Convert.ToInt32(i + 1) + "</a></li>");
                    }
                    else
                    {
                        xml.Append("<li><a href=\"" + url + "?pageIndex=" + Convert.ToInt32(i + 1) + "\">" + Convert.ToInt32(i + 1) + "</a></li>");

                    }
                }
                int nextindex = 1;

                if (pageIndex < pcount)
                {
                    nextindex = pageIndex + 1;

                    xml.Append("<li><a href=\"" + url + "?pageIndex=" + (pageIndex + 1) + "\">&raquo;</a></li>");
                }
                else
                {
                    xml.Append("<li><a href=\"javascript:void(0)\">&raquo;</a></li>");
                }

                xml.Append("<pre class=\"ng-binding\">第" + pageIndex + "页</pre>");
            }

            return xml.ToString();
        }
        public static string SplitPageHelpControl(int RecordCount, int pageSize, int pageIndex, string posturl)
        {
            StringBuilder xml = new StringBuilder();
            //总数只有一页不显示翻页
            if (RecordCount / pageSize >= 1 && RecordCount % pageSize >= 0)
            {
                    xml.Append("<div style=\"padding-right:20px;border-top:1px solid #f2f2f2\"><div class=\"splitpagediv\" ><ul><li>");

                int pcount = RecordCount / pageSize;
                if (RecordCount % pageSize != 0)
                {
                    pcount += 1;
                }

                xml.Append("<span class=\"splitpage_a_total\">共" + pcount + "页</span>");
                int preindex = 1;
                if (pageIndex > 1)
                {
                    preindex = pageIndex - 1;

                        xml.Append("<a href=\""+posturl+"?pageIndex="+preindex+"\" class=\"splitpage_a_pre\">上一页</a>");

                }
                else
                {
                    xml.Append("<span href=\"javascript:void(0)\" class=\"splitpage_a_pre\">上一页</span>");
                }


                int a = pageIndex / 5;
                if (pageIndex % 5 == 0)
                {
                    a -= 1;
                }
                for (int i = 5 * a; i < 5 * a + 5; i++)
                {
                    if (Convert.ToInt32(i + 1) > pcount)
                    {
                        break;
                    }
                    if (Convert.ToInt32(i + 1) == pageIndex)
                    {

                        xml.Append("<a href=\"" + posturl + "?pageIndex=" + Convert.ToInt32(i + 1) + "\" class=\"splitpage_a_current\">" + Convert.ToInt32(i + 1) + "</a>");
                    }
                    else
                    {
                        xml.Append("<a href=\"" + posturl + "?pageIndex=" + Convert.ToInt32(i + 1) + "\" class=\"splitpage_a_current\">" + Convert.ToInt32(i + 1) + "</a>");
                        
                    }
                }
                int nextindex = 1;
                if (pageIndex < pcount)
                {
                    nextindex = pageIndex + 1;

                        xml.Append("<a href=\""+posturl+"?pageIndex="+nextindex+"\" class=\"splitpage_a_next\">下一页</a>");
                }
                else
                {
                    xml.Append("<span href=\"javascript:void(0)\" class=\"splitpage_a_next\">下一页</span>");
                }
                    xml.Append("</li></ul></div></div>");
            }
            return xml.ToString();
        }
        #endregion
    }
}
