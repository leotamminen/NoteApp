namespace NoteApp
{
    partial class NoteTaker
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
            titleBox = new TextBox();
            noteBox = new TextBox();
            label1 = new Label();
            noteText = new Label();
            previousNotes = new DataGridView();
            LoadButton = new Button();
            deleteButton = new Button();
            newNoteButton = new Button();
            saveButton = new Button();
            ExistingNotesText = new Label();
            DoubleClickToEditText = new Label();
            ((System.ComponentModel.ISupportInitialize)previousNotes).BeginInit();
            SuspendLayout();
            // 
            // titleBox
            // 
            titleBox.Location = new Point(455, 40);
            titleBox.Name = "titleBox";
            titleBox.Size = new Size(574, 27);
            titleBox.TabIndex = 0;
            // 
            // noteBox
            // 
            noteBox.Location = new Point(455, 121);
            noteBox.Multiline = true;
            noteBox.Name = "noteBox";
            noteBox.Size = new Size(574, 487);
            noteBox.TabIndex = 1;
            // 
            // label1
            // 
            label1.Font = new Font("Showcard Gothic", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(455, 17);
            label1.Name = "label1";
            label1.Size = new Size(574, 25);
            label1.TabIndex = 2;
            label1.Text = "Title:";
            // 
            // noteText
            // 
            noteText.AutoSize = true;
            noteText.Font = new Font("Showcard Gothic", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            noteText.Location = new Point(455, 81);
            noteText.Name = "noteText";
            noteText.Size = new Size(66, 18);
            noteText.TabIndex = 3;
            noteText.Text = "Notes:";
            // 
            // previousNotes
            // 
            previousNotes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            previousNotes.BackgroundColor = Color.White;
            previousNotes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            previousNotes.GridColor = SystemColors.InactiveCaption;
            previousNotes.Location = new Point(12, 40);
            previousNotes.Name = "previousNotes";
            previousNotes.RowHeadersWidth = 51;
            previousNotes.Size = new Size(421, 371);
            previousNotes.TabIndex = 4;
            previousNotes.CellDoubleClick += previousNotes_CellDoubleClick;
            // 
            // LoadButton
            // 
            LoadButton.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            LoadButton.Location = new Point(15, 510);
            LoadButton.Name = "LoadButton";
            LoadButton.Size = new Size(194, 29);
            LoadButton.TabIndex = 5;
            LoadButton.Text = "Load";
            LoadButton.UseVisualStyleBackColor = true;
            LoadButton.Click += LoadButton_Click;
            // 
            // deleteButton
            // 
            deleteButton.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            deleteButton.Location = new Point(215, 510);
            deleteButton.Name = "deleteButton";
            deleteButton.Size = new Size(194, 29);
            deleteButton.TabIndex = 6;
            deleteButton.Text = "Delete";
            deleteButton.UseVisualStyleBackColor = true;
            deleteButton.Click += deleteButton_Click;
            // 
            // newNoteButton
            // 
            newNoteButton.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            newNoteButton.Location = new Point(15, 560);
            newNoteButton.Name = "newNoteButton";
            newNoteButton.Size = new Size(194, 29);
            newNoteButton.TabIndex = 7;
            newNoteButton.Text = "New Note";
            newNoteButton.UseVisualStyleBackColor = true;
            newNoteButton.Click += newNoteButton_Click;
            // 
            // saveButton
            // 
            saveButton.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            saveButton.Location = new Point(215, 560);
            saveButton.Name = "saveButton";
            saveButton.Size = new Size(194, 29);
            saveButton.TabIndex = 8;
            saveButton.Text = "Save";
            saveButton.UseVisualStyleBackColor = true;
            saveButton.Click += saveButton_Click;
            // 
            // ExistingNotesText
            // 
            ExistingNotesText.AutoSize = true;
            ExistingNotesText.Font = new Font("Showcard Gothic", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            ExistingNotesText.Location = new Point(134, 9);
            ExistingNotesText.Name = "ExistingNotesText";
            ExistingNotesText.Size = new Size(169, 23);
            ExistingNotesText.TabIndex = 9;
            ExistingNotesText.Text = "Existing notes:";
            // 
            // DoubleClickToEditText
            // 
            DoubleClickToEditText.AutoSize = true;
            DoubleClickToEditText.Font = new Font("Showcard Gothic", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            DoubleClickToEditText.Location = new Point(25, 433);
            DoubleClickToEditText.Name = "DoubleClickToEditText";
            DoubleClickToEditText.Size = new Size(399, 18);
            DoubleClickToEditText.TabIndex = 10;
            DoubleClickToEditText.Text = "You can also double click a note to edit it!";
            // 
            // NoteTaker
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Fuchsia;
            ClientSize = new Size(1041, 620);
            Controls.Add(DoubleClickToEditText);
            Controls.Add(ExistingNotesText);
            Controls.Add(saveButton);
            Controls.Add(newNoteButton);
            Controls.Add(deleteButton);
            Controls.Add(LoadButton);
            Controls.Add(previousNotes);
            Controls.Add(noteText);
            Controls.Add(label1);
            Controls.Add(noteBox);
            Controls.Add(titleBox);
            Name = "NoteTaker";
            Text = "NoteTaker";
            ((System.ComponentModel.ISupportInitialize)previousNotes).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox titleBox;
        private TextBox noteBox;
        private Label label1;
        private Label noteText;
        private DataGridView previousNotes;
        private Button LoadButton;
        private Button deleteButton;
        private Button newNoteButton;
        private Button saveButton;
        private Label ExistingNotesText;
        private Label DoubleClickToEditText;
    }
}
