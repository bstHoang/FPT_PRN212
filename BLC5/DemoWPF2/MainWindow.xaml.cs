using DemoWPF2.Model;
using Microsoft.Win32;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace DemoWPF2
{
    public partial class MainWindow : Window
    {
        private ObservableCollection<Product> products = new ObservableCollection<Product>();
        private ObservableCollection<string> categories = new ObservableCollection<string>();
        private string jsonFilePath;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
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
                products.Clear();
                categories.Clear();
                string jsonData = File.ReadAllText(jsonFilePath);
                var productList = JsonConvert.DeserializeObject<ObservableCollection<Product>>(jsonData);

                if (productList != null)
                {
                    foreach (var product in productList)
                    {
                        products.Add(product);
                        if (!categories.Contains(product.Category))
                        {
                            categories.Add(product.Category);
                        }
                    }
                }

                ProductListBox.ItemsSource = products;
                CategoryFilterComboBox.ItemsSource = categories;
                MessageBox.Show($"Số lượng sản phẩm đã tải: {products.Count}");
            }
            else
            {
                MessageBox.Show("Vui lòng chọn file JSON hợp lệ!");
            }
        }

        private void ProductListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ProductListBox.SelectedItem is Product selectedProduct)
            {
                IdTextBox.Text = selectedProduct.Id.ToString();
                NameTextBox.Text = selectedProduct.Name;
                CategoryTextBox.Text = selectedProduct.Category;
                PriceTextBox.Text = selectedProduct.Price.ToString();
                IsActiveTextBox.Text = selectedProduct.IsActive.ToString();
            }
        }

        private void FilterButton_Click(object sender, RoutedEventArgs e)
        {
            string selectedCategory = CategoryFilterComboBox.SelectedItem as string;
            double? priceFrom = null;
            double? priceTo = null;

            // Validate and parse price filters
            if (double.TryParse(PriceFromTextBox.Text, out double parsedPriceFrom))
            {
                priceFrom = parsedPriceFrom;
            }
            if (double.TryParse(PriceToTextBox.Text, out double parsedPriceTo))
            {
                priceTo = parsedPriceTo;
            }

            // Filter products
            var filteredProducts = products.Where(p =>
                (string.IsNullOrEmpty(selectedCategory) || p.Category == selectedCategory) &&
                (!priceFrom.HasValue || p.Price >= priceFrom.Value) &&
                (!priceTo.HasValue || p.Price <= priceTo.Value)).ToList();

            ProductListBox.ItemsSource = filteredProducts;
        }

        private void Button_Insert(object sender, RoutedEventArgs e)
        {
            var newProduct = new Product
            {
                Id = products.Count > 0 ? products[^1].Id + 1 : 1,
                Name = NameTextBox.Text,
                Category = CategoryTextBox.Text,
                Price = double.TryParse(PriceTextBox.Text, out double parsedPrice) ? parsedPrice : 0,
                IsActive = bool.TryParse(IsActiveTextBox.Text, out bool parsedIsActive) ? parsedIsActive : false
            };

            products.Add(newProduct);
            ProductListBox.ItemsSource = products;
        }

        private void Button_Update(object sender, RoutedEventArgs e)
        {
            if (ProductListBox.SelectedItem is Product selectedProduct)
            {
                selectedProduct.Name = NameTextBox.Text;
                selectedProduct.Category = CategoryTextBox.Text;
                selectedProduct.Price = double.TryParse(PriceTextBox.Text, out double parsedPrice) ? parsedPrice : 0;
                selectedProduct.IsActive = bool.TryParse(IsActiveTextBox.Text, out bool parsedIsActive) ? parsedIsActive : false;

                ProductListBox.ItemsSource = null;
                ProductListBox.ItemsSource = products;
            }
        }

        private void Button_Delete(object sender, RoutedEventArgs e)
        {
            if (ProductListBox.SelectedItem is Product selectedProduct)
            {
                products.Remove(selectedProduct);
                ProductListBox.ItemsSource = null;
                ProductListBox.ItemsSource = products;
            }
        }

        private void Button_Clear(object sender, RoutedEventArgs e)
        {
            IdTextBox.Clear();
            NameTextBox.Clear();
            CategoryTextBox.Clear();
            PriceTextBox.Clear();
            IsActiveTextBox.Clear();
        }

        private void Button_Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
