using Microsoft.EntityFrameworkCore;
using OrbitaChallengeBackEnd.Models;
using System.Collections.Generic;

namespace OrbitaChallengeBackEnd.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    public DbSet<Student> Students { get; set; }
}

