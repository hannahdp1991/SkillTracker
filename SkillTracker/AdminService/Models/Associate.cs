using DataAnnotationsExtensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdminService.Models
{
    public class Associate
    {
        public string Name { get; set; }

        public int AssociateId { get; set; }

        public string Email { get; set; }

        public int Mobile { get; set; }
    }
}
