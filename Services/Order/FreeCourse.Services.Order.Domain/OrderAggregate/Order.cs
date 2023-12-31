﻿using FreeCourse.Services.Order.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeCourse.Services.Order.Domain.OrderAggregate
{
    //EF Core features
    // -- Owned Types
    // -- Shadow Property
    // -- Backing Field
    public class Order : Entity,IAggregateRoot
    {
        public DateTime CreatedDate { get; private set; }

        public Address Address { get; private set; }

        public string BuyerId { get; private set; }

        private readonly List<OrderItem> _orderItems;//Backing fields amaç encapculating i arttırmak. Dışarıdan kimse _orderItems' a veri ekleyemesin

        public IReadOnlyCollection<OrderItem> OrderItems => _orderItems; //Burada encapculating yapıldı

        public Order()//Default olarak bu ctor u yazmamız gerekiyor
        {

        }
        public Order(string buyerId, Address address)
        {
            _orderItems = new List<OrderItem>();
            CreatedDate = DateTime.Now;
            BuyerId = buyerId;
            Address = address;
        }

        public void AddOrderItem(string productId,string productName,decimal price, string pictureUrl ) 
        {
            var existProduct = _orderItems.Any(x => x.ProductId == productId);

            if (!existProduct)
            {
                var newOrderItem = new OrderItem(productId, productName, pictureUrl, price);
                _orderItems.Add(newOrderItem);
            }
        }
    
        public decimal GetTotalPrice => _orderItems.Sum(x => x.Price); //BU bir property sadece lambda kullandık get i var yani

    }
}
