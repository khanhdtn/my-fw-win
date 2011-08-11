namespace ProtocolVN.Framework.Win
{
    using System.Data;
    /// <summary>Lớp dùng để phân DataTable thành nhiều trang con.
    /// </summary>
    public class PagerInfo
    {
        public static string PAGE_INFO = "PAGE_INFO";

        public DataTable Data;

        public int NumPerPage;
        public int CurrentPage = -1;
        public int TotalPage = -1;
        private int startIndex;
        private int endIndex;
        
        /// <summary>Làm tươi dữ liệu dựa số dòng tren 1 trang.
        /// </summary>        
        public void Refresh(int numPerPage)
        {
            if (numPerPage != -1)
            {
                this.NumPerPage = numPerPage;
            }

            int totalRow = this.Data.Rows.Count;
            if (totalRow % this.NumPerPage == 0)
            {
                this.TotalPage = totalRow / this.NumPerPage;
            }
            else
            {
                this.TotalPage = (totalRow / this.NumPerPage) + 1;
            }

            if (this.CurrentPage > this.TotalPage)
            {
                this.CurrentPage = this.TotalPage;
            }
        }

        /// <summary>Lấy dữ liệu DataTable của trang hiện hành.
        /// </summary>
        public DataTable GetCurrentPage()
        {
            if (this.CurrentPage == 1)
            {
                this.startIndex = 0;
                this.endIndex = this.CurrentPage * this.NumPerPage;
            }
            else
            {
                this.startIndex = (this.CurrentPage - 1) * this.NumPerPage;
                this.endIndex = this.CurrentPage * this.NumPerPage;
            }

            DataTable dtTempt = this.Data.Clone();

            //if (endIndex > Data.Rows.Count - 1)
            //    endIndex = Data.Rows.Count - 1;

            for (int i = this.startIndex; i < this.endIndex; i++)
            {
                if (i <= this.Data.Rows.Count - 1)
                {
                    dtTempt.ImportRow(this.Data.Rows[i]);
                }
            }

            return dtTempt;
        }

        /// <summary>Đi đến trang kế
        /// </summary>
        public void NextPage()
        {
            if (this.CurrentPage < this.TotalPage)
            {
                this.CurrentPage++;
            }
        }

        /// <summary>Đi đến trang trước
        /// </summary>
        public void PrevPage()
        {
            if (this.CurrentPage > 1)
            {
                this.CurrentPage--;
            }
        }

        /// <summary>Đi đến trang đầu
        /// </summary>
        public void FirstPage()
        {
            this.CurrentPage = 1;
        }

        /// <summary>Đi đến trang cuối
        /// </summary>
        public void LastPage()
        {
            this.CurrentPage = this.TotalPage;
        }
    }
}
