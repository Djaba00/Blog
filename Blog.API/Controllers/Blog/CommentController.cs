using System;
using AutoMapper;
using Blog.API.Models.Requests.Comment;
using Blog.API.Models.Responses.Comment;
using Blog.API.Models.ViewModels;
using Blog.BLL.Exceptions;
using Blog.BLL.Interfaces;
using Blog.BLL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.API.Controllers.Blog
{
    [Route("api/[controller]")]
    public class CommentController : Controller
    {
        readonly ILogger<CommentController> logger;
        readonly IMapper mapper;
        readonly ICommentService commentService;

        public CommentController(ILogger<CommentController> logger, IMapper mapper, ICommentService commentService)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.commentService = commentService;
        }

        /// <summary>
        /// Comments
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /Comments
        /// </remarks>
        /// <returns>Returns GetCommentsResponce</returns>
        /// <response code="200">Success</response>
        [HttpGet]
        [Route("Comments")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCommentsListAsync()
        {
            var comments = await commentService.GetAllCommentsAsync();

            var model = new GetCommentsResponse();

            foreach (var comment in comments)
            {
                model.Coments.Add(mapper.Map<CommentViewModel>(comment));
            }

            logger.LogInformation("GET CommentList page responsed");

            return View("CommentList", model);
        }

        /// <summary>
        /// Comments by Author
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /AuthorComments/{id:int}
        /// </remarks>
        /// <returns>Returns GetCommentsByAuthorResponce</returns>
        /// <response code="200">Success</response>
        [HttpGet]
        [Route("AuthorComments/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCommentsByAuthorIdAsync(string id)
        {
            var comments = await commentService.GetCommentsByAuthorIdAsync(id);

            var model = new GetCommentsByAuthorResponce();

            foreach (var comment in comments)
            {
                model.Coments.Add(mapper.Map<CommentViewModel>(comment));
            }

            logger.LogInformation("GET CommentList by user-{0} page responsed",
                id);

            return Ok(model);
        }

        /// <summary>
        /// Create comment
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /Add
        /// </remarks>
        /// <response code="200">Success</response>
        /// <response code="400">If invalid request</response>
        /// <response code="401">If the user is unauthorized</response>
        /// <response code="403">If the user doesn't have rights</response>
        [Authorize]
        [Route("Add")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> CreateCommentAsync(CreateCommentRequest commentModel)
        {
            logger.LogInformation("POST User-{0} send newComment data",
                User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier")).Value);

            var comment = mapper.Map<CommentModel>(commentModel);

            await commentService.CreateCommentAsync(comment);

            logger.LogInformation("POST User-{0} created comment",
                User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier")).Value);

            return Ok();
        }

        /// <summary>
        /// Edit comment
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// POST /Edit
        /// </remarks>
        /// <response code="200">Success</response>
        /// <response code="400">If invalid request</response>
        /// <response code="401">If the user is unauthorized</response>
        [Authorize]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> UpdateCommentAsync(EditCommentRequest updateComment)
        {
            try
            {
                logger.LogInformation("POST User-{0} send updateComment data",
                User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier")).Value);

                var comment = mapper.Map<CommentModel>(updateComment);

                await commentService.UpdateCommentAsync(User, comment);

                logger.LogInformation("POST User-{0} updated comment",
                    User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier")).Value);

                return Ok();
            }
            catch (ForbiddenException)
            {
                return Forbid();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Delete comment
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// POST /Delete
        /// </remarks>
        /// <response code="200">Success</response>
        /// <response code="400">If invalid request</response>
        /// <response code="401">If the user is unauthorized</response>
        [Authorize]
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> DeleteCommentAsync(int commentId)
        {
            try
            {
                await commentService.DeleteCommentAsync(User, commentId);

                return Ok();
            }
            catch (ForbiddenException)
            {
                return Forbid();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}

