using RockLife.Models;
using RockLife.Repositories;
using RockLife.Repository;
using RockLife.Windows;
using System;
using System.Collections.Generic;
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

namespace RockLife.Pages
{
    /// <summary>
    /// Interaction logic for PageTwo.xaml
    /// </summary>
    public partial class PageTwo : Page
    {
        private ProductRepository _productRepository;
        private CartItemRepository _cartItemRepository;
        public PageTwo()
        {
            InitializeComponent();

            _productRepository = new ProductRepository(new MyAppContext());
            _cartItemRepository = new CartItemRepository(new MyAppContext());

            int productId = 2; 
            var product = _productRepository.GetProductByIdAsync(productId).Result; 

            if (product != null)
            {
                Product1.ItemsSource = new List<Product> { product };
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PageThree());
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PageOne());
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

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var userIdNullable = Application.Current.Properties["UserId"] as int?;
            int userId = userIdNullable.Value;
            CartItem newItem = new CartItem
            {
                UserId = userId,
                ProductId = 2,
                Quantity = 1
            };

            _cartItemRepository.AddCartItem(newItem);
        }
    }
}
