using FightClub.Model;
using System.Globalization;

namespace FightClub.Converters
{
    public class BeltToImageConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var belt = values[0] as Belt?;
            var stripe = values[1] as Stripe?;

            if (belt != null)
            {
                if (stripe != null)
                {
                    if (belt == Belt.Biały)
                    {

                        return "white0.png";
                    }
                    if (belt == Belt.Niebieski)
                    {

                        return "blue0.png";
                    }
                    if (belt == Belt.Purpurowy)
                    {

                        return "purple0.png";
                    }
                    if (belt == Belt.Brązowy)
                    {

                        return "brown0.png";
                    }
                    if (belt == Belt.Czarny)
                    {

                        return "black0.png";
                    }
                }
            }
            return null;
        }


        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
