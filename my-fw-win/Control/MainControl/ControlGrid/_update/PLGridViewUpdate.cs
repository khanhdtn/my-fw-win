using System;
using System.Collections.Generic;
using System.Text;
using ProtocolVN.Framework.Core;
using System.Data;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;

namespace ProtocolVN.Framework.Win
{
    public class PLGridViewUpdate : DMBasicGrid
    {
        public PLGridViewUpdate() : base()
        {
        }
        public static PLGridViewUpdate ToPLGridViewUpdate(GridControl grid, PLGridView gridView, 
            string TableName, string IDField, 
            string[] NameFields, string[] Subjects,
            InitGridColumns InitGridCol, GetRule Rule,
            DelegationLib.DefinePermission permission, int RowPerPage )
        {
            PLGridViewUpdate update = new PLGridViewUpdate();            
            for (int i = 0; i < gridView.Columns.Count; i++)
            {
                update.gridView.Columns.Add(gridView.Columns[i]);
            }

            update._init(TableName, IDField, NameFields, Subjects, InitGridCol, Rule, permission);
            PLGridViewUpdateHelp page = new PLGridViewUpdateHelp(update.gridControl, RowPerPage, update);
            page.AddFunction = update.AddAction;
            page.DeleteFunction = update.DeleteAction;
            page.EditFunction = update.EditAction;
            page.NoSaveFunction = update.NoSaveAction;
            page.PrintFunction = update.PrintAction;
            page.SaveFunction = update.SaveAction;

            update.Dock = grid.Dock;
            update.Location = grid.Location;

            return update;
        }

        //Chú ý tên table trong DataTable phải giống như tên table mà mình cần thao tác thêm xóa
        public static PLGridViewUpdate ToPLGridViewUpdate(GridControl grid, PLGridView gridView,
            DataTable DataSource, string IDField, string[] NameFields, string[] Subjects,
            InitGridColumns InitGridCol, GetRule Rule,
            DelegationLib.DefinePermission permission,
            PLDelegation.ProcessDataRow InsertFunc, PLDelegation.ProcessDataRow DeleteFunc, 
            PLDelegation.ProcessDataRow UpdateFunc, int RowPerPage)
        {
            PLGridViewUpdate update = new PLGridViewUpdate();
            for (int i = 0; i < gridView.Columns.Count; i++)
            {
                update.gridView.Columns.Add(gridView.Columns[i]);
            }

            update._init(DataSource, IDField, NameFields, Subjects, InitGridCol, Rule,
                        permission, InsertFunc, DeleteFunc, UpdateFunc);

            PLGridViewUpdateHelp page = new PLGridViewUpdateHelp(update.gridControl, RowPerPage, update);
            page.AddFunction = update.AddAction;
            page.DeleteFunction = update.DeleteAction;
            page.EditFunction = update.EditAction;
            page.NoSaveFunction = update.NoSaveAction;
            page.PrintFunction = update.PrintAction;
            page.SaveFunction = update.SaveAction;

            update.Dock = grid.Dock;
            update.Location = grid.Location;

            return update;
        }



        public void _init(
            string TableName, string IDField, 
            string[] NameFields, string[] Subjects,
            InitGridColumns InitGridCol, GetRule Rule,
            DelegationLib.DefinePermission permission, int RowPerPage )
        {
            base._init(TableName, IDField, NameFields, Subjects, InitGridCol, Rule, permission);

            PLGridViewUpdateHelp page = new PLGridViewUpdateHelp(gridControl, RowPerPage, this);
            page.AddFunction = AddAction;
            page.DeleteFunction = DeleteAction;
            page.EditFunction = EditAction;
            page.NoSaveFunction = NoSaveAction;
            page.PrintFunction = PrintAction;
            page.SaveFunction = SaveAction;
        }

        public void _init(DataTable DataSource, string IDField, string[] NameFields, string[] Subjects,
            InitGridColumns InitGridCol, GetRule Rule,
            DelegationLib.DefinePermission permission,
            PLDelegation.ProcessDataRow InsertFunc, PLDelegation.ProcessDataRow DeleteFunc, PLDelegation.ProcessDataRow UpdateFunc, int RowPerPage)
        {
            base._init(DataSource, IDField, NameFields, Subjects, InitGridCol, Rule,
                        permission, InsertFunc, DeleteFunc, UpdateFunc);

            PLGridViewUpdateHelp page = new PLGridViewUpdateHelp(gridControl, RowPerPage, this);
            page.AddFunction = AddAction;
            page.DeleteFunction = DeleteAction;
            page.EditFunction = EditAction;
            page.NoSaveFunction = NoSaveAction;
            page.PrintFunction = PrintAction;
            page.SaveFunction = SaveAction;
        }
    }
}
