using System;
using System.Text;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using UnityEngine;

public class ServerAPI {
	private static readonly HttpClient client = new HttpClient();
	private StringWithQualityHeaderValue lang;

	public ServerAPI() {
		client.BaseAddress = new Uri("http://localhost:443/");
	}

	public void SetHeaderLanguage(bool isEnglish) {
			client.DefaultRequestHeaders.Clear();
		if (isEnglish)
			client.DefaultRequestHeaders.Add("Accept-Language", "en");
		else
			client.DefaultRequestHeaders.Add("Accept-Language", "pt-BR");
		return;
	}

	public async Task<string> LogIn(CredentialsModel cred) {
		string paramts = "{\"email\": \"" + cred.Email + "\", \"password\": \"" + cred.Password + "\"}";

		var content = new StringContent(paramts, Encoding.UTF8, "application/json");
		HttpResponseMessage res = await client.PostAsync("/sessions/", content);
		return GetResponseMessage(res);
	}

	public async Task<string> SignIn(CredentialsModel cred) {
		string paramts = "{\"email\": \"" + cred.Email + "\", \"password\": \"" + cred.Password + "\"}";

		var content = new StringContent(paramts, Encoding.UTF8, "application/json");
		HttpResponseMessage res = await client.PostAsync("/players/", content);
		return GetResponseMessage(res);
	}

	private string GetResponseMessage(HttpResponseMessage res) {
		if ((int)res.StatusCode >= 400 && (int)res.StatusCode < 500) {
			ServerMessage msg = JsonUtility.FromJson<ServerMessage>(res.Content.ReadAsStringAsync().Result);
			return msg.message;
		}

		if ((int)res.StatusCode >= 200 && (int)res.StatusCode < 300)
			return "OK";

		return res.StatusCode.ToString();
	}

}
