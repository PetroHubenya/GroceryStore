using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using GroceryStore.Models;
using System.Reflection;

namespace GroceryStore.Pages
{
	public class DetailsModel : PageModel
	{
		public List<GroceryItem> Foods = Inventory.ToList();

		public GroceryItem CurrentFood;

		public int Quantity { get; set; }

		public double TotalPrice {
			get
			{
				return Quantity * CurrentFood.Price;
			} 
		}

		public async Task<IActionResult> OnGetAsync(string name)
		{
			using (StreamWriter writer = new StreamWriter("log.txt", append: true))
			{
				await writer.WriteLineAsync($"{DateTime.Now} {name}");
			}

			CurrentFood = Foods.Find(food => food.Name.ToLower() == name.ToLower());

			if (CurrentFood == null )
			{
				return NotFound();
			}			

			return Page();
		}
	}
}
