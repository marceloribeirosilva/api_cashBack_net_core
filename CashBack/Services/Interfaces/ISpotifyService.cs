using CashBack.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CashBack.Services.Interfaces
{
    public interface ISpotifyService
    {
        TokenSpotify ObterTokenSpotify();
        RetornoAlbunsSpotify.RootObject ObterListaAlbuns(TokenSpotify token, string genero);
    }
}
