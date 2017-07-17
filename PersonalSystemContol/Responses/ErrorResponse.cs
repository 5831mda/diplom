namespace PersonalSystemContol.Responses
{
    /// <summary>
    /// Генерация ответа с информацией об ошибке.
    /// </summary>
    public class ErrorResponse
    {
        public string status;
        public string error;

        public ErrorResponse(string status, string error)
        {
            this.status = status;
            this.error = error;
        }
    }
}
