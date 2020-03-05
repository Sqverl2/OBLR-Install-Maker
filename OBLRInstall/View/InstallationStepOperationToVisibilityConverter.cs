using OBLRInstall.Data;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace OBLRInstall.View
{
    class InstallationStepOperationToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var operation = (StepType)value;
            switch (operation)
            {
                case StepType.UNPACK:
                    return Visibility.Visible;
                case StepType.RUN:
                    return Visibility.Collapsed;
                case StepType.COPY:
                    return Visibility.Collapsed;
                default:
                    return Visibility.Collapsed;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("This could be supported, but logically does not make sense.");
        }
    }
}
