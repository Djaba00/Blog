using AutoMapper;
using Blog.BLL.Interfaces;
using Blog.BLL.Models;
using Microsoft.AspNetCore.Mvc;
using Blog.WebService.ViewModels.Article;
using Microsoft.AspNetCore.Authorization;
using Blog.WebService.ViewModels.Tag;
using Blog.WebService.ViewModels.Account;
using Blog.WebService.ViewModels.Comment;

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
            var currentUser = User;

            var result = accountService.GetAuthAccountAsync(currentUser);

            var dbTags = tagService.GetAllTagsAsync();

            result.Wait();
            dbTags.Wait();

            var user = mapper.Map<AccountViewModel>(result.Result);

            var model = new CreateArticleViewModel();

            model.AuthorId = user.Id;

            foreach (var tag in dbTags.Result)
            {
                model.ArticleTags.Add(mapper.Map<HashTagViewModel>(tag));
            }

            logger.LogInformation("{0} GET CreateArticle page responsed for user-{1}",
                DateTime.UtcNow.ToLongTimeString(),
                User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier")).Value);

            return View("CreateArticle", model);
        }

        [Authorize]
        [Route("Add")]
        [HttpPost]
        public async Task<IActionResult> CreateArticleAsync(CreateArticleViewModel articleModel)
        {
            logger.LogInformation("{0} POST User-{1} send newArticle data",
                DateTime.UtcNow.ToLongTimeString(),
                User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier")).Value);

            var selectedTags = articleModel.ArticleTags.Where(c => c.Selected).ToList();

            articleModel.ArticleTags = selectedTags;
            
            var article = mapper.Map<ArticleModel>(articleModel);

            await articleService.CreateArticleAsync(article);

            logger.LogInformation("{0} POST User-{1} created article",
                DateTime.UtcNow.ToLongTimeString(),
                User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier")).Value);

            return RedirectToAction("Articles");
        }

        [Authorize]
        [Route("Edit")]
        [HttpGet]
        public async Task<IActionResult> UpdateArticleAsync(int id)
        {
            var article = await articleService.GetArticleByIdForEditAsync(id);

            var currentUser = await accountService.GetAuthAccountAsync(User);
            
            var model = mapper.Map<EditArticleViewModel>(article);

            if (article.UserId == currentUser.Id || currentUser.Roles.Select(r => r.Name).Contains("Admin") 
                || currentUser.Roles.Select(r => r.Name).Contains("Moderator"))
            {
                logger.LogInformation("{0} GET EditArticle page responsed for user-{1}",
                  DateTime.UtcNow.ToLongTimeString(),
                  User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier")).Value);

                return View("EditArticle", model);
            }

            logger.LogInformation("{0} GET EditArticle page forbidden for user-{1}",
                DateTime.UtcNow.ToLongTimeString(),
                User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier")).Value);

            return RedirectToAction("Articles");
        }

        [Authorize]
        [Route("Edit")]
        [HttpPost]
        public async Task<IActionResult> UpdateArticleAsync(EditArticleViewModel updateArticle)
        {
            logger.LogInformation("{0} POST User-{1} send editArticle data",
              DateTime.UtcNow.ToLongTimeString(),
               User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier")).Value);

            var currentUser = await accountService.GetAuthAccountAsync(User);

            if (updateArticle.UserId == currentUser.Id || currentUser.Roles.Select(r => r.Name).Contains("Admin")
                || currentUser.Roles.Select(r => r.Name).Contains("Moderator"))
            {
                var selectedTags = updateArticle.Tags.Where(c => c.Selected).ToList();

                updateArticle.Tags = selectedTags;

                var article = mapper.Map<ArticleModel>(updateArticle);

                await articleService.UpdateArticleAsync(article);

                logger.LogInformation("{0} POST User-{1} edited article-{2}",
                    DateTime.UtcNow.ToLongTimeString(),
                    User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier")).Value,
                    updateArticle.Id);

                return RedirectToAction("Articles");
            }

            logger.LogInformation("{0} POST Edit article-{1} forbidden for user-{2}",
                DateTime.UtcNow.ToLongTimeString(),
                updateArticle.Id,
                User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier")).Value);

            return RedirectToAction("Articles");
        }

        [Authorize]
        [Route("Delete")]
        [HttpPost]
        public async Task<IActionResult> DeleteArticleAsync(int id)
        {
            await articleService.DeleteArticleAsync(id);

            logger.LogInformation("{0} POST User-{1} deleted article-{2}",
                DateTime.UtcNow.ToLongTimeString(),
                User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier")).Value,
                id);

            return RedirectToAction("Articles");
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

            logger.LogInformation("{0} GET Articles page responed",
                DateTime.UtcNow.ToLongTimeString());

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

            model.CurrentUser = userVm;

            model.AddComment = new CreateCommentViewModel();

            logger.LogInformation("{0} GET Article-{1} page responsed",
                DateTime.UtcNow.ToLongTimeString(),
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

            logger.LogInformation("{0} GET Articles by user-{1} page responsed",
                DateTime.UtcNow.ToLongTimeString(),
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

            logger.LogInformation("{0} GET Articles by tag-{1} page responsed",
                DateTime.UtcNow.ToLongTimeString(),
                tagName);

            return View("ArticleListByTag", model);
        }
    }
}
