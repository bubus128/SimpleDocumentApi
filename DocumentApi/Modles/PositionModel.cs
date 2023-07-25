using System.Globalization;
using System.Reflection;

namespace DocumentApi.Modles
{
    public class PositionModel
    {
        public PositionModel(string positionString)
        {
            string[] paramsList = positionString.Split(separator: new string[] { "," }, options: StringSplitOptions.RemoveEmptyEntries).Skip(1).ToArray();
            PropertyInfo[] properties = this.GetType().GetProperties();
            if (paramsList.Length < properties.Length)
            {
                throw new ArgumentException(message: "To few parameters in position");
            }
            if (paramsList.Length > properties.Length)
            {
                throw new ArgumentException(message: "To meny parameters in position");
            }
            for (int i = 0; i < paramsList.Length; i++)
            {
                if (properties[i].PropertyType == typeof(float))
                {
                    properties[i].SetValue(this, ParseFloat(paramsList[i]));
                }
                else
                {
                    properties[i].SetValue(this, paramsList[i]);
                }
            }
        }

        public string productCode { get; set; }
        public string productName { get; set; }
        public float quantity { get; set; }
        public float netPrice { get; set; }
        public float netValue { get; set; }
        public float vat { get; set; }
        public float quantityBefore { get; set; }
        public float avgBefore { get; set; }
        public float quantityAfter { get; set; }
        public float avgAfter { get; set; }
        public float group { get; set; }

        private static float ParseFloat(string input)
        {
            CultureInfo[] possibleCultureInfos = new CultureInfo[]
            {
            CultureInfo.InvariantCulture,           // For "20.12" format (period as decimal separator)
            CultureInfo.GetCultureInfo("en-US")    // For "23,12" format (comma as decimal separator)
                                                   // You can add more CultureInfo objects representing other possible formats
            };

            foreach (CultureInfo cultureInfo in possibleCultureInfos)
            {
                if (float.TryParse(input, NumberStyles.Float, cultureInfo, out float floatValue))
                {
                    return floatValue;
                }
            }

            // If parsing fails for all possible formats, you can throw an exception or handle the error in a way that fits your application's requirements.
            throw new FormatException("Invalid float format.");
        }
    }
}
