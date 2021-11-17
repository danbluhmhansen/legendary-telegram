namespace BlazorApp1.Server.Migrations;

using BlazorApp1.Server.Data;

using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

[DbContext(typeof(ApplicationDbContext))]
[Migration("00000000000000_Quartz")]
public partial class Quartz { }
