using System;
using System.Text;
using Duende.IdentityServer.Models;
using IdentityModel;

namespace Spp.Common.Authentication.TokenAuthenticationScheme.IdentityServer;

public readonly struct AuthorizationCode
{
    public AuthorizationCode()
    {
        Verifier = CreateCodeVerifier();
        Challenge = CreateCodeChallenge(Verifier);
    }

    public string Verifier { get; }

    public string Challenge { get; }

    public string HashedChallenge => Challenge.Sha256();

    private static string CreateCodeVerifier()
    {
        const string hex = "0123456789abcdef";
        var random = new Random();
        var chars = new char[128];

        for (var i = 0; i < chars.Length; ++i)
        {
            chars[i] = hex[random.Next(hex.Length)];
        }

        return new string(chars);
    }

    private static string CreateCodeChallenge(string codeVerifier)
    {
        var codeVerifierBytes = Encoding.ASCII.GetBytes(codeVerifier);
        var hashedBytes = codeVerifierBytes.Sha256();
        return Base64Url.Encode(hashedBytes);
    }
}
