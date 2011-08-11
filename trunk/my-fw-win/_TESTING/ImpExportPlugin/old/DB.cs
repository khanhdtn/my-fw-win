using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace ProtocolVN.Plugin.OldImpExp
{
    public class DB
    {
        DataSet dataSet;
        public DataSet _DataSet
        {
            set { dataSet = value; }
        }
        int _indexfirst = -1, _indexend = -1, _indexrow = -1;
        int indexrow, indexfirst, indexend;
        DataRow row;
        string[] tencot = { "F1", "F2", "F3", "F4", "F5", "F6", "F7", "F8", "F9" };// MẮC ĐỊNH TÊN CỘT KHI CHƯA XỬ LÝ SẼ CÓ TÊN KIỂU F1 ĐẾN F...
        //  phương thức trả về false nếu tên cột có dạng giống với mạng tencot
        // trả về true nếu hoặc bảng không có dòng nào hoặc bảng có tên các cột ko giống với biến mạng tencot
        public bool TestCot()
        {
            if (dataSet.Tables[0].Rows.Count <= 0) return true;
            foreach (DataColumn dc in dataSet.Tables[0].Columns)
            {
                for (int i = 0; i < tencot.Length; i++)
                    if (dc.ColumnName == tencot[i])
                    {
                        return false;
                    }
            }
            return true;
        }
        // trả về một dataset chứa bảng đã được lọc ra từ bảng thô ban đầu, bảng mới này sẽ có tên cột được lọc ra từ các dòng dữ liệu
        public DataSet xulydataset()
        {
            // duyet qua cac dong trong bang du lieu
            //neu dong nao chi co 1 o co du lieu thi bo
            // dong nao lan dau tien duyet ma co day du du lieu thi dong do lam ten cot
            if (!TestCot())
            {
                try
                {
                    bool kiemtra = true, kiemtra2 = false;
                    int maxcolumn = 0;
                    int count = 0;

                    #region Đoạn code này thực hiện việc đếm các cell trên từng dòng có giá trị khác rỗng,sau đó lấy max của dòng có nhiều cell <> rỗng nhất.
                    //từ đó lấy được vị trí dòng mà chứa tên các cột ,các dòng tiếp theo sẽ chứa dữ liệu
                    // đồng thời ta sẽ lấy được vị trí đầu và cuối của cột trong bảng sẽ được lọc
                    for (int k = 0; k < dataSet.Tables[0].Rows.Count; k++)
                    {
                        for (int i = 0; i < dataSet.Tables[0].Columns.Count; i++)
                        {
                            if ((Convert.ToString(dataSet.Tables[0].Rows[k][i]).Trim() != "") && (kiemtra))
                            {
                                count += 1;
                                _indexfirst = i;
                                kiemtra = false;
                            }
                            if ((Convert.ToString(dataSet.Tables[0].Rows[k][i]).Trim() != "") && (kiemtra2 == true))
                            {
                                _indexend = i;
                                count += 1;
                            }
                            kiemtra2 = true;
                            if ((Convert.ToString(dataSet.Tables[0].Rows[k][i]).Trim() == "") && (kiemtra == false))
                                break;
                        }
                        if (count > maxcolumn)
                        {
                            indexrow = k;
                            indexfirst = _indexfirst;
                            indexend = _indexend;
                            maxcolumn = count;
                        }
                        count = 0;
                    }
                    #endregion



                    DataTable dt = new DataTable(dataSet.Tables[0].TableName);
                    // tao va dat ten cot moi cho bảng mới chứa dữ liệu sẽ được lọc
                    for (int indexname = indexfirst; indexname <= indexend; indexname++)
                    {
                        dt.Columns.Add(Convert.ToString(dataSet.Tables[0].Rows[indexrow][indexname]));
                    }

                    // thực hiện lọc dữ liệu từ bảng thô ban đầu sang bảng mới dt
                    for (int irow = indexrow + 1; irow < dataSet.Tables[0].Rows.Count; irow++)
                    {
                        int k = 0;
                        row = dt.NewRow();
                        object[] values = new object[indexend - indexfirst + 1];
                        for (int icol = indexfirst; icol <= indexend; icol++)
                        {
                            values[k] = dataSet.Tables[0].Rows[irow][icol]; k += 1;
                        }
                        row.ItemArray = values;
                        dt.Rows.Add(row);
                    }

                    dataSet.Tables.Remove(dataSet.Tables[0]);
                    dataSet.Tables.Add(dt);
                }
                catch  {}
            }

            return dataSet;
        }
    }
}
