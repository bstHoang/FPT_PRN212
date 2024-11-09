using Question2.Models;
using System.Windows;
using System.Windows.Controls;


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
            LoadProjects();
            LoadComboBoxType();
        }

        // Nạp danh sách các dự án từ database lên DataGrid
        private void LoadProjects()
        {
            using (var context = new PePrn21224sumB5Context())
            {
                var projectList = context.Projects.ToList();
                dataGridProjects.ItemsSource = projectList;
            }
        }
        private void LoadComboBoxType()
        {
            using (var context = new PePrn21224sumB5Context())
            {
                // Lấy danh sách các dự án và chọn chỉ những giá trị Type duy nhất
                var distinctTypes = context.Projects
                                           .Select(p => p.Type)
                                           .Distinct()
                                           .ToList();

                comboBoxType.ItemsSource = distinctTypes;
            }
        }

        // Xử lý khi chọn một dự án trong DataGrid
        private void dataGridProjects_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dataGridProjects.SelectedItem is Project selectedProject)
            {
                txtID.Text = selectedProject.Id.ToString();
                txtName.Text = selectedProject.Name;
                txtDescription.Text = selectedProject.Description;
                datePickerStartDate.SelectedDate = selectedProject.StartDate?.ToDateTime(TimeOnly.MinValue); // Chuyển từ DateOnly sang DateTime
                comboBoxType.Text = selectedProject.Type; // Cập nhật loại dự án
            }
        }

        // Xử lý khi nhấn nút "Refresh"
        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            txtID.Clear();
            txtName.Clear();
            txtDescription.Clear();
            datePickerStartDate.SelectedDate = null; 
            comboBoxType.SelectedItem = null;
        }

        // Xử lý khi nhấn nút "Add" để thêm dự án mới
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text) 
                || string.IsNullOrWhiteSpace(txtDescription.Text) 
                || datePickerStartDate.SelectedDate == null 
                || string.IsNullOrWhiteSpace(comboBoxType.Text))
            {
                MessageBox.Show("Please fill in all fields.");
                return;
            }
            using (var context = new PePrn21224sumB5Context())
            {
                var newProject = new Project
                {
                    Name = txtName.Text,
                    Description = txtDescription.Text,
                    StartDate = DateOnly.FromDateTime(datePickerStartDate.SelectedDate.Value), // Chuyển từ DateTime sang DateOnly
                    Type = comboBoxType.Text // Lấy loại dự án từ ComboBox
                };
                context.Projects.Add(newProject);
                context.SaveChanges();
                LoadProjects(); // Cập nhật lại DataGrid
            }
        }

        // Xử lý khi nhấn nút "Edit" để chỉnh sửa dự án hiện tại
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text)
                || string.IsNullOrWhiteSpace(txtDescription.Text)
                || datePickerStartDate.SelectedDate == null
                || string.IsNullOrWhiteSpace(comboBoxType.Text))
            {
                MessageBox.Show("Please fill in all fields.");
                return;
            }
            using (var context = new PePrn21224sumB5Context())
            {
                var projectId = int.Parse(txtID.Text);
                var project = context.Projects.FirstOrDefault(p => p.Id == projectId);
                if (project != null)
                {
                    project.Name = txtName.Text;
                    project.Description = txtDescription.Text;
                    project.StartDate = DateOnly.FromDateTime(datePickerStartDate.SelectedDate.Value); // Chuyển từ DateTime sang DateOnly
                    project.Type = comboBoxType.Text; // Cập nhật loại dự án

                    context.SaveChanges();
                    LoadProjects(); // Cập nhật lại DataGrid
                }
            }
        }
    }
}