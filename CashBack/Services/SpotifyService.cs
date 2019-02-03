using CashBack.Models;
using CashBack.Services.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CashBack.Services
{
    public class SpotifyService : ISpotifyService
    {
        public RetornoAlbunsSpotify.RootObject ObterListaAlbuns(TokenSpotify token, string genero)
        {
            RetornoAlbunsSpotify.RootObject results = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.spotify.com/v1/");
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token.access_token);
                string url = $"browse/categories/{genero}/playlists?limit=50";

                HttpResponseMessage response = null;
                try
                {
                    response = client.GetAsync(url).Result;
                }
                catch (Exception ex)
                {
                    if (response == null)
                    {
                        response = new HttpResponseMessage();
                    }
                    response.StatusCode = HttpStatusCode.InternalServerError;
                    response.ReasonPhrase = string.Format("RestHttpClient.SendRequest failed: {0}", ex);

                }

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = response.Content.ReadAsStringAsync().Result;

                    results = JsonConvert.DeserializeObject<RetornoAlbunsSpotify.RootObject>(responseBody);                                     
                }
            }

            return results;
        }

        public TokenSpotify ObterTokenSpotify()
        {
            string clientId = "89dfee72193c43258d1239e95fe0d757";
            string clientSecret = "ed5524154573408da1370e6de7d830e8";
            string credentials = String.Format("{0}:{1}", clientId, clientSecret);
            TokenSpotify token = null;

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes(credentials)));

                List<KeyValuePair<string, string>> requestData = new List<KeyValuePair<string, string>>();
                requestData.Add(new KeyValuePair<string, string>("grant_type", "client_credentials"));

                FormUrlEncodedContent requestBody = new FormUrlEncodedContent(requestData);

                //Request Token
                try
                {
                    var request = client.PostAsync("https://accounts.spotify.com/api/token", requestBody);
                    var response = request.Result;
                    token =  JsonConvert.DeserializeObject<TokenSpotify>(response.Content.ReadAsStringAsync().Result);
                }
                catch (Exception)
                {   
                }

                return token;
            }
        }
    }
}
