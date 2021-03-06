﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEAS
{
    public class Cart
    {
        private List<CartLine> _cartLines = new List<CartLine>();

        public void AddLine(Product product,int quantity)
        {
            var exists = this._cartLines.FirstOrDefault(x => x.Product.Equals(product));
            if (exists != null)
                exists.Quantity += quantity;
            else
                this._cartLines.Add(new CartLine { Product = product, Quantity = quantity });
        }

        public void RemoveLine(Product product)
        {
            this._cartLines.RemoveAll(x => x.Product.Equals(product));
        }

        public IEnumerable<CartLine> Lines
        {
            get { return this._cartLines; }
        }

        public decimal ComputeTotalValue()
        {
            decimal result = 0;
            this.Lines.ToList().ForEach(x => result += x.Quantity * x.Product.Price);
            return result;
        } 
    }

    public class CartLine
    {
        public Product Product { get; set; }

        public int Quantity { get; set; }

        public long ProductId
        {
            get
            {
                if (Product == null)
                    return -1;
                return this.Product.Id;
            }
        }
    }
}
