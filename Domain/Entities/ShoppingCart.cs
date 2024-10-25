namespace Domain.Entities
{
    public class ShoppingCart
    {
        public Guid Id { get; set; } // Identificator unic pentru cos
        public Guid UserId { get; set; } // Identificator unic al utilizatorului caruia ii apartine cosul
        public DateTime CreatedAt { get; set; } // Data crearii cosului
        public decimal TotalPrice { get; set; } // Pretul total al produselor din cos
        public bool IsEmpty { get; set; } // Variabila care indica daca cosul este gol
    }
}
