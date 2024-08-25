using DemoWPF.Model;
using Microsoft.Win32;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using Formatting = Newtonsoft.Json.Formatting;

namespace DemoWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<Student> students = new ObservableCollection<Student>();
        private string jsonFilePath;

        public MainWindow()
        {
            InitializeComponent();
            StudentDataGrid.ItemsSource = students;
        }

        private void SelectFileButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "JSON files (*.json)|*.json";
            if (openFileDialog.ShowDialog() == true)
            {
                jsonFilePath = openFileDialog.FileName;
                FilePathTextBox.Text = jsonFilePath;
            }
        }

        private void ShowButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(jsonFilePath) && File.Exists(jsonFilePath))
            {
                students.Clear();
                string jsonData = File.ReadAllText(jsonFilePath);
                var studentList = JsonConvert.DeserializeObject<ObservableCollection<Student>>(jsonData);
                foreach (var student in studentList)
                {
                    students.Add(student);
                }
            }
            else
            {
                MessageBox.Show("Please select a valid JSON file!");
            }
        }

        private void StudentDataGrid_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (StudentDataGrid.SelectedItem is Student selectedStudent)
            {
                IdTextBox.Text = selectedStudent.Id.ToString();
                NameTextBox.Text = selectedStudent.Name;
                DOBDatePicker.SelectedDate = DateTime.Parse(selectedStudent.DOB);
                SexComboBox.SelectedItem = SexComboBox.Items.Cast<ComboBoxItem>()
                                      .FirstOrDefault(item => item.Content.ToString() == selectedStudent.Sex);
            }
        }

        private void InsertButton_Click(object sender, RoutedEventArgs e)
        {
            var newStudent = new Student
            {
                Id = students.Count > 0 ? students[^1].Id + 1 : 1, // Tạo ID mới dựa trên ID hiện có
                Name = NameTextBox.Text,
                Sex = (SexComboBox.SelectedItem as ComboBoxItem)?.Content.ToString(),
                DOB = DOBDatePicker.SelectedDate.HasValue ? DOBDatePicker.SelectedDate.Value.ToString("yyyy-MM-dd") : null
            };
            students.Add(newStudent);
            SaveToJsonFile();
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            if (StudentDataGrid.SelectedItem is Student selectedStudent)
            {
                selectedStudent.Name = NameTextBox.Text;
                selectedStudent.Sex = (SexComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
                selectedStudent.DOB = DOBDatePicker.SelectedDate.HasValue ? DOBDatePicker.SelectedDate.Value.ToString("yyyy-MM-dd") : null;
                SaveToJsonFile();
                StudentDataGrid.Items.Refresh(); // Cập nhật lại DataGrid
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (StudentDataGrid.SelectedItem is Student selectedStudent)
            {
                students.Remove(selectedStudent);
                SaveToJsonFile();
            }
        }

        private void SaveToJsonFile()
        {
            if (!string.IsNullOrEmpty(jsonFilePath))
            {
                string jsonData = JsonConvert.SerializeObject(students, Formatting.Indented);
                File.WriteAllText(jsonFilePath, jsonData);
            }
            else
            {
                MessageBox.Show("Please select a valid JSON file to save data!");
            }
        }
    }
}