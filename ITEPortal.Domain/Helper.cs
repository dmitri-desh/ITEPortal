using ITEPortal.Domain.Dto;

namespace ITEPortal.Domain
{
    public static class Helper
    {
        public static ResponseDto GetErrorDto(Exception e)
        {
            return new ResponseDto
            {
                Success = false,
                Errors = new List<string> { e.Message }
            };
        }
    }
}
