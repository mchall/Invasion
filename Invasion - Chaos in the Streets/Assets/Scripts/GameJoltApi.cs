using System;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;
using System.Collections;

public class GameJoltApi 
{
	public IEnumerator DoAddHighScore(string userName, string token, int score)
	{
		var tableUri = string.Format("http://gamejolt.com/api/game/v1/scores/add/?game_id=13018&score={0}&sort={1}&username={2}&user_token={3}",
                score, score, EscapeUrl(userName), token);
        var uri = String.Format("{0}&signature={1}", tableUri, GetSigniture(tableUri));
		
		WWW www = new WWW(uri);
		while(!www.isDone)
		{
			yield return null;	
		}
	}

	public IEnumerator DoAddHighScore(string userName, int score)
	{
		var tableUri = string.Format("http://gamejolt.com/api/game/v1/scores/add/?game_id=13018&score={0}&sort={1}&guest={2}",
                score, score, EscapeUrl(userName));
        var uri = String.Format("{0}&signature={1}", tableUri, GetSigniture(tableUri));
		
		WWW www = new WWW(uri);
		while(!www.isDone)
		{
			yield return null;	
		}
	}
	
	private string GetSigniture(string uri)
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
	
	private string EscapeUrl(string url) 
	{
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