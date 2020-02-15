using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using WebApplication1.Models.ViewModels;

namespace WebApplication1.Models
{
    public class ProductModel
    {
        public int Id { get; set; }

        [Display(Name = "Введите цену")]
        [Required(ErrorMessage = "Не указана цена")]
        [Range(1, 1000000, ErrorMessage = "Недопустимая цена")]
        public uint Price { get; set; }

        [Display(Name = "Введите название товара")]
        [Required(ErrorMessage = "Не указано название")]
        public string Name { get; set; }

        [Display(Name = "Введите Url картинки")]
        [DataType(DataType.ImageUrl)]
        public string ImgURL { get; set; }

        [Display(Name = "Введите описание товара")]
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Не указано описание товара")]
        public string Description { get; set; }

        public RateModel Rate { get; set; }

        public ProductModel(string name, uint price, string description, string imgUrl)
        {
            Price = price;
            Name = name;
            ImgURL = imgUrl;
            Description = description;
        }

        public ProductModel(int id, string name, uint price, string description, string imgUrl)
        {
            Id = id;
            Price = price;
            Name = name;
            ImgURL = imgUrl;
            Description = description;
        }

        public ProductModel() {}

        public float GetRate()
        {
            float result = 0;

            if (Rate != null && Rate.Votes != 0)
            {
                result = (float)Math.Round((float)Rate.Stars / (float)Rate.Votes, 1);
            }

            return result;
        }
    }
}