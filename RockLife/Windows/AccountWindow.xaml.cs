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
using System.Windows.Shapes;
using RockLife.Repository;
using RockLife.Models;

namespace RockLife
{
    /// <summary>
    /// Interaction logic for AccountWindow.xaml
    /// </summary>
    public partial class AccountWindow : Window
    {
        private UserRepository _userRepository;
        private OrderRepository _orderRepository;
        public AccountWindow()
        {
            InitializeComponent();

            InitializeUserData();
        }
        private async void InitializeUserData()
        {
            try
            {
                _userRepository = new UserRepository(new MyAppContext());
                _orderRepository = new OrderRepository(new MyAppContext());

                var userIdNullable = Application.Current.Properties["UserId"] as int?;
                if (!userIdNullable.HasValue)
                {
                    return;
                }

                int userId = userIdNullable.Value;

                var user = await _userRepository.GetUserByIdAsync(userId);
                if (user == null)
                {
                    return;
                }

                var orderQuantity = await _orderRepository.GetTotalOrdersByUserIdAsync(userId);

                var userOrderInfo = new UserOrderInfo()
                {
                    User = user,
                    TotalOrders = orderQuantity
                };

                Account.ItemsSource = new List<UserOrderInfo> { userOrderInfo };
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            SecondMainWindow mainWindow = new SecondMainWindow();
            mainWindow.Show();
            this.Hide();
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            CatalogWindow catalogWindow = new CatalogWindow();  
            catalogWindow.Show();
            this.Hide();
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            CartItemWindow catalogWindow = new CartItemWindow();
            catalogWindow.Show();
            this.Hide();
        }

        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Hide();
        }
    }
}
