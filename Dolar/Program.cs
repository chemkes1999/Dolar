using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using iTextSharp.text;
using iTextSharp.text.pdf;

class Program
{
    static async Task Main(string[] args)
    {
        string apiKey = "apikey";
        string baseCurrency = "USD";
        string apiUrl = $"https://v6.exchangerate-api.com/v6/{apiKey}/latest/{baseCurrency}";
        try
        {
                using (var httpClient = new HttpClient())
                {
                    HttpResponseMessage response = await httpClient.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseContent = await response.Content.ReadAsStringAsync();
                        // Aquí tienes la respuesta en formato JSON. Puedes procesarla según tus necesidades.
                        Console.WriteLine(responseContent);

                        // Guardar la respuesta como archivo PDF
                        string pdfFileName = "exchange_rates.pdf";
                        using (FileStream fs = new FileStream(pdfFileName, FileMode.Create))
                        {
                            Document document = new Document();
                            PdfWriter.GetInstance(document, fs);

                            document.Open();
                            document.Add(new Paragraph(responseContent));
                            document.Close();
                        }

                        Console.WriteLine($"Los datos se han guardado correctamente en {pdfFileName}.");
                    }
                    else
                    {
                        Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                    }
                }
        }
        catch (Exception ex)
        {
                Console.WriteLine($"Error: {ex.Message}");
        }
    }
}
