namespace KIPServiceTestTask.Models
{
    public class Result
    {
        public Guid user_id { get; set; }
        public int count_sign_in { get; set; }
        public Result(Guid userId, int countSignIn)
        {
            user_id = userId;
            count_sign_in = countSignIn;
        }

    }
}
