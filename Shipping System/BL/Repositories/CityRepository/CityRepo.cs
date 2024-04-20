using Microsoft.EntityFrameworkCore;
using Shipping_System.DAL.Database;
using Shipping_System.DAL.Entites;
using Shipping_System.ViewModels;

namespace Shipping_System.BL.Repositories.CityRepository
{
    public class CityRepo : ICityRepo
    {
        private readonly Context _Context;


        public CityRepo(Context context)
        {
            _Context = context;
        }
        public async Task<int> Add(CityVM City)
        {
            var CityDB = new City
            {
                Name = City.Name,
                Shipping_Cost = City.Shipping_Cost,
                Governate_Id = City.Governate_Id,

            };
            await _Context.Cities.AddAsync(CityDB);
            var result = await _Context.SaveChangesAsync();
            return result;
        }

        public  async Task<int> Delete(int id)
        {
            var cityDB = await _Context.Cities.FindAsync(id);

            if (cityDB != null)
            {
                _Context.Entry(cityDB).State = EntityState.Deleted;


                return await _Context.SaveChangesAsync();
            }
            else
            {
                return 0;
            }
        }

        public async Task<int> Edit(CityVM city)
        {

            var cityDB = await _Context.Cities.FindAsync(city.Id);

            if (cityDB != null)
            {
                _Context.Entry(cityDB).CurrentValues.SetValues(city);
                _Context.Entry(cityDB).State = EntityState.Modified;

                return await _Context.SaveChangesAsync();
            }
            else
            {
                return 0;
            }
        }

        public Task<List<CityVM>> Get()
        {
            var Cities = _Context.Cities.Select(City => new CityVM
            {
                Id = City.Id,
                Name = City.Name,
                Shipping_Cost =City.Shipping_Cost,
                Governate_Id = City.Governate_Id,
            }).ToListAsync();
            return Cities;
        }

        public async Task<CityVM> GetById(int id)
        {
            var CityDB = await _Context.Cities.FindAsync(id);
            var City = new CityVM()
            {
                Id = CityDB.Id,
                Name = CityDB.Name,
                Shipping_Cost = CityDB.Shipping_Cost,
                Governate_Id = CityDB.Governate_Id,
                Governates = await _Context.Governates.ToListAsync(),

            };
            return City;

        }
        public async Task<CityVM> IncludeLists()
        {
            var Lists = new CityVM
            {
                Governates = await _Context.Governates.ToListAsync(),

            };
            return Lists;
        }
        public Task<List<CityVM>> GetByGovernateId(int id)
        {
            var Cities = _Context.Cities.Where(C => C.Governate_Id == id).Select(City => new CityVM
            {
                Id = City.Id,
                Name = City.Name,
                Shipping_Cost = City.Shipping_Cost,
                Governate_Id = City.Governate_Id,
            }).ToListAsync();
            return Cities;
        }
    }
}
