using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimpleNotes
{
    public partial class Form1 : Form
    {
        string filePath = "notes.bin";
        public Form1()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string title = txtTitle.Text;
                if (string.IsNullOrWhiteSpace(title))
                {
                    MessageBox.Show("Please enter a title.");
                    return;
                }

                List<string> notes = LoadNotes();
                notes.Add(title);
                SaveNotes(notes);

                MessageBox.Show("Note saved successfully.");
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving note: {ex.Message}");
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            try
            {
                List<string> notes = LoadNotes();
                lstNotes.Items.Clear();
                foreach (var note in notes)
                {
                    lstNotes.Items.Add(note);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading notes: {ex.Message}");
            }
        }

        private List<string> LoadNotes()
        {
            if (!File.Exists(filePath))
                return new List<string>();

            using (FileStream fs = new FileStream(filePath, FileMode.Open))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                return (List<string>)formatter.Deserialize(fs);
            }
        }

        private void SaveNotes(List<string> notes)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Create))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(fs, notes);
            }
        }

        private void ClearForm()
        {
            txtTitle.Clear();
        }
    }
}
