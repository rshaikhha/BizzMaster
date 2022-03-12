using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;

namespace API.Extensions
{
    public static class ProductExtensions
    {

        public static IQueryable<Product> Sort(this IQueryable<Product> query, string orderBy)
        {
            if (string.IsNullOrWhiteSpace(orderBy)) return query.OrderBy(p => p.Order);

            query = orderBy switch
            {
                "default" => query.OrderBy(p => p.Order),
                "PartNumber" => query.OrderBy(p => p.PartNumber),
                "Title" => query.OrderBy(p => p.Title),
                _ => query.OrderBy(x => x.Order)
            };

            return query;
        }

        public static IQueryable<Product> Search(this IQueryable<Product> query, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm)) return query;

            var lowerCaseSearchTerm = searchTerm.Trim().ToLower();

            return query.Where(p => (p.Description + "**" + p.Title).ToLower().Contains(lowerCaseSearchTerm));
        }

        public static IQueryable<Product> Filter(this IQueryable<Product> query, string brands, string categories)
        {

            var brandList = new List<int>();
            var catList = new List<int>();

            if (!string.IsNullOrWhiteSpace(brands))
                brandList.AddRange(brands.Split(',').Select(x=>int.Parse(x)));
            if (!string.IsNullOrWhiteSpace(categories))
                catList.AddRange(categories.Split(',').Select(x=>int.Parse(x)));


            query = query.Where(p => brandList.Count == 0 || brandList.Contains(p.BrandId));
            query = query.Where(p => catList.Count == 0 || catList.Contains(p.CategoryId));

            return query;
        }
        
    }
}