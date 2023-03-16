using System.Text.Json;
using MalmöTradera.web.Models;
using MalmöTradera.web.ViewModel;
using Microsoft.AspNetCore.Mvc;
using westcoast_cars.web.Data;


[Route("Items")]
    public class ItemController : Controller
    {
        private readonly ItemsContext _Context;
        private readonly JsonSerializerOptions _Options;
        private readonly IHttpClientFactory _HTTPClient;
        private readonly string _BaseUrl;

        public ItemController(ItemsContext context, IConfiguration config, IHttpClientFactory HttpClient)
        {
            _HTTPClient = HttpClient;
            _Context = context;
            _BaseUrl = config.GetSection("apiSettings:baseUrl").Value;
            _Options = new JsonSerializerOptions {PropertyNameCaseInsensitive = true};

        }

        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            using var client =_HTTPClient.CreateClient();

            var response = await client.GetAsync($"{_BaseUrl}/items");

            if (!response.IsSuccessStatusCode) return Content("Ops något gick fel!");

            var Json = await response.Content.ReadAsStringAsync();
            var Items = JsonSerializer.Deserialize<List<ItemsListViewModel>>(Json, _Options);
            return View("Index", Items);

        }

    [HttpGet("details/{id}")]
    public async Task<IActionResult> Details(int id)
    {
        using var client = _HTTPClient.CreateClient();
        var response = await client.GetAsync($"{_BaseUrl}/Items/{id}");

        if (!response.IsSuccessStatusCode) return Content("Oops det gick fel");

        var json = await response.Content.ReadAsStringAsync();

        var Items = JsonSerializer.Deserialize<ItemsDetailListViewModel>(json, _Options);

        return View("Details", Items);
    }

    [HttpGet("Create")]

    public IActionResult Create()
    {
        var Item = new ItemPostViewModel{};
        return View("Create", Item);
    }

    [HttpPost("Create")]

    public async Task<IActionResult> Create (ItemPostViewModel Item)
    {
        var client = _HTTPClient.CreateClient();
        var post = await client.PostAsJsonAsync($"{_BaseUrl}/Items", Item);

        if (!post.IsSuccessStatusCode) return Content("Bad request");

        return RedirectToAction("Index");
    }
}
