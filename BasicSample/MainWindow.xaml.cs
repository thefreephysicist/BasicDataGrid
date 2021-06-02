namespace BasicSample
{
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Windows;
	using System.Windows.Controls;
	using System.Windows.Data;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
		{
            InitializeComponent();

			var dv = new DataGridTextColumn()
			{
				Header = "Index",
				Binding = new Binding("Index")
			};

			Control control = new Controls.NumberFilterPopUpControl();
			dv.SetValue(DataGridExtensions.DataGridFilterColumn.TemplateProperty, control.Template);

			Dg.Columns.Add(dv);
		}
	}
}
