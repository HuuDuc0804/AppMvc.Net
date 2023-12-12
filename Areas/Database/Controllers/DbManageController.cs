using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Data;
using App.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace App.Areas.Database.Controllers
{
    [Area("Database")]
    [Route("/database-manage/[action]")]
    [Authorize(Roles = RoleName.Administrator)]
    public class DbManageController : Controller
    {
        private readonly AppDbContext _dbContext;
        private readonly UserManager<AppUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public DbManageController(AppDbContext dbContext, UserManager<AppUser> userManager,
        RoleManager<IdentityRole> roleManager)
        {
            _dbContext = dbContext;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult DeleteDb()
        {
            return View();
        }
        [TempData]
        public string? StatusMessage { get; set; }
        [HttpPost]
        public async Task<IActionResult> DeleteDbAsync()
        {
            var result = await _dbContext.Database.EnsureDeletedAsync();
            if (result)
            {
                StatusMessage = "Xóa database thành công!";
            }
            else
            {
                StatusMessage = "Không xóa được database!";
            }
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> MigrateAsync()
        {
            await _dbContext.Database.MigrateAsync();
            StatusMessage = "Cập nhật database thành công!";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> SeedDataAsync()
        {
            var roleNames = typeof(RoleName).GetFields().ToList();
            foreach (var role in roleNames)
            {
                var roleName = role.GetRawConstantValue();
                var roleOld = await roleManager.FindByNameAsync((string)roleName!);
                if (roleOld == null)
                {
                    await roleManager.CreateAsync(new IdentityRole((string)roleName!));
                }
            }

            var userAdmin = await userManager.FindByEmailAsync("admin");
            if (userAdmin == null)
            {
                userAdmin = new AppUser(){
                    UserName = "admin",
                    Email = "admin@gmail.com",
                    EmailConfirmed = true
                };
                await userManager.CreateAsync(userAdmin, "admin123");
                await userManager.AddToRoleAsync(userAdmin, RoleName.Administrator);
            }
            StatusMessage = "Seed database thành công";
            return RedirectToAction(nameof(Index));
        }
    }
}