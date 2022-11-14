using BPMS_2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BPMS_2.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Data.Common;
using Newtonsoft.Json;
using System.Net;
using BPMS_2.ViewModels;
using BPMS_2.Data;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Headers;

namespace BPMS_2.Tests
{
    public class UnitTest1 : IClassFixture<WebApplicationFactory<BPMS_2Context>>
    {
        private readonly WebApplicationFactory<BPMS_2Context> _webApplicationFactory;
        public UnitTest1(WebApplicationFactory<BPMS_2Context> webApplicationFactory)
        {
            _webApplicationFactory = webApplicationFactory;
        }
       
        
        [Fact]
        public async void Cart_Index_Loads()
        {
            var client = _webApplicationFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:7043/Cart/Index");
            int code = (int)response.StatusCode;

            Assert.Equal(200, code);
        }
        [Fact]
        public async void Rent_Index_Loads()
        {
            var client = _webApplicationFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:7043/Rent/Index");
            int code = (int)response.StatusCode;

            Assert.Equal(200, code);
        }
        [Fact]
        public async void Home_Index_Loads()
        {
            var client = _webApplicationFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:7043/Home/Index");
            int code = (int)response.StatusCode;

            Assert.Equal(200, code);
        }

		


    }
}