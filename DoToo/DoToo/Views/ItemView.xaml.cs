using DoToo.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DoToo.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemView : ContentPage
    {
        private readonly ItemViewModel _viewModel;

        public ItemView(ItemViewModel viewModel)
        {
            viewModel.Navigation = Navigation;
            BindingContext = viewModel;
            InitializeComponent();

            SubtasksListView.ItemSelected += (s, e) => SubtasksListView.SelectedItem = null;
        }
        
    }
}