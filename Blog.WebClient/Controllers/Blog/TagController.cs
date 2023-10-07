﻿using AutoMapper;
using Blog.BLL.Interfaces;
using Blog.BLL.Models;
using Blog.WebClient.VIewModels.Tag;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.WebClient.Controllers.Blog
{
    public class TagController : Controller
    {
        IMapper mapper;
        ITagService tagService;
        IAccountService accountService;

        public TagController(IMapper mapper, ITagService tagService, IAccountService accountService)
        {
            this.mapper = mapper;
            this.tagService = tagService;
            this.accountService = accountService;
        }

        [Authorize]
        [Route("NewTag")]
        [HttpPost]
        public async Task<IActionResult> CreateTagAsync(CreateTagViewModel tagModel)
        {
            if (ModelState.IsValid)
            {
                var tag = mapper.Map<TagModel>(tagModel);

                await tagService.CreateTagAsync(tag);

                return RedirectToAction("MyPage");
            }

            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        [Route("EditTag")]
        [HttpPost]
        public async Task<IActionResult> UpdateTagAsync(EditTagViewModel updateTag)
        {
            if (ModelState.IsValid)
            {
                var tag = mapper.Map<TagModel>(updateTag);

                await tagService.UpdateTagAsync(tag);

                return RedirectToAction("MyPage");
            }

            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        [Route("DeleteTag")]
        [HttpPost]
        public async Task<IActionResult> DeleteTagAsync(int id)
        {
            await tagService.DeleteTagAsync(id);

            return RedirectToAction("MyPage");
        }

        [Route("Tag")]
        [HttpGet]
        public async Task<IActionResult> GetTagsListAsync()
        {
            var tags = await tagService.GetAllTagsAsync();

            var models = new List<TagModel>();

            foreach (var tag in tags)
            {
                models.Add(mapper.Map<TagModel>(tag));
            }

            return View("ArticlesList", models);
        }

        [Route("Tags")]
        [HttpGet]
        public async Task<IActionResult> GetTagByIdAsync(int id)
        {
            var tag = await tagService.GetTagByIdAsync(id);

            var model = mapper.Map<TagModel>(tag);

            return View("ArticlesList", model);
        }
    }
}
