using System;
using System.Collections.Generic;
using System.Text;
using ZXing;
using ZXing.QrCode;
using ZXing.Common;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using System.Collections;
using System.Data;
using System.Linq;

namespace Common
{
   public static class Extensions
    {
        public static DataSet ToDataSet<T>(this IList<T> list)
        {
            Type elementType = typeof(T);
            var ds = new DataSet();
            var t = new DataTable();
            ds.Tables.Add(t);
            elementType.GetProperties().ToList().ForEach(propInfo => t.Columns.Add(propInfo.Name, Nullable.GetUnderlyingType(propInfo.PropertyType) ?? propInfo.PropertyType));
            foreach (T item in list)
            {
                var row = t.NewRow();
                elementType.GetProperties().ToList().ForEach(propInfo => row[propInfo.Name] = propInfo.GetValue(item, null) ?? DBNull.Value);
                t.Rows.Add(row);
            }
            return ds;
        }
    }
}
