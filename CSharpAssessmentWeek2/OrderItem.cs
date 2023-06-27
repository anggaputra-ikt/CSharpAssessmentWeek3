﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpAssessmentWeek2
{
    public class OrderItem
    {
        public OrderItem()
        {
            Id = Guid.NewGuid().ToString();
        }
        public string Id { get; set; }
        public Product? Product { get; set; }
        public int Quantity { get; set; }
    }
}