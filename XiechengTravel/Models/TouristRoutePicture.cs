using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace XiechengTravel.Models
{
    //旅游路线照片类
    //关系：一个旅游路线对应多个路由旅游路线照片（一对多的关系）
    public class TouristRoutePicture
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]//使用数据库的自增类型
        public int Id { get; set; }//主键
        [MaxLength(100)]
        public string Url { get; set; }
        //这里用到了创建数据库的规则：
        //一对多的关系中，在多的一方创建表添加一的一方作为外键
        [ForeignKey("TouristRouteId")]//TouristRoute表+Id字段
        public Guid TouristRouteId { get; set; }//外键
        public TouristRoute TouristRoute { get; set; }//链接属性
    }
}
