using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace ProtocolVN.Framework.Win
{

	#region InputBox return result

	/// <summary>
	/// Class used to store the result of an InputBox.Show message.
	/// </summary>
	public class InputBoxResult 
	{
		public DialogResult ReturnCode;
		public string Text;
	}

	#endregion
    
	/// <summary>
	/// Hop thoai don gian cho phep nguoi dung vao 1 thong tin.
	/// </summary>
    [Obsolete("Sử dụng HelpMsgBox._showMsgInput")]
	public class PLInputBox
	{
        
		#region Private Windows Contols and Constructor

		// Create a new instance of the form.
		private static XtraForm frmInputDialog;
		private static Label lblPrompt;
		private static SimpleButton btnOK;
        private static SimpleButton btnCancel;
		public static TextEdit txtInput;

		public PLInputBox()
		{
		}

		#endregion

		#region Private Variables

		private static string _formCaption = string.Empty;
		private static string _formPrompt = string.Empty;
		private static InputBoxResult _outputResponse = new InputBoxResult();
		private static string _defaultValue = string.Empty;
		private static int _xPos = -1;
		private static int _yPos = -1;

		#endregion

		#region Windows Form code

		private static void InitializeComponent()
		{
			// Create a new instance of the form.
			frmInputDialog = new XtraForm();
			lblPrompt = new Label();
			btnOK = new SimpleButton();
			btnCancel = new SimpleButton();
			txtInput = new TextEdit();
			frmInputDialog.SuspendLayout();
			// 
			// lblPrompt
			// 
			lblPrompt.Anchor = ((AnchorStyles)((((AnchorStyles.Top | AnchorStyles.Bottom) | AnchorStyles.Left) | AnchorStyles.Right)));			
			lblPrompt.Location = new Point(12, 9);
			lblPrompt.Name = "lblPrompt";
			lblPrompt.Size = new Size(302, 82);
			lblPrompt.TabIndex = 3;
			// 
			// btnOK
			// 
			btnOK.DialogResult = DialogResult.OK;
			btnOK.Location = new Point(326, 8);
			btnOK.Name = "btnOK";
			btnOK.Size = new Size(64, 24);
			btnOK.TabIndex = 1;
			btnOK.Text = "Đồng ý";
			btnOK.Click += new EventHandler(btnOK_Click);
			// 
			// btnCancel
			// 
			btnCancel.DialogResult = DialogResult.Cancel;
			btnCancel.Location = new Point(326, 40);
			btnCancel.Name = "btnCancel";
			btnCancel.Size = new Size(64, 24);
			btnCancel.TabIndex = 2;
			btnCancel.Text = "Đóng";
			btnCancel.Click += new EventHandler(btnCancel_Click);
			// 
			// txtInput
			// 
			txtInput.Location = new Point(8, 100);
			txtInput.Name = "txtInput";
			txtInput.Size = new Size(379, 20);
			txtInput.TabIndex = 0;
			txtInput.Text = "";
			// 
			// InputBoxDialog
			// 
			frmInputDialog.AutoScaleBaseSize = new Size(5, 13);
			frmInputDialog.ClientSize = new Size(398, 128);
			frmInputDialog.Controls.Add(txtInput);
			frmInputDialog.Controls.Add(btnCancel);
			frmInputDialog.Controls.Add(btnOK);
			frmInputDialog.Controls.Add(lblPrompt);
			frmInputDialog.FormBorderStyle = FormBorderStyle.FixedDialog;
			frmInputDialog.MaximizeBox = false;
			frmInputDialog.MinimizeBox = false;
			frmInputDialog.Name = "InputBoxDialog";
			frmInputDialog.ResumeLayout(false);
		}

		#endregion

		#region Private function, InputBox Form move and change size

		static private void LoadForm()
		{
			OutputResponse.ReturnCode = DialogResult.Ignore;
			OutputResponse.Text = string.Empty;

			txtInput.Text = _defaultValue;
			lblPrompt.Text = _formPrompt;
			frmInputDialog.Text = _formCaption;

			// Retrieve the working rectangle from the Screen class
			// using the PrimaryScreen and the WorkingArea properties.
			System.Drawing.Rectangle workingRectangle = Screen.PrimaryScreen.WorkingArea;

			if((_xPos >= 0 && _xPos < workingRectangle.Width-100) && (_yPos >= 0 && _yPos < workingRectangle.Height-100))
			{
				frmInputDialog.StartPosition = FormStartPosition.Manual;
        frmInputDialog.Location = new System.Drawing.Point(_xPos, _yPos);
			}
			else
				frmInputDialog.StartPosition = FormStartPosition.CenterScreen;


			string PrompText = lblPrompt.Text;

			int n = 0;
			int Index = 0;
			while(PrompText.IndexOf("\n",Index) > -1)
			{
				Index = PrompText.IndexOf("\n",Index)+1;
				n++;
			}

			if( n == 0 )
				n = 1;

			System.Drawing.Point Txt = txtInput.Location; 
			Txt.Y = Txt.Y + (n*4);
			txtInput.Location = Txt; 
			System.Drawing.Size form = frmInputDialog.Size; 
			form.Height = form.Height + (n*4);
			frmInputDialog.Size = form; 

			txtInput.SelectionStart = 0;
			txtInput.SelectionLength = txtInput.Text.Length;
			txtInput.Focus();
		}

		#endregion

		#region Button control click event

		static private void btnOK_Click(object sender, System.EventArgs e)
		{
			OutputResponse.ReturnCode = DialogResult.OK;
			OutputResponse.Text = txtInput.Text;
			frmInputDialog.Dispose();
		}

		static private void btnCancel_Click(object sender, System.EventArgs e)
		{
			OutputResponse.ReturnCode = DialogResult.Cancel;
			OutputResponse.Text = string.Empty; //Clean output response
			frmInputDialog.Dispose();
		}

		#endregion

		#region Public Static Show functions

		static public InputBoxResult Show(string Prompt)
		{
			InitializeComponent();
			FormPrompt = Prompt;

			// Display the form as a modal dialog box.
			LoadForm();
			frmInputDialog.ShowDialog();
            
			return OutputResponse;
		}

		static public InputBoxResult Show(string Prompt,string Title)
		{
			InitializeComponent();

			FormCaption = Title;
			FormPrompt = Prompt;

			// Display the form as a modal dialog box.
			LoadForm();
			frmInputDialog.ShowDialog();
			return OutputResponse;
		}

		static public InputBoxResult Show(string Prompt,string Title,string Default)
		{
			InitializeComponent();

			FormCaption = Title;
			FormPrompt = Prompt;
			DefaultValue = Default;

			// Display the form as a modal dialog box.
			LoadForm();
			frmInputDialog.ShowDialog();
			return OutputResponse;
		}

		static public InputBoxResult Show(string Prompt,string Title,string Default,int XPos,int YPos)
		{
			InitializeComponent();
			FormCaption = Title;
			FormPrompt = Prompt;
			DefaultValue = Default;
			XPosition = XPos;
			YPosition = YPos;

			// Display the form as a modal dialog box.
			LoadForm();
			frmInputDialog.ShowDialog();
			return OutputResponse;
		}

		#endregion

		#region Private Properties

		static private string FormCaption
		{
			set
			{
				_formCaption = value;
			}
		} // property FormCaption
		
		static private string FormPrompt
		{
			set
			{
				_formPrompt = value;
			}
		} // property FormPrompt
		
		static private InputBoxResult OutputResponse
		{
			get
			{
				return _outputResponse;
			}
			set
			{
				_outputResponse = value;
			}
		} // property InputResponse
		
		static private string DefaultValue
		{
			set
			{
				_defaultValue = value;
			}
		} // property DefaultValue

		static private int XPosition
		{
			set
			{
				if( value >= 0 )
					_xPos = value;
			}
		} // property XPos

		static private int YPosition
		{
			set
			{
				if( value >= 0 )
					_yPos = value;
			}
		} // property YPos

		#endregion 
	}
}
