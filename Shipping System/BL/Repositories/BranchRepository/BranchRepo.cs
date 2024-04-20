using Microsoft.EntityFrameworkCore;
using Shipping_System.DAL.Database;
using Shipping_System.ViewModels;
using Shipping_System.DAL.Entites;


namespace Shipping_System.BL.Repositories.BranchRepository
{ 
    public class BranchRepo :IBranchRepo
    {
        private readonly Context _Context;


        public BranchRepo(Context context)
        {
            _Context = context;
        }
        public async Task<int> Add(BranchVM Branch)
        {
            var BranchDB = new Branch
            {
                Name = Branch.Name,
                Creation_Date = Branch.Creation_Date,
                City_Id =Branch.City_Id,

            };
            await _Context.Branches.AddAsync(BranchDB);
            var result = await _Context.SaveChangesAsync();
            return result;
        }

        public async Task<int> Delete(int id)
        {
            var BranchDB = await _Context.Branches.FindAsync(id);

            if (BranchDB != null)
            {
                _Context.Entry(BranchDB).State = EntityState.Deleted;


                return await _Context.SaveChangesAsync();
            }
            else
            {
                return 0;
            }
        }

        public async Task<int> Edit(BranchVM Branch)
        {

            var BranchDB = await _Context.Branches.FindAsync(Branch.Id);

            if (BranchDB != null)
            {
                _Context.Entry(BranchDB).CurrentValues.SetValues(Branch);
                _Context.Entry(BranchDB).State = EntityState.Modified;

                return await _Context.SaveChangesAsync();
            }
            else
            {
                return 0;
            }
        }

        public Task<List<BranchVM>> Get()
        {
            var Branches = _Context.Branches.Select(Branch => new BranchVM
            {
                Id = Branch.Id,
                Name = Branch.Name,
                Creation_Date = Branch.Creation_Date,
                City_Id = Branch.City_Id,
            }).ToListAsync();
            return Branches;
        }

        public async Task<BranchVM> GetById(int id)
        {
            var BranchDB = await _Context.Branches.FindAsync(id);
            var Branch = new BranchVM()
            {
                Id = BranchDB.Id,
                Name = BranchDB.Name,
                Creation_Date = BranchDB.Creation_Date,
                City_Id = BranchDB.City_Id,
                Cities = await _Context.Cities.ToListAsync()
            };
            return Branch;

        }
        public async Task<BranchVM> IncludeLists()
        {
            var Lists = new BranchVM
            {
                Cities = await _Context.Cities.ToListAsync(),

            };
            return Lists;
        }
        public Task<List<BranchVM>> GetByCityId(int id)
        {
            var branches = _Context.Branches.Where(b => b.City_Id == id).Select(branch => new BranchVM
            {
                Id = branch.Id,
                Name = branch.Name,
            }).ToListAsync();
            return branches;
        }
    }
}
