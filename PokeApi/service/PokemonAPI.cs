using PokeApi.models;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace PokeApi.service
{

    public class PokemonAPI
    {
        private const string BASE_URL = "https://pokeapi.co/api/v2/";

        private static readonly Lazy<PokemonAPI> instance = new Lazy<PokemonAPI>(() => new PokemonAPI());
        public readonly HttpClient httpClient;
        public static PokemonAPI Instance => instance.Value;

        private PokemonAPI()
        {
            httpClient = new HttpClient
            {
                BaseAddress = new Uri(BASE_URL)
            };
        }
    }

    interface PokemonService {
        Task<PokemonPageDTO?> GetAllFromPage(string page);
        Task<PokemonPageDTO?> GetAllTypes();
        Task<PokemonPageDTO?> GetBySpecie(string Specie);
        Task<PokemonInfo?> GetByName(string Name);
        Task<List<PokemonListItem>?> GetByType(string Type);
    }    
}
    