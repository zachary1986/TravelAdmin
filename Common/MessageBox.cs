using System;
using System.Text;
using System.Web.UI;

namespace Common
{
    /// <summary>
    /// 显示消息提示对话框。
    /// 动软卓越	
    /// </summary>
    public class MessageBox
    {
        private MessageBox()
        {
        }

        /// <summary>
        /// 显示消息提示对话框
        /// </summary>
        /// <param name="page">当前页面指针，一般为this</param>
        /// <param name="msg">提示信息</param>
        public static void Show(System.Web.UI.Page page, string msg)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "message", "<script language='javascript' defer>alert('" + msg + "');</script>");

            // UpdatePanel采用如下方式弹出对话框
            //System.Web.UI.ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "javascript", "alert('您已经投过票！');", true);
        }

        /// <summary>
        /// 显示服务器繁忙提示信息
        /// </summary>
        /// <param name="page"></param>
        /// <param name="msg"></param>
        public static void ShowServerBusyTip(System.Web.UI.Page page, string msg)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "message", "<script language='javascript' defer>ShowServerBusyTip('" + msg + "');</script>");
        }

        /// <summary>
        /// 显示操作成功提示信息
        /// </summary>
        /// <param name="page"></param>
        /// <param name="msg"></param>
        public static void ShowSuccessTip(System.Web.UI.Page page, string msg)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "message", "<script language='javascript' defer>ShowSuccessTip('" + msg + "');</script>");
        }

        /// <summary>
        /// 显示操作失败的提示信息
        /// </summary>
        /// <param name="page"></param>
        /// <param name="msg"></param>
        public static void ShowFailTip(System.Web.UI.Page page, string msg)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "message", "<script language='javascript' defer>ShowFailTip('" + msg + "');</script>");
        }

        /// <summary>
        /// 显示正在加载的提示信息
        /// </summary>
        /// <param name="page"></param>
        /// <param name="msg"></param>
        public static void ShowLoadingTip(System.Web.UI.Page page, string msg)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "message", "<script language='javascript' defer>ShowLoadingTip('" + msg + "');</script>");
        }

        /// <summary>
        /// 显示消息提示对话框，并返回原页面
        /// </summary>
        /// <param name="page">当前页面指针，一般为this</param>
        /// <param name="msg">提示信息</param>
        public static void ShowAndBack(System.Web.UI.Page page, string msg)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "message", "<script language='javascript' defer>alert('" + msg + "');history.back();</script>");
        }

        /// <summary>
        /// 控件点击 消息确认提示框
        /// </summary>
        /// <param name="page">当前页面指针，一般为this</param>
        /// <param name="msg">提示信息</param>
        public static void ShowConfirm(System.Web.UI.WebControls.WebControl Control, string msg)
        {
            //Control.Attributes.Add("onClick","if (!window.confirm('"+msg+"')){return false;}");
            Control.Attributes.Add("onclick", "return confirm('" + msg + "');");
        }

        /// <summary>
        /// 显示消息提示对话框，并进行页面跳转
        /// </summary>
        /// <param name="page">当前页面指针，一般为this</param>
        /// <param name="msg">提示信息</param>
        /// <param name="url">跳转的目标URL</param>
        public static void ShowAndRedirect(System.Web.UI.Page page, string msg, string url)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "message", "<script language='javascript' defer>alert('" + msg + "');window.location=\"" + url + "\"</script>");
        }

        /// <summary>
        /// 显示消息提示对话框，(父页面)并进行页面跳转
        /// </summary>
        /// <param name="page">当前页面指针，一般为this</param>
        /// <param name="msg">提示信息</param>
        /// <param name="url">跳转的目标URL</param>
        public static void ShowAndRedirects(System.Web.UI.Page page, string msg, string url)
        {
            StringBuilder Builder = new StringBuilder();
            Builder.Append("<script language='javascript'defer>");
            Builder.AppendFormat("alert('{0}');", msg);
            Builder.AppendFormat("top.location.href='{0}'", url);
            Builder.Append("</script>");
            page.ClientScript.RegisterStartupScript(page.GetType(), "message", Builder.ToString());

        }

        /// <summary>
        /// 输出自定义脚本信息
        /// </summary>
        /// <param name="page">当前页面指针，一般为this</param>
        /// <param name="script">输出脚本</param>
        public static void ResponseScript(System.Web.UI.Page page, string script)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "message", "<script language='javascript' defer>" + script + "</script>");
        }

        /// <summary>
        /// 显示服务器繁忙提示信息
        /// </summary>
        /// <param name="page"></param>
        /// <param name="msg"></param>
        public static void ShowServerBusyTip(System.Web.UI.Page page, string msg, string url)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "message", "<script language='javascript' defer>ShowServerBusyTip('" + msg + "');function jump(count){window.setTimeout(function(){count--;if(count>0){jump(count)}else{window.location.href=\"" + url + "\"}},1000)}jump(3);</script>");
        }

        /// <summary>
        /// 显示操作成功提示信息
        /// </summary>
        /// <param name="page"></param>
        /// <param name="msg"></param>
        public static void ShowSuccessTip(System.Web.UI.Page page, string msg, string url)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "message", "<script language='javascript' defer>ShowSuccessTip('" + msg + "');function jump(count){window.setTimeout(function(){count--;if(count>0){jump(count)}else{window.location.href=\"" + url + "\"}},1000)}jump(3);</script>");
        }

        /// <summary>
        /// 显示操作失败的提示信息
        /// </summary>
        /// <param name="page"></param>
        /// <param name="msg"></param>
        public static void ShowFailTip(System.Web.UI.Page page, string msg, string url)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "message", "<script language='javascript' defer>ShowFailTip('" + msg + "');function jump(count){window.setTimeout(function(){count--;if(count>0){jump(count)}else{window.location.href=\"" + url + "\"}},1000)}jump(3);</script>");
        }

        /// <summary>
        /// 显示正在加载的提示信息
        /// </summary>
        /// <param name="page"></param>
        /// <param name="msg"></param>
        public static void ShowLoadingTip(System.Web.UI.Page page, string msg, string url)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "message", "<script language='javascript' defer>ShowLoadingTip('" + msg + "');function jump(count){window.setTimeout(function(){count--;if(count>0){jump(count)}else{window.location.href=\"" + url + "\"}},1000)}jump(3);</script>");
        }

    }
}
