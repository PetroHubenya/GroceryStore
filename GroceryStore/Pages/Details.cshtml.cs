using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using GroceryStore.Models;
using System.Reflection;
using System.Xml.Linq;

namespace GroceryStore.Pages
{
	public class DetailsModel : PageModel
	{
		public List<GroceryItem> Foods = Inventory.ToList();

		public GroceryItem CurrentFood;

		[BindProperty]
		public int Quantity { get; set; }

		public double TotalPrice { get; set; }

		public async Task<IActionResult> OnGetAsync(string name)
		{
			using (StreamWriter writer = new StreamWriter("log.txt", append: true))
			{
				await writer.WriteLineAsync($"{DateTime.Now} {name}");
			}

			CurrentFood = Foods.Find(food => food.Name.ToLower() == name.ToLower());

			TotalPrice = Quantity * CurrentFood.Price;

			if (CurrentFood == null )
			{
				return NotFound();
			}			

			return Page();
		}

		public void OnPost(string name)
		{
			CurrentFood = Foods.Find(food => food.Name.ToLower() == name.ToLower());

			TotalPrice = Quantity * CurrentFood.Price;
		}
	}
}
