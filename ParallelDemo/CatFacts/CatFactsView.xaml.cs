namespace ParallelDemo.CatFacts
{
    public partial class CatFactsView : ContentPage
    {
        public CatFactsView()
        {
            InitializeComponent();

            BindingContext = new CatFactsViewModel();
        }
    }
}