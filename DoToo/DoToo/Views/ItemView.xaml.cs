using DoToo.ViewModels;
using System.Linq;
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
            _viewModel = viewModel;
            viewModel.Navigation = Navigation;
            BindingContext = viewModel;
            InitializeComponent();

            SubtasksListView.ItemSelected += (s, e) => SubtasksListView.SelectedItem = null;
            //AddSubTaskBtn.Clicked += (s, e) => { NewSubTaskEntry.Text = string.Empty; };
        }

        private void Entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            //var test = _viewModel.SubTasks.Where(t => t.SubTask.Detail.Equals(e.OldTextValue));
        }
    }
}