using System.Net.Http.Headers;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using RestSharp;

namespace ApiDockerDemo.Controllers;

[ApiController]
[Route("[controller]")]
public class HttpClientTestController : ControllerBase {
    private const string UrlString = "https://fedsu8jehud8u-lvbn2.doraclp.cn/test_loading";

    [HttpPost]
    public async Task<string> Post(int model) {
        using var httpClient = new HttpClient {
            BaseAddress = new Uri(UrlString)
        };
        var appJson = new MediaTypeWithQualityHeaderValue("application/json");
        httpClient.DefaultRequestHeaders.Accept.Add(appJson);
        var stringContent = model switch {
            0 => new StringContent(GetString(), appJson),
            1 => new StringContent(GetString(), Encoding.UTF8, "application/json"),
            2 => new StringContent(GetString(), null, "application/json"),
            3 => new StringContent(GetString(), Encoding.UTF8, appJson),
            4 => new StringContent(GetString(), null, appJson),
            _ => new StringContent(GetString()) {
                Headers = { ContentType = appJson }
            }
        };
        //streamContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        var responseMessage = await httpClient.PostAsync(UrlString,
            stringContent);
        if(responseMessage.IsSuccessStatusCode == false)
            throw new HttpRequestException(responseMessage.ReasonPhrase);

        var readAsStringAsync = await responseMessage.Content.ReadAsStringAsync();
        return readAsStringAsync;
    }

    [HttpPost]
    [Route("by-request")]
    public async Task<string> PostByRequest() {
        using var httpClient = new HttpClient {
            BaseAddress = new Uri(UrlString)
        };
        httpClient.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
        using var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, UrlString);
        httpRequestMessage.Content =
            new StringContent(GetString(), Encoding.UTF8, "application/json");
        httpRequestMessage.Content.Headers.ContentType =
            new MediaTypeWithQualityHeaderValue("application/json");
        var responseMessage = await httpClient.SendAsync(httpRequestMessage);
        if(responseMessage.IsSuccessStatusCode == false)
            throw new HttpRequestException(responseMessage.ReasonPhrase);

        var readAsStringAsync = await responseMessage.Content.ReadAsStringAsync();
        return readAsStringAsync;
    }

    [HttpPost]
    [Route("RestSharpTest")]
    public async Task<string?> RestSharpTest() {
        using var restClient = new RestClient();
        var request = new RestRequest(UrlString) { Method = Method.Post };
        //restClient.Options.UserAgent = "Apifox/1.0.0 (https://www.apifox.cn)";
        request.AddHeader("Content-Type", "application/json");
        request.AddParameter("application/json", GetString(), ParameterType.RequestBody);
        var response = await restClient.ExecuteAsync(request);
        return response.Content;
    }

    private static string GetString() {
        return """
            {
              "configuration": {
                "global_constraints": {
                  "container_weight_bearing": {
                    "enabled": false,
                    "max_weight_bearing": 30000
                  },
                  "light_on_heavy": {
                    "enabled": false,
                    "tolerance": 0
                  },
                  "load_by_sequence": {
                    "enabled": true,
                    "max_intersection_depth": 0
                  },
                  "min_support_ratio": {
                    "default_min_support_ratio": 0.8,
                    "enabled": false
                  },
                  "small_on_big": {
                    "enabled": false,
                    "tolerance": 0
                  }
                }
              },
              "containers": [
                {
                  "type": "shipping_container",
                  "value": {
                    "exterior_shape": [
                      4000,
                      600,
                      600
                    ],
                    "name": "容器1",
                    "uid": "8473ced3-eb9d-4a69-801d-70d36cdda6c5",
                    "loadable_space": {
                      "type": "sculpture_space",
                      "value": {
                        "interior_space": {
                          "dimension": [
                            4000,
                            600,
                            600
                          ],
                          "location": [
                            0,
                            0,
                            0
                          ]
                        },
                        "obstacles": []
                      }
                    }
                  }
                },
                {
                  "type": "shipping_container",
                  "value": {
                    "exterior_shape": [
                      5000,
                      1000,
                      1000
                    ],
                    "name": "容器2",
                    "uid": "936dcaf5-6a14-4a13-ac97-1bd52ac8babb",
                    "loadable_space": {
                      "type": "sculpture_space",
                      "value": {
                        "interior_space": {
                          "dimension": [
                            5000,
                            1000,
                            1000
                          ],
                          "location": [
                            0,
                            0,
                            0
                          ]
                        },
                        "obstacles": []
                      }
                    }
                  }
                },
                {
                  "type": "shipping_container",
                  "value": {
                    "exterior_shape": [
                      4000,
                      1500,
                      800
                    ],
                    "name": "容器3",
                    "uid": "0dda1ae9-084a-44ac-be7e-d00c23d8c52f",
                    "loadable_space": {
                      "type": "sculpture_space",
                      "value": {
                        "interior_space": {
                          "dimension": [
                            4000,
                            1500,
                            800
                          ],
                          "location": [
                            0,
                            0,
                            0
                          ]
                        },
                        "obstacles": []
                      }
                    }
                  }
                }
              ],
              "inventory_containers": [
                {
                  "quantity": 999,
                  "uid": "8473ced3-eb9d-4a69-801d-70d36cdda6c5"
                },
                {
                  "quantity": 999,
                  "uid": "936dcaf5-6a14-4a13-ac97-1bd52ac8babb"
                },
                {
                  "quantity": 999,
                  "uid": "0dda1ae9-084a-44ac-be7e-d00c23d8c52f"
                }
              ],
              "inventory_items": [
                {
                  "quantity": 50,
                  "uid": "ff2edee8-0f94-43b9-aa32-9ad0e99f5de3"
                },
                {
                  "quantity": 50,
                  "uid": "cfe5ac9c-74ab-4754-ba58-f1c00e88717b"
                },
                {
                  "quantity": 50,
                  "uid": "bb131013-6363-4073-8b85-d1b1d2b0f44c"
                }
              ],
              "items": [
                {
                  "type": "unit_item",
                  "value": {
                    "constraints": {
                      "allowed_orientations": [
                        0,
                        1,
                        2,
                        3,
                        4,
                        5
                      ]
                    },
                    "extended_properties": {
                      "weight": 1,
                      "sequence_number": 0
                    },
                    "exterior_shape": [
                      150,
                      180,
                      210
                    ],
                    "name": "item 0",
                    "uid": "ff2edee8-0f94-43b9-aa32-9ad0e99f5de3"
                  }
                },
                {
                  "type": "unit_item",
                  "value": {
                    "constraints": {
                      "allowed_orientations": [
                        0,
                        1,
                        2,
                        3,
                        4,
                        5
                      ]
                    },
                    "extended_properties": {
                      "weight": 1,
                      "sequence_number": 1
                    },
                    "exterior_shape": [
                      200,
                      120,
                      160
                    ],
                    "name": "item 1",
                    "uid": "cfe5ac9c-74ab-4754-ba58-f1c00e88717b"
                  }
                },
                {
                  "type": "unit_item",
                  "value": {
                    "constraints": {
                      "allowed_orientations": [
                        0,
                        1,
                        2,
                        3,
                        4,
                        5
                      ]
                    },
                    "extended_properties": {
                      "weight": 1,
                      "sequence_number": 2
                    },
                    "exterior_shape": [
                      300,
                      180,
                      110
                    ],
                    "name": "item 2",
                    "uid": "bb131013-6363-4073-8b85-d1b1d2b0f44c"
                  }
                }
              ]
            }
            """;
    }
}