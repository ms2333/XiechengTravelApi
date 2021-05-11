using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XiechengTravel.Helper;
using XiechengTravel.Models;

namespace XiechengTravel.Services
{
    public interface ITouristRouteRepository
    {
        Task<PaginationList<TouristRoute>> GetTouristRoutes(string keyword,string operatorType,int ratingValue, int currentPage, int pageSize);//取得所有路线

        TouristRoute GetTouristRoute(Guid TouristRouteId);//根据旅游路线的唯一ID取得路线

        bool TouristRouteExists(Guid touristRouteId);//判断路线是否存在

        Task<IEnumerable<TouristRoutePicture>> GetPicturesByTouristRouteId(Guid TouristRouteId);//根据路线Id获取路线图片组

        TouristRoutePicture GetPicture(int picId);//获取单个路线图片

        void AddTouristRoute(TouristRoute touristRoute);//创建新的路线

        void AddTouristRoutePicture(Guid TouristRouteId,TouristRoutePicture touristRoutePicture);//添加子资源

        void DeleteTouristRoute(Guid TouristRouteId);

        void DeleteTouristRoutes(IEnumerable<TouristRoute> TouristTouteId);

        Task<IEnumerable<TouristRoute>> GetTouristRoutesByList(IList<Guid> guids);

        Task<ShoppingCart> GetShoppingCartsByUserId(string UserId);

        Task CreateShoppingCart(ShoppingCart shoppingCart);

        Task AddShoppingCartItems(LineItem lineItem);

        void DeleteShoppingCartItem(LineItem lineItem);

        void DeleteShoppingCartItems(IEnumerable<LineItem> lineItems);

        Task AddOrderAsync(Order order);

        Task<IEnumerable<Order>> GetOrdersByUserId(string userId);

        Task<Order> GetOrderInfo(Guid OrderId);

        Task deleteOrder(Guid OrderId);

        void deleteAllOrders(string UserId);

        Task<bool> Save();//返回保存状态是否成功



    }
}
