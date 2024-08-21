namespace AdminGestaoApi.ApplicationService.Services.Password
{
    public static class PasswordHasher
    {
        public static string HashPassword(string? password) => BCrypt.Net.BCrypt.HashPassword(password);

        public static bool VerificaPassword(string? password, string? hashedPassword) => BCrypt.Net.BCrypt.Verify(password, hashedPassword);
    }
}
