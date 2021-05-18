namespace dotnet.rpg.Models
{
    public class Character
    {
        public int Id { get; set; }

        public string Name { get; set; } = "Ahmet";

        public int HitPoints { get; set; } = 100;

        public int Strength { get; set; } = 10 ;

        public int Deffense { get; set; } = 10 ;

        public int Intelligence { get; set; } = 10 ;

        public RPGClass Class { get; set; } = RPGClass.Knight;

    }
}