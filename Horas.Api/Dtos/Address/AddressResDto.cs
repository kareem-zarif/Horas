﻿namespace Horas.Api.Dtos.Address
{
    public class AddressResDto
    {
        public Guid Id { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; } 
        public Guid PersonId { get; set; }
    }
}
