using Microsoft.AspNetCore.Mvc;
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
        public DataController(ILogger<DataController> logger, CoreService service)
        {
            _logger = logger;
            _service = service;
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

        [HttpGet("timeseries")]
        public async Task<TimeSeries?> GetTimeSeries(string fieldName)
        {
            var ts = await Task.Run(() => _service.GetTimeSeries(fieldName));
            // TODO: proper typing
            var res = new
            {
                name = ts.Value.Name,
                entries = ts.Value.Entries.ToArray() // TODO: to 'series'
            };
            return await Task.Run(() => _service.GetTimeSeries(fieldName));
        }
    }
}
