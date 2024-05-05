using RockLife.Interfaces;
using RockLife.Models;
using RockLife.Repositories;
using RockLife.Repository;
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
using System.Windows.Shapes;

namespace RockLife.Windows
{
    /// <summary>
    /// Interaction logic for CartItemWindow.xaml
    /// </summary>
    public partial class CartItemWindow : Window
    {
        private CartItemRepository _repository;
        private OrderRepository _orderRepository;

        public CartItemWindow()
        {
            _repository = new CartItemRepository(new MyAppContext());
            _orderRepository = new OrderRepository(new MyAppContext());
            InitializeComponent();
            var userIdNullable = Application.Current.Properties["UserId"] as int?;
            int userId = userIdNullable.Value;

            LoadCartItemsAsync(userId);
        }

        private async void LoadCartItemsAsync(int userId)
        {
            try
            {
                var quantities = await _repository.GetCartItemQuantitiesByUserIdAsync(userId);

                var products = await _repository.GetProductDetailsByUserIdAsync(userId);

                List<CartProductDetails> cartItemsInfo = products.Select((product, index) => new CartProductDetails
                {
                    Product = product,
                    Quantity = quantities[index].Quantity
                }).ToList();

                CartItem.ItemsSource = cartItemsInfo;
            }
            catch (Exception e)
            {
                MessageBox.Show($"Не удалось загрузить данные: {e.Message}");
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            SecondMainWindow secondMainWindow = new SecondMainWindow();
            secondMainWindow.Show();
            this.Hide();
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            AccountWindow accountWindow = new AccountWindow();
            accountWindow.Show();
            this.Hide();
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            CatalogWindow catalogWindow = new CatalogWindow();
            catalogWindow.Show();
            this.Hide();
        }

        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow();
            window.Show();
            this.Hide();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                var userIdNullable = Application.Current.Properties["UserId"] as int?;
                int userId = userIdNullable.Value;
                double totalPrice = await _repository.CalculateTotalCartValueAsync(userId);
                await _orderRepository.AddOrderAsync(userId, totalPrice);
                CartItem.ItemsSource = null;
                MessageBox.Show("Заказ успешно оформлен!");

                await _repository.RemoveCartItemsByUserIdAsync(userId);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка при оформлении заказа: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var userIdNullable = Application.Current.Properties["UserId"] as int?;
            int userId = userIdNullable.Value;
            await _repository.RemoveCartItemsByUserIdAsync(userId);
            CartItem.ItemsSource = null;
            MessageBox.Show("Корзина очищена");
        }
    }
}
