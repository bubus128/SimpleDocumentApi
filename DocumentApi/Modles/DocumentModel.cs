namespace DocumentApi.Modles
{
    public class DocumentModel
    {

        public DocumentModel(string headerString)
        {
            Comment = string.Empty;
            positions = new();
            header = new(headerString);
        }
        public HeaderModel header { get; }
        public List<PositionModel> positions { get; set; }
        public string Comment { get; set; }

        public void AddPosition(string positionString)
        {
            positions.Add(new(positionString));
        }

        public void AddComment(string line)
        {
            Comment = line;
        }
    }
}
