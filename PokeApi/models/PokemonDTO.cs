using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PokeApi.models
{
    public class PokemonPageDTO
    {
        [JsonPropertyName("count")]
        public int Count { get; set; }

        [JsonPropertyName("next")]
        public string Next { get; set; }

        [JsonPropertyName("previous")]
        public string Previous { get; set; }

        [JsonPropertyName("results")]
        public List<PokemonListItem> Items { get; set; }
    }

    public class PokemonDataDTO
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("types")]
        public List<TypesDTO> Types { get; set; }
        [JsonPropertyName("weight")]
        public int Weight { get; set; }
        [JsonPropertyName("height")]
        public int Height { get; set; }
        public List<StatDTO> Stats { get; set; }
    }
    public class StatDTO
    {
        [JsonPropertyName("base_stat")]
        public int BaseStat { get; set; }
        [JsonPropertyName("effort")]
        public int Effort { get; set; }
        [JsonPropertyName("stat")]
        public PokemonListItem Stat { get; set; }
    }

    public class TypesDTO
    {
        [JsonPropertyName("type")]
        public PokemonListItem Type { get; set; }
        [JsonPropertyName("slot")]
        public int Slot { get; set; }
    }

    public class PokemonDTO
    {
        [JsonPropertyName("pokemon")]
        public PokemonListItem Pokemon { get; set; }
        [JsonPropertyName("slot")]
        public int Slot { get; set; }
    }

    public class PokemonTypesDTO
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("pokemon")]
        public List<PokemonDTO> Pokemons { get; set; }
    }
}

