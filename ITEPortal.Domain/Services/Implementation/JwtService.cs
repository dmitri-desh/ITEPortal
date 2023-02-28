using ITEPortal.Domain.Models;
using ITEPortal.Domain.Services.Interfaces;
using JWT.Algorithms;
using JWT.Serializers;
using JWT;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using JWT.Exceptions;

namespace ITEPortal.Domain.Services.Implementation
{
    public class JwtService : IJwtService
    {
        private readonly IJsonSerializer _serializer;
        private readonly IDateTimeProvider _provider;
        private readonly IBase64UrlEncoder _urlEncoder;
        private readonly IJwtAlgorithm _algorithm;

        public JwtService()
        {
            _serializer = new JsonNetSerializer();
            _provider = new UtcDateTimeProvider();
            _urlEncoder = new JwtBase64UrlEncoder();
            _algorithm = new HMACSHA256Algorithm();
        }
        public TokenResultModel IsExpiredToken(string accessToken)
        {
            var result = new TokenResultModel();
            var expiryDate = GetExpiryTimestamp(accessToken);
            if (expiryDate > DateTime.UtcNow)
            {
                result.IsExpired = false;
            }
            else
            {
                result.IsExpired = true;
            }

            return result;
        }
        private DateTime GetExpiryTimestamp(string accessToken)
        {
            try
            {
                IJwtValidator validator = new JwtValidator(_serializer, _provider);
                IJwtDecoder decoder = new JwtDecoder(_serializer, validator, _urlEncoder, _algorithm);
                var token = decoder.DecodeToObject<JwtToken>(accessToken);
                DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(token.Exp);
                return dateTimeOffset.UtcDateTime;
            }
            catch (TokenExpiredException)
            {
                return DateTime.MinValue;
            }
            catch (SignatureVerificationException)
            {
                return DateTime.MinValue;
            }
            catch
            {
                return DateTime.MinValue;
            }
        }
    }
}
