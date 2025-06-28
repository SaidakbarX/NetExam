using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetExam.Application.Dtos;

public class LoginResponseDto
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
    public string TokenType { get; set; } = "Bearer";
    public int Expires { get; set; }

    // optional: foydalanuvchi haqida ham qaytarish
    public UserGetDto? User { get; set; }
}