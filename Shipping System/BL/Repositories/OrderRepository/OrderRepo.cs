using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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



        public OrderRepo(Context context, IVillageSettingRepoe villageRepo, IShippingSettingRepo shippingRepo, UserManager<ApplicationUser> userManager, IWeightSettingsRepo weightRepo = null)
        {
            _Context = context;
            _VillageRepo = villageRepo;
            _ShippingRepo = shippingRepo;
            _UserManager = userManager;
            _WeightRepo = weightRepo;
        }

        public async Task<int> Add(OrderVM Order)
        {
            decimal costDeliverToVillage = await Cost_DeliverToVillage(Order.Village_Flag);
            decimal costShippingType  =  await Cost_ShippingType(Order.ShippingSetting_Id);
            decimal costAllProducts = await Cost_AllProducts(Order.Products);
            double countWeight =  await CountWeight(Order.Products);
            decimal costAddititonalWeight = (decimal) await Cost_AdditionalWeight(countWeight);

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
                Village_Flag =Order.Village_Flag,
                ShippingSetting_Id = Order.ShippingSetting_Id,
                Payment_Type = Order.Payment_Type,
                Branch_Id = Order.Branch_Id,
                Status_Id = 1,
                Order_Date = DateTime.Now,
                Notes = Order.Notes,
                Representitive_Id = Order.Representitive_Id,
                Products_Total_Cost = costAllProducts,
                Order_Total_Cost = costDeliverToVillage + costAddititonalWeight  + costShippingType,
                Total_weight = (int) countWeight,
                Products = Order.Products.Select(prod => new Product
                {
                    Name = prod.Name,
                    Qunatity = prod.Qunatity,
                    Price = prod.Price,
                    Weight = prod.Weight,
                }).ToList(),
            };

           
              await _Context.Orders.AddAsync(order);
              await  _Context.Products.AddRangeAsync(order.Products.ToList());
               var result= await _Context.SaveChangesAsync();
            
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

        public Task<int> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<OrderVM>> GetAll()
        {
            List<OrderVM> orders =await _Context.Orders.Select(Order => new OrderVM
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
                Representitive_Id = Order.Representitive_Id,
                Products_Total_Cost = Order.Products_Total_Cost,
                Order_Total_Cost = Order.Order_Total_Cost,
                Total_weight = Order.Total_weight,
                GovernateName = Order.Governate.Name,
                CityName = Order.City.Name,
                BranchName = Order.Branch.Name,
                RepresntiveName = Order.Representitive.FullName,
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

        public Task<OrderVM> GetById(int orderId)
        {
            throw new NotImplementedException();
        }

        public Task<List<OrderVM>> GetOrdersByDateRange(DateTime fromDate, DateTime toDate)
        {
            throw new NotImplementedException();
        }

        public Task<int> Update(OrderVM order)
        {
            throw new NotImplementedException();
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


        private async Task< decimal> Cost_ShippingType(int shippingTypeId)
        {
            var result = await _ShippingRepo.GetById(shippingTypeId);
            if (result != null)
            {
                return result.Shipping_Price;
            }
            return 0;
        }

        private async Task <double>CountWeight(ICollection<Product> products)
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
    }
}
