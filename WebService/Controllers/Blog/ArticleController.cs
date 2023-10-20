using AutoMapper;
using Blog.BLL.Interfaces;
using Blog.BLL.Models;
using Microsoft.AspNetCore.Mvc;
using Blog.WebService.ViewModels.Article;
using Microsoft.AspNetCore.Authorization;
using Blog.WebService.ViewModels.User;
using Blog.WebService.ViewModels.Tag;
using Blog.DAL.Entities;

namespace Blog.WebService.Controllers.Blog
{
    [Route("Article")]
    public class ArticleController : Controller
    {
        IMapper mapper;
        IArticleService articleService;
        IAccountService accountService;
        ITagService tagService;

        public ArticleController(IMapper mapper, IArticleService articleService, IAccountService accountService, ITagService tagService)
        {
            this.mapper = mapper;
            this.articleService = articleService;
            this.accountService = accountService;
            this.tagService = tagService;
        }

        [Authorize]
        [Route("AddArticle")]
        [HttpGet]
        public async Task<IActionResult> CreateArticleAsync()
        {
            var currentUser = User;

            var result = accountService.GetAuthAccountAsync(currentUser);

            var dbTags = tagService.GetAllTagsAsync();

            result.Wait();
            dbTags.Wait();

            var user = mapper.Map<UserViewModel>(result.Result.Profile);


            var model = new CreateArticleViewModel();

            model.AuthorId = user.Id;

            foreach (var tag in dbTags.Result)
            {
                model.ArticleTags.Add(mapper.Map<HashTagViewModel>(tag));
            }

            return View("CreateArticle", model);
        }

        [Authorize]
        [Route("AddArticle")]
        [HttpPost]
        public async Task<IActionResult> CreateArticleAsync(CreateArticleViewModel articleModel)
        {
            var selectedTags = articleModel.ArticleTags.Where(c => c.Selected).ToList();

            articleModel.ArticleTags = selectedTags;
            
            var article = mapper.Map<ArticleModel>(articleModel);

            await articleService.CreateArticleAsync(article);

            return RedirectToAction("Articles");
        }

        [Authorize]
        [Route("EditArticle/{id:int}")]
        [HttpGet]
        public async Task<IActionResult> UpdateArticleAsync(int id)
        {
            var article = await articleService.GetArticleByIdForEditAsync(id);
            
            var model = mapper.Map<EditArticleViewModel>(article);

            return View("EditArticle", model);
        }

        [Authorize]
        [Route("EditArticle")]
        [HttpPost]
        public async Task<IActionResult> UpdateArticleAsync(EditArticleViewModel updateArticle)
        {
            var selectedTags = updateArticle.Tags.Where(c => c.Selected).ToList();

            updateArticle.Tags = selectedTags;

            var article = mapper.Map<ArticleModel>(updateArticle);

            await articleService.UpdateArticleAsync(article);

            return RedirectToAction("Articles");
        }

        [Authorize]
        [Route("DeleteArticle")]
        [HttpPost]
        public async Task<IActionResult> DeleteArticleAsync(int id)
        {
            await articleService.DeleteArticleAsync(id);

            return RedirectToAction("Articles");
        }

        [Route("Articles")]
        [HttpGet]
        public async Task<IActionResult> GetArticlesListAsync()
        {
            var articles = await articleService.GetAllArticlesAsync();

            var models = new List<ArticleViewModel>();

            foreach (var article in articles)
            {
                models.Add(mapper.Map<ArticleViewModel>(article));
            }

            return View("ArticleList", models);
        }

        [Route("Article")]
        [HttpGet]
        public async Task<IActionResult> GetArticleByIdAsync(int id)
        {
            var article = await articleService.GetArticleByIdAsync(id);

            var model = mapper.Map<ArticleViewModel>(article);

            return View("ArticleList", model);
        }

        [Route("Author")]
        [HttpGet]
        public async Task<IActionResult> GetArticlesByAuthorIdAsync(int authorId)
        {
            var articles = await articleService.GetArticlesByAuthorIdAsync(authorId);

            var model = new List<ArticleViewModel>();

            foreach (var article in articles)
            {
                model.Add(mapper.Map<ArticleViewModel>(article));
            }
            
            return View("ArticleList", model);
        }

        [Route("Tag")]
        [HttpGet]
        public async Task<IActionResult> GetArticlesByTagAsync(string tagName)
        {
            var articles = await articleService.GetArticlesByTagAsync(tagName);

            var model = new List<ArticleViewModel>();

            foreach (var article in articles)
            {
                model.Add(mapper.Map<ArticleViewModel>(article));
            }

            return View("ArticleList", model);
        }
    }
}
