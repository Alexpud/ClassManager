namespace ClassManager.Business.Dtos.Authentication
{
    public class LoginResponseDto
    {
        public string Token { get; set; }
        public int ExpiracaoEmMinutos { get; set; }
    }
}
