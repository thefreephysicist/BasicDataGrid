using DataGridExtensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DiffAnalysisGUI.Controls
{
	/// <summary>
	/// Interaction logic for MassFilterPopUpControl.xaml
	/// </summary>
	public partial class MassFilterPopUpControl
	{
		public MassFilterPopUpControl()
		{
			InitializeComponent();
		}

        public string Caption
        {
            get => (string)GetValue(CaptionProperty);
            set => SetValue(CaptionProperty, value);
        }

        /// <summary>
        /// Identifies the Minimum dependency property
        /// </summary>
        public static readonly DependencyProperty CaptionProperty = DependencyProperty.Register("Caption", typeof(string), typeof(MassFilterPopUpControl)
                , new FrameworkPropertyMetadata("Enter the limits:", FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public double Minimum
        {
            get => (double)GetValue(MinimumProperty);
            set => SetValue(MinimumProperty, value);
        }
        /// <summary>
        /// Identifies the Minimum dependency property
        /// </summary>
        public static readonly DependencyProperty MinimumProperty = DependencyProperty.Register("Minimum", typeof(double), typeof(MassFilterPopUpControl)
                , new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, (sender, e) => ((MassFilterPopUpControl)sender).Range_Changed()));

        public double Maximum
        {
            get => (double)GetValue(MaximumProperty);
            set => SetValue(MaximumProperty, value);
        }
        /// <summary>
        /// Identifies the Maximum dependency property
        /// </summary>
        public static readonly DependencyProperty MaximumProperty = DependencyProperty.Register("Maximum", typeof(double), typeof(MassFilterPopUpControl)
                , new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, (sender, e) => ((MassFilterPopUpControl)sender).Range_Changed()));


        public bool IsPopupVisible
        {
            get => (bool)GetValue(IsPopupVisibleProperty);
            set => SetValue(IsPopupVisibleProperty, value);
        }
        /// <summary>
        /// Identifies the IsPopupVisible dependency property
        /// </summary>
        public static readonly DependencyProperty IsPopupVisibleProperty =
            DependencyProperty.Register("IsPopupVisible", typeof(bool), typeof(MassFilterPopUpControl), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, (sender, e) => ((MassFilterPopUpControl)sender).IsPopupVisible_Changed()));

        private void IsPopupVisible_Changed()
        {
        }

        private void Range_Changed()
        {
            Filter = Maximum > Minimum ? new ContentFilter(Minimum, Maximum) : null;
        }

        public IContentFilter? Filter
        {
            get => (IContentFilter?)GetValue(FilterProperty);
            set => SetValue(FilterProperty, value);
        }
        /// <summary>
        /// Identifies the Filter dependency property
        /// </summary>
        public static readonly DependencyProperty FilterProperty =
            DependencyProperty.Register("Filter", typeof(IContentFilter), typeof(MassFilterPopUpControl), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, (sender, e) => ((MassFilterPopUpControl)sender).Filter_Changed()));


        private void Filter_Changed()
        {
            if (!(Filter is ContentFilter filter))
                return;

            Minimum = filter.Min;
            Maximum = filter.Max;
        }

        public class ContentFilter : IContentFilter
        {
            private readonly double _min;
            private readonly double _max;

            public ContentFilter(double min, double max)
            {
                _min = min;
                _max = max;
            }

            public double Min => _min;

            public double Max => _max;

            public bool IsMatch(object? value)
            {
                if (value == null)
                    return false;

                if (!double.TryParse(value.ToString(), out var number))
                {
                    return false;
                }

                return (number >= _min) && (number <= _max);
            }
        }
    }
}
