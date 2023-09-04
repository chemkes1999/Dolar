using iTextSharp.text;
using iTextSharp.text.pdf;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var apiKey = "apikey";
        var baseCurrency = "USD";
        var apiUrl = $"https://v6.exchangerate-api.com/v6/{apiKey}/latest/{baseCurrency}";

        try
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(responseContent);

                    var pdfFileName = "exchange_rates.pdf";

                    using (var fs = new FileStream(pdfFileName, FileMode.Create))
                    {
                        var document = new Document();
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