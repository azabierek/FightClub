using FightClub.Model;
using Microsoft.UI.Xaml;
using System.Globalization;

namespace FightClub.Converters
{
    public class BeltToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var belt = value as Belt?;
            if (belt != null)
            {
                if (belt == Belt.Biały)
                    return "white.png";
                if (belt == Belt.Niebieski)
                    return "blue.png";
                if (belt == Belt.Purpurowy)
                    return "purple.png";
                if (belt == Belt.Brązowy)
                    return "brown.png";
                if (belt == Belt.Czarny)
                    return "black.png";
            }
            return DependencyProperty.UnsetValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
