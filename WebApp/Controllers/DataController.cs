using Common;
using Core;
using Microsoft.AspNetCore.Mvc;

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
                _service.LoadRawData(requestParams.FolderPath, requestParams.FilenameRegex);
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
            return await Task.Run(() => _service.GetDataSeries(fieldName));
        }

        [HttpPost("timeseries")]
        public async Task<DataSeries[]> GetMultipleTimeSeries(GetMultipleTimeSeriesRequestParams requestParams)
        {
            return await Task.Run(() => requestParams.FieldNames.Select(
                f => _service.GetDataSeries(f)).Where(ds => ds != null).Select(ds => ds.Value).ToArray());
        }

        public struct GetMultipleTimeSeriesRequestParams
        {
            public string[] FieldNames { get; set; }
        }
    }
}
