using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using RockLife.Models;
using RockLife.Repository;
using RockLife.Windows;

namespace RockLife.Pages
{
    /// <summary>
    /// Interaction logic for RageOne.xaml
    /// </summary>
    public partial class PageOne : Page
    {
        private ProductRepository _productRepository;
        private CartItemRepository _cartItemRepository;
        public ObservableCollection<Product> Products { get; set; } = new ObservableCollection<Product>();
        public PageOne()
        {
            InitializeComponent();

            _productRepository = new ProductRepository(new MyAppContext());
            _cartItemRepository = new CartItemRepository(new MyAppContext());

            int productId = 1; 
            var product = _productRepository.GetProductByIdAsync(productId).Result;

            if (product != null)
            {
                Product1.ItemsSource = new List<Product> { product };
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            SecondMainWindow secondMainWindow = new SecondMainWindow();
            secondMainWindow.Show();
            var window = Window.GetWindow(this);
            window?.Hide();
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            AccountWindow accountWindow = new AccountWindow();
            accountWindow.Show();
            var window = Window.GetWindow(this);
            window?.Hide();
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            CartItemWindow cartItemWindow = new CartItemWindow();
            cartItemWindow.Show();
            var window = Window.GetWindow(this);
            window?.Hide();
        }

        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            var window = Window.GetWindow(this);
            window?.Hide();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PageTwo());
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            


            try
            {
                var userIdNullable = Application.Current.Properties["UserId"] as int?;
                int userId = userIdNullable.Value;
                CartItem newItem = new CartItem
                {
                    UserId = userId,
                    ProductId = 1,
                    Quantity = 1
                };
                await _cartItemRepository.AddCartItem(newItem);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }
    }
}
