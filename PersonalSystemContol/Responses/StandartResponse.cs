namespace PersonalSystemContol.Responses
{
    /// <summary>
    /// Генерация обычного ответа.
    /// </summary>
    public class StandartResponse
    {
        public string status;
        public int id;

        public StandartResponse(string status, int id)
        {
            this.status = status;
            this.id = id;
        }
    }
}
