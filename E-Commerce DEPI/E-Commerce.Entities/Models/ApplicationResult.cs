namespace Models
{
    public class ApplicationResult<T>
    {
        public bool IsSuccess { get; set; }
        public T? Result { get; set; }
        public Dictionary<string, string> Messages { get; set; }
    }
}
