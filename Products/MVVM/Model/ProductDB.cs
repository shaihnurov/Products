namespace Products.MVVM.Model
{
    public class ProductsDB
    {
        public int Id { get; set; }
        public UClassType UClass { get; set; }
        public string? Name { get; set; }
        public float Proteins { get; set; }
        public float Fats { get; set; }
        public float Carbohy { get; set; }
        public double Kilocalories { get; set; }
    }

    public enum UClassType
    {
        Крупа,
        Хлеб,
        Сладости,
        Фрукты,
        Овощи,
        Напитки,
        Молочка,
        Мясо,
        Прочее,
    }
}
