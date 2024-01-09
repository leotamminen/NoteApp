using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace NoteApp
{
    public partial class NoteTaker : Form
    {
        // Create DataTable notes for storing the notes
        DataTable notes = new DataTable();
        bool editing = false; 
        private const string ConnectionString = "Server=localhost;Database=NoteApp;User=user;Password=salasana;";


        private void SaveToDatabase()
        {
            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {
                try
                {
                    connection.Open();

                    using (MySqlTransaction transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            foreach (DataRow row in notes.Rows)
                            {
                                // Check if a similar note already exists in the database by Id
                                bool noteExists = NoteExists(connection, transaction, row["Id"]);

                                if (!noteExists)
                                {
                                    using (MySqlCommand cmd = new MySqlCommand("INSERT INTO Notes (Id, Title, Note) VALUES (@Id, @Title, @Note)", connection, transaction))
                                    {
                                        // Set the Id explicitly
                                        if (row["Id"] == DBNull.Value || row["Id"] == null)
                                        {
                                            cmd.Parameters.AddWithValue("@Id", DBNull.Value);
                                        }
                                        else
                                        {
                                            cmd.Parameters.AddWithValue("@Id", Convert.ToInt32(row["Id"]));
                                        }

                                        cmd.Parameters.AddWithValue("@Title", row["Title"]);
                                        cmd.Parameters.AddWithValue("@Note", row["Note"]);
                                        cmd.ExecuteNonQuery();
                                    }
                                }
                            }

                            // Commit the transaction if all inserts succeed
                            transaction.Commit();

                            // Reload existing notes from the database
                            LoadNotesFromDatabase();

                            MessageBox.Show("Data saved to the database successfully!");
                        }
                        catch (Exception ex)
                        {
                            // Rollback the transaction if an exception occurs
                            transaction.Rollback();
                            throw ex; // Re-throw the exception after rolling back the transaction
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error saving data to the database: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private bool NoteExists(MySqlConnection connection, MySqlTransaction transaction, object id)
        {
            using (MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM Notes WHERE Id = @Id", connection, transaction))
            {
                // Check for DBNull and null before converting to int
                if (id == DBNull.Value || id == null)
                {
                    cmd.Parameters.AddWithValue("@Id", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Id", Convert.ToInt32(id));
                }

                int count = Convert.ToInt32(cmd.ExecuteScalar());
                return count > 0; // Return true if a matching note is found
            }
        }

        private void ResetAutoIncrementCounter(MySqlConnection connection)
        {
            // Reset the auto-increment counter to 1
            using (MySqlCommand resetCmd = new MySqlCommand("ALTER TABLE Notes AUTO_INCREMENT = 1;", connection))
            {
                resetCmd.ExecuteNonQuery();
            }
        }

        private void LoadNotesFromDatabase()
        {
            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {
                try
                {
                    connection.Open();

                    // Clear existing rows from the DataTable
                    notes.Clear();

                    // Retrieve notes from the database
                    using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM notes", connection))
                    {
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                // Add a new row to the DataTable and populate columns
                                DataRow newRow = notes.NewRow();
                                newRow["Id"] = reader.GetInt32("Id");
                                newRow["Title"] = reader.GetString("Title");
                                newRow["Note"] = reader.GetString("Note");
                                notes.Rows.Add(newRow);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error loading notes from database: {ex.Message}");
                    // Handle the exception as needed (e.g., show an error message to the user)
                }
            }
        }

        public NoteTaker()
        {
            InitializeComponent();

            // Initialize DataTable
            notes.Columns.Add("Id", typeof(int));
            notes.Columns.Add("Title");
            notes.Columns.Add("Note");

            // Load existing notes from the database
            LoadNotesFromDatabase();

            // Bind the DataTable to the DataGridView
            previousNotes.DataSource = notes;
        }

        private void NoteTaker_Load(object sender, EventArgs e)
        {
            // Set up columns for the DataTable, including the "Id" column
            notes.Columns.Add("Id", typeof(int));
            notes.Columns.Add("Title");
            notes.Columns.Add("Note");

            // Bind the DataTable to the DataGridView
            previousNotes.DataSource = notes;

            // Load notes from the database
            LoadNotesFromDatabase();
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (previousNotes.CurrentCell != null)
                {
                    int rowIndex = previousNotes.CurrentCell.RowIndex;

                    // Check if the row index is valid
                    if (rowIndex >= 0 && rowIndex < notes.Rows.Count)
                    {
                        // Get the ID of the note to be deleted from the selected row in the DataGridView
                        int noteId = GetNoteIdForRow(rowIndex);

                        // Delete the note from the database using its ID
                        DeleteNoteFromDatabase(noteId);

                        // Delete the note from the DataTable
                        notes.Rows.RemoveAt(rowIndex);

                        // Update the DataGridView to reflect the changes
                        previousNotes.DataSource = null;
                        previousNotes.DataSource = notes;
                    }
                    else
                    {
                        Console.WriteLine("Invalid row index for deletion.");
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle the exception as needed (fo example show error message to the user)
                Console.WriteLine("Error deleting note: " + ex.Message);
            }
        }

        private void DeleteNoteFromDatabase(int noteId)
        {
            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {
                try
                {
                    connection.Open();

                    // Delete note from the database using the right ID
                    using (MySqlCommand cmd = new MySqlCommand("DELETE FROM notes WHERE Id = @Id", connection))
                    {
                        cmd.Parameters.AddWithValue("@Id", noteId);
                        cmd.ExecuteNonQuery();
                    }
                    // For debugging
                    Console.WriteLine("Note deleted successfully!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error deleting note from database: {ex.Message}");
                }
            }
        }

        // Only used in deleteButton_Click
        private int GetNoteIdForRow(int rowIndex)
        {
            // Working if DataTable has a column Id
            return Convert.ToInt32(notes.Rows[rowIndex]["Id"]);
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (editing)
                {
                    if (previousNotes.CurrentCell != null)
                    {
                        int rowIndex = previousNotes.CurrentCell.RowIndex;

                        if (rowIndex >= 0 && rowIndex < notes.Rows.Count)
                        {
                            // Update existing row
                            notes.Rows[rowIndex]["Title"] = titleBox.Text;
                            notes.Rows[rowIndex]["Note"] = noteBox.Text;
                        }
                        else
                        {
                            Console.WriteLine("Invalid row index for editing.");
                        }

                        editing = false;
                    }
                }
                else
                {
                    // Add a new row to the DataTable
                    DataRow newRow = notes.NewRow();
                    newRow["Id"] = DBNull.Value; // Set the Id to DBNull.Value for new rows
                    newRow["Title"] = titleBox.Text;
                    newRow["Note"] = noteBox.Text;
                    notes.Rows.Add(newRow);
                }

                // Save the data to the database
                SaveToDatabase();

                // Clear textboxes
                titleBox.Text = "";
                noteBox.Text = "";

                // refresh the DataGridView with the updated data
                previousNotes.DataSource = notes;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in saveButton_Click: {ex.Message}");
            }
        }

        private void newNoteButton_Click(object sender, EventArgs e)
        {
            titleBox.Text = "";
            noteBox.Text = "";
            editing = false;
        }

        private void LoadButton_Click(object sender, EventArgs e)
        {
            if (previousNotes.CurrentCell != null)
            {
                int rowIndex = previousNotes.CurrentCell.RowIndex;

                if (rowIndex >= 0 && rowIndex < notes.Rows.Count)
                {
                    titleBox.Text = notes.Rows[rowIndex]["Title"].ToString();
                    noteBox.Text = notes.Rows[rowIndex]["Note"].ToString();
                    editing = true;
                }
                else
                {
                    Console.WriteLine("Invalid row index for loading.");
                }
            }
        }

        private void previousNotes_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            LoadButton_Click(sender, e); // Exactly same logic than LoadButton_Click when double clicking a existing note
        }
    }
}