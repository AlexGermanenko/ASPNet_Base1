﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Controllers;

namespace WebApplication1.Models.Data
{
    public static class DBInit
    {
        public static void Init(AppDBContext context)
        {
            if (!context.Product.Any())
            {
                context.Product.AddRange(GetGoodsList());
                context.SaveChanges();
            }

            if (!context.Rate.Any())
            {
                context.Rate.AddRange(GetRateList());
                context.SaveChanges();
            }
        }

        private static List<RateModel> GetRateList()
        {
            List<RateModel> rateList = new List<RateModel>();

            foreach (Product product in GetGoodsList())
            {
                rateList.Add(new RateModel(product.Id, 0, 0,product));
            }

            return rateList;
        }

        private static List<Product> GetGoodsList()
        {
            List<Product> goodsList = new List<Product>()
                {
                    new Product("19.5\" Моноблок Lenovo Ideacentre AIO 330-20IGM [F0D70048RK]", 19999,
                        "Intel Celeron J4005, 2x2000 МГц, VA, 1440x900, 4 ГБ DDR4, HDD 500 ГБ, Windows 10 Домашняя",
                        "https://c.dns-shop.ru/thumb/st4/fit/320/250/045c4bbcbd6ec022308fa59b4c484802/a6a64cf8e2dd9d005f7b7eb5ebfb8de635d49d8d201ddabd536494fdf0859eeb.jpg"),
                    new Product("21.5\" Моноблок Acer Aspire C22-820 [DQ.BCKER.012]", 19999,
                        "Intel Celeron J4005, 2x2000 МГц, IPS, 1920x1080, 4 ГБ DDR4, HDD 500 ГБ, Linux",
                        "https://c.dns-shop.ru/thumb/st1/fit/320/250/0e7c6e779e68ea2709a025f71e7c8cd6/3d21b73512ea573646d6bcc310cfccb88460052630a011b0c64c5649550ca957.jpg"),
                    new Product( "27\" Моноблок Apple iMac Retina 5K [MRQY2RU/A]", 139999,
                        "Intel Core i5, 6x3000 МГц, IPS, 5120×2880, 8 ГБ DDR4, HDD 1 ТБ, Radeon Pro 570x, Mac OS X",
                        "https://c.dns-shop.ru/thumb/st4/fit/320/250/9ea62774b47fa86e44e46e8b1f4e7021/8e384cd8850391022f698b756a1c26422d5f30d5f9efb3299ce3443acfdc62e0.png"),
                    new Product("21.5\" Моноблок Acer Aspire C22-820 DQ.BCMER.001", 30999,
                        "Intel Pentium J5005, 4x1500 МГц, IPS, 1920x1080, 4 ГБ DDR4, HDD 1 ТБ, без ОС",
                        "https://c.dns-shop.ru/thumb/st4/fit/320/250/bb7d7713a5a387d416532aa893a4a593/27302e4215ad547defd940d58bb6e06ac3913c96eda227dfae10a3058c73858e.jpg"),
                    new Product("21.5\" Моноблок Apple iMac MMQA2RU/A [MMQA2RU/A]", 79999,
                        "Intel Core i5 7360U, 2x2300 МГц, IPS, 1920x1080, 8 ГБ DDR4, HDD 1 ТБ, Mac OS X",
                        "https://c.dns-shop.ru/thumb/st4/fit/320/250/ffac6c3e5173da5964ac793975a25835/724150cd0e07359cd03165d9ceef699e55e7a1990216b6dff7a5c992eb466294.jpg"),
                    new Product("21.5\" Моноблок Lenovo 520-22AST [F0D6000JRK]", 26999,
                        "AMD A6 9220, 2x2500 МГц, IPS, 1920x1080, 4 ГБ DDR4, HDD 1 ТБ, Windows 10 Домашняя",
                        "https://c.dns-shop.ru/thumb/st4/fit/320/250/6c27d6c2cf672aec6315494e7a3f9303/b8d85b42985dd529ebb3dbc0c801b11a470993ad98008b3a2663b20530d57501.jpg"),
                    new Product("19.5\" Моноблок HP 20-c409ur [4RK48EA]", 20999,
                        "AMD E2 9000, 2x1800 МГц, VA, 1920x1080, 4 ГБ DDR4, HDD 500 ГБ, DVD±RW, клавиатура, мышь, без ОС",
                        "https://c.dns-shop.ru/thumb/st1/fit/320/250/4e3e8e4c722c730c9accabbe1e47c661/fcb4859c38add6484f7358919f1a678c8a0d298a9f40a2944e291b521fb6ac78.jpg"),
                    new Product("21.5\" Моноблок Lenovo IdeaCentre A340-22IGM [F0EA0020RK]", 26999,
                        "Intel Celeron J4005, 2x2000 МГц, IPS, 1920x1080, 4 ГБ DDR4, HDD 1 ТБ, клавиатура, мышь, Windows 10 Домашняя",
                        "https://c.dns-shop.ru/thumb/st1/fit/320/250/2ed3beac268963f4109f64dff3bc9340/fe05f2dbf43f178594f046f795295fdedc12e99d1d17cd4e9a707c0761edfb8d.jpg"),
                    new Product("23.8\" Моноблок Acer Aspire C24-860 [DQ.BACER.004]", 36899,
                        "Intel Core i3 7130U, 2x2700 МГц, IPS, 1920x1080, 4 ГБ DDR4, HDD 1 ТБ, клавиатура, мышь, Windows 10 Домашняя",
                        "https://c.dns-shop.ru/thumb/st4/fit/320/250/cf08430a32b16b1fed4de1f9acea735e/1108bb2b96eddaf65a43ac4db7f2a3fde69e702013e82bad67bc2a79b91bc8fd.jpg"),
                    new Product("27\" Моноблок Dell Inspiron 7777 [7777-6566]", 116499,
                        "Intel Core i7-8700T, 6x2400 МГц, IPS, 1920x1080, 16 ГБ DDR4, HDD 1 ТБ, SSD 256 ГБ, GeForce GTX 1050, Linux",
                        "https://c.dns-shop.ru/thumb/st1/fit/320/250/d30ab963ab4563f629e670f44cd96254/dc3ce0ccd3828cf301ae1847ee8c0db7a2e823d42cdd11d2669715dc73527a6b.jpg")
                };

            return goodsList;
        }
    }
}
