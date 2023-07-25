using System.Collections.Generic;
using System.Globalization;
using System.Reflection;

namespace DocumentApi.Modles
{
    public class HeaderModel
    {
        public HeaderModel(string headerString)
        {
            string[] paramsList = headerString.Split(separator: new string[] {","}, options: StringSplitOptions.RemoveEmptyEntries).Skip(1).ToArray();
            PropertyInfo[] properties = this.GetType().GetProperties();
            if(paramsList.Length < properties.Length)
            {
                throw new ArgumentException(message: "To few parameters in header");
            }
            if (paramsList.Length > properties.Length)
            {
                throw new ArgumentException(message: "To meny parameters in header");
            }
            for(int i=0; i<paramsList.Length; i++)
            {
                if (properties[i].PropertyType == typeof(DateTime))
                {
                    properties[i].SetValue(this, DateTime.Parse(paramsList[i]));
                }
                else if (properties[i].PropertyType == typeof(float))
                {
                    properties[i].SetValue(this, ParseFloat(paramsList[i]));
                }
                else if (properties[i].PropertyType == typeof(string))
                {
                    properties[i].SetValue(this, paramsList[i]);
                }
            }
        }

        public float BaCode { get; set; }
        public float Type { get; set; }
        public string DocumentNumber { get; set; }
        public DateTime OperationDate { get; set; }
        public float DocumentDayNumber { get; set; }
        public float ContractorNumber { get; set; }
        public string ContractorName { get; set;}
        public string ExternalDocumentNumber { get; set; }
        public DateTime ExternalDocumentDate { get; set; }
        public float Net { get; set; }
        public float Vat { get; set; }
        public float Gross { get; set; }
        public float F1 { get; set; }
        public float F2 { get; set; }
        public float F3 { get; set; }

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
