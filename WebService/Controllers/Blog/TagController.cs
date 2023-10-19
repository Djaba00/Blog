using AutoMapper;
using Blog.BLL.Interfaces;
using Blog.BLL.Models;
using Blog.WebService.ViewModels.Tag;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.WebService.Controllers.Blog
{
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
        [Route("Tag/NewTag")]
        [HttpGet]
        public IActionResult CreateTag()
        {
            return View("NewTag");
        }


        [Authorize]
        [Route("Tag/NewTag")]
        [HttpPost]
        public async Task<IActionResult> CreateTagAsync(CreateTagViewModel tagModel)
        {
            var tag = mapper.Map<TagModel>(tagModel);

            await tagService.CreateTagAsync(tag);

            return RedirectToAction("Tags");
        }

        [Authorize]
        [Route("Tag/EditTag/{id:int}")]
        [HttpGet]
        public async Task<IActionResult> UpdateTagAsync(int id)
        {
            var tag = await tagService.GetTagByIdAsync(id);

            var model = mapper.Map<EditTagViewModel>(tag);

            return View("EditTag", model);
        }

        [Authorize]
        [Route("Tag/EditTag")]
        [HttpPost]
        public async Task<IActionResult> UpdateTagAsync(EditTagViewModel updateTag)
        {
            var tag = mapper.Map<TagModel>(updateTag);

            await tagService.UpdateTagAsync(tag);

            return RedirectToAction("Tags");
        }

        [Authorize]
        [Route("Tag/DeleteTag")]
        [HttpPost]
        public async Task<IActionResult> DeleteTagAsync(int id)
        {
            await tagService.DeleteTagAsync(id);

            return RedirectToAction("Tags");
        }

        [Route("Tag/Tags")]
        [HttpGet]
        public async Task<IActionResult> GetTagsListAsync()
        {
            var tags = await tagService.GetAllTagsAsync();

            var models = new List<TagModel>();

            foreach (var tag in tags)
            {
                models.Add(mapper.Map<TagModel>(tag));
            }

            return View("TagsList", models);
        }

        [Route("Tag/{id:int}")]
        [HttpGet]
        public async Task<IActionResult> GetTagByIdAsync(int id)
        {
            var tag = await tagService.GetTagByIdAsync(id);

            var model = mapper.Map<TagModel>(tag);

            return View("Tag", model);
        }
    }
}
