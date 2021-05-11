using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace XiechengTravel.Dtos
{
    public class TouristRoutePictureDto
    {
        public int Id { get; set; }//主键
        public string Url { get; set; }
        public Guid TouristRouteId { get; set; }//外键
    }
}
