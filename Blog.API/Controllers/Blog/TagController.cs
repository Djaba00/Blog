using System;
using AutoMapper;
using Blog.API.Models.Requests.Tag;
using Blog.API.Models.Responses.Tag;
using Blog.API.Models.ViewModels;
using Blog.BLL.Interfaces;
using Blog.BLL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.API.Controllers.Blog
{
    [Route("api/[controller]")]
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

        /// <summary>
        /// Tags list
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// Get /Tags
        /// </remarks>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [Route("Tags")]
        [HttpGet]
        public async Task<IActionResult> GetTagsListAsync()
        {
            var tags = await tagService.GetAllTagsAsync();

            var models = new GetTagsResponse();

            foreach (var tag in tags)
            {
                models.Tags.Add(mapper.Map<TagViewModel>(tag));
            }

            logger.LogInformation("GET TagList page responsed}");

            return Ok(models);
        }

        /// <summary>
        /// Tag by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// Get /Tag/{id:int}
        /// </remarks>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [Route("Tag/{id:int}")]
        [HttpGet]
        public async Task<IActionResult> GetTagByIdAsync(int id)
        {
            var tag = await tagService.GetTagByIdAsync(id);

            var model = mapper.Map<GetTagResponse>(tag);

            logger.LogInformation("GET Tag-{0} page responsed",
                  id);

            return Ok(model);
        }

        /// <summary>
        /// Create tag
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// Get /Add
        /// </remarks>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [Authorize]
        [Route("Add")]
        [HttpPost]
        public async Task<IActionResult> CreateTagAsync(CreateTagRequest tagModel)
        {
            logger.LogInformation("POST User-{0} send newTag data",
                User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier")).Value);

            var tag = mapper.Map<TagModel>(tagModel);

            await tagService.CreateTagAsync(tag);

            logger.LogInformation("POST User-{0} created tag",
                User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier")).Value);

            return Ok();
        }

        /// <summary>
        /// Edit tag
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// PUT
        /// </remarks>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [Authorize]
        [HttpPut]
        public async Task<IActionResult> UpdateTagAsync(EditTagRequest updateTag)
        {
            logger.LogInformation("POST User-{0} send updateTag data",
                User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier")).Value);

            var tag = mapper.Map<TagModel>(updateTag);

            await tagService.UpdateTagAsync(tag);

            logger.LogInformation("POST User-{0} edited article",
                User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier")).Value);

            return Ok();
        }

        /// <summary>
        /// Delete tag
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// DELETE
        /// </remarks>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> DeleteTagAsync(int id)
        {
            await tagService.DeleteTagAsync(id);

            logger.LogInformation("POST User-{0} deleted tag-{1}",
                User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier")).Value,
                id);

            return Ok();
        }
    }
}

