using api.tags.Dto;
using api.tags.Interface;
using api.tags.Model;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace api.tags.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class WipTagsController : ControllerBase
    {


        private readonly ILogger<WipTagsController> _logger;
        private readonly IWiptagRepository _wipTagRepository;

        public WipTagsController(ILogger<WipTagsController> logger, IWiptagRepository wipTagRepository)
        {
            _logger = logger;
            _wipTagRepository = wipTagRepository;
        }
        public class ReponseLogin
        {
            [JsonPropertyName("data")]
            public ResponseData data { get; set; }
        }
        public class ResponseData
        {
            [JsonPropertyName("access_token")]
            public string AccessToken { get; set; }
        }
        [HttpGet]
        public async Task<ActionResult> Get(CancellationToken cancellationToken)
        {
            try
            {
                var result = await _wipTagRepository.GetAll(cancellationToken);

                return Ok(result);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Insert(CancellationToken cancellationToken)
        {
            try
            {
                var token = "";
                using (var http = new HttpClient())
                {
                    var userInfo = new
                    {
                        userName = "myelis",
                        password = "Melis@2021"
                    };
                    var json = new StringContent(JsonSerializer.Serialize(userInfo), Encoding.UTF8, "application/json");
                    var response = await http.PostAsync("https://api-laundry-hml.elisbrasil.com/v1/login", json);

                    if (response.IsSuccessStatusCode)
                    {
                        var responseAsString = await response.Content.ReadAsStringAsync();

                        var responseDeserialize = JsonSerializer.Deserialize<ReponseLogin>(responseAsString);

                        token = responseDeserialize.data.AccessToken;
                    }
                }
                var filter = new FilterBaseDto
                {
                    CurrentePage = 1,
                    PageSize = 1000,
                    Parameters = [],
                    MultiSort = [ new MultiSortDto
                    {
                        Field = "insertdate",
                        Type = "desc"
                    } ]
                };
                var page = 1;
                do
                {
                    using (var http = new HttpClient())
                    {
                        http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                        var response = await http.GetAsync("https://api-customer-hml.elisbrasil.com/api/tags/region?filters=" +
                                                           JsonSerializer.Serialize(filter), cancellationToken);

                        if (response.IsSuccessStatusCode)
                        {
                            var responseAsString = await response.Content.ReadAsStringAsync();

                            var responseDeserialize = JsonSerializer.Deserialize<ResponseCustomerDto<WipTagDto>>(responseAsString);


                            var entity = responseDeserialize.Data.Items.Select(x => new WipTagModel
                            {
                                DeleteDate = x.DeleteDate,
                                EpcCode = x.EpcCode,
                                InsertDate = x.InsertDate,
                                LastUpdate = x.LastUpdate,
                                RegionRfid = x.RegionRfid
                            });
                            var obj = _wipTagRepository.Insert(entity, cancellationToken);

                            var dateInsert = responseDeserialize.Data.Items.Max(x => x.InsertDate);

                            filter.Parameters =
                            [
                                new ParametersDto
                                {
                                    FilterCondition = "latter-then-or-equal",
                                    FilterDataField = "insertdate",
                                    FilterValue = dateInsert.ToString()
                                }
                            ];
                        }
                        page++;
                    }
                } while (page <= 1000);

                return Ok();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}
