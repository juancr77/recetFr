using recetFr.Views;


namespace recetFr
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute("AddMealPage", typeof(AddMealPage));
            Routing.RegisterRoute("EditMealPage", typeof(EditMealPage));

        }
    }
}
