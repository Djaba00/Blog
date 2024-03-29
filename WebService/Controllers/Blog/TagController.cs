﻿using AutoMapper;
using Blog.BLL.Interfaces;
using Blog.BLL.Models;
using Blog.WebService.ViewModels.Tag;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.WebService.Controllers.Blog
{
    [Route("Tag")]
    public class TagController : Controller
    {
        readonly ILogger<TagController> logger;
        readonly IMapper mapper;
        readonly ITagService tagService;

        public TagController(ILogger<TagController> logger, IMapper mapper, ITagService tagService)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.tagService = tagService;
        }

        [Authorize]
        [Route("Add")]
        [HttpGet]
        public IActionResult CreateTag()
        {
            logger.LogInformation("GET CreateTag page responsed for user-{0}",
                User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier")).Value);

            return View("CreateTag");
        }


        [Authorize]
        [Route("Add")]
        [HttpPost]
        public async Task<IActionResult> CreateTagAsync(CreateTagViewModel tagModel)
        {
            logger.LogInformation("POST User-{0} send newTag data",
                User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier")).Value);

            var tag = mapper.Map<TagModel>(tagModel);

            await tagService.CreateTagAsync(tag);

            logger.LogInformation("POST User-{0} created tag",
                User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier")).Value);

            return RedirectToAction("Tags");
        }

        [Authorize]
        [Route("Edit/{id:int}")]
        [HttpGet]
        public async Task<IActionResult> UpdateTagAsync(int id)
        {
            var tag = await tagService.GetTagByIdAsync(id);

            var model = mapper.Map<EditTagViewModel>(tag);

            logger.LogInformation("GET EditArticle page responsed for user-{0}",
                  User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier")).Value);

            return View("EditTag", model);
        }

        [Authorize]
        [Route("Edit")]
        [HttpPost]
        public async Task<IActionResult> UpdateTagAsync(EditTagViewModel updateTag)
        {
            logger.LogInformation("POST User-{0} send updateTag data",
                User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier")).Value);

            var tag = mapper.Map<TagModel>(updateTag);

            await tagService.UpdateTagAsync(tag);

            logger.LogInformation("POST User-{0} edited article",
                User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier")).Value);

            return RedirectToAction("Tags");
        }

        [Authorize]
        [Route("Delete/{id:int}")]
        [HttpPost]
        public async Task<IActionResult> DeleteTagAsync(int id)
        {
            await tagService.DeleteTagAsync(id);

            logger.LogInformation("POST User-{0} deleted tag-{1}",
                User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier")).Value,
                id);

            return RedirectToAction("Tags");
        }

        [Route("Tags")]
        [HttpGet]
        public async Task<IActionResult> GetTagsListAsync()
        {
            var tags = await tagService.GetAllTagsAsync();

            var models = new List<TagViewModel>();

            foreach (var tag in tags)
            {
                models.Add(mapper.Map<TagViewModel>(tag));
            }

            logger.LogInformation("GET TagList page responsed}");

            return View("TagList", models);
        }

        [Route("Tag")]
        [HttpGet]
        public async Task<IActionResult> GetTagByIdAsync(int id)
        {
            var tag = await tagService.GetTagByIdAsync(id);

            var model = mapper.Map<TagModel>(tag);

            logger.LogInformation("GET Tag-{0} page responsed",
                  id);

            return View("Tag", model);
        }
    }
}
