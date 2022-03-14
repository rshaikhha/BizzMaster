using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace API.Entities
{

    public class CommercialCard : BaseEntity
    {
        public string Description { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ValidityDate {get; set;}

        public string FirstName { get; set; }
        public string LastName { get; set; }

    }

    public class OrderRegistration : BaseEntity
    {

        public int CommercialCardId {get; set;}
        public CommercialCard CommercialCard {get; set;}


        public string DocumentNumber {get; set;}
        public string RegistrationNumber { get; set; }

        public string Currency {get; set;}
        public int Amount { get; set; }

        public string Unit {get; set;}

        public int Quantity {get; set;}

        public virtual List<Category> Categories {get; set;}

        public DateTime IssueDate { get; set; }

        public DateTime ValidityDate { get; set; }

        public OrderRegistrationStatus OrderRegistrationStatus { get; set;}

        

    }

    public enum OrderRegistrationStatus {
        requested = 1,
        registered = 2,
    }
}