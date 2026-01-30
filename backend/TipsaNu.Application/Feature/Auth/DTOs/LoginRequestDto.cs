using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TipsaNu.Application.Feature.Auth.DTOs
{
    public record LoginRequestDto(string Email, string Password);
}
