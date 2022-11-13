using BPMS_2.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BPMS_2.Tests
{
	public class HomeControllerTest
	{
		[Fact]
		public void Test1()
		{
			var controller = new HomeController();
			var result = controller.Index() as ViewResult;
			Assert.Equal("Index", result?.ViewName);
		}
	}
}