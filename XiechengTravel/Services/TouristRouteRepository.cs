using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XiechengTravel.Database;
using XiechengTravel.Models;
using XiechengTravel.Helper;

namespace XiechengTravel.Services
{
    public class TouristRouteRepository : ITouristRouteRepository
    {
        private readonly AppDbContext _context;
        public TouristRouteRepository(AppDbContext appDbContext)
        {
            this._context = appDbContext;
        }

        public async Task AddOrderAsync(Order order)
        {
          await  _context.Orders.AddAsync(order);
        }

        public async Task AddShoppingCartItems(LineItem lineItem)
        {
             await _context.LineItems.AddAsync(lineItem);
        }

        public void AddTouristRoute(TouristRoute touristRoute)
        {
           _context.Add(touristRoute);
           // _context.SaveChangesAsync();
        }

        public void AddTouristRoutePicture(Guid TouristRouteId, TouristRoutePicture touristRoutePicture)
        {
            if (TouristRouteId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(TouristRouteId));
            }
            if (touristRoutePicture==null)
            {
                throw new ArgumentNullException(nameof(touristRoutePicture));
            }
            touristRoutePicture.TouristRouteId = TouristRouteId;
            _context.TouristRoutePictures.Add(touristRoutePicture);
        }

        public async Task  CreateShoppingCart(ShoppingCart shoppingCart)
        {
            await _context.ShoppingCarts.AddAsync(shoppingCart);
        }

        public async void deleteAllOrders(string UserId)
        {
            var orders = await _context.Orders.Where(x => x.UserId == UserId).ToListAsync();
            _context.RemoveRange(orders);
            
        }

        public async  Task deleteOrder(Guid OrderId)
        {
            var order = await _context.Orders.Where(x => x.Id == OrderId).FirstOrDefaultAsync();
            _context.Orders.Remove(order);
        }

        public void DeleteShoppingCartItem(LineItem lineItem)
        {
             _context.LineItems.Remove(lineItem);
        }

        public void DeleteShoppingCartItems(IEnumerable<LineItem> lineItems)
        {
            _context.LineItems.RemoveRange(lineItems);
        }

        public async void DeleteTouristRoute(Guid TouristRouteId)
        {
            var T = await _context.TouristRoutes.Where(x => x.Id == TouristRouteId).FirstOrDefaultAsync();
            _context.TouristRoutes.Remove(T);
        }

        public void DeleteTouristRoutes(IEnumerable<TouristRoute> TouristTouteIds)
        {
            _context.TouristRoutes.RemoveRange(TouristTouteIds);
        }

        public async Task<Order> GetOrderInfo(Guid OrderId)
        {
           return await _context.Orders.Include(x => x.OrderLineitems)
                     .Where(x => x.Id == OrderId)
                     .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Order>> GetOrdersByUserId(string userId)
        {
           return  await _context.Orders.Where(x => x.UserId == userId).ToListAsync();
        }

        public TouristRoutePicture GetPicture(int picId)
        {
           return _context.TouristRoutePictures.FirstOrDefault(x => x.Id == picId);
        }

        public async Task<IEnumerable<TouristRoutePicture>> GetPicturesByTouristRouteId(Guid TouristRouteId)
        {
           return await _context.TouristRoutePictures.Where(x => x.TouristRouteId == TouristRouteId).ToListAsync();
        }

        public async Task<ShoppingCart> GetShoppingCartsByUserId(String UserId)
        {
            return await _context.ShoppingCarts
                .Include(x => x.ShopingCartItems)
                .ThenInclude(x => x.TouristRoute)
                .Where(x => x.userId == UserId)
                .FirstOrDefaultAsync();
        }

        public TouristRoute GetTouristRoute(Guid TouristRouteId)
        {
            return _context.TouristRoutes.Include(x=>x.TouristRoutePictures).FirstOrDefault(t => t.Id == TouristRouteId);
        }

        public async Task<PaginationList<TouristRoute>> GetTouristRoutes(
            string keyword, string operatorType, int ratingValue,
            int currentPage,int pageSize
            )
        {
            IQueryable<TouristRoute> Queryable = _context.TouristRoutes.Include(x => x.TouristRoutePictures);
            //Include就是efcore中连接两张表的方法
            if (!String.IsNullOrWhiteSpace(keyword))
            {
               Queryable = Queryable.Where(x => x.Tittle.Contains(keyword));
            }
            //ratingVlaue>=0的时候才需要过滤
            if (ratingValue >= 0 && operatorType != null)
            {
                switch (operatorType)
                {
                    case "lessThan": Queryable = Queryable.Where(x => x.Rating <= ratingValue);break;
                    case "largerThan": Queryable = Queryable.Where(x => x.Rating >= ratingValue);break;
                    case "equalThan": Queryable = Queryable.Where(x => x.Rating == ratingValue);break;
                    default:
                        break;
                }
                return await PaginationList<TouristRoute>.createPageAsync(currentPage, pageSize, Queryable);
            }
            return await PaginationList<TouristRoute>.createPageAsync(currentPage, pageSize, Queryable);
        }

        public async Task<IEnumerable<TouristRoute>> GetTouristRoutesByList(IList<Guid> guids)
        {
            var TouristRoutes = await _context.TouristRoutes.Where(x => guids.Contains(x.Id)).ToListAsync();
            return TouristRoutes;
        }

        public async Task<bool> Save()
        {
            return (await _context.SaveChangesAsync())>0;
        }

        public bool TouristRouteExists(Guid touristRouteId)
        {
            return _context.TouristRoutes.Any(x => x.Id == touristRouteId);//返回对象是否存在
        }

    }
}
