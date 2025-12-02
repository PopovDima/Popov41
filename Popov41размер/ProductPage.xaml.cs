using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
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

namespace Popov41размер
{
    public partial class ProductPage : Page
    {
        int CurrentNumRecords, AllNumRecords;

        double[,] discountRanges = new double[4, 2]
        {
            {0, 100},
            {0, 10},
            {10, 15},
            {15, 100}
        };

        List<(double, double)> discountRangesList = new List<(double, double)>
        {
            (0, 100),
            (0, 9.99),
            (10, 14.99),
            (15, 100)
        };

        public ProductPage(User user)
        {
            InitializeComponent();

            UserName.Text = user.UserSurname + " " + user.UserName + " " + user.UserPatronymic;

            var currentProducts = Popov41Entities.GetContext().Product.ToList();

            ProductListView.ItemsSource = currentProducts;

            CBoxFilter.SelectedIndex = 0;

            UpdateServices();
        }

        private bool FilterDiscount(Product product, int ind)
        {
            return product.ProductDiscountAmount >= discountRangesList[ind].Item1 && product.ProductDiscountAmount <= discountRangesList[ind].Item2;
        }

        private void UpdateServices()
        {
            var currentServices = Popov41Entities.GetContext().Product.ToList();

            AllNumRecords = currentServices.Count;

            currentServices = currentServices.Where(p => FilterDiscount(p, CBoxFilter.SelectedIndex)).ToList();

            currentServices = currentServices.Where(p => p.ProductName.ToLower().Contains(TBoxSearch.Text.ToLower())).ToList();

            if (RButtonUp.IsChecked.Value)
            {
                currentServices = currentServices.OrderBy(p => p.ProductCost).ToList();
            }

            if (RButtonDown.IsChecked.Value)
            {
                currentServices = currentServices.OrderByDescending(p => p.ProductCost).ToList();
            }

            ProductListView.ItemsSource = currentServices;

            CurrentNumRecords = currentServices.Count;
            TBNumRecords.Text = CurrentNumRecords.ToString() + " из " + AllNumRecords.ToString();
        }

        private void TBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateServices();
        }

        private void CBoxFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateServices();
        }

        private void RButtonUp_Checked(object sender, RoutedEventArgs e)
        {
            UpdateServices();
        }

        private void RButtonDown_Checked(object sender, RoutedEventArgs e)
        {
            UpdateServices();
        }
    }
}