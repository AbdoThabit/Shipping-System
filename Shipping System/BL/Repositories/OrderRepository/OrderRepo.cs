using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.Operations;
using Microsoft.EntityFrameworkCore;
using Shipping_System.BL.Repositories.BranchRepository;
using Shipping_System.BL.Repositories.CityRepository;
using Shipping_System.BL.Repositories.RepresentativeRepository;
using Shipping_System.BL.Repositories.ShippingSettingRepository;
using Shipping_System.BL.Repositories.VillageSettingsRepository;
using Shipping_System.BL.Repositories.WeightSettingsRepository;
using Shipping_System.DAL.Database;
using Shipping_System.DAL.Entites;
using Shipping_System.ViewModels;

namespace Shipping_System.BL.Repositories.OrderRepo
{
    public class OrderRepo : IOrderRepo
    {
        private readonly Context _Context;
        private readonly UserManager<ApplicationUser> _UserManager;
        private readonly IVillageSettingRepoe _VillageRepo;
        private readonly IShippingSettingRepo _ShippingRepo;
        private readonly IWeightSettingsRepo _WeightRepo;
        private readonly ICityRepo _CityRepo;
        private readonly IBranchRepo _BranchRepo;
        private readonly IRepresentativeRepo _RepresentiveRepo;


        public OrderRepo(Context context, IVillageSettingRepoe villageRepo, IShippingSettingRepo shippingRepo, UserManager<ApplicationUser> userManager, IWeightSettingsRepo weightRepo, ICityRepo cityRepo)
        {
            _Context = context;
            _VillageRepo = villageRepo;
            _ShippingRepo = shippingRepo;
            _UserManager = userManager;
            _WeightRepo = weightRepo;
            _CityRepo = cityRepo;

        }

        public async Task<int> Add(OrderVM Order)
        {
            decimal ShippingCost = await ShhipinlPrice(Order.Village_Flag, Order.ShippingSetting_Id, Order.Products, Order.City_Id);
            decimal costAllProducts = await Cost_AllProducts(Order.Products);
            double countWeight = await CountWeight(Order.Products);


            Order order = new Order()
            {
                Client_Name = Order.Client_Name,
                FristPhoneNumber = Order.FristPhoneNumber,
                SecoundPhoneNumber = Order.SecoundPhoneNumber,
                Email = Order.Email,
                Address = Order.Address,
                Village_Name = Order.Village_Name,
                Governate_Id = Order.Governate_Id,
                City_Id = Order.City_Id,
                Village_Flag = Order.Village_Flag,
                ShippingSetting_Id = Order.ShippingSetting_Id,
                Payment_Type = Order.Payment_Type,
                Branch_Id = Order.Branch_Id,
                Status_Id = 1,
                Order_Date = DateTime.Now,
                Notes = Order.Notes,
                Representitive_Id = Order.Representitive_Id,
                Trader_Id = Order.Trader_Id,
                Products_Total_Cost = costAllProducts,
                Shipping_Total_Cost = ShippingCost,
                Total_weight = (int)countWeight,
                Products = Order.Products.Select(prod => new Product
                {
                    Name = prod.Name,
                    Qunatity = prod.Qunatity,
                    Price = prod.Price,
                    Weight = prod.Weight,
                }).ToList(),
            };


            await _Context.Orders.AddAsync(order);
            await _Context.Products.AddRangeAsync(order.Products.ToList());
            var result = await _Context.SaveChangesAsync();

            return result;


        }

        public async Task<OrderVM> IncludeLists()
        {
            var Lists = new OrderVM
            {
                Governates = await _Context.Governates.ToListAsync(),
                Cities = await _Context.Cities.ToListAsync(),
                Branches = await _Context.Branches.ToListAsync(),
                shippingSettings = await _Context.ShippingSettings.ToListAsync(),
                Representitve = await _UserManager.GetUsersInRoleAsync("مندوب"),
                Traders = await _UserManager.GetUsersInRoleAsync("تاجر"),


            };
            return Lists;
        }

        public async Task<int> Delete(int id)
        {
            var orderDB = await _Context.Orders.FindAsync(id);

            if (orderDB != null)
            {
                _Context.Entry(orderDB).State = EntityState.Deleted;


                return await _Context.SaveChangesAsync();
            }
            else
            {
                return 0;
            }
        }

        public async Task<List<OrderVM>> GetAll()
        {
            List<OrderVM> orders = await _Context.Orders.Select(Order => new OrderVM
            {
                Id = Order.Id,
                Client_Name = Order.Client_Name,
                FristPhoneNumber = Order.FristPhoneNumber,
                SecoundPhoneNumber = Order.SecoundPhoneNumber,
                Email = Order.Email,
                Address = Order.Address,
                Village_Name = Order.Village_Name,
                Governate_Id = Order.Governate_Id,
                City_Id = Order.City_Id,
                Village_Flag = Order.Village_Flag,
                ShippingSetting_Id = Order.ShippingSetting_Id,
                Payment_Type = Order.Payment_Type,
                Branch_Id = Order.Branch_Id,
                Status_Id = Order.Status_Id,
                Order_Date = Order.Order_Date,
                Notes = Order.Notes,
                Products_Total_Cost = Order.Products_Total_Cost,
                Order_Total_Cost = Order.Order_Total_Cost,
                Total_weight = Order.Total_weight,
                GovernateName = Order.Governate.Name,
                CityName = Order.City.Name,
                BranchName = Order.Branch.Name,
                RepresntiveName = Order.Representitive.FullName,
                TraderName = Order.Trader.FullName,
                statusName = Order.Status.Name,
                Products = Order.Products.Select(prod => new Product
                {
                    Name = prod.Name,
                    Qunatity = prod.Qunatity,
                    Price = prod.Price,
                    Weight = prod.Weight,
                }).ToList()
            }).ToListAsync();
            return orders;
        }
        public async Task<List<OrderVM>> GetTraderOrders(string Trader_UserName)
        {
            List<OrderVM> orders = await _Context.Orders.Where(O => O.Trader.UserName == Trader_UserName).Select(Order => new OrderVM
            {
                Id = Order.Id,
                Client_Name = Order.Client_Name,
                FristPhoneNumber = Order.FristPhoneNumber,
                SecoundPhoneNumber = Order.SecoundPhoneNumber,
                Email = Order.Email,
                Address = Order.Address,
                Village_Name = Order.Village_Name,
                Governate_Id = Order.Governate_Id,
                City_Id = Order.City_Id,
                Village_Flag = Order.Village_Flag,
                ShippingSetting_Id = Order.ShippingSetting_Id,
                Payment_Type = Order.Payment_Type,
                Branch_Id = Order.Branch_Id,
                Status_Id = Order.Status_Id,
                Order_Date = Order.Order_Date,
                Notes = Order.Notes,
                Products_Total_Cost = Order.Products_Total_Cost,
                Order_Total_Cost = Order.Order_Total_Cost,
                Total_weight = Order.Total_weight,
                GovernateName = Order.Governate.Name,
                CityName = Order.City.Name,
                BranchName = Order.Branch.Name,
                RepresntiveName = Order.Representitive.FullName,
                TraderName = Order.Trader.FullName,
                statusName = Order.Status.Name,

                Products = Order.Products.Select(prod => new Product
                {
                    Name = prod.Name,
                    Qunatity = prod.Qunatity,
                    Price = prod.Price,
                    Weight = prod.Weight,
                }).ToList()
            }).ToListAsync();
            return orders;
        }
        public async Task<List<OrderVM>> GetRepresntiveOrders(string Representive_UserName)
        {
            List<OrderVM> orders = await _Context.Orders.Where(O => O.Representitive.UserName == Representive_UserName).Select(Order => new OrderVM
            {
                Id = Order.Id,
                Client_Name = Order.Client_Name,
                FristPhoneNumber = Order.FristPhoneNumber,
                SecoundPhoneNumber = Order.SecoundPhoneNumber,
                Email = Order.Email,
                Address = Order.Address,
                Village_Name = Order.Village_Name,
                Governate_Id = Order.Governate_Id,
                City_Id = Order.City_Id,
                Village_Flag = Order.Village_Flag,
                ShippingSetting_Id = Order.ShippingSetting_Id,
                Payment_Type = Order.Payment_Type,
                Branch_Id = Order.Branch_Id,
                Status_Id = Order.Status_Id,
                Order_Date = Order.Order_Date,
                Notes = Order.Notes,
                Products_Total_Cost = Order.Products_Total_Cost,
                Order_Total_Cost = Order.Order_Total_Cost,
                Total_weight = Order.Total_weight,
                GovernateName = Order.Governate.Name,
                CityName = Order.City.Name,
                BranchName = Order.Branch.Name,
                RepresntiveName = Order.Representitive.FullName,
                TraderName = Order.Trader.FullName,
                statusName = Order.Status.Name,

                Products = Order.Products.Select(prod => new Product
                {
                    Name = prod.Name,
                    Qunatity = prod.Qunatity,
                    Price = prod.Price,
                    Weight = prod.Weight,
                }).ToList()
            }).ToListAsync();
            return orders;
        }
        public async Task<OrderVM> GetById(int orderId)
        {
            var representives = await _UserManager.GetUsersInRoleAsync("مندوب");
            var Order = await _Context.Orders.FindAsync(orderId);
            OrderVM orderVM = new OrderVM()
            {
                Id = Order.Id,
                Client_Name = Order.Client_Name,
                FristPhoneNumber = Order.FristPhoneNumber,
                SecoundPhoneNumber = Order.SecoundPhoneNumber,
                Email = Order.Email,
                Address = Order.Address,
                Village_Name = Order.Village_Name,
                Governate_Id = Order.Governate_Id,
                City_Id = Order.City_Id,
                Village_Flag = Order.Village_Flag,
                ShippingSetting_Id = Order.ShippingSetting_Id,
                Payment_Type = Order.Payment_Type,
                Branch_Id = Order.Branch_Id,
                OrderStatusId = Order.Status_Id,
                Order_Date = Order.Order_Date,
                Notes = Order.Notes,
                Products_Total_Cost = Order.Products_Total_Cost,
                Order_Total_Cost = Order.Order_Total_Cost,
                Total_weight = Order.Total_weight,
                GovernateName = Order.Governate.Name,
                CityName = Order.City.Name,
                BranchName = Order.Branch.Name,
                RepresntiveName = Order.Representitive.FullName,
                TraderName = Order.Trader.FullName,
                statusName = Order.Status.Name,
                ShippingSettingName = Order.ShippingSetting.Shipping_Type,
                Products = Order.Products.Select(prod => new Product
                {
                    Id = prod.Id,
                    Name = prod.Name,
                    Qunatity = prod.Qunatity,
                    Price = prod.Price,
                    Weight = prod.Weight,
                }).ToList(),
                Cities = _Context.Cities.Where(c => c.Governate_Id == Order.Governate_Id).ToList(),
                Branches = _Context.Branches.Where(b => b.City_Id == Order.City_Id).ToList(),
                Representitve = representives.Where(r => r.Branch_Id == Order.Branch_Id).ToList(),
                shippingSettings = _Context.ShippingSettings.ToList(),
                Statuses = _Context.Order_Statuses.ToList(),

            };
            return orderVM;
        }
        public async Task<OrderStatusVM> GetStatus(int orderId)
        {
            var order = await _Context.Orders.FindAsync(orderId);

            OrderStatusVM orderStatusVM = new OrderStatusVM()
            {
                OrderId = order.Id,
                StatusId = order.Status_Id,
                Status = await _Context.Order_Statuses.ToListAsync()
            };
            return orderStatusVM;


        }
        public async Task<StatusCountVM> GetAllStatusCount()
        {
            var orders = await _Context.Orders.ToListAsync();
            var statusCount = new StatusCountVM()
            {
                All_Status_Count = orders.Count(),
                New_Status_Count = orders.Where(o => o.Status.Name == "جديد").Count(),
                Waiting_Status_Count = orders.Where(o => o.Status.Name == "قيد الانتظار").Count(),
                deliveredToRepresentive_Status_Count = orders.Where(o => o.Status.Name == "تم التسليم للمندوب").Count(),
                CantReach_Status_Count = orders.Where(o => o.Status.Name == "لا يمكن الوصول").Count(),
                Suspended_Status_Count = orders.Where(o => o.Status.Name == "تم التاجيل").Count(),
                partlyDelivered_Status_Count = orders.Where(o => o.Status.Name == "تم التسليم جزئيا").Count(),
                CanceledByClient_Status_Count = orders.Where(o => o.Status.Name == "تم الالغاء من قبل المستلم").Count(),
                rejectedWithFullPaying_Status_Count = orders.Where(o => o.Status.Name == "تم الرفض مع الدفع").Count(),
                rejectedWithSomePaying_Status_Count = orders.Where(o => o.Status.Name == "رفض مع سداد جزء").Count(),
                rejectedWithoutPaying_Status_Count = orders.Where(o => o.Status.Name == "رفض و لم يتم الدفع").Count(),
                Delivered_Status_Count = orders.Where(o => o.Status.Name == "تم التوصيل").Count(),


            };
            return statusCount;
        }
        public async Task<StatusCountVM> GetTraderStatusCount(string User_name)
        {
            var orders = await _Context.Orders.Where(o => o.Trader.UserName == User_name).ToListAsync();
            var statusCount = new StatusCountVM()
            {
                All_Status_Count = orders.Count,
                New_Status_Count = orders.Where(o => o.Status.Name == "جديد").Count(),
                Waiting_Status_Count = orders.Where(o => o.Status.Name == "قيد الانتظار").Count(),
                deliveredToRepresentive_Status_Count = orders.Where(o => o.Status.Name == "تم التسليم للمندوب").Count(),
                CantReach_Status_Count = orders.Where(o => o.Status.Name == "لا يمكن الوصول").Count(),
                Suspended_Status_Count = orders.Where(o => o.Status.Name == "تم التاجيل").Count(),
                partlyDelivered_Status_Count = orders.Where(o => o.Status.Name == "تم التسليم جزئيا").Count(),
                CanceledByClient_Status_Count = orders.Where(o => o.Status.Name == "تم الالغاء من قبل المستلم").Count(),
                rejectedWithFullPaying_Status_Count = orders.Where(o => o.Status.Name == "تم الرفض مع الدفع").Count(),
                rejectedWithSomePaying_Status_Count = orders.Where(o => o.Status.Name == "رفض مع سداد جزء").Count(),
                rejectedWithoutPaying_Status_Count = orders.Where(o => o.Status.Name == "رفض و لم يتم الدفع").Count(),
                Delivered_Status_Count = orders.Where(o => o.Status.Name == "تم التوصيل").Count(),


            };
            return statusCount;
        }
        public async Task<StatusCountVM> GetRepresentiveStatusCount(string User_name)
        {
            var orders = await _Context.Orders.Where(o => o.Representitive.UserName == User_name).ToListAsync();
            var statusCount = new StatusCountVM()
            {
                All_Status_Count = orders.Count,
                New_Status_Count = orders.Where(o => o.Status.Name == "جديد").Count(),
                Waiting_Status_Count = orders.Where(o => o.Status.Name == "قيد الانتظار").Count(),
                deliveredToRepresentive_Status_Count = orders.Where(o => o.Status.Name == "تم التسليم للمندوب").Count(),
                CantReach_Status_Count = orders.Where(o => o.Status.Name == "لا يمكن الوصول").Count(),
                Suspended_Status_Count = orders.Where(o => o.Status.Name == "تم التاجيل").Count(),
                partlyDelivered_Status_Count = orders.Where(o => o.Status.Name == "تم التسليم جزئيا").Count(),
                CanceledByClient_Status_Count = orders.Where(o => o.Status.Name == "تم الالغاء من قبل المستلم").Count(),
                rejectedWithFullPaying_Status_Count = orders.Where(o => o.Status.Name == "تم الرفض مع الدفع").Count(),
                rejectedWithSomePaying_Status_Count = orders.Where(o => o.Status.Name == "رفض مع سداد جزء").Count(),
                rejectedWithoutPaying_Status_Count = orders.Where(o => o.Status.Name == "رفض و لم يتم الدفع").Count(),
                Delivered_Status_Count = orders.Where(o => o.Status.Name == "تم التوصيل").Count(),


            };
            return statusCount;
        }
        public async Task<int> updateStatus(OrderStatusVM orderStatusVM)
        {
            var order = await _Context.Orders.FindAsync(orderStatusVM.OrderId);
            order.Status_Id = orderStatusVM.StatusId;
            return await _Context.SaveChangesAsync();
        }



        public async Task<int> Edit(OrderVM ordervm)
        {

            var order = await _Context.Orders.FindAsync(ordervm.Id);
            if (order != null)
            {
                decimal ShippingCost = await ShhipinlPrice(ordervm.Village_Flag, ordervm.ShippingSetting_Id, ordervm.Products, ordervm.City_Id);
                decimal costAllProducts = await Cost_AllProducts(ordervm.Products);
                double countWeight = await CountWeight(ordervm.Products);

                order.Client_Name = ordervm.Client_Name;
                order.Address = ordervm.Address;
                order.Email = ordervm.Email;
                order.FristPhoneNumber = ordervm.FristPhoneNumber;
                order.SecoundPhoneNumber = ordervm.SecoundPhoneNumber;
                order.Products_Total_Cost = costAllProducts;
                order.Shipping_Total_Cost = ShippingCost;
                order.Total_weight = (int)countWeight;
                order.Status_Id = ordervm.OrderStatusId;
                order.Village_Flag = ordervm.Village_Flag;
                order.Village_Name = ordervm.Village_Name;
                if (ordervm.product_Ids_To_Delete != null)
                {
                    foreach (int id in ordervm.product_Ids_To_Delete)
                    {
                        var productToDelete = order.Products.FirstOrDefault(p => p.Id == id);
                        if (productToDelete != null)
                        {
                            order.Products.Remove(productToDelete);
                        }
                    }
                }
                foreach (Product product in ordervm.Products)
                {
                    var productdb = await _Context.Products.FindAsync(product.Id);
                    if (productdb != null)
                    {
                        productdb.Name = product.Name;
                        productdb.Price = product.Price;
                        productdb.Qunatity = product.Qunatity;
                        productdb.Weight = product.Weight;

                    }
                    else
                    {
                        order.Products.Add(product);
                    }
                }

                return await _Context.SaveChangesAsync();
            }
            else
            {
                return 0;
            }
        }

        private async Task<decimal> Cost_DeliverToVillage(bool isDeliverToVillage)
        {
            var result = await _VillageRepo.GetVillageSettings();

            if (isDeliverToVillage && result != null)
            {
                return result.Price;
            }
            return 0;
        }


        private async Task<decimal> Cost_ShippingType(int shippingTypeId)
        {
            var result = await _ShippingRepo.GetById(shippingTypeId);
            if (result != null)
            {
                return result.Shipping_Price;
            }
            return 0;
        }
        private async Task<decimal> Cost_CityShipping(int cityId)
        {
            var result = await _CityRepo.GetById(cityId);
            if (result != null)
            {
                return result.Shipping_Cost;
            }
            return 0;
        }

        private async Task<double> CountWeight(ICollection<Product> products)
        {
            double weight = 0;
            foreach (var item in products)
            {
                weight += item.Weight * item.Qunatity;
            }
            return weight;
        }

        private async Task<decimal> Cost_AllProducts(ICollection<Product> products)
        {
            decimal price = 0;
            foreach (var item in products)
            {
                price += item.Price * item.Qunatity;
            }
            return price;
        }

        private async Task<decimal> ShhipinlPrice(bool Village_Flag, int ShippingSetting_Id, List<Product> products, int cityId)
        {
            decimal costDeliverToVillage = await Cost_DeliverToVillage(Village_Flag);
            decimal costShippingType = await Cost_ShippingType(ShippingSetting_Id);
            double countWeight = await CountWeight(products);
            decimal costAddititonalWeight = (decimal)await Cost_AdditionalWeight(countWeight);
            decimal costCityShippingPrice = await Cost_CityShipping(cityId);
            return costDeliverToVillage + costAddititonalWeight + costShippingType + costCityShippingPrice;
        }
        private async Task<double> Cost_AdditionalWeight(double totalWeight)
        {
            double cost = 0;
            double defaultWeight = 0;
            double additionalPrice = 0;
            var result = await _WeightRepo.GetWeightSettings();
            if (result != null)
            {
                defaultWeight = result.Default_Weight;

                if (totalWeight > defaultWeight)
                {

                    additionalPrice = result.Extra_Weight;

                    totalWeight = totalWeight - defaultWeight;

                    cost = totalWeight * additionalPrice;
                }
            }

            return cost;
        }

        public async Task<List<OrderVM>> GetOrdersByDateRange(DateTime fromDate, DateTime toDate, string UserName)
        {
            IQueryable<Order> query = _Context.Orders.Where(o => o.Order_Date >= fromDate && o.Order_Date <= toDate);

            if (!string.IsNullOrEmpty(UserName))
            {
                var user = await _UserManager.FindByNameAsync(UserName);

                if (user != null)
                {
                    var userType = await _UserManager.GetRolesAsync(user);
                    if (userType.FirstOrDefault() == "مندوب")
                    {
                        query = query.Where(o => o.Representitive.UserName == UserName);

                    }
                    else if(userType.FirstOrDefault() == "تاجر")
                    {
                        query = query.Where(o => o.Trader.UserName == UserName);

                    }
                }
            }

                List<OrderVM> orders = await query.Select(Order => new OrderVM
                {
                    Id = Order.Id,
                    Client_Name = Order.Client_Name,
                    FristPhoneNumber = Order.FristPhoneNumber,
                    SecoundPhoneNumber = Order.SecoundPhoneNumber,
                    Email = Order.Email,
                    Address = Order.Address,
                    Village_Name = Order.Village_Name,
                    Governate_Id = Order.Governate_Id,
                    City_Id = Order.City_Id,
                    Village_Flag = Order.Village_Flag,
                    ShippingSetting_Id = Order.ShippingSetting_Id,
                    Payment_Type = Order.Payment_Type,
                    Branch_Id = Order.Branch_Id,
                    Status_Id = Order.Status_Id,
                    Order_Date = Order.Order_Date,
                    Notes = Order.Notes,
                    Products_Total_Cost = Order.Products_Total_Cost,
                    Order_Total_Cost = Order.Order_Total_Cost,
                    Total_weight = Order.Total_weight,
                    GovernateName = Order.Governate.Name,
                    CityName = Order.City.Name,
                    BranchName = Order.Branch.Name,
                    RepresntiveName = Order.Representitive.FullName,
                    TraderName = Order.Trader.FullName,
                    statusName= Order.Status.Name,
                    Products = Order.Products.Select(prod => new Product
                    {
                        Name = prod.Name,
                        Qunatity = prod.Qunatity,
                        Price = prod.Price,
                        Weight = prod.Weight,
                    }).ToList()
                }).ToListAsync();

                return orders;
            
        }
    }
}
