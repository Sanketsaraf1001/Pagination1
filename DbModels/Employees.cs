﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DataTablesInDotNetCore.DbModels
{
    public class Product
    {
        [Key]
        public int ProductId
        {
            get;
            set;
        }
        public string ProductName
        {
            get;
            set;
        }
        public int CategoryId
        {
            get;
            set;
        }
        public string CategoryName
        {
            get;
            set;
        }
    }
}
