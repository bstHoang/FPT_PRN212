using System;
using System.Linq;
using System.Windows;
using ConnectDB.Models;
using Microsoft.EntityFrameworkCore;

namespace ConnectDB
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoadCategories();
            LoadProducts();
        }

        private void LoadCategories()
        {
            using (NortwindContext context = new NortwindContext())
            {
                var categories = context.Categories.ToList();
                CategoryComboBox.ItemsSource = categories;
            }
        }
        private void LoadProducts(int? categoryId = null)
        {
            using (NortwindContext context = new NortwindContext())
            {
                var productsQuery = context.Products
                                           .Include(p => p.Supplier)
                                           .Include(p => p.Category)
                                           .AsQueryable();

                if (categoryId.HasValue)
                {
                    productsQuery = productsQuery.Where(p => p.CategoryId == categoryId.Value);
                }

                var products = productsQuery.ToList();
                ProductDataGrid.ItemsSource = products;
            }
        }

        private void CategoryComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (CategoryComboBox.SelectedItem is Category selectedCategory)
            {
                LoadProducts(selectedCategory.CategoryId);
            }
            else
            {
                LoadProducts();
            }
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            using (NortwindContext context = new NortwindContext())
            {
                var product = new Product
                {
                    ProductName = ProductNameTextBox.Text,
                    SupplierId = int.TryParse(SupplierIdTextBox.Text, out int supplierId) ? supplierId : (int?)null,
                    CategoryId = int.TryParse(CategoryIdTextBox.Text, out int categoryId) ? categoryId : (int?)null,
                    QuantityPerUnit = QuantityPerUnitTextBox.Text,
                    UnitPrice = decimal.TryParse(UnitPriceTextBox.Text, out decimal unitPrice) ? unitPrice : (decimal?)null,
                    UnitsInStock = short.TryParse(UnitsInStockTextBox.Text, out short unitsInStock) ? unitsInStock : (short?)null,
                    UnitsOnOrder = short.TryParse(UnitsOnOrderTextBox.Text, out short unitsOnOrder) ? unitsOnOrder : (short?)null,
                    ReorderLevel = short.TryParse(ReorderLevelTextBox.Text, out short reorderLevel) ? reorderLevel : (short?)null,
                    Discontinued = DiscontinuedCheckBox.IsChecked ?? false
                };

                context.Products.Add(product);
                context.SaveChanges();
            }
            LoadProducts();
            ClearFields();
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(ProductIdTextBox.Text, out int productId))
            {
                using (NortwindContext context = new NortwindContext())
                {
                    var product = context.Products.Find(productId);
                    if (product != null)
                    {
                        product.ProductName = ProductNameTextBox.Text;
                        product.SupplierId = int.TryParse(SupplierIdTextBox.Text, out int supplierId) ? supplierId : (int?)null;
                        product.CategoryId = int.TryParse(CategoryIdTextBox.Text, out int categoryId) ? categoryId : (int?)null;
                        product.QuantityPerUnit = QuantityPerUnitTextBox.Text;
                        product.UnitPrice = decimal.TryParse(UnitPriceTextBox.Text, out decimal unitPrice) ? unitPrice : (decimal?)null;
                        product.UnitsInStock = short.TryParse(UnitsInStockTextBox.Text, out short unitsInStock) ? unitsInStock : (short?)null;
                        product.UnitsOnOrder = short.TryParse(UnitsOnOrderTextBox.Text, out short unitsOnOrder) ? unitsOnOrder : (short?)null;
                        product.ReorderLevel = short.TryParse(ReorderLevelTextBox.Text, out short reorderLevel) ? reorderLevel : (short?)null;
                        product.Discontinued = DiscontinuedCheckBox.IsChecked ?? false;

                        context.SaveChanges();
                    }
                }
                LoadProducts();
                ClearFields();
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(ProductIdTextBox.Text, out int productId))
            {
                using (NortwindContext context = new NortwindContext())
                {
                    var product = context.Products.Find(productId);
                    if (product != null)
                    {
                        context.Products.Remove(product);
                        context.SaveChanges();
                    }
                }
                LoadProducts();
                ClearFields();
            }
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            ClearFields();
        }

        private void ClearFields()
        {
            ProductIdTextBox.Clear();
            ProductNameTextBox.Clear();
            SupplierIdTextBox.Clear();
            CategoryIdTextBox.Clear();
            QuantityPerUnitTextBox.Clear();
            UnitPriceTextBox.Clear();
            UnitsInStockTextBox.Clear();
            UnitsOnOrderTextBox.Clear();
            ReorderLevelTextBox.Clear();
            DiscontinuedCheckBox.IsChecked = false;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ProductDataGrid_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (ProductDataGrid.SelectedItem is Product selectedProduct)
            {
                ProductIdTextBox.Text = selectedProduct.ProductId.ToString();
                ProductNameTextBox.Text = selectedProduct.ProductName;
                SupplierIdTextBox.Text = selectedProduct.SupplierId?.ToString();
                CategoryIdTextBox.Text = selectedProduct.CategoryId?.ToString();
                QuantityPerUnitTextBox.Text = selectedProduct.QuantityPerUnit;
                UnitPriceTextBox.Text = selectedProduct.UnitPrice?.ToString();
                UnitsInStockTextBox.Text = selectedProduct.UnitsInStock?.ToString();
                UnitsOnOrderTextBox.Text = selectedProduct.UnitsOnOrder?.ToString();
                ReorderLevelTextBox.Text = selectedProduct.ReorderLevel?.ToString();
                DiscontinuedCheckBox.IsChecked = selectedProduct.Discontinued;
            }
        }

    }
}