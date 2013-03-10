using System;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

public class Global : MonoBehaviour 
{	
	public static bool GameOver = false;
	public static bool HardcoreMode = false;
	
	public static string UserName;
	public static string Token;

	public static void AddHighScore(float score)
	{
		if(string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(Token))
			return;
		
		var tableUri = string.Format("http://gamejolt.com/api/game/v1/scores/add/?game_id=13018&score={0}&sort={1}&username={2}&user_token={3}",
                EscapeUrl(score.ToString()), EscapeUrl(score.ToString()), EscapeUrl(UserName), EscapeUrl(Token));
        var uri = new Uri(String.Format("{0}&signature={1}", tableUri, GetSigniture(tableUri)));
		
		WWW www = new WWW(uri.ToString());
	}
	
	private static string GetSigniture(string uri)
    {
        using (SHA1Managed sha1 = new SHA1Managed())
        {
            var data = sha1.ComputeHash(Encoding.UTF8.GetBytes(uri + "<secret>"));

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
                sb.Append(data[i].ToString("x2"));

            return sb.ToString();
        }
    }
	
	private static string EscapeUrl(string url) {
		return url
                .Replace("%", "%25")
                .Replace(" ", "%20")
                .Replace("!", "%21")
                .Replace("\"", "%22")
                .Replace("#", "%23")
                .Replace("$", "%24")
                .Replace("&", "%26")
                .Replace("'", "%27")
                .Replace("(", "%28")
                .Replace(")", "%29")
                .Replace("*", "%2A")
                .Replace("+", "%2B")
                .Replace(",", "%2C")
                .Replace("-", "%2D")
                .Replace(".", "%2E")
                .Replace("/", "%2F")
                .Replace(":", "%3A")
                .Replace(";", "%3B")
                .Replace("<", "%3C")
                .Replace("=", "%3D")
                .Replace(">", "%3E")
                .Replace("?", "%3F")
                .Replace("@", "%40")
                .Replace("[", "%5B")
                .Replace("\\", "%5C")
                .Replace("]", "%5D")
                .Replace("^", "%5E")
                .Replace("_", "%5F")
                .Replace("`", "%60")
                .Replace("{", "%7B")
                .Replace("|", "%7C")
                .Replace("}", "%7D")
                .Replace("~", "%7E");
	}
}