using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MalmöTradera.web.ViewModel
{
    public class ItemPostViewModel
    {
        public int id { get; set; }
         [Required(ErrorMessage = "namn måste anges")]
         [DisplayName("Namn")]
        public string Name { get; set; }
         [Required(ErrorMessage = "pris måste anges")]
        [DisplayName("Pris")]
        public int price { get; set; }
        [DisplayName("Beskrivning")]
        public string Description { get; set; }
        [DisplayName("ImageUrl")]
        public string ImageUrl { get; set; }
    }
}