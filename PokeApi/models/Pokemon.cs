namespace PokeApi.models
{
    public class PokemonListItem
    {
        public string Name { get; set; }

        public PokemonListItem(string name)
        {
            Name = name;
        }

    }

    public class PokemonInfo
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public int Weight { get; set; }
        public int Height { get; set; }
        public int Hp { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int Speed { get; set; }
        public int SpAttack { get; set; }
        public int SpDefense { get; set; }
    }

}
