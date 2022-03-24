using Core.Entities.Base;
using Microsoft.AspNetCore.Identity;

namespace Core.Entities
{
    public class Role : IdentityRole, IEntity
    {
        public string Description { get; set; }
    }
}