using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Pagination;

public class ProductsViewModel 
{
    private ProductService productService;
    private int currentPage = 1;
    private bool isBusy = false;
    private int totalProducts = 50; 
    private int pageSize = 10; 

    public ObservableCollection<Product> Products { get; } = new ();
    public ICommand LoadNextPageCommand { get; }
    public ICommand LoadPreviousPageCommand { get; }

    public ProductsViewModel(ProductService productService)
    {
        this.productService = productService;
        LoadNextPageCommand = new Command(async () => await LoadNextPage(), () => !isBusy && currentPage < (totalProducts / pageSize));
        LoadPreviousPageCommand = new Command(async () => await LoadPreviousPage(), () => !isBusy && currentPage > 1);
        LoadProducts(); 
    }

    private async Task LoadProducts()
    {
        if (isBusy) return;
        isBusy = true;
        Products.Clear();
        var products = await productService.GetProductsAsync(currentPage);
        foreach (var product in products)
        {
            Products.Add(product);
        }
        isBusy = false;
        ((Command)LoadNextPageCommand).ChangeCanExecute();
        ((Command)LoadPreviousPageCommand).ChangeCanExecute();
    }

    private async Task LoadNextPage()
    {
        if (isBusy) return;
        currentPage++;
        await LoadProducts();
    }

    private async Task LoadPreviousPage()
    {
        if (isBusy) return;
        currentPage--;
        await LoadProducts();
    }
}
