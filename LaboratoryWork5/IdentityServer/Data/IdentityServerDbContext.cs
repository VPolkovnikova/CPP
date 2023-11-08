using IdentityServer.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IdentityServer.Data;


public class IdentityServerDbContext(
    DbContextOptions<IdentityServerDbContext> options
) : IdentityUserContext<User>(options)
{ }