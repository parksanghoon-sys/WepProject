//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using BlazorBigexecutionExample.Services;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace BlazorBigexecutionExample.Services.Tests
//{
//	[TestClass()]
//	public class UniversityHttpServiceTests
//	{


//		[TestMethod()]
//		public async Task GetMethodListTest()
//		{
//			HttpClient client = new HttpClient();
//			UniversityHttpService universityHttpService = new UniversityHttpService(client);
//			var list = await universityHttpService.GetMethodList("http://universities.hipolabs.com/search?country=Korea,+Republic+of");
//			Assert.IsTrue(list is not null);
//		}

//		[TestMethod()]
//		public async Task GetMethodTest()
//		{
//			HttpClient client = new HttpClient();
//			UniversityHttpService universityHttpService = new UniversityHttpService(client);
//			var list = await universityHttpService.GetMethodList("http://universities.hipolabs.com/search?country=Korea,+Republic+of");
//			Assert.IsTrue(list is not null);
//		}

//		[TestMethod()]
//		public void PostMethodTest()
//		{
//			Assert.Fail();
//		}

//		[TestMethod()]
//		public void PostMethodGetObjectTest()
//		{
//			Assert.Fail();
//		}

//		[TestMethod()]
//		public void PostMethodListTest()
//		{
//			Assert.Fail();
//		}
//	}
//}