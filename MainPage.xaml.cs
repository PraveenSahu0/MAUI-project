namespace Pagination
{
    public partial class MainPage : ContentPage
    {
        public MainPage(ProductsViewModel productsViewModel)
        {
            InitializeComponent();
            BindingContext = productsViewModel;
        }

    }
}
