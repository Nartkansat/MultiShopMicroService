﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.FeatureSliderDtos;
using MultiShop.WebUI.Services.CatalogServices.FeatureSliderServices;
using Newtonsoft.Json;
using System.Text;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/FeatureSlider")]

    public class FeatureSliderController : Controller
    {
        private readonly IFeatureSliderService _featureSliderService;

        public FeatureSliderController(IFeatureSliderService featureSliderService)
        {
            _featureSliderService = featureSliderService;
        }

        void FeatureSliderViewBagList()
        {
            ViewBag.v0 = "Öne Çıkan Slider Görsel İşlemleri";
            ViewBag.v1 = "Anasayfa";
            ViewBag.v2 = "Öne Çıkan Görseller";
            ViewBag.v3 = "Slider Öne Çıkan Görsel Listesi";
        }

        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            FeatureSliderViewBagList();

            var values = await _featureSliderService.GetAllFeatureSliderAsync();

            return View(values);
        }


        [HttpGet]
        [Route("CreateFeatureSlider")]
        public IActionResult CreateFeatureSlider()
        {
            FeatureSliderViewBagList();

            return View();
        }


        [HttpPost]
        [Route("CreateFeatureSlider")]
        public async Task<IActionResult> CreateFeatureSlider(CreateFeatureSliderDto createFeatureSliderDto)
        {
            createFeatureSliderDto.Status = false;

            await _featureSliderService.CreateFeatureSliderAsync(createFeatureSliderDto);
            return RedirectToAction("Index", "FeatureSlider", new { area = "Admin" });
        }


        // [HttpDelete] olmayacak burada. 
        [Route("DeleteFeatureSlider/{id}")]
        public async Task<IActionResult> DeleteFeatureSlider(string id)
        {
            await _featureSliderService.DeleteFeatureSliderAsync(id);

            return RedirectToAction("Index", "FeatureSlider", new { area = "Admin" });
        }


        [Route("UpdateFeatureSlider/{id}")]
        [HttpGet]
        public async Task<IActionResult> UpdateFeatureSlider(string id)
        {
            FeatureSliderViewBagList();

            var values = await _featureSliderService.GetByIdFeatureSliderAsync(id);
            return View(values);
        }


        [Route("UpdateFeatureSlider/{id}")]
        [HttpPost]
        public async Task<IActionResult> UpdateFeatureSlider(UpdateFeatureSliderDto updateFeatureSliderDto)
        {

            await _featureSliderService.UpdateFeatureSliderAsync(updateFeatureSliderDto);

            return RedirectToAction("Index", "FeatureSlider", new { area = "Admin" });
        }


    }
}
