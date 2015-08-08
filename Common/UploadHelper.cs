using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace Common
{
    //自定义泛型类       泛型约束
    //public class UploadHelper<T> where T : HuRongClub.Model.UploadBase, new()
    //{
    //    /// <summary>
    //    /// 单个文件
    //    /// </summary>
    //    /// <param name="up"></param>
    //    /// <param name="hfs"></param>
    //    /// <param name="extension"></param>
    //    /// <param name="dam"></param>
    //    /// <param name="size"></param>
    //    /// <param name="saveurl"></param>
    //    public UploadHelper(HuRongClub.Model.UploadBase up
    //       , HttpPostedFile hfs
    //       , string[] extension
    //       , string[] dam
    //       , int size
    //       , string saveurl)
    //    {
    //        if (hfs.FileName == string.Empty)
    //        {
    //            //记录错误信息
    //            up.IsSuccess = false;
    //            up.Message = "没有文件";
    //            return;
    //        }
    //        //组装保存的文件名
    //        string fileclientname = Path.GetFileName(hfs.FileName);
    //        string extesion = Path.GetExtension(fileclientname).ToLower();
    //        string fileservername = Guid.NewGuid().ToString() + extesion;
    //        if (!extension.Contains(extesion))
    //        {
    //            //记录错误信息
    //            up.FileClientName = fileclientname
    //            ;
    //            up.FileServerName = fileservername
    //            ;
    //            up.Size = hfs.ContentLength
    //            ;
    //            up.IsSuccess = false;
    //            up.Message = "后缀不符合";
    //            return;
    //        }
    //        if (hfs.ContentLength > size)
    //        {
    //            //记录错误信息
    //            up.FileClientName = fileclientname;
    //            up.FileServerName = fileservername;
    //            up.Size = hfs.ContentLength;
    //            up.IsSuccess = false;
    //            up.Message = "文件过大";
    //            return;
    //        }
    //        byte[] arr;
    //        using (Stream sr = hfs.InputStream)
    //        {
    //            arr = new byte[sr.Length];
    //            sr.Read(arr, 0, arr.Length);
    //            string filetype = arr[0].ToString() + arr[1].ToString();
    //            if (dam.Contains(filetype))
    //            {
    //                //记录错误信息
    //                up.FileClientName = fileclientname;
    //                up.FileServerName = fileservername;
    //                up.Size = hfs.ContentLength;
    //                up.IsSuccess = false;
    //                up.Message = "危险类型";
    //                return;
    //            }
    //            hfs.SaveAs(saveurl + fileservername);
    //            up.FileClientName = fileclientname;
    //            up.FileServerName = fileservername;
    //            up.Size = hfs.ContentLength;
    //            up.IsSuccess = true;
    //            up.Message = "ac";
    //        }
    //    }
    //}
}
