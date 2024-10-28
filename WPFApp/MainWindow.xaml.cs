using BusinessObjects;
using Repositories;
using System.Windows;
using System.Windows.Controls;

namespace WPFApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IProductRepository productRepository;
        private readonly ICategoryRepository categoryRepository;

        public MainWindow()
        {
            InitializeComponent();
            productRepository = new ProductRepository();
            categoryRepository = new CategoryRepository();
        }

        public void LoadCategoryList()
        {
            try
            {
                var list = categoryRepository.GetCategories();
                if (list != null && list.Any())
                {
                    cboCategory.ItemsSource = list;
                    cboCategory.DisplayMemberPath = "CategoryName";
                    cboCategory.SelectedValuePath = "CategoryID"; // Ensure this is correctly set
                    cboCategory.SelectedIndex = -1; // Clear any selected item
                }
                else
                {
                    MessageBox.Show("No categories available.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading categories: {ex.Message}");
            }
        }

        public void LoadProductList()
        {
            var list = productRepository.GetProducts();
            dgData.ItemsSource = null;
            dgData.ItemsSource = list;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadCategoryList();
            LoadProductList();
        }

        private void dgData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgData.SelectedItem is Product selectedProduuct)
            {
                txtProductID.Text = selectedProduuct.ProductID.ToString();
                txtProductName.Text = selectedProduuct.ProductName;
                txtUnitsInStock.Text = selectedProduuct.UnitsInStock.ToString();
                txtPrice.Text = selectedProduuct.UnitPrice.ToString();
                cboCategory.SelectedValue = selectedProduuct.CategoryID;
            }
            //DataGrid dataGrid = sender as DataGrid;
            //DataGridRow row = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromIndex(dgData.SelectedIndex);
            //DataGridCell column = dataGrid.Columns[0].GetCellContent(row).Parent as DataGridCell;
            //string id = ((TextBlock)column.Content).Text;
            //Product product = productRepository.GetProductById(Int32.Parse(id));
            //if(product != null)
            //{
            //    txtProductID.Text = product.ProductId.ToString();
            //    txtProductName.Text = product.ProductName;
            //    txtUnitsInStock.Text = product.UnitsInStock.ToString();
            //    txtPrice.Text = product.UnitPrice.ToString();
            //    cboCategory.SelectedValue = product.CategoryId;
            //}
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Product p = new Product()
                {
                    ProductName = txtProductName.Text,
                    UnitsInStock = short.Parse(txtUnitsInStock.Text),
                    UnitPrice = Decimal.Parse(txtPrice.Text),
                    CategoryID = Int32.Parse(cboCategory.SelectedValue.ToString()),
                };
                productRepository.SaveProduct(p);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                LoadProductList();
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtProductID.Text))
                {
                    Product p = productRepository.GetProductById(Int32.Parse(txtProductID.Text));
                    if (p != null)
                    {
                        p.ProductID = Int32.Parse(txtProductID.Text);
                        p.ProductName = txtProductName.Text;
                        p.UnitsInStock = short.Parse(txtUnitsInStock.Text);
                        p.UnitPrice = Decimal.Parse(txtPrice.Text);
                        p.CategoryID = Int32.Parse(cboCategory.SelectedValue.ToString());
                        productRepository.UpdateProduct(p);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("You must selected!");
            }
            finally { LoadProductList(); }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtProductID.Text))
                {
                    Product p = productRepository.GetProductById(Int32.Parse(txtProductID.Text));
                    productRepository.DeleteProduct(p);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("You must selected!");
            }
            finally { LoadProductList(); }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            //this.Close();
            Application.Current.Shutdown();
        }
    }
}