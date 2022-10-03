using Microsoft.AspNetCore.Mvc.RazorPages;
#pragma warning disable CS8618

namespace IsolationWebApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;

            /*
             * Mock-up spoken language
             */
            Language = "sp";
        }

        public string Result { get; set; }
        /// <summary>
        /// Application spoken language
        /// </summary>
        public string Language { get; set; }

        public void OnGet()
        {

        }
        public void OnGetMenu1(string id1)
        {
            Result = "Id1: " + id1;
        }
    }
}