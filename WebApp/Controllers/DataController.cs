﻿using Microsoft.AspNetCore.Mvc;
using Core;
using Common;

namespace WebApp.Controllers
{

    
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : Controller
    {
        private ILogger<DataController> _logger;
        private readonly CoreService _service;
        public DataController(ILogger<DataController> logger)
        {
            _logger = logger;
            _service = new CoreService();
        }



        // TODO: dailyNoteFormatForRegex
        [HttpPost("load")]
        public async Task<IActionResult> LoadData(LoadDataRequestParams requestParams)
        {
            await Task.Run(() => 
            {
                _service.InitDataProvider(requestParams.FolderPath, requestParams.FilenameRegex);
                _service.GetRawData();
                _service.ProcessData();
            });
            return Ok();
        }

        public struct LoadDataRequestParams
        {
            public string FolderPath { get; set; }
            public string FilenameRegex { get; set; }
        }

        [HttpGet("shape")]
        public async Task<DataShape> GetDataShape()
        {
            return await Task.Run(() => _service.GetDataShape());
        }
    }
}
