using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class ShoppingCartDto
    {
        public Guid Id { get; set; } // Identificator unic pentru coș
        public Guid UserId { get; set; } // Identificator unic al utilizatorului căruia îi aparține coșul
        public DateTime CreatedAt { get; set; } // Data creării coșului
        public decimal TotalPrice { get; set; } // Prețul total al produselor din coș
        public bool IsEmpty { get; set; } // Variabilă care indică dacă coșul este gol
    }
}
