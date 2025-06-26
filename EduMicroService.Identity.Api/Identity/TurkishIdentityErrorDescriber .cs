using Microsoft.AspNetCore.Identity;

namespace EduMicroService.Identity.Api.Identity
{
    public class TurkishIdentityErrorDescriber : IdentityErrorDescriber
    {


        public override IdentityError DefaultError() =>
            new() { Code = nameof(DefaultError), Description = "Bilinmeyen bir hata oluştu." };

        public override IdentityError ConcurrencyFailure() =>
            new() { Code = nameof(ConcurrencyFailure), Description = "Eşzamanlılık hatası: Bu kayıt başka bir kullanıcı tarafından değiştirildi." };

        public override IdentityError PasswordTooShort(int length) =>
            new() { Code = nameof(PasswordTooShort), Description = $"Şifre en az {length} karakter olmalıdır." };

        public override IdentityError PasswordRequiresNonAlphanumeric() =>
            new() { Code = nameof(PasswordRequiresNonAlphanumeric), Description = "Şifre en az bir alfanümerik olmayan karakter içermelidir." };

        public override IdentityError PasswordRequiresDigit() =>
            new() { Code = nameof(PasswordRequiresDigit), Description = "Şifre en az bir rakam içermelidir ('0'-'9')." };

        public override IdentityError PasswordRequiresLower() =>
            new() { Code = nameof(PasswordRequiresLower), Description = "Şifre en az bir küçük harf içermelidir ('a'-'z')." };

        public override IdentityError PasswordRequiresUpper() =>
            new() { Code = nameof(PasswordRequiresUpper), Description = "Şifre en az bir büyük harf içermelidir ('A'-'Z')." };

        public override IdentityError DuplicateUserName(string userName) =>
            new() { Code = nameof(DuplicateUserName), Description = $"'{userName}' kullanıcı adı zaten alınmış." };

        public override IdentityError DuplicateEmail(string email) =>
            new() { Code = nameof(DuplicateEmail), Description = $"'{email}' e-posta adresi zaten kullanılmakta." };

        public override IdentityError InvalidUserName(string? userName) =>
            new() { Code = nameof(InvalidUserName), Description = $"'{userName}' geçersiz bir kullanıcı adıdır." };

        public override IdentityError InvalidEmail(string? email) =>
            new() { Code = nameof(InvalidEmail), Description = $"'{email}' geçersiz bir e-posta adresidir." };


        // Kullanıcı ve Role işlemleri
        public override IdentityError UserAlreadyHasPassword()
            => new() { Code = nameof(UserAlreadyHasPassword), Description = "Kullanıcının zaten bir şifresi mevcut." };

        public override IdentityError UserLockoutNotEnabled()
            => new() { Code = nameof(UserLockoutNotEnabled), Description = "Bu kullanıcı için kilitleme etkin değil." };

        public override IdentityError UserAlreadyInRole(string role)
            => new() { Code = nameof(UserAlreadyInRole), Description = $"Kullanıcı zaten '{role}' rolünde." };

        public override IdentityError UserNotInRole(string role)
            => new() { Code = nameof(UserNotInRole), Description = $"Kullanıcı '{role}' rolünde değil." };

        public override IdentityError InvalidRoleName(string? role)
            => new() { Code = nameof(InvalidRoleName), Description = $"'{role}' geçersiz bir rol adıdır." };

        public override IdentityError DuplicateRoleName(string role)
            => new() { Code = nameof(DuplicateRoleName), Description = $"'{role}' rol adı zaten kullanımda." };

        public override IdentityError LoginAlreadyAssociated()
            => new() { Code = nameof(LoginAlreadyAssociated), Description = "Bu giriş bilgileri başka bir kullanıcıya bağlı." };



    }
}
