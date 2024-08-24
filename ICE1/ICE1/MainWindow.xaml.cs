using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace ICE1
{
    public partial class MainWindow : Window
    {
        private readonly Dictionary<string, Product> _products = new Dictionary<string, Product>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            var productID = txtAddProductID.Text;
            var name = txtAddName.Text;
            var category = txtAddCategory.Text;
            var price = decimal.Parse(txtAddPrice.Text);
            var stockQuantity = int.Parse(txtAddStockQuantity.Text);

            if (!_products.ContainsKey(productID))
            {
                var product = new Product
                {
                    ProductID = productID,
                    Name = name,
                    Category = category,
                    Price = price,
                    InitialStockQuantity = stockQuantity, // Set initial stock
                    StockQuantity = stockQuantity, // Set initial stock
                    ItemsSold = 0 // Initialize items sold
                };

                _products[productID] = product;
                MessageBox.Show("Product added successfully.");
            }
            else
            {
                MessageBox.Show("Product with this ID already exists.");
            }
        }

        private void UpdateProduct_Click(object sender, RoutedEventArgs e)
        {
            var productID = txtEditProductID.Text;
            if (_products.ContainsKey(productID))
            {
                var product = _products[productID];
                product.Name = txtEditName.Text;
                product.Category = txtEditCategory.Text;
                product.Price = decimal.Parse(txtEditPrice.Text);
                product.StockQuantity = int.Parse(txtEditStockQuantity.Text);

                MessageBox.Show("Product updated successfully.");
            }
            else
            {
                MessageBox.Show("Product with this ID does not exist.");
            }
        }

        private void ProcessSale_Click(object sender, RoutedEventArgs e)
        {
            var productID = txtProcessSaleProductID.Text;
            var quantity = int.Parse(txtProcessSaleQuantity.Text);

            if (_products.ContainsKey(productID))
            {
                var product = _products[productID];

                if (product.StockQuantity >= quantity)
                {
                    product.StockQuantity -= quantity;
                    product.ItemsSold += quantity; // Track sold items

                    MessageBox.Show("Sale processed successfully.");
                }
                else
                {
                    MessageBox.Show("Insufficient stock.");
                }
            }
            else
            {
                MessageBox.Show("Product with this ID does not exist.");
            }
        }

        private void ViewStock_Click(object sender, RoutedEventArgs e)
        {
            var productID = txtViewProductID.Text;
            if (_products.ContainsKey(productID))
            {
                var product = _products[productID];
                txtStockDetails.Text = $"Name: {product.Name}\n" +
                                       $"Category: {product.Category}\n" +
                                       $"Price: R{product.Price:F2}\n" +
                                       $"Initial Stock Quantity: {product.InitialStockQuantity}\n" +
                                       $"Current Stock Quantity: {product.StockQuantity}\n" +
                                       $"Items Sold: {product.ItemsSold}";
            }
            else
            {
                MessageBox.Show("Product with this ID does not exist.");
            }
        }

        private void GenerateSummaryReport_Click(object sender, RoutedEventArgs e)
        {
            GenerateSummaryReport();
        }

        private void GenerateSummaryReport()
        {
            var report = _products.Values.Select(p => new
            {
                p.Name,
                p.Category,
                p.InitialStockQuantity,
                p.StockQuantity,
                p.ItemsSold,
                TotalStockCost = p.Price * p.StockQuantity // Calculate total cost for each item
            });

            txtReport.Text = "Inventory Summary:\n";
            foreach (var item in report)
            {
                txtReport.Text += $"Name: {item.Name}, Category: {item.Category}, " +
                                  $"Initial Stock: {item.InitialStockQuantity}, " +
                                  $"Current Stock: {item.StockQuantity}, " +
                                  $"Items Sold: {item.ItemsSold}, " +
                                  $"Total Stock Cost: R{item.TotalStockCost:F2}\n";
            }
        }
    }

    public class Product
    {
        public string ProductID { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
        public int InitialStockQuantity { get; set; }
        public int StockQuantity { get; set; }
        public int ItemsSold { get; set; }
    }
}
