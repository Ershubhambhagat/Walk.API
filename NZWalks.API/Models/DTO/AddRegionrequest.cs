﻿namespace NZWalks.API.Models.DTO
{
    public class AddRegionrequest
    {
        public string Code2 { get; set; }
        public string Name { get; set; }
        public double Area { get; set; }
        public double Lat { get; set; }
        public double Long { get; set; }
        public long Population { get; set; }
    }
}
