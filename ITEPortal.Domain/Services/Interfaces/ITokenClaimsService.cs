﻿using ITEPortal.Domain.Models;

namespace ITEPortal.Domain.Services.Interfaces
{
    public interface ITokenClaimsService
    {
        Task<TokenModel> GetTokenAsync(string username);
        Task<TokenModel> RefreshTokenAsync(string token);
    }
}
