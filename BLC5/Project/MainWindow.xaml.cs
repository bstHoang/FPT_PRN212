using Microsoft.Win32;
using Newtonsoft.Json;
using Project.Model;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Project
{
    public partial class MainWindow : Window
    {
        private Root data;
        private List<CheckBox> scoreCheckBoxes = new List<CheckBox>();
        private string selectedFilePath; // Lưu đường dẫn tệp JSON đã chọn

        public MainWindow()
        {
            InitializeComponent();
        }

        private void LoadJsonFile(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*"
            };
            if (openFileDialog.ShowDialog() == true)
            {
                selectedFilePath = openFileDialog.FileName;
                string json = File.ReadAllText(selectedFilePath);

                try
                {
                    data = JsonConvert.DeserializeObject<Root>(json);

                    if (data?.Courses != null)
                    {
                        foreach (var course in data.Courses)
                        {
                            foreach (var student in course.Students)
                            {
                                var keys = student.Scores.Keys.ToList();
                                foreach (var key in keys)
                                {
                                    // Giữ giá trị null nếu không hợp lệ
                                    if (!student.Scores.ContainsKey(key) || student.Scores[key] == null || !double.TryParse(student.Scores[key]?.ToString(), out double score) || score < 0 || score > 10)
                                    {
                                        student.Scores[key] = null;
                                    }
                                }
                            }
                        }

                        CourseComboBox.ItemsSource = data.Courses;
                        CourseComboBox.DisplayMemberPath = "CourseID";
                    }
                    else
                    {
                        MessageBox.Show("Failed to load data from JSON file.");
                    }
                }
                catch (JsonReaderException ex)
                {
                    MessageBox.Show($"Error reading JSON file: {ex.Message}", "JSON Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }




        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (data != null && CourseComboBox.SelectedItem is Course selectedCourse)
            {
                if (StudentDataGrid.SelectedItem is Student selectedStudent)
                {
                    CalculateGPA(selectedCourse); // Tính lại GPA nếu cần

                    if (!string.IsNullOrEmpty(selectedFilePath))
                    {
                        // Lưu dữ liệu vào tệp JSON đã chọn
                        string json = JsonConvert.SerializeObject(data, Formatting.Indented);
                        File.WriteAllText(selectedFilePath, json);
                        MessageBox.Show("Data saved to " + selectedFilePath);

                        // Cập nhật lại DataGrid để hiển thị thông tin mới nhất
                        UpdateDataGrid();
                    }
                    else
                    {
                        MessageBox.Show("No file selected. Please load a JSON file first.");
                    }
                }
            }
        }

        private void CourseComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CourseComboBox.SelectedItem is Course selectedCourse)
            {
                ScoreCheckBoxPanel.Visibility = Visibility.Visible;

                StudentDataGrid.Columns.Clear();
                ScoreCheckBoxPanel.Children.Clear();
                scoreCheckBoxes.Clear();

                StudentDataGrid.Columns.Add(new DataGridTextColumn
                {
                    Header = "Student ID",
                    Binding = new Binding("StudentID"),
                    Width = new DataGridLength(1, DataGridLengthUnitType.Star),
                    IsReadOnly = true
                });

                StudentDataGrid.Columns.Add(new DataGridTextColumn
                {
                    Header = "Name",
                    Binding = new Binding("Name"),
                    Width = new DataGridLength(2, DataGridLengthUnitType.Star),
                    IsReadOnly = true
                });

                if (selectedCourse.Students != null && selectedCourse.Students.Count > 0)
                {
                    var scoreKeys = selectedCourse.Students.First().Scores.Keys;
                    foreach (var key in scoreKeys)
                    {
                        CheckBox checkBox = new CheckBox
                        {
                            Content = key,
                            IsChecked = true,
                            Tag = key
                        };
                        checkBox.Checked += ScoreCheckBox_Checked;
                        checkBox.Unchecked += ScoreCheckBox_Unchecked;
                        ScoreCheckBoxPanel.Children.Add(checkBox);
                        scoreCheckBoxes.Add(checkBox);
                    }
                }

                CheckBox checkAllCheckBox = new CheckBox
                {
                    Content = "Choose All",
                    IsChecked = true
                };
                checkAllCheckBox.Checked += CheckAllCheckBox_Checked;
                checkAllCheckBox.Unchecked += CheckAllCheckBox_Unchecked;
                ScoreCheckBoxPanel.Children.Insert(0, checkAllCheckBox);

                StudentDataGrid.ItemsSource = selectedCourse.Students;

                UpdateDataGrid();
            }
            else
            {
                ScoreCheckBoxPanel.Visibility = Visibility.Collapsed;
            }
        }

        private void UpdateDataGrid()
        {
            if (CourseComboBox.SelectedItem is Course selectedCourse)
            {
                StudentDataGrid.Columns.Clear();

                StudentDataGrid.Columns.Add(new DataGridTextColumn
                {
                    Header = "Student ID",
                    Binding = new Binding("StudentID"),
                    Width = new DataGridLength(1, DataGridLengthUnitType.Star),
                    IsReadOnly = true
                });

                StudentDataGrid.Columns.Add(new DataGridTextColumn
                {
                    Header = "Name",
                    Binding = new Binding("Name"),
                    Width = new DataGridLength(2, DataGridLengthUnitType.Star),
                    IsReadOnly = true
                });

                if (selectedCourse.Students != null)
                {
                    foreach (var checkBox in scoreCheckBoxes)
                    {
                        if (checkBox.IsChecked == true)
                        {
                            string key = checkBox.Tag.ToString();
                            StudentDataGrid.Columns.Add(new DataGridTextColumn
                            {
                                Header = key,
                                Binding = new Binding($"Scores[{key}]")
                                {
                                    StringFormat = "{0:N2}" // Định dạng để hiển thị giá trị null một cách chính xác
                                },
                                Width = new DataGridLength(1, DataGridLengthUnitType.Star)
                            });
                        }
                    }

                    CalculateGPA(selectedCourse);
                    StudentDataGrid.Columns.Add(new DataGridTextColumn
                    {
                        Header = "GPA",
                        Binding = new Binding("GPA"),
                        Width = new DataGridLength(1, DataGridLengthUnitType.Star),
                        IsReadOnly = true
                    });

                    StudentDataGrid.ItemsSource = selectedCourse.Students;
                }
            }
        }


        private void CalculateGPA(Course course)
        {
            if (course.Students != null)
            {
                foreach (var student in course.Students)
                {
                    if (student.Scores.Count > 0)
                    {
                        student.GPA = student.Scores.Values.Average(v => v ?? 0);
                    }
                    else
                    {
                        student.GPA = 0;
                    }
                }
            }
        }

        private void ScoreCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            UpdateDataGrid();
        }

        private void ScoreCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            UpdateDataGrid();
        }

        private void CheckAllCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            foreach (var checkBox in scoreCheckBoxes)
            {
                checkBox.IsChecked = true;
            }
            UpdateDataGrid();
        }

        private void CheckAllCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            foreach (var checkBox in scoreCheckBoxes)
            {
                checkBox.IsChecked = false;
            }
            UpdateDataGrid();
        }

        private void StudentDataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (e.EditAction == DataGridEditAction.Commit)
            {
                if (e.Row.Item is Student student)
                {
                    var column = e.Column as DataGridTextColumn;
                    if (column != null)
                    {
                        var cellContent = e.EditingElement as TextBox;
                        if (cellContent != null)
                        {
                            if (double.TryParse(cellContent.Text, out double score))
                            {
                                if (score < 0 || score > 10)
                                {
                                    MessageBox.Show("Score must be greater than 0 and less than 10.", "Invalid Score", MessageBoxButton.OK, MessageBoxImage.Warning);
                                    e.Cancel = true; // Hủy thay đổi nếu điểm không hợp lệ
                                    return;
                                }

                                var bindingPath = (column.Binding as Binding)?.Path.Path;
                                if (!string.IsNullOrEmpty(bindingPath) && student.Scores.ContainsKey(bindingPath))
                                {
                                    student.Scores[bindingPath] = score;
                                }
                            }
                            else
                            {
                                // Đặt giá trị là null nếu không hợp lệ
                                var bindingPath = (column.Binding as Binding)?.Path.Path;
                                if (!string.IsNullOrEmpty(bindingPath) && student.Scores.ContainsKey(bindingPath))
                                {
                                    student.Scores[bindingPath] = null; // Giữ giá trị null nếu điểm không hợp lệ
                                }
                            }
                        }
                    }

                    CalculateGPA(CourseComboBox.SelectedItem as Course);
                    StudentDataGrid.ItemsSource = (CourseComboBox.SelectedItem as Course)?.Students;
                }
            }
        }



    }
}
