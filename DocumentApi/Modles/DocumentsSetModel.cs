using System.Text.Json.Serialization;

namespace DocumentApi.Modles
{
    public class DocumentsSetModel
    {
        public List<DocumentModel> documents { get; }
        public int lineCount { get; }
        public int charCount { get; }
        public float sum { get => documents.SelectMany(document => document.positions).Sum(position => position.netValue); }
        public int xcount { get => documents.Count(document => document.positions.Count > x); }
        public string productsWithMaxNetValue { get => 
                documents.Count > 0 ? documents.SelectMany(document => document.positions).Count() > 0 ?
                documents.SelectMany(document => document.positions).MaxBy(document => document.netValue).ToString() : "positions list is empty"
                : "document list is empty";}

        [JsonIgnore]
        private int x { get; }

        public DocumentsSetModel(string documentsSetString, int x)
        {
            this.x = x;
            charCount = documentsSetString.Length;
            documents = new();
            List<string> lines = documentsSetString.Split(separator: new string[] {"\n", "\r", "\n\r", "\r\n"}, options: StringSplitOptions.RemoveEmptyEntries).ToList();
            lineCount = lines.Count;
            int lineNumber = 0;
            foreach(string line in lines)
            {
                lineNumber++;
                try
                {
                    if (line.StartsWith('H'))
                    {
                        documents.Add(new(line));
                    }
                    else if (line.StartsWith('B'))
                    {
                        if (documents.Count > 0)
                        {
                            documents[documents.Count - 1].AddPosition(line);
                        }
                    }
                    else if (line.StartsWith('C'))
                    {
                        if (documents.Count > 0)
                        {
                            documents[documents.Count - 1].AddComment(line);
                        }
                    }
                    else
                    {
                        throw new ArgumentException($"Incorrect line {lineNumber}. Line should start with H, B or C");
                    }
                }
                catch(ArgumentException e)
                {
                    throw new ArgumentException(e.Message + $" in line {lineNumber}");
                }
            }
        }
    }
}
