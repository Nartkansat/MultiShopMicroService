﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.AboutDtos;
using MultiShop.WebUI.Services.CatalogServices.AboutServices;
using Newtonsoft.Json;
using System.Text;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AllowAnonymous]
    [Route("Admin/About")]
    public class AboutController : Controller
    {
        private readonly IAboutService _aboutService;

        public AboutController(IAboutService aboutService)
        {
            _aboutService = aboutService;
        }
        void AboutViewBagList()
        {
            ViewBag.v0 = "Hakkımda İşlemleri";
            ViewBag.v1 = "Anasayfa";
            ViewBag.v2 = "Hakkımda";
            ViewBag.v3 = "Hakkımda Listesi";
        }


        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            AboutViewBagList();

            var values = await _aboutService.GetAllAboutAsync();

            return View(values);
        }

        [HttpGet]
        [Route("CreateAbout")]
        public IActionResult CreateAbout()
        {
            AboutViewBagList();


            return View();
        }


        [HttpPost]
        [Route("CreateAbout")]
        public async Task<IActionResult> CreateAbout(CreateAboutDto createAboutDto)
        {
            await _aboutService.CreateAboutAsync(createAboutDto);
            return RedirectToAction("Index", "About", new { area = "Admin" });
        }


        // [HttpDelete] olmayacak burada. 
        [Route("DeleteAbout/{id}")]
        public async Task<IActionResult> DeleteAbout(string id)
        {
            await _aboutService.DeleteAboutAsync(id);

            return RedirectToAction("Index", "About", new { area = "Admin" });
        }


        [Route("UpdateAbout/{id}")]
        [HttpGet]
        public async Task<IActionResult> UpdateAbout(string id)
        {
            AboutViewBagList();

            var values = await _aboutService.GetByIdAboutAsync(id);
            return View(values);
        }


        [Route("UpdateAbout/{id}")]
        [HttpPost]
        public async Task<IActionResult> UpdateAbout(UpdateAboutDto updateAboutDto)
        {

            await _aboutService.UpdateAboutAsync(updateAboutDto);

            return RedirectToAction("Index", "About", new { area = "Admin" });
        }
    }
}
