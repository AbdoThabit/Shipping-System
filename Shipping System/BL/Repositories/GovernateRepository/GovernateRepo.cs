using Microsoft.EntityFrameworkCore;
using NToastNotify;
using Shipping_System.DAL.Database;
using Shipping_System.DAL.Entites;
using Shipping_System.ViewModels;

namespace Shipping_System.BL.Repositories.GovernateRepository
{
    public class GovernateRepo : IGovernateRepo
    {
        private readonly Context _Context;


        public GovernateRepo(Context context)
        {
            _Context = context;
        }

        public async Task<int> Add(GovernateVM Governate)
        {
            var GovernateDB = new Governate
            {
                Name = Governate.Name,
            };
          await _Context.Governates.AddAsync(GovernateDB);
           var result = await _Context.SaveChangesAsync();
            return result;
        }


        public async Task<int> Delete(int id)
        {
            var governateDB = await _Context.Governates.FindAsync(id);

            if (governateDB != null)
            {
                _Context.Entry(governateDB).State = EntityState.Deleted;

             
                return await _Context.SaveChangesAsync();
            }
            else
            {
                return 0;
            }
        }


        public async Task<int> Edit(GovernateVM governate)
        {
            var governateDB = await _Context.Governates.FindAsync(governate.Id);

            if (governateDB != null)
            {
                _Context.Entry(governateDB).CurrentValues.SetValues(governate);
                _Context.Entry(governateDB).State = EntityState.Modified;

                return await _Context.SaveChangesAsync();
            }
            else
            {
                return 0;
            }
        }


        public Task<List<GovernateVM>> Get()
        {
         var Governates= _Context.Governates.Select(Gov=> new GovernateVM
            {
                Id = Gov.Id,
                Name = Gov.Name,
            }).ToListAsync();
            return Governates;
        }

        public async Task<GovernateVM> GetById(int id)
        {
            var GovernateDB = await _Context.Governates.FindAsync(id);
            var Governate = new GovernateVM()
            {
                 Id=    GovernateDB.Id,
                Name = GovernateDB.Name
            };
            return Governate;

        }
    }
}
