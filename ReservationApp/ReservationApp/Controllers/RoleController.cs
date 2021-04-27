using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReservationApp.Models;
using ReservationApp.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReservationApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RoleController : Controller
    {
        RoleManager<IdentityRole> _roleManager;
        UserManager<IdentityUser> _userManager;

        public RoleController(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;

        }
        public IActionResult Index()
        {
            var roles = _roleManager.Roles.ToList();
            return View(roles);
        }

        public IActionResult Create()
        {
            return View(new IdentityRole());
        }

        [HttpPost]
        public async Task<IActionResult> Create(IdentityRole role)
        {
            await _roleManager.CreateAsync(role);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            IdentityRole role = await _roleManager.FindByIdAsync(id);
            
            Role model = new Role()
            {
                Id = role.Id,
                Name = role.Name,
                Users = new List<string>()
            };

            foreach (IdentityUser user in  _userManager.Users.ToList()) // userManager.Users is not awaitble so change to (await userManager.Users.ToListAsync())
            {
                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    model.Users.Add(user.UserName);
                }
            }
            return View(model);
        }



        [HttpPost]
        public async Task<IActionResult> Edit(Role model)
        {
            if (ModelState.IsValid)
            {
                var role = await _roleManager.FindByIdAsync(model.Id);
               
                role.Name = model.Name;

                IdentityResult result = await _roleManager.UpdateAsync(role);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);

        }

        [HttpGet]
        public IActionResult Delete()
        {
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            IdentityRole role = await _roleManager.FindByIdAsync(id);

            if (!(role is null))
            {
                IdentityResult result = await _roleManager.DeleteAsync(role);

                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            else
            {
                return View("../Error/NotFound", $"The role Id : {id} cannot be found");

            }
            return RedirectToAction("Index");
        }

       
        [HttpGet]
        public async Task<IActionResult> EditUsersRole(string roleId)
        {
            if (string.IsNullOrEmpty(roleId))
            {
                return View("../Error/NotFound", $"The role must be exist and not empty in Url");

            }
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role is null)
            {
                return View("../Error/NotFound", $"The role Id : {role.Id} cannot be found");
            }

            var Models = new List<EditUsersRoleViewModel>(); 

            foreach (var user in await _userManager.Users.ToListAsync())
            {
                EditUsersRoleViewModel model = new EditUsersRoleViewModel()
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    //IsSelected = false
                };
                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    model.IsSelected = true;
                }
                else
                {
                    model.IsSelected = false;
                }

                Models.Add(model);
            }
            ViewBag.RoleId = roleId;
            return View(Models);

       }

        [HttpPost]
        public async Task<IActionResult> EditUsersRole(List<EditUsersRoleViewModel> model, string roleId)
        {
            if (string.IsNullOrEmpty(roleId))
            {
                return View("../Error/NotFound", $"The role must be exist and not empty in Url");

            }
            var role = await this._roleManager.FindByIdAsync(roleId);
            if (role is null)
            {
                return View("../Error/NotFound", $"The role Id : {role.Id} cannot be found");
            }

            // role if deja affectté et in model is select il faut le supprimer , ou l'affecté si il est selecté au model mais non affecté before

            IdentityResult result = null;

            for (int i = 0; i < model.Count; i++)
            {
                IdentityUser user = await _userManager.FindByIdAsync(model[i].UserId);

                if (await _userManager.IsInRoleAsync(user, role.Name) && !model[i].IsSelected)
                {
                    result = await _userManager.RemoveFromRoleAsync(user, role.Name);
                }
                else if (!(await _userManager.IsInRoleAsync(user, role.Name)) && model[i].IsSelected)
                {
                    result = await _userManager.AddToRoleAsync(user, role.Name);
                }
            }

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

            }
            return RedirectToAction("Edit", new { id = roleId });

        }


    }
}
