using Bulky.DataAccess.Repository.IRepository;
using BulkyWeb.Services.Tools;
using Google.GenAI;
using Google.GenAI.Types;
using System.Text.Json;

namespace BulkyWeb.Services
{
    public class GeminiService
    {
        private readonly Client _client;
        private readonly ProductTools _productTools;

        public GeminiService(
            IConfiguration configuration,
            ProductTools productTools)
        {
            string apiKey = configuration["Gemini:ApiKey"]
                ?? throw new InvalidOperationException(
                    "Gemini API key is not configured.");

            _client = new Client(apiKey: apiKey);
            _productTools = productTools;
        }

        public async Task<string> AskAsync(string question)
        {
            FunctionDeclaration getProductsByCategoryFunction =
                new FunctionDeclaration
                {
                    Name = "GetProductsByCategory",
                    Description = "Gets all products belonging to a product category.",
                    Parameters = new Schema
                    {
                        Type = Google.GenAI.Types.Type.Object,
                        Properties = new Dictionary<string, Schema>
                        {
                            ["categoryName"] = new Schema
                            {
                                Type = Google.GenAI.Types.Type.String,
                                Description = "The name of the category, for example SciFi."
                            }
                        },
                        Required = new List<string>
                        {
                            "categoryName"
                        }
                    }
                };

            GenerateContentConfig config = new GenerateContentConfig
            {
                Tools = new List<Tool>
                {
                    new Tool
                    {
                        FunctionDeclarations = new List<FunctionDeclaration>
                        {
                            getProductsByCategoryFunction
                        }
                    }
                }
            };

            List<Content> contents = new List<Content>
            {
                new Content
                {
                    Role = "user",
                    Parts = new List<Part>
                    {
                        new Part
                        {
                            Text = question
                        }
                    }
                }
            };

            while (true)
            {
                var response = await _client.Models.GenerateContentAsync(
                    model: "gemini-3.5-flash-lite",
                    contents: contents,
                    config: config);

                var parts = response.Candidates[0].Content.Parts;

                Part? functionCallPart = parts
                    .FirstOrDefault(p => p.FunctionCall != null);

                if (functionCallPart == null)
                {
                    return response.Text;
                }

                FunctionCall functionCall = functionCallPart.FunctionCall;

                if (functionCall.Name == "GetProductsByCategory")
                {
                    string categoryName =
                        functionCall.Args["categoryName"]?.ToString()
                        ?? "";

                    Console.WriteLine(
                        $"AI requested tool: GetProductsByCategory({categoryName})");

                    var result =
                        _productTools.GetProductsByCategory(categoryName);

                    string resultJson =
                        JsonSerializer.Serialize(result);

                    contents.Add(response.Candidates[0].Content);

                    contents.Add(new Content
                    {
                        Role = "user",
                        Parts = new List<Part>
                        {
                            new Part
                            {
                                FunctionResponse = new FunctionResponse
                                {
                                    Name = functionCall.Name,
                                    Response = new Dictionary<string, object>
                                    {
                                        ["result"] = resultJson
                                    }
                                }
                            }
                        }
                    });
                }
            }
        }
    }
}