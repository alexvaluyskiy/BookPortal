﻿namespace BookPortal.Web.Domain.Models
{
    // TODO: add a field: Order
    public class PersonWork
    {
        public int Id { get; set; }

        public WorkPersonType Type { get; set; }

        public int PersonId { get; set; }
        public Person Person { get; set; }

        public int WorkId { get; set; }
        public Work Work { get; set; }
    }
}