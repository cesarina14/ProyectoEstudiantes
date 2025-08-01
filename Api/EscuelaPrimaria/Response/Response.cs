namespace EscuelaPrimaria.Response
{
    public  class Response<T>
    {
        public long Code { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
        public object Value { get; set; }
    }
}
