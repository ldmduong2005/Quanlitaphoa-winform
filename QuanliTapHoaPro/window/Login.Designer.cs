namespace QuanliTapHoaPro
{
    partial class Login
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.loginBtn = new Guna.UI2.WinForms.Guna2GradientButton();
            this.showPassword = new Guna.UI2.WinForms.Guna2CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.RegisterBtn = new System.Windows.Forms.Label();
            this.guna2ShadowPanel1 = new Guna.UI2.WinForms.Guna2ShadowPanel();
            this.guna2ShadowPanel2 = new Guna.UI2.WinForms.Guna2ShadowPanel();
            this.guna2AnimateWindow1 = new Guna.UI2.WinForms.Guna2AnimateWindow(this.components);
            this.guna2GradientPanel1 = new Guna.UI2.WinForms.Guna2GradientPanel();
            this.guna2GradientPanel2 = new Guna.UI2.WinForms.Guna2GradientPanel();
            this.userName = new Guna.UI2.WinForms.Guna2TextBox();
            this.guna2CirclePictureBox1 = new Guna.UI2.WinForms.Guna2CirclePictureBox();
            this.passWord = new Guna.UI2.WinForms.Guna2TextBox();
            this.guna2ShadowPanel1.SuspendLayout();
            this.guna2ShadowPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.guna2CirclePictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // loginBtn
            // 
            this.loginBtn.BackColor = System.Drawing.Color.Transparent;
            this.loginBtn.BorderRadius = 12;
            this.loginBtn.BorderThickness = 2;
            this.loginBtn.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14F);
            this.loginBtn.ForeColor = System.Drawing.Color.White;
            this.loginBtn.Location = new System.Drawing.Point(130, 576);
            this.loginBtn.Name = "loginBtn";
            this.loginBtn.ShadowDecoration.BorderRadius = 15;
            this.loginBtn.ShadowDecoration.Color = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.loginBtn.ShadowDecoration.Depth = 50;
            this.loginBtn.ShadowDecoration.Enabled = true;
            this.loginBtn.Size = new System.Drawing.Size(179, 61);
            this.loginBtn.TabIndex = 2;
            this.loginBtn.Text = "LOGIN";
            this.loginBtn.Click += new System.EventHandler(this.loginBtn_Click);
            // 
            // showPassword
            // 
            this.showPassword.AutoSize = true;
            this.showPassword.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.showPassword.CheckedState.BorderRadius = 0;
            this.showPassword.CheckedState.BorderThickness = 0;
            this.showPassword.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.showPassword.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.showPassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.showPassword.Location = new System.Drawing.Point(76, 490);
            this.showPassword.Name = "showPassword";
            this.showPassword.Size = new System.Drawing.Size(164, 29);
            this.showPassword.TabIndex = 3;
            this.showPassword.Text = "Show password";
            this.showPassword.UncheckedState.BorderRadius = 0;
            this.showPassword.UncheckedState.BorderThickness = 0;
            this.showPassword.CheckedChanged += new System.EventHandler(this.showPassword_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label1.Location = new System.Drawing.Point(54, 246);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(128, 32);
            this.label1.TabIndex = 6;
            this.label1.Text = "Username";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label2.Location = new System.Drawing.Point(55, 373);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(122, 32);
            this.label2.TabIndex = 7;
            this.label2.Text = "Password";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label3.Location = new System.Drawing.Point(106, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(236, 45);
            this.label3.TabIndex = 8;
            this.label3.Text = "Login Account";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label4.Location = new System.Drawing.Point(34, 668);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(195, 30);
            this.label4.TabIndex = 11;
            this.label4.Text = "Have no account?";
            // 
            // RegisterBtn
            // 
            this.RegisterBtn.AutoSize = true;
            this.RegisterBtn.Font = new System.Drawing.Font("Segoe UI", 11F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RegisterBtn.ForeColor = System.Drawing.SystemColors.Highlight;
            this.RegisterBtn.Location = new System.Drawing.Point(250, 668);
            this.RegisterBtn.Name = "RegisterBtn";
            this.RegisterBtn.Size = new System.Drawing.Size(150, 30);
            this.RegisterBtn.TabIndex = 12;
            this.RegisterBtn.Text = "Register here";
            this.RegisterBtn.Click += new System.EventHandler(this.RegisterBtn_Click);
            // 
            // guna2ShadowPanel1
            // 
            this.guna2ShadowPanel1.BackColor = System.Drawing.Color.Transparent;
            this.guna2ShadowPanel1.Controls.Add(this.guna2ShadowPanel2);
            this.guna2ShadowPanel1.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(41)))), ((int)(((byte)(61)))));
            this.guna2ShadowPanel1.Location = new System.Drawing.Point(61, 0);
            this.guna2ShadowPanel1.Name = "guna2ShadowPanel1";
            this.guna2ShadowPanel1.Radius = 10;
            this.guna2ShadowPanel1.ShadowColor = System.Drawing.Color.Black;
            this.guna2ShadowPanel1.Size = new System.Drawing.Size(598, 751);
            this.guna2ShadowPanel1.TabIndex = 13;
            // 
            // guna2ShadowPanel2
            // 
            this.guna2ShadowPanel2.BackColor = System.Drawing.Color.Transparent;
            this.guna2ShadowPanel2.Controls.Add(this.label1);
            this.guna2ShadowPanel2.Controls.Add(this.userName);
            this.guna2ShadowPanel2.Controls.Add(this.label2);
            this.guna2ShadowPanel2.Controls.Add(this.RegisterBtn);
            this.guna2ShadowPanel2.Controls.Add(this.loginBtn);
            this.guna2ShadowPanel2.Controls.Add(this.showPassword);
            this.guna2ShadowPanel2.Controls.Add(this.label3);
            this.guna2ShadowPanel2.Controls.Add(this.label4);
            this.guna2ShadowPanel2.Controls.Add(this.guna2CirclePictureBox1);
            this.guna2ShadowPanel2.Controls.Add(this.passWord);
            this.guna2ShadowPanel2.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(48)))), ((int)(((byte)(69)))));
            this.guna2ShadowPanel2.Location = new System.Drawing.Point(62, 12);
            this.guna2ShadowPanel2.Name = "guna2ShadowPanel2";
            this.guna2ShadowPanel2.Radius = 15;
            this.guna2ShadowPanel2.ShadowColor = System.Drawing.Color.Black;
            this.guna2ShadowPanel2.Size = new System.Drawing.Size(454, 720);
            this.guna2ShadowPanel2.TabIndex = 13;
            // 
            // guna2AnimateWindow1
            // 
            this.guna2AnimateWindow1.AnimationType = Guna.UI2.WinForms.Guna2AnimateWindow.AnimateWindowType.AW_BLEND;
            this.guna2AnimateWindow1.Interval = 300;
            this.guna2AnimateWindow1.TargetForm = this;
            // 
            // guna2GradientPanel1
            // 
            this.guna2GradientPanel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.guna2GradientPanel1.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(77)))), ((int)(((byte)(165)))));
            this.guna2GradientPanel1.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.guna2GradientPanel1.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.BackwardDiagonal;
            this.guna2GradientPanel1.Location = new System.Drawing.Point(0, 0);
            this.guna2GradientPanel1.Name = "guna2GradientPanel1";
            this.guna2GradientPanel1.Size = new System.Drawing.Size(68, 744);
            this.guna2GradientPanel1.TabIndex = 14;
            // 
            // guna2GradientPanel2
            // 
            this.guna2GradientPanel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.guna2GradientPanel2.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(77)))), ((int)(((byte)(165)))));
            this.guna2GradientPanel2.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.guna2GradientPanel2.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.guna2GradientPanel2.Location = new System.Drawing.Point(629, 0);
            this.guna2GradientPanel2.Name = "guna2GradientPanel2";
            this.guna2GradientPanel2.Size = new System.Drawing.Size(71, 744);
            this.guna2GradientPanel2.TabIndex = 15;
            // 
            // userName
            // 
            this.userName.BorderColor = System.Drawing.Color.Silver;
            this.userName.BorderRadius = 10;
            this.userName.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.userName.DefaultText = "";
            this.userName.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(46)))));
            this.userName.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.userName.IconLeft = global::QuanliTapHoaPro.Properties.Resources.People;
            this.userName.IconLeftOffset = new System.Drawing.Point(10, 0);
            this.userName.IconLeftSize = new System.Drawing.Size(25, 25);
            this.userName.Location = new System.Drawing.Point(60, 284);
            this.userName.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.userName.Name = "userName";
            this.userName.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(114)))), ((int)(((byte)(147)))));
            this.userName.PlaceholderText = "Username";
            this.userName.SelectedText = "";
            this.userName.Size = new System.Drawing.Size(321, 60);
            this.userName.TabIndex = 4;
            this.userName.TextOffset = new System.Drawing.Point(10, 0);
            // 
            // guna2CirclePictureBox1
            // 
            this.guna2CirclePictureBox1.Image = global::QuanliTapHoaPro.Properties.Resources._295128;
            this.guna2CirclePictureBox1.ImageRotate = 0F;
            this.guna2CirclePictureBox1.Location = new System.Drawing.Point(142, 85);
            this.guna2CirclePictureBox1.Name = "guna2CirclePictureBox1";
            this.guna2CirclePictureBox1.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.guna2CirclePictureBox1.Size = new System.Drawing.Size(151, 145);
            this.guna2CirclePictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.guna2CirclePictureBox1.TabIndex = 10;
            this.guna2CirclePictureBox1.TabStop = false;
            // 
            // passWord
            // 
            this.passWord.BorderRadius = 10;
            this.passWord.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.passWord.DefaultText = "";
            this.passWord.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(46)))));
            this.passWord.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.passWord.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.passWord.IconLeft = global::QuanliTapHoaPro.Properties.Resources.Secure;
            this.passWord.IconLeftOffset = new System.Drawing.Point(10, 0);
            this.passWord.IconLeftSize = new System.Drawing.Size(21, 21);
            this.passWord.Location = new System.Drawing.Point(60, 410);
            this.passWord.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.passWord.Name = "passWord";
            this.passWord.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(114)))), ((int)(((byte)(147)))));
            this.passWord.PlaceholderText = "Password";
            this.passWord.SelectedText = "";
            this.passWord.Size = new System.Drawing.Size(321, 57);
            this.passWord.TabIndex = 5;
            this.passWord.TextOffset = new System.Drawing.Point(10, 0);
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(41)))), ((int)(((byte)(61)))));
            this.ClientSize = new System.Drawing.Size(700, 744);
            this.Controls.Add(this.guna2GradientPanel2);
            this.Controls.Add(this.guna2GradientPanel1);
            this.Controls.Add(this.guna2ShadowPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.HelpButton = true;
            this.Name = "Login";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login";
            this.guna2ShadowPanel1.ResumeLayout(false);
            this.guna2ShadowPanel2.ResumeLayout(false);
            this.guna2ShadowPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.guna2CirclePictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private Guna.UI2.WinForms.Guna2GradientButton loginBtn;
        private Guna.UI2.WinForms.Guna2CheckBox showPassword;
        private Guna.UI2.WinForms.Guna2TextBox userName;
        private Guna.UI2.WinForms.Guna2TextBox passWord;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label RegisterBtn;
        private Guna.UI2.WinForms.Guna2CirclePictureBox guna2CirclePictureBox1;
        private Guna.UI2.WinForms.Guna2ShadowPanel guna2ShadowPanel1;
        private Guna.UI2.WinForms.Guna2AnimateWindow guna2AnimateWindow1;
        private Guna.UI2.WinForms.Guna2GradientPanel guna2GradientPanel2;
        private Guna.UI2.WinForms.Guna2GradientPanel guna2GradientPanel1;
        private Guna.UI2.WinForms.Guna2ShadowPanel guna2ShadowPanel2;
    }
}

