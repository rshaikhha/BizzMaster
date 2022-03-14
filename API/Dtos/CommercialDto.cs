using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace API.Dtos
{
    public class CommercialCardDto
    {
        public int Id {get; set;}
        public string Title {get; set;}
        public string Description { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ValidityDate {get; set;}

        public string IDate { get => IssueDate.ToShortDateString(); }
        public string VDate { get => ValidityDate.ToShortDateString(); }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string FullName { get => string.Format("{0} {1}", this.FirstName, this.LastName);}
    }

    public class OrderRegistrationDto
    {
        public int Id {get; set;}
        public string Title {get; set;}

        public int CommercialCardId {get; set;}
        public string CommercialCardTitle {get; set;}

        public string DocumentNumber {get; set;}
        public string RegistrationNumber { get; set; }

        public string Currency {get; set;}
        public int Amount { get; set; }

        public string Unit {get; set;}

        public int Quantity {get; set;}

        public string Categories {get; set;}

        public DateTime IssueDate { get; set; }

        public DateTime ValidityDate { get; set; }

        public string IDate { get => IssueDate.ToShortDateString(); }
        public string VDate { get => ValidityDate.ToShortDateString(); }


        public string OrderRegistrationStatus { get; set;}



        

    }
}