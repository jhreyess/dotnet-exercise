using PokeApi.models;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace PokeApi.service
{
    class Service : PokemonService
    {
        private readonly HttpClient client;
        public Service(HttpClient client)
        {
            this.client = client;
        }

        async Task<PokemonPageDTO?> PokemonService.GetAllFromPage(string page)
        {
            var response = await client.GetAsync(page);
            if(response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<PokemonPageDTO>();
                return result;
            }
            return null;
        }
        async Task<PokemonPageDTO?> PokemonService.GetAllTypes()
        {
            var response = await client.GetAsync("type");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<PokemonPageDTO>();
                return result;
            }
            return null;
        }

        async Task<PokemonPageDTO?> PokemonService.GetBySpecie(string Specie)
        {
            var response = await client.GetAsync(Specie);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<PokemonPageDTO>();
                return result;
            }
            return null;
        }

        async Task<PokemonInfo?> PokemonService.GetByName(string Name)
        {
            var response = await client.GetAsync("pokemon/"+Name);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<PokemonDataDTO>();
                PokemonInfo res = new PokemonInfo
                {
                    Name = result?.Name ?? "unknown",
                    Type = result?.Types?.FirstOrDefault()?.Type.Name ?? "unknown",
                    Weight = result?.Weight ?? 0,
                    Height = result?.Height ?? 0,
                    Hp = result.Stats.ElementAt(0).BaseStat,
                    Attack = result.Stats.ElementAt(1).BaseStat,
                    Defense = result.Stats.ElementAt(2).BaseStat,
                    SpAttack = result.Stats.ElementAt(3).BaseStat,
                    SpDefense = result.Stats.ElementAt(4).BaseStat,
                    Speed = result.Stats.ElementAt(5).BaseStat,

                };
                return res;
            }
            return null;
        }

        async Task<List<PokemonListItem>?> PokemonService.GetByType(string Type)
        {
            var response = await client.GetAsync("type/"+Type);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<PokemonTypesDTO>();
                List<PokemonListItem>? mapped = result?.Pokemons.Select(it => it.Pokemon).ToList();
                return mapped ?? new List<PokemonListItem>();
            }
            return null;
        }
    }
}
