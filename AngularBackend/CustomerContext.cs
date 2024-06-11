using System;
using System.Collections.Generic;
using AngularBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace AngularBackend;

public class CustomerContext : DbContext
{
    public CustomerContext(DbContextOptions<CustomerContext> options) : base(options)
    {
    }

    public DbSet<CustomerModel> Customer { get; set; }
}