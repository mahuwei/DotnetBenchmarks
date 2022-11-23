using System.Text;

namespace ThrowHelperTest;

public class HttpClientTest {
    public static async Task PostTest() {
        using var httpClient = new HttpClient {
            BaseAddress = new Uri("https://fedsu8jehud8u-lvbn2.doraclp.cn"),
            MaxResponseContentBufferSize = 15724800
        };

        httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Accept-Encoding",
            new List<string?> { "gzip", " deflate", " br" });
        httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Strict-Transport-Security",
            "max-age=15724800; includeSubDomains");
        var content = new StringContent(GetString(), Encoding.UTF8, "application/json");
        var httpResponseMessage = await httpClient.PostAsync("/test_loading", content);
        if(httpResponseMessage.IsSuccessStatusCode == false){
            Console.WriteLine(httpResponseMessage.StatusCode);
            return;
        }

        var stream = await httpResponseMessage.Content.ReadAsStreamAsync();
        var streamLength = stream.Length;
        var buffers = new byte[streamLength + 100000];
        var readAsync = await stream.ReadAsync(buffers, 0, (int)streamLength);
        Console.ReadKey();
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