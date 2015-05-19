namespace TrueChessGame.DesktopUI
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.btnChosen = new System.Windows.Forms.Button();
            this.btnReversedChosen = new System.Windows.Forms.Button();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.btnNewGame = new System.Windows.Forms.Button();
            this.pcbReversedChessboard = new System.Windows.Forms.PictureBox();
            this.pcbMainChessboard = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pcbReversedChessboard)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcbMainChessboard)).BeginInit();
            this.SuspendLayout();
            // 
            // btnChosen
            // 
            this.btnChosen.BackColor = System.Drawing.Color.Red;
            this.btnChosen.FlatAppearance.BorderColor = System.Drawing.Color.Crimson;
            this.btnChosen.FlatAppearance.BorderSize = 0;
            this.btnChosen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChosen.ForeColor = System.Drawing.Color.Red;
            this.btnChosen.Location = new System.Drawing.Point(845, 521);
            this.btnChosen.Name = "btnChosen";
            this.btnChosen.Size = new System.Drawing.Size(20, 15);
            this.btnChosen.TabIndex = 1;
            this.btnChosen.TabStop = false;
            this.btnChosen.UseVisualStyleBackColor = false;
            this.btnChosen.Visible = false;
            this.btnChosen.Click += new System.EventHandler(this.Chessboard_Click);
            // 
            // btnReversedChosen
            // 
            this.btnReversedChosen.BackColor = System.Drawing.Color.Red;
            this.btnReversedChosen.FlatAppearance.BorderColor = System.Drawing.Color.Crimson;
            this.btnReversedChosen.FlatAppearance.BorderSize = 0;
            this.btnReversedChosen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReversedChosen.ForeColor = System.Drawing.Color.Red;
            this.btnReversedChosen.Location = new System.Drawing.Point(845, 542);
            this.btnReversedChosen.Name = "btnReversedChosen";
            this.btnReversedChosen.Size = new System.Drawing.Size(20, 15);
            this.btnReversedChosen.TabIndex = 3;
            this.btnReversedChosen.TabStop = false;
            this.btnReversedChosen.UseVisualStyleBackColor = false;
            this.btnReversedChosen.Visible = false;
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel1.LinkArea = new System.Windows.Forms.LinkArea(0, 0);
            this.linkLabel1.Location = new System.Drawing.Point(766, 635);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(230, 17);
            this.linkLabel1.TabIndex = 4;
            this.linkLabel1.Text = "Graphics created by Virtual Pieces ";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // btnNewGame
            // 
            this.btnNewGame.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNewGame.Location = new System.Drawing.Point(883, 521);
            this.btnNewGame.Name = "btnNewGame";
            this.btnNewGame.Size = new System.Drawing.Size(100, 100);
            this.btnNewGame.TabIndex = 5;
            this.btnNewGame.Text = "NEW GAME";
            this.btnNewGame.UseVisualStyleBackColor = true;
            this.btnNewGame.Click += new System.EventHandler(this.button1_Click);
            // 
            // pcbReversedChessboard
            // 
            this.pcbReversedChessboard.Image = global::TrueChessGame.DesktopUI.Properties.Resources.Board_black_reversed;
            this.pcbReversedChessboard.Location = new System.Drawing.Point(510, 6);
            this.pcbReversedChessboard.Name = "pcbReversedChessboard";
            this.pcbReversedChessboard.Size = new System.Drawing.Size(494, 494);
            this.pcbReversedChessboard.TabIndex = 2;
            this.pcbReversedChessboard.TabStop = false;
            this.pcbReversedChessboard.Click += new System.EventHandler(this.Chessboard_Click);
            // 
            // pcbMainChessboard
            // 
            this.pcbMainChessboard.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pcbMainChessboard.Image = global::TrueChessGame.DesktopUI.Properties.Resources.Board_black_2;
            this.pcbMainChessboard.Location = new System.Drawing.Point(4, 6);
            this.pcbMainChessboard.Name = "pcbMainChessboard";
            this.pcbMainChessboard.Size = new System.Drawing.Size(494, 494);
            this.pcbMainChessboard.TabIndex = 0;
            this.pcbMainChessboard.TabStop = false;
            this.pcbMainChessboard.Click += new System.EventHandler(this.Chessboard_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(1008, 661);
            this.Controls.Add(this.btnNewGame);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.btnReversedChosen);
            this.Controls.Add(this.pcbReversedChessboard);
            this.Controls.Add(this.btnChosen);
            this.Controls.Add(this.pcbMainChessboard);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Dan\'s True Chess Game. White, your move";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pcbReversedChessboard)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcbMainChessboard)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pcbMainChessboard;
        private System.Windows.Forms.Button btnChosen;
        private System.Windows.Forms.PictureBox pcbReversedChessboard;
        private System.Windows.Forms.Button btnReversedChosen;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Button btnNewGame;





    }
}

