using Microsoft.EntityFrameworkCore;
using Model.Mahasiswa;

class mhsDB : DbContext
{
    public mhsDB(DbContextOptions<mhsDB> options)
        : base(options) { }

    public DbSet<Mahasiswa> mhs => Set<Mahasiswa>();
}