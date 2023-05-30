
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThunderBank.Models
{

    public class ApplicationRole : IdentityRole
    {
        // Puedes agregar propiedades adicionales si es necesario
        // ...

        public ApplicationRole() : base()
        {
        }

        public ApplicationRole(string roleName) : base(roleName)
        {
        }
    }
}
