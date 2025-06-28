using NetExam.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetExam.Application.Services;

public interface IAuthService
{
    /// <summary>
    /// Yangi foydalanuvchini ro'yxatdan o'tkazadi.
    /// </summary>
    /// <param name="dto">Yangi foydalanuvchi ma'lumotlari</param>
    /// <returns>Foydalanuvchi ID</returns>
    Task<long> SignUpUserAsync(UserCreateDto dto);

    /// <summary>
    /// Login qilish uchun email/parolni tekshiradi va token beradi.
    /// </summary>
    /// <param name="dto">Login DTO (UserName va Password)</param>
    /// <returns>Access va Refresh tokenlar</returns>
    Task<LoginResponseDto> LoginUserAsync(UserLoginDto dto);

    /// <summary>
    /// JWT muddati tugaganida yangilaydi.
    /// </summary>
    /// <param name="request">Access + Refresh tokenlar</param>
    /// <returns>Yangi tokenlar</returns>
    Task<LoginResponseDto> RefreshTokenAsync(RefreshRequestDto request);

    /// <summary>
    /// Refresh tokenni tizimdan o'chiradi (logout).
    /// </summary>
    /// <param name="token">Refresh token qiymati</param>
    Task LogOut(string token);

    /// <summary>
    /// Email orqali tasdiqlash kodini yuboradi.
    /// </summary>
    /// <param name="email">Foydalanuvchi emaili</param>
    Task EailCodeSender(string email);

    /// <summary>
    /// Email orqali yuborilgan kodni tasdiqlaydi.
    /// </summary>
    /// <param name="userCode">Foydalanuvchi kiritgan kod</param>
    /// <param name="email">Foydalanuvchi emaili</param>
    /// <returns>Tasdiqlanganmi yoki yo'qmi</returns>
    Task<bool> ConfirmCode(string userCode, string email);
}
