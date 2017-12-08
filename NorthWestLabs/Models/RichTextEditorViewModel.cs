using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
namespace NorthWestLabs.Models
{
    public class RichTextEditorViewModel
    {
        [AllowHtml]
        [Display(Name = "Message")]
        public string Message
        {
            get;
            set;
        }
    }
}