namespace ITEPortal.Domain.Dto
{
    public class ResponseDto
    {
        public bool Success { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
        public object Data { get; set; } = new object();
    }
}
