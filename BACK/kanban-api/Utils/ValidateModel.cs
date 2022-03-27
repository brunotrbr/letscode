namespace kanban_api.Utils
{
    public class ValidateModel
    {
        private readonly List<string> _fail;

        private ValidateModel()
        {
            _fail = new List<string>();
        }

        public static ValidateModel Start()
        {
            return new ValidateModel();
        }

        public ValidateModel Fail(bool hasError, string mensagem)
        {
            if (hasError)
                _fail.Add(mensagem);

            return this;
        }

        public void Validate(int statusCodeExpectedOnFail = StatusCodes.Status400BadRequest)
        {
            if (_fail.Any())
                throw new BusinessException(_fail, statusCodeExpectedOnFail);
        }
    }

    public class BusinessException : ArgumentException
    {
        public List<string> Fail { get; set; }
        public int StatusCode { get; set; }

        public BusinessException(List<string> fail, int statusCode)
        {
            Fail = fail;
            StatusCode = statusCode;
        }
    }
}
