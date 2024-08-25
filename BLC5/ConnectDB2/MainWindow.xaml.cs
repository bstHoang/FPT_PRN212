using ConnectDB2.Models;
using Microsoft.EntityFrameworkCore;
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

namespace ConnectDB2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoadCustomer();
            LoadEmployee();
        }

        private void LoadOrder()
        {
            using (NortwindContext context = new NortwindContext())
            {
                var orders = context.Orders.Include(c => c.Employee).ToList();
                OrderDataGrid.ItemsSource = orders;
            }
        }


        private void OrderDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (OrderDataGrid.SelectedItem is Order selectedOrder)
            {
                OrderIdTextBox.Text = selectedOrder.OrderId.ToString();
                CustomerComboBox.SelectedValue = selectedOrder.CustomerId;
                EmployeeIdTextBox.SelectedValue = selectedOrder.EmployeeId;
                OrderDatePicker.SelectedDate = selectedOrder.OrderDate;
            }
        }

        private void LoadCustomer()
        {
            using (NortwindContext context = new NortwindContext())
            {
                var customers = context.Customers.ToList();
                CustomerComboBox.ItemsSource = customers;
                CustomerIDFilterComboBox.ItemsSource = customers;
            }
        }
        private void LoadEmployee()
        {
            using (NortwindContext context = new NortwindContext())
            {
                var employees = context.Employees.ToList();
                EmployeeIdTextBox.ItemsSource = employees;
                EmpIDFilterComboBox.ItemsSource = employees;
            }
        }

        private void FilterButton_Click(object sender, RoutedEventArgs e)
        {
            using (NortwindContext context = new NortwindContext())
            {
                var ordersQuery = context.Orders.AsQueryable();

                // Filter by Customer ID
                if (CustomerIDFilterComboBox.SelectedValue != null)
                {
                    string selectedCustomerId = CustomerIDFilterComboBox.SelectedValue.ToString();
                    ordersQuery = ordersQuery.Where(o => o.CustomerId == selectedCustomerId);
                }

                // Filter by Employee ID
                if (EmpIDFilterComboBox.SelectedValue != null)
                {
                    int selectedEmployeeId = (EmpIDFilterComboBox.SelectedValue as Employee)?.EmployeeId ?? 0;
                    ordersQuery = ordersQuery.Where(o => o.EmployeeId == selectedEmployeeId);
                }

                // Filter by Order Date (From and To)
                if (FromTextBox.SelectedDate != null || ToTextBox.SelectedDate != null)
                {
                    DateTime? startDate = FromTextBox.SelectedDate;
                    DateTime? endDate = ToTextBox.SelectedDate;

                    if (startDate != null && endDate == null)
                    {
                        ordersQuery = ordersQuery.Where(o => o.OrderDate >= startDate);
                    }
                    else if (startDate == null && endDate != null)
                    {
                        ordersQuery = ordersQuery.Where(o => o.OrderDate <= endDate);
                    }
                    else if (startDate != null && endDate != null)
                    {
                        ordersQuery = ordersQuery.Where(o => o.OrderDate >= startDate && o.OrderDate <= endDate);
                    }
                }

                // Apply the filtered result to the DataGrid
                OrderDataGrid.ItemsSource = ordersQuery.ToList();
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (OrderDataGrid.SelectedItem is Order selectedOrder)
            {
                using (NortwindContext context = new NortwindContext())
                {
                    var orderToUpdate = context.Orders.FirstOrDefault(o => o.OrderId == selectedOrder.OrderId);

                    if (orderToUpdate != null)
                    {
                        // Cập nhật các thuộc tính của đơn hàng từ form
                        orderToUpdate.CustomerId = (CustomerComboBox.SelectedItem as Customer)?.CustomerId;
                        orderToUpdate.EmployeeId = (EmployeeIdTextBox.SelectedItem as Employee)?.EmployeeId ?? 0;
                        orderToUpdate.OrderDate = OrderDatePicker.SelectedDate ?? orderToUpdate.OrderDate;

                        // Lưu thay đổi
                        context.SaveChanges();

                        // Tải lại DataGrid sau khi chỉnh sửa
                        LoadOrder();
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select an order to edit.");
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (OrderDataGrid.SelectedItem is Order selectedOrder)
            {
                using (NortwindContext context = new NortwindContext())
                {
                    var orderToDelete = context.Orders
                        .Include(o => o.OrderDetails) // Include liên kết OrderDetails để có thể xóa chúng
                        .FirstOrDefault(o => o.OrderId == selectedOrder.OrderId);

                    if (orderToDelete != null)
                    {
                        // Xóa các chi tiết đơn hàng liên quan
                        context.OrderDetails.RemoveRange(orderToDelete.OrderDetails);

                        // Xóa đơn hàng
                        context.Orders.Remove(orderToDelete);

                        // Lưu thay đổi
                        context.SaveChanges();

                        // Tải lại DataGrid sau khi xóa
                        LoadOrder();
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select an order to delete.");
            }
        }


        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            LoadOrder();
        }

        private void OrderDetailButton_Click(object sender, RoutedEventArgs e)
        {
            if (OrderDataGrid.SelectedItem is Order selectedOrder)
            {
                int selectedOrderId = selectedOrder.OrderId;
                OrderDetailWindow orderDetailWindow = new OrderDetailWindow(selectedOrderId);
                orderDetailWindow.Show();
                this.Close(); // Đóng MainWindow nếu cần thiết
            }
            else
            {
                MessageBox.Show("Please select an order to view its details.");
            }
        }

    }
}