using AutoMapper;
using Blog.BLL.Interfaces;
using Blog.BLL.Models;
using Microsoft.AspNetCore.Mvc;
using Blog.WebService.ViewModels.Article;
using Microsoft.AspNetCore.Authorization;
using Blog.WebService.ViewModels.Tag;
using Blog.WebService.ViewModels.Account;
using Blog.WebService.ViewModels.Comment;
using Blog.BLL.Exceptions;

namespace Blog.WebService.Controllers.Blog
{
    [Route("Article")]
    public class ArticleController : Controller
    {
        readonly ILogger<ArticleController> logger;
        readonly IMapper mapper;
        readonly IArticleService articleService;
        readonly IAccountService accountService;
        readonly ITagService tagService;

        public ArticleController(ILogger<ArticleController> logger, IMapper mapper, IArticleService articleService, IAccountService accountService, ITagService tagService)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.articleService = articleService;
            this.accountService = accountService;
            this.tagService = tagService;
        }

        [Authorize]
        [Route("Add")]
        [HttpGet]
        public async Task<IActionResult> CreateArticleAsync()
        {
            var result = accountService.GetAuthAccountAsync(User);

            var dbTags = tagService.GetAllTagsAsync();

            result.Wait();
            dbTags.Wait();

            var user = mapper.Map<AccountViewModel>(result.Result);

            var model = new CreateArticleViewModel();

            model.UserId = user.Id;

            foreach (var tag in dbTags.Result)
            {
                model.Tags.Add(mapper.Map<HashTagViewModel>(tag));
            }

            logger.LogInformation("GET CreateArticle page responsed for user-{0}",
                User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier")).Value);

            return View("CreateArticle", model);
        }

        [Authorize]
        [Route("Add")]
        [HttpPost]
        public async Task<IActionResult> CreateArticleAsync(CreateArticleViewModel articleModel)
        {
            logger.LogInformation("POST User-{0} send newArticle data",
                User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier")).Value);

            articleModel.Tags = articleModel.GetSelectedTags();
            
            var article = mapper.Map<ArticleModel>(articleModel);

            await articleService.CreateArticleAsync(article);

            logger.LogInformation("POST User-{0} created article",
                User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier")).Value);

            return RedirectToAction("Articles");
        }

        [Authorize]
        [Route("Edit/{id:int}")]
        [HttpGet]
        public async Task<IActionResult> UpdateArticleAsync(int id)
        {
            try
            {
                var article = await articleService.GetUpdateArticle(User, id);

                var model = mapper.Map<EditArticleViewModel>(article);

                logger.LogInformation("GET EditArticle page responsed for user-{0}",
                    User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier")).Value);

                return View("EditArticle", model);
            }
            catch (ForbiddenException)
            {
                return RedirectToAction("403", "Error");
            }
        }

        [Authorize]
        [Route("Edit")]
        [HttpPost]
        public async Task<IActionResult> UpdateArticleAsync(EditArticleViewModel updateArticle)
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

                return RedirectToAction("Articles");
            }
            catch (ForbiddenException)
            {
                return RedirectToAction("403", "Error");
            }
            
        }

        [Authorize]
        [Route("Delete")]
        [HttpPost]
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
                return RedirectToAction("403", "Error");
            }
        }

        [Route("Articles")]
        [HttpGet]
        public async Task<IActionResult> GetArticlesListAsync()
        {
            var articles = articleService.GetAllArticlesAsync();

            var user = accountService.GetAuthAccountAsync(User);

            articles.Wait();
            user.Wait();

            var account = mapper.Map<AccountViewModel>(user.Result);

            var model = new ArticleListViewModel();

            model.CurrentAccount = account;

            foreach (var article in articles.Result)
            {
                model.Articles.Add(mapper.Map<ArticleViewModel>(article));
            }

            logger.LogInformation("GET Articles page responed");

            return View("ArticleList", model);
        }

        [Route("Article/{id:int}")]
        [HttpGet]
        public async Task<IActionResult> GetArticleByIdAsync(int id)
        {
            var article = articleService.GetArticleByIdAsync(id);

            var user = accountService.GetAuthAccountAsync(User);

            article.Wait();
            user.Wait();

            var model = mapper.Map<ArticleViewModel>(article.Result);

            var userVm = mapper.Map<AccountViewModel>(user.Result);

            model.CurrentAccount = userVm;

            model.AddComment = new CreateCommentViewModel();

            logger.LogInformation("GET Article-{0} page responsed",
                id);

            return View("Article", model);
        }

        [Route("Author")]
        [HttpGet]
        public async Task<IActionResult> GetArticlesByAuthorIdAsync(string authorId)
        {
            var articles = await articleService.GetArticlesByAuthorIdAsync(authorId);

            var model = new List<ArticleViewModel>();

            foreach (var article in articles)
            {
                model.Add(mapper.Map<ArticleViewModel>(article));
            }

            logger.LogInformation("GET Articles by user-{0} page responsed",
                authorId);

            return View("ArticleList", model);
        }

        [Route("Tag")]
        [HttpGet]
        public async Task<IActionResult> GetArticlesByTagAsync(string tagName)
        {
            var user = accountService.GetAuthAccountAsync(User);
            var articles = articleService.GetArticlesByTagAsync(tagName);

            user.Wait();
            articles.Wait();

            var model = new ArticleListByTagViewModel();

            var account = mapper.Map<AccountViewModel>(user.Result);

            model.CurrentAccount = account;
            model.Tag = tagName;

            foreach (var article in articles.Result)
            {
                model.Articles.Add(mapper.Map<ArticleViewModel>(article));
            }

            logger.LogInformation("GET Articles by tag-{0} page responsed",
                tagName);

            return View("ArticleListByTag", model);
        }
    }
}
