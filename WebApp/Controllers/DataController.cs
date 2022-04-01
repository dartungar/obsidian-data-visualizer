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
        public async Task<DataSeries?> GetTimeSeries(string fieldName)
        {
            return await Task.Run(() => _service.GetTimeSeries(fieldName));
        }

        [HttpPost("timeseries")]
        public async Task<DataSeries[]> GetMultipleTimeSeries(GetMultipleTimeSeriesRequestParams requestParams)
        {
            return await Task.Run(() => requestParams.FieldNames.Select(
                f => _service.GetTimeSeries(f)).Where(ts => ts != null).Select(ts => ts.Value).ToArray());
        }

        public struct GetMultipleTimeSeriesRequestParams
        {
            public string[] FieldNames { get; set; }
        }
    }
}
