using AutoMapper;
using Blog.API.Models.Requests.Article;
using Blog.API.Models.Responses.Article;
using Blog.API.Models.ViewModels;
using Blog.BLL.Exceptions;
using Blog.BLL.Interfaces;
using Blog.BLL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.API.Controllers.Blog
{
    [Route("api/[controller]")]
    public class ArticleController : Controller
    {
        readonly ILogger<ArticleController> logger;
        readonly IMapper mapper;
        readonly IArticleService articleService;

        public ArticleController(ILogger<ArticleController> logger, IMapper mapper, IArticleService articleService)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.articleService = articleService;
        }

        /// <summary>
        /// Articles list
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /Articles
        /// </remarks>
        /// <returns>Returns GetAccountsResponse</returns>
        /// <response code="200">Success</response>
        [Route("Articles")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetArticlesListAsync()
        {
            var articles = await articleService.GetAllArticlesAsync();

            var model = new GetArticlesResponse();

            foreach (var article in articles)
            {
                model.Articles.Add(mapper.Map<ArticleViewModel>(article));
            }

            logger.LogInformation("GET Articles page responed");

            return Ok(model);
        }

        /// <summary>
        /// Article by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /Article/{id:int}
        /// </remarks>
        /// <returns>Returns GetAccountResponse</returns>
        /// <response code="200">Success</response>
        [Route("Article/{id:int}")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetArticleByIdAsync(int id)
        {
            var article = await articleService.GetArticleByIdAsync(id);

            var model = mapper.Map<GetArticleResponse>(article);

            logger.LogInformation("GET Article-{0} page responsed",
                id);

            return Ok(model);
        }

        /// <summary>
        /// Articles by author
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /Author/{authorId:string}
        /// </remarks>
        /// <returns>Returns GetAccountsResponse</returns>
        /// <response code="200">Success</response>
        [Route("Author/{authorId:string}")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetArticlesByAuthorIdAsync(string authorId)
        {
            var articles = await articleService.GetArticlesByAuthorIdAsync(authorId);

            var model = new GetArticlesByAuthorPesponse();

            foreach (var article in articles)
            {
                model.Articles.Add(mapper.Map<ArticleViewModel>(article));
            }

            logger.LogInformation("GET Articles by user-{0} page responsed",
                authorId);

            return Ok(model);
        }

        /// <summary>
        /// Articles by tag
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /Tag/{tagName:string}
        /// </remarks>
        /// <returns>Returns GetAccountsResponse</returns>
        /// <response code="200">Success</response>
        [Route("Tag/{tagName:string}")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetArticlesByTagAsync(string tagName)
        {
            var articles = await articleService.GetArticlesByTagAsync(tagName);

            var model = new GetArticlesByTagResponse();

            model.Tag = tagName;

            foreach (var article in articles)
            {
                model.Articles.Add(mapper.Map<ArticleViewModel>(article));
            }

            logger.LogInformation("GET Articles by tag-{0} page responsed",
                tagName);

            return Ok(model);
        }

        /// <summary>
        /// Create article
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// POST /Add
        /// </remarks>
        /// <response code="200">Success</response>
        /// <response code="400">If invalid request</response>
        [Authorize]
        [Route("Add")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> CreateArticleAsync(CreateArticleRequest articleModel)
        {
            logger.LogInformation("POST User-{0} send newArticle data",
                User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier")).Value);

            articleModel.Tags = articleModel.GetSelectedTags();

            var article = mapper.Map<ArticleModel>(articleModel);

            await articleService.CreateArticleAsync(article);

            logger.LogInformation("POST User-{0} created article",
                User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier")).Value);

            return Ok();
        }

        /// <summary>
        /// Edit article
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// PUT
        /// </remarks>
        /// <response code="200">Success</response>
        /// <response code="400">If invalid request</response>
        /// <response code="401">If the user is unauthorized</response>
        /// <response code="403">If the user doesn't have rights</response>
        [Authorize]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> UpdateArticleAsync(EditArticleRequest updateArticle)
        {
            try
            {
                logger.LogInformation("POST User-{0} send editArticle data",
                User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier")).Value);

                var article = mapper.Map<ArticleModel>(updateArticle);

                await articleService.UpdateArticleAsync(User, article);

                logger.LogInformation("POST User-{0} edited article-{1}",
                    User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier")).Value,
                    updateArticle.Id);

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
        /// Delete article
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// DELETE
        /// </remarks>
        /// <response code="200">Success</response>
        /// <response code="400">If invalid request</response>
        /// <response code="401">If the user is unauthorized</response>
        /// <response code="403">If the user doesn't have rights</response>
        [Authorize]
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> DeleteArticleAsync(int articleId)
        {
            try
            {
                await articleService.DeleteArticleAsync(User, articleId);

                logger.LogInformation("POST User-{0} deleted article-{1}",
                    User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier")).Value,
                    articleId);

                return RedirectToAction("Articles");
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

