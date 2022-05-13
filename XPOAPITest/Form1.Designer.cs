
namespace XPOAPITest
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabQuotes = new System.Windows.Forms.TabControl();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblCarrier = new System.Windows.Forms.Label();
            this.lblQuoteId = new System.Windows.Forms.Label();
            this.lblOrderId = new System.Windows.Forms.Label();
            this.lblQuotedAmount = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tabControlUserInput = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.button4 = new System.Windows.Forms.Button();
            this.label16 = new System.Windows.Forms.Label();
            this.comboBox3 = new System.Windows.Forms.ComboBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.button5 = new System.Windows.Forms.Button();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label15 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.comboBox5 = new System.Windows.Forms.ComboBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.comboBox4 = new System.Windows.Forms.ComboBox();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.tabControlMain = new System.Windows.Forms.TabControl();
            this.tabPageQuotes = new System.Windows.Forms.TabPage();
            this.tabPageOrder = new System.Windows.Forms.TabPage();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControlUserInput.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabControlMain.SuspendLayout();
            this.tabPageQuotes.SuspendLayout();
            this.tabPageOrder.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabQuotes
            // 
            this.tabQuotes.Location = new System.Drawing.Point(19, 19);
            this.tabQuotes.Name = "tabQuotes";
            this.tabQuotes.SelectedIndex = 0;
            this.tabQuotes.Size = new System.Drawing.Size(927, 754);
            this.tabQuotes.TabIndex = 2;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(306, 789);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(325, 76);
            this.button2.TabIndex = 1;
            this.button2.Text = "Convert to Order";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(90, 956);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(300, 56);
            this.button1.TabIndex = 0;
            this.button1.Text = "Get Quote";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 25);
            this.label1.TabIndex = 3;
            this.label1.Text = "Carrier";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 25);
            this.label2.TabIndex = 4;
            this.label2.Text = "Quote Id";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 105);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 25);
            this.label3.TabIndex = 5;
            this.label3.Text = "Order Id";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(24, 140);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(143, 25);
            this.label4.TabIndex = 6;
            this.label4.Text = "Quoted Amount";
            // 
            // lblCarrier
            // 
            this.lblCarrier.AutoSize = true;
            this.lblCarrier.Location = new System.Drawing.Point(200, 35);
            this.lblCarrier.Name = "lblCarrier";
            this.lblCarrier.Size = new System.Drawing.Size(0, 25);
            this.lblCarrier.TabIndex = 7;
            // 
            // lblQuoteId
            // 
            this.lblQuoteId.AutoSize = true;
            this.lblQuoteId.Location = new System.Drawing.Point(200, 70);
            this.lblQuoteId.Name = "lblQuoteId";
            this.lblQuoteId.Size = new System.Drawing.Size(0, 25);
            this.lblQuoteId.TabIndex = 8;
            // 
            // lblOrderId
            // 
            this.lblOrderId.AutoSize = true;
            this.lblOrderId.Location = new System.Drawing.Point(200, 105);
            this.lblOrderId.Name = "lblOrderId";
            this.lblOrderId.Size = new System.Drawing.Size(0, 25);
            this.lblOrderId.TabIndex = 9;
            // 
            // lblQuotedAmount
            // 
            this.lblQuotedAmount.AutoSize = true;
            this.lblQuotedAmount.Location = new System.Drawing.Point(200, 140);
            this.lblQuotedAmount.Name = "lblQuotedAmount";
            this.lblQuotedAmount.Size = new System.Drawing.Size(0, 25);
            this.lblQuotedAmount.TabIndex = 10;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Cursor = System.Windows.Forms.Cursors.VSplit;
            this.splitContainer1.Location = new System.Drawing.Point(31, 12);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tabControlUserInput);
            this.splitContainer1.Panel1.Controls.Add(this.button1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabControlMain);
            this.splitContainer1.Panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainer1_Panel2_Paint);
            this.splitContainer1.Size = new System.Drawing.Size(1661, 1015);
            this.splitContainer1.SplitterDistance = 584;
            this.splitContainer1.TabIndex = 11;
            // 
            // tabControlUserInput
            // 
            this.tabControlUserInput.Controls.Add(this.tabPage1);
            this.tabControlUserInput.Controls.Add(this.tabPage2);
            this.tabControlUserInput.Location = new System.Drawing.Point(19, 14);
            this.tabControlUserInput.Name = "tabControlUserInput";
            this.tabControlUserInput.SelectedIndex = 0;
            this.tabControlUserInput.Size = new System.Drawing.Size(545, 936);
            this.tabControlUserInput.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(4, 34);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(537, 898);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Items";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.button4);
            this.tabPage2.Controls.Add(this.label16);
            this.tabPage2.Controls.Add(this.comboBox3);
            this.tabPage2.Controls.Add(this.tabControl1);
            this.tabPage2.Controls.Add(this.dateTimePicker2);
            this.tabPage2.Controls.Add(this.dateTimePicker1);
            this.tabPage2.Controls.Add(this.label7);
            this.tabPage2.Controls.Add(this.label6);
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Controls.Add(this.comboBox1);
            this.tabPage2.Location = new System.Drawing.Point(4, 34);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(537, 898);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Stops";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(145, 837);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(171, 39);
            this.button4.TabIndex = 16;
            this.button4.Text = "Add Stop";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(9, 9);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(57, 25);
            this.label16.TabIndex = 15;
            this.label16.Text = "Stops";
            // 
            // comboBox3
            // 
            this.comboBox3.FormattingEnabled = true;
            this.comboBox3.Location = new System.Drawing.Point(105, 6);
            this.comboBox3.Name = "comboBox3";
            this.comboBox3.Size = new System.Drawing.Size(409, 33);
            this.comboBox3.TabIndex = 14;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Location = new System.Drawing.Point(39, 210);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(447, 622);
            this.tabControl1.TabIndex = 13;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.button5);
            this.tabPage3.Controls.Add(this.textBox5);
            this.tabPage3.Controls.Add(this.textBox4);
            this.tabPage3.Controls.Add(this.textBox3);
            this.tabPage3.Controls.Add(this.comboBox2);
            this.tabPage3.Controls.Add(this.panel1);
            this.tabPage3.Controls.Add(this.radioButton1);
            this.tabPage3.Controls.Add(this.label12);
            this.tabPage3.Controls.Add(this.label11);
            this.tabPage3.Controls.Add(this.label10);
            this.tabPage3.Controls.Add(this.label9);
            this.tabPage3.Controls.Add(this.textBox1);
            this.tabPage3.Controls.Add(this.label8);
            this.tabPage3.Location = new System.Drawing.Point(4, 34);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(439, 584);
            this.tabPage3.TabIndex = 0;
            this.tabPage3.Text = "Contacts";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(91, 524);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(196, 32);
            this.button5.TabIndex = 12;
            this.button5.Text = "Add Contact";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(153, 174);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(245, 31);
            this.textBox5.TabIndex = 11;
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(155, 137);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(242, 31);
            this.textBox4.TabIndex = 10;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(156, 100);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(241, 31);
            this.textBox3.TabIndex = 9;
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(31, 11);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(366, 33);
            this.comboBox2.TabIndex = 8;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label15);
            this.panel1.Controls.Add(this.button3);
            this.panel1.Controls.Add(this.radioButton2);
            this.panel1.Controls.Add(this.comboBox5);
            this.panel1.Controls.Add(this.textBox2);
            this.panel1.Controls.Add(this.label14);
            this.panel1.Controls.Add(this.label13);
            this.panel1.Controls.Add(this.comboBox4);
            this.panel1.Location = new System.Drawing.Point(24, 278);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(394, 219);
            this.panel1.TabIndex = 7;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(109, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(140, 25);
            this.label15.TabIndex = 9;
            this.label15.Text = "Phone Numbers";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(275, 163);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(86, 43);
            this.button3.TabIndex = 7;
            this.button3.Text = "Add Phone Number";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(7, 177);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(115, 29);
            this.radioButton2.TabIndex = 6;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "Is Primary";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // comboBox5
            // 
            this.comboBox5.FormattingEnabled = true;
            this.comboBox5.Items.AddRange(new object[] {
            "MOBILE",
            "WORK",
            "HOME"});
            this.comboBox5.Location = new System.Drawing.Point(109, 75);
            this.comboBox5.Name = "comboBox5";
            this.comboBox5.Size = new System.Drawing.Size(252, 33);
            this.comboBox5.TabIndex = 5;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(109, 126);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(252, 31);
            this.textBox2.TabIndex = 4;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(7, 132);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(77, 25);
            this.label14.TabIndex = 2;
            this.label14.Text = "Number";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(7, 83);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(49, 25);
            this.label13.TabIndex = 1;
            this.label13.Text = "Type";
            // 
            // comboBox4
            // 
            this.comboBox4.FormattingEnabled = true;
            this.comboBox4.Location = new System.Drawing.Point(25, 36);
            this.comboBox4.Name = "comboBox4";
            this.comboBox4.Size = new System.Drawing.Size(336, 33);
            this.comboBox4.TabIndex = 0;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(24, 243);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(115, 29);
            this.radioButton1.TabIndex = 6;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Is Primary";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(5, 204);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(44, 25);
            this.label12.TabIndex = 5;
            this.label12.Text = "Title";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(7, 179);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(140, 25);
            this.label11.TabIndex = 4;
            this.label11.Text = "Phone Numbers";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(7, 143);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(54, 25);
            this.label10.TabIndex = 3;
            this.label10.Text = "Email";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(7, 100);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(95, 25);
            this.label9.TabIndex = 2;
            this.label9.Text = "Last Name";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(158, 60);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(239, 31);
            this.textBox1.TabIndex = 1;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(7, 66);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(97, 25);
            this.label8.TabIndex = 0;
            this.label8.Text = "First Name";
            // 
            // tabPage4
            // 
            this.tabPage4.Location = new System.Drawing.Point(4, 34);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(439, 584);
            this.tabPage4.TabIndex = 1;
            this.tabPage4.Text = "References";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // tabPage5
            // 
            this.tabPage5.Location = new System.Drawing.Point(4, 34);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Size = new System.Drawing.Size(439, 584);
            this.tabPage5.TabIndex = 2;
            this.tabPage5.Text = "Special Requirement";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Location = new System.Drawing.Point(267, 158);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(224, 31);
            this.dateTimePicker2.TabIndex = 12;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(271, 123);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(220, 31);
            this.dateTimePicker1.TabIndex = 11;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(45, 158);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(160, 25);
            this.label7.TabIndex = 10;
            this.label7.Text = "Scheduled Time To";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(35, 123);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(184, 25);
            this.label6.TabIndex = 9;
            this.label6.Text = "Scheduled Time From";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 64);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(49, 25);
            this.label5.TabIndex = 8;
            this.label5.Text = "Type";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "PICKUP",
            "INTERMEDIATE",
            "DELIVERY"});
            this.comboBox1.Location = new System.Drawing.Point(105, 61);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(409, 33);
            this.comboBox1.TabIndex = 7;
            // 
            // tabControlMain
            // 
            this.tabControlMain.Controls.Add(this.tabPageQuotes);
            this.tabControlMain.Controls.Add(this.tabPageOrder);
            this.tabControlMain.Location = new System.Drawing.Point(21, 25);
            this.tabControlMain.Name = "tabControlMain";
            this.tabControlMain.SelectedIndex = 0;
            this.tabControlMain.Size = new System.Drawing.Size(988, 909);
            this.tabControlMain.TabIndex = 0;
            // 
            // tabPageQuotes
            // 
            this.tabPageQuotes.Controls.Add(this.tabQuotes);
            this.tabPageQuotes.Controls.Add(this.button2);
            this.tabPageQuotes.Location = new System.Drawing.Point(4, 34);
            this.tabPageQuotes.Name = "tabPageQuotes";
            this.tabPageQuotes.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageQuotes.Size = new System.Drawing.Size(980, 871);
            this.tabPageQuotes.TabIndex = 0;
            this.tabPageQuotes.Text = "Quotes";
            this.tabPageQuotes.UseVisualStyleBackColor = true;
            this.tabPageQuotes.Click += new System.EventHandler(this.tabPageQuotes_Click);
            // 
            // tabPageOrder
            // 
            this.tabPageOrder.Controls.Add(this.label1);
            this.tabPageOrder.Controls.Add(this.label2);
            this.tabPageOrder.Controls.Add(this.lblQuotedAmount);
            this.tabPageOrder.Controls.Add(this.label3);
            this.tabPageOrder.Controls.Add(this.lblOrderId);
            this.tabPageOrder.Controls.Add(this.label4);
            this.tabPageOrder.Controls.Add(this.lblQuoteId);
            this.tabPageOrder.Controls.Add(this.lblCarrier);
            this.tabPageOrder.Location = new System.Drawing.Point(4, 34);
            this.tabPageOrder.Name = "tabPageOrder";
            this.tabPageOrder.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageOrder.Size = new System.Drawing.Size(980, 871);
            this.tabPageOrder.TabIndex = 1;
            this.tabPageOrder.Text = "Order";
            this.tabPageOrder.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1745, 1028);
            this.Controls.Add(this.splitContainer1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabControlUserInput.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabControlMain.ResumeLayout(false);
            this.tabPageQuotes.ResumeLayout(false);
            this.tabPageOrder.ResumeLayout(false);
            this.tabPageOrder.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TabControl tabQuotes;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblCarrier;
        private System.Windows.Forms.Label lblQuoteId;
        private System.Windows.Forms.Label lblOrderId;
        private System.Windows.Forms.Label lblQuotedAmount;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TabControl tabControlMain;
        private System.Windows.Forms.TabPage tabPageQuotes;
        private System.Windows.Forms.TabPage tabPageOrder;
        private System.Windows.Forms.TabControl tabControlUserInput;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ComboBox comboBox3;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.ComboBox comboBox5;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox comboBox4;
        private System.Windows.Forms.Button button5;
    }
}

