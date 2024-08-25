using ConnectDB2.Models;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace ConnectDB2
{
    public partial class OrderDetailWindow : Window
    {
        private NortwindContext _context;
        private int _selectedOrderId;

        public OrderDetailWindow(int orderId)
        {
            InitializeComponent();
            _context = new NortwindContext();
            _selectedOrderId = orderId;
            LoadProductDetails();
        }

        private void LoadProductDetails()
        {
            var productDetails = _context.OrderDetails
                .Where(od => od.OrderId == _selectedOrderId)
                .Select(od => new
                {
                    OrderDetailId = od.OrderId, // Assuming OrderDetailId is the primary key
                    ProductId = od.ProductId,
                    Quantity = od.Quantity,
                    UnitPrice = od.UnitPrice,
                })
                .ToList();

            ProductDetailsListBox.ItemsSource = productDetails;
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var item = button?.DataContext as dynamic;

            if (item != null)
            {
                int orderDetailId = item.OrderDetailId;

                var orderDetailToRemove = _context.OrderDetails
                    .FirstOrDefault(od => od.OrderId == orderDetailId);

                if (orderDetailToRemove != null)
                {
                    _context.OrderDetails.Remove(orderDetailToRemove);
                    _context.SaveChanges();
                    LoadProductDetails();
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }
    }
}
