using Microsoft.EntityFrameworkCore;
using PE_SU24_Q2_MyAnswer.Models;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PE_SU24_Q2_MyAnswer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoadUser();
            LoadInstructors();
        }

        private void LoadUser()
        {
            using (PePrn24sumB1Context context = new PePrn24sumB1Context())
            {
                var users = context.Users.ToList();
                UserComboBox.ItemsSource = users;
            }
        }
        private void UserComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (UserComboBox.SelectedItem is User selectedUser)
            {
                LoadCoursesForUser(selectedUser.UserId);
            }
        }

        private void LoadCoursesForUser(int userId)
        {
            using (var context = new PePrn24sumB1Context())
            {
                var courses = context.Enrollments
                                     .Where(enrollment => enrollment.UserId == userId)
                                     .Include(enrollment => enrollment.Course.Instructor) 
                                     .Select(enrollment => enrollment.Course)
                                     .ToList();

                CourseDataGrid.ItemsSource = courses;
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
                LoadCoursesForUser((int)UserComboBox.SelectedValue); // Refresh DataGrid
            }
        }

    }
}