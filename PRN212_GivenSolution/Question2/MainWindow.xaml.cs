using Question2.Models;
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

namespace Question2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoadProject();
        }

        private void ProjectDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ProjectDataGrid.SelectedItem is Project selectedProject)
            {
                ID.Text = selectedProject.Id.ToString();
                Name.Text = selectedProject.Name;
                Description.Text = selectedProject.Description;
                //StartDatePicker.= selectedProject.OrderDate;
                ProjectTypeComboBox.SelectedValue = selectedProject.Type;
            }
        }

        private void LoadProject()
        {
            using (PePrn21224sumB5Context context = new PePrn21224sumB5Context())
            {
                var project = context.Projects.ToList();
                ProjectDataGrid.ItemsSource = project;
                ProjectTypeComboBox.ItemsSource = project;
            }
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            ID.Clear();
            Name.Clear();
            Description.Clear();
            
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new PePrn21224sumB5Context())
            {
                var newProject = new Project
                {
                    Name = Name.Text,
                    Description = Description.Text
                    
                };

                if (ProjectTypeComboBox.SelectedValue is string  valueType)
                {
                    newProject.Type = valueType;
                }

                context.Projects.Add(newProject);
                context.SaveChanges();
            }

            LoadProject(); 

        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}