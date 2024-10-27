using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Use_Cases.Commands
{
    public class UpdateShoppingCartCommand : IRequest<Guid>
    {
        public Guid Id { get; set; } 
        public Guid UserId { get; set; } 
        public decimal TotalPrice { get; set; } 
        public bool IsEmpty { get; set; } 
    }
}
