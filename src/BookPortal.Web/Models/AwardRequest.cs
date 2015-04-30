using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;

namespace BookPortal.Web.Models
{
    public class AwardRequest
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
