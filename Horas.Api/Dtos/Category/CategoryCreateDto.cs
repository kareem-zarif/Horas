﻿

namespace Horas.Api.Dtos.Category
{
    public class CategoryCreateDto 
    {
        [Required, MaxLength(100)]
        public string Name { get; set; }
        [Required, MaxLength(500)]
        public string Description { get; set; }


    }
}
