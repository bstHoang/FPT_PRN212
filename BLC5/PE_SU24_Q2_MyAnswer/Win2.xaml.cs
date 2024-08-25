using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Microsoft.EntityFrameworkCore;
using PE_SU24_Q2_MyAnswer.Models;

namespace PE_SU24_Q2_MyAnswer
{
    public partial class Win2 : Window
    {
        public Win2()
        {
            InitializeComponent();
            LoadCourses();
            LoadInstructors();
        }

        private void LoadCourses()
        {
            using (var context = new PePrn24sumB1Context())
            {
                CourseDataGrid.ItemsSource = context.Courses.Include(c => c.Instructor).ToList();
            }
        }

        private void LoadInstructors()
        {
            using (var context = new PePrn24sumB1Context())
            {
                InstructorComboBox.ItemsSource = context.Instructors.ToList();
            }
        }

        private void CourseDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CourseDataGrid.SelectedItem is Course selectedCourse)
            {
                CourseIdTextBlock.Text = selectedCourse.CourseId.ToString();
                TitleTextBox.Text = selectedCourse.Title;
                DescriptionTextBox.Text = selectedCourse.Description;
                InstructorComboBox.SelectedValue = selectedCourse.Instructor?.InstructorId;
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new PePrn24sumB1Context())
            {
                var newCourse = new Course
                {
                    Title = TitleTextBox.Text,
                    Description = DescriptionTextBox.Text
                };

                if (InstructorComboBox.SelectedValue is int instructorId)
                {
                    var instructor = context.Instructors.Find(instructorId);
                    if (instructor != null)
                    {
                        newCourse.InstructorId = instructorId;
                        newCourse.Instructor = instructor;
                    }
                }

                context.Courses.Add(newCourse);
                context.SaveChanges();
            }

            LoadCourses(); // Refresh DataGrid
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (CourseDataGrid.SelectedItem is Course selectedCourse)
            {
                using (var context = new PePrn24sumB1Context())
                {
                    var courseToUpdate = context.Courses.Include(c => c.Instructor)
                                                        .FirstOrDefault(c => c.CourseId == selectedCourse.CourseId);

                    if (courseToUpdate != null)
                    {
                        courseToUpdate.Title = TitleTextBox.Text;
                        courseToUpdate.Description = DescriptionTextBox.Text;

                        if (InstructorComboBox.SelectedValue is int instructorId)
                        {
                            var instructor = context.Instructors.Find(instructorId);
                            if (instructor != null)
                            {
                                courseToUpdate.InstructorId = instructorId;
                                courseToUpdate.Instructor = instructor;
                            }
                        }

                        context.SaveChanges();
                    }
                }

                LoadCourses(); // Refresh DataGrid
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (CourseDataGrid.SelectedItem is Course selectedCourse)
            {
                using (var context = new PePrn24sumB1Context())
                {
                    var courseToDelete = context.Courses.FirstOrDefault(c => c.CourseId == selectedCourse.CourseId);
                    if (courseToDelete != null)
                    {
                        context.Courses.Remove(courseToDelete);
                        context.SaveChanges();
                    }
                }

                LoadCourses(); // Refresh DataGrid
            }
        }
    }
}
