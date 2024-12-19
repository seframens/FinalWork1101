using DatabaseLibrary.Models;
using DatabaseLibrary.Services;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace FragrantWorld.Pages
{
    /// <summary>
    /// Логика взаимодействия для ShopPage.xaml
    /// </summary>
    public partial class ShopPage : Page
    {
        private readonly ProductService _productService = new();
        private readonly UserService _userService = new();

        public ShopPage()
        {
            InitializeComponent();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadProductsAsync();
        }

        private async Task LoadProductsAsync()
        {
            try
            {
                var product = await _productService.GetProductsAsync();
                foreach (Product productItem in product)
                    CreatProductConteiner(productItem);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CreatProductConteiner(Product productItem)
        {
            try
            {
                StackPanel panel = new()
                {
                    Width = 630,
                    Margin = new Thickness(15),
                    Background = new SolidColorBrush(Color.FromRgb(255, 237, 237)),

                };

                Border topBorder = new()
                {
                    BorderBrush = new SolidColorBrush(Color.FromRgb(0, 0, 0)),
                    BorderThickness = new Thickness(1),
                    CornerRadius = new CornerRadius(5)
                };
                Border botBorder = new()
                {
                    BorderBrush = new SolidColorBrush(Color.FromRgb(0, 0, 0)),
                    BorderThickness = new Thickness(1),
                    CornerRadius = new CornerRadius(5)
                };

                Grid grid = new() { };
                grid.RowDefinitions.Add(new RowDefinition());
                grid.RowDefinitions.Add(new RowDefinition());
                grid.RowDefinitions.Add(new RowDefinition());
                grid.RowDefinitions.Add(new RowDefinition());
                grid.ColumnDefinitions.Add(new ColumnDefinition());
                grid.ColumnDefinitions.Add(new ColumnDefinition());

                TextBlock ProductTextBlock = new TextBlock()
                {
                    Text = productItem.Name,
                    FontWeight = FontWeights.Bold
                };
                Grid.SetRow(ProductTextBlock, 0);
                Grid.SetColumn(ProductTextBlock, 0);
                grid.Children.Add(ProductTextBlock);

                TextBlock DescriptionTextBlock = new TextBlock
                {
                    Text = productItem.Description
                };
                Grid.SetRow(DescriptionTextBlock, 1);
                Grid.SetColumn(DescriptionTextBlock, 0);
                grid.Children.Add(DescriptionTextBlock);

                TextBlock ManufacturerTextBlock = new TextBlock
                {
                    Text = $"Производитель: {productItem.Manufacturer}",
                };
                Grid.SetRow(ManufacturerTextBlock, 2);
                Grid.SetColumn(ManufacturerTextBlock, 0);
                grid.Children.Add(ManufacturerTextBlock);

                TextBlock PriceTextBlock = new TextBlock
                {
                    Text = $"Цена: {productItem.Cost}",
                };
                Grid.SetRow(PriceTextBlock, 3);
                Grid.SetColumn(PriceTextBlock, 0);
                grid.Children.Add(PriceTextBlock);

                Button OrderButton = new Button
                {
                    Content = "Заказать",
                    HorizontalAlignment = HorizontalAlignment.Right,
                };
                Grid.SetRow(OrderButton, 3);
                Grid.SetColumn(OrderButton, 1);
                grid.Children.Add(OrderButton);

                panel.Children.Add(topBorder);
                panel.Children.Add(grid);
                panel.Children.Add(botBorder);
                ProductStackPanel.Children.Add(panel);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (App.CurrentFrame.CanGoBack)
                App.CurrentFrame.GoBack();
        }
    }
}
