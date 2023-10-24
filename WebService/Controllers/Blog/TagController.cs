using AutoMapper;
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
        IMapper mapper;
        ITagService tagService;

        public TagController(IMapper mapper, ITagService tagService)
        {
            this.mapper = mapper;
            this.tagService = tagService;
        }

        [Authorize]
        [Route("Add")]
        [HttpGet]
        public IActionResult CreateTag()
        {
            return View("CreateTag");
        }


        [Authorize]
        [Route("Add")]
        [HttpPost]
        public async Task<IActionResult> CreateTagAsync(CreateTagViewModel tagModel)
        {
            var tag = mapper.Map<TagModel>(tagModel);

            await tagService.CreateTagAsync(tag);

            return RedirectToAction("Tags");
        }

        [Authorize]
        [Route("Edit")]
        [HttpGet]
        public async Task<IActionResult> UpdateTagAsync(int id)
        {
            var tag = await tagService.GetTagByIdAsync(id);

            var model = mapper.Map<EditTagViewModel>(tag);

            return View("EditTag", model);
        }

        [Authorize]
        [Route("Edit")]
        [HttpPost]
        public async Task<IActionResult> UpdateTagAsync(EditTagViewModel updateTag)
        {
            var tag = mapper.Map<TagModel>(updateTag);

            await tagService.UpdateTagAsync(tag);

            return RedirectToAction("Tags");
        }

        [Authorize]
        [Route("Delete")]
        [HttpPost]
        public async Task<IActionResult> DeleteTagAsync(int id)
        {
            await tagService.DeleteTagAsync(id);

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

            return View("TagList", models);
        }

        [Route("Tag")]
        [HttpGet]
        public async Task<IActionResult> GetTagByIdAsync(int id)
        {
            var tag = await tagService.GetTagByIdAsync(id);

            var model = mapper.Map<TagModel>(tag);

            return View("Tag", model);
        }
    }
}
