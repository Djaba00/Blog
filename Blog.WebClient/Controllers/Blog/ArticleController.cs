using AutoMapper;
using Blog.BLL.Interfaces;
using Blog.BLL.Models;
using Microsoft.AspNetCore.Mvc;
using Blog.WebService.VIewModels.Article;
using Microsoft.AspNetCore.Authorization;
using Blog.WebService.VIewModels.User;

namespace Blog.WebService.Controllers.Blog
{
    public class ArticleController : Controller
    {
        IMapper mapper;
        IArticleService articleService;
        IAccountService accountService;

        public ArticleController(IMapper mapper, IArticleService articleService, IAccountService accountService)
        {
            this.mapper = mapper;
            this.articleService = articleService;
            this.accountService = accountService;
        }

        [Authorize]
        [Route("NewArticle")]
        [HttpGet]
        public async Task<IActionResult> CreateArticleAsync()
        {
            var user = User;

            var result = await accountService.GetAuthAccountAsync(user);

            var model = mapper.Map<UserViewModel>(result.Profile);

            return View("NewArticle", model);
        }

        [Authorize]
        [Route("NewArticle")]
        [HttpPost]
        public async Task<IActionResult> CreateArticleAsync(CreateArticleViewModel articleModel)
        {
            if (ModelState.IsValid)
            {
                var article = mapper.Map<ArticleModel>(articleModel);

                article.Created = DateTime.Now;

                await articleService.CreateArticleAsync(article);

                return RedirectToAction("MyPage");
            }

            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        [Route("EditArticle")]
        [HttpPost]
        public async Task<IActionResult> UpdateArticleAsync(EditArticleViewModel updateArticle)
        {
            if (ModelState.IsValid)
            {
                var article = mapper.Map<ArticleModel>(updateArticle);

                article.Changed = DateTime.Now;

                await articleService.UpdateArticleAsync(article);

                return RedirectToAction("MyPage");
            }

            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        [Route("DeleteArticle")]
        [HttpPost]
        public async Task<IActionResult> DeleteArticleAsync(int id)
        {
            await articleService.DeleteArticleAsync(id);

            return RedirectToAction("MyPage");
        }

        [Route("Articles")]
        [HttpGet]
        public async Task<IActionResult> GetArticlesListAsync()
        {
            var articles = await articleService.GetAllArticlesAsync();

            var models = new List<ArticleModel>();

            foreach (var article in articles)
            {
                models.Add(mapper.Map<ArticleModel>(article));
            }

            return View("ArticlesList", models);
        }

        [HttpGet]
        public async Task<IActionResult> GetArticlesByAuthorIdAsync(string id)
        {
            var articles = await articleService.GetArticlesByAuthotIdAsync(id);

            var models = new List<ArticleModel>();

            foreach (var article in articles)
            {
                models.Add(mapper.Map<ArticleModel>(article));
            }

            return View("ArticlesList", models);
        }

        [Route("Article/{id:int}")]
        [HttpGet]
        public async Task<IActionResult> GetArticleByAuthorIdAsync(string id)
        {
            var article = await articleService.GetArticlesByAuthotIdAsync(id);

            var model = mapper.Map<ArticleModel>(article);

            return View("ArticlesList", model);
        }
    }
}
