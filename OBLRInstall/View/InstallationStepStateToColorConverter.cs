using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace OBLRInstall.View
{
    public class InstallationStepStateToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var state = (Data.InstallationStep.State)value;
            switch (state)
            {
                case Data.InstallationStep.State.MISSING:
                    return Brushes.Gray;
                case Data.InstallationStep.State.WRONG_LOCATION:
                    return Brushes.Orange;
                case Data.InstallationStep.State.READY:
                    return Brushes.Blue;
                case Data.InstallationStep.State.COMPLETED:
                    return Brushes.Green;
                case Data.InstallationStep.State.FAILED:
                    return Brushes.Red;
                default:
                    return Brushes.Magenta;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("This could be supported, but logically does not make sense.");
        }
    }
}
