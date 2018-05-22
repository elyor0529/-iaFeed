using ClosedXML.Excel;
using KiaFeed.Parser.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace KiaFeed.Parser.Helpers
{
    public class ExcelHelper
    {
        public static byte[] ConvertToBytes(IEnumerable<KiaFeedModel> items)
        {
            using (var ms = new MemoryStream())
            {
                var wb = new XLWorkbook();

                foreach (var group in items.GroupBy(g => g.Condition))
                {
                    var ws = wb.Worksheets.Add(group.Key.ToUpperInvariant());

                    ws.Cell(1, 1).Value = "Kia model";
                    ws.Cell(1, 2).Value = "Year";
                    ws.Cell(1, 3).Value = "Car category";
                    ws.Cell(1, 4).Value = "Equipment type";
                    ws.Cell(1, 5).Value = "Drivetrain type";
                    ws.Cell(1, 6).Value = "Body type";
                    ws.Cell(1, 7).Value = "Engine";
                    ws.Cell(1, 8).Value = "Fuel type";
                    ws.Cell(1, 9).Value = "MPG";
                    ws.Cell(1, 10).Value = "Transmission";
                    ws.Cell(1, 11).Value = "Exterior color";
                    ws.Cell(1, 12).Value = "Interior color";
                    ws.Cell(1, 13).Value = "Sale price";
                    ws.Cell(1, 14).Value = "Model URL";
                    ws.Cell(1, 15).Value = "Image Url";

                    for (int i = 2; i < group.Count(); i++)
                    {
                        var item = group.ElementAt(i);

                        ws.Cell(i, 1).Value = item.KiaModel;
                        ws.Cell(i, 2).Value = item.Year;
                        ws.Cell(i, 3).Value = item.CarCategory;
                        ws.Cell(i, 4).Value = item.EquipmentType;
                        ws.Cell(i, 5).Value = item.DrivetrainType;
                        ws.Cell(i, 6).Value = item.BodyType;
                        ws.Cell(i, 7).Value = item.Engine;
                        ws.Cell(i, 8).Value = item.FuelType;
                        ws.Cell(i, 9).Value = item.MPG;
                        ws.Cell(i, 10).Value = item.Transmission;
                        ws.Cell(i, 11).Value = item.ExteriorColor;
                        ws.Cell(i, 12).Value = item.InteriorColor;
                        ws.Cell(i, 13).Value = item.SalePrice;
                        ws.Cell(i, 14).Value = item.ModelUrl;
                        ws.Cell(i, 15).Value = item.ImageUrl;
                    }

                }

                wb.SaveAs(ms);
                ms.Position = 0;

                return ms.ToArray();
            }
        }
    }
}