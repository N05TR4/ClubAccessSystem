

namespace ClubAccessSystem.Domain.Result
{
    public class OperationResult<TData>
    {
        public OperationResult()
        {
            this.Success = true;
        }

        public string? Message { get; set; }
        public bool Success { get; set; }
        public TData? Data { get; set; }
    }
}
