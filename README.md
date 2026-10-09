Restore dependencies and apply migration
### Gemini API Configuration

The AI Assistant uses the Google Gemini API. To run the application locally, configure your Gemini API key using ASP.NET Core User Secrets.

**1. Initialize User Secrets**

From the solution root, run:

```bash
cd BulkyWeb
dotnet user-secrets init
```

**2. Add your Gemini API key**

```bash
dotnet user-secrets set "Gemini:ApiKey" "YOUR_GEMINI_API_KEY"
```

Replace `YOUR_GEMINI_API_KEY` with your actual API key.

**3. Run the application**

```bash
dotnet run
```

The application reads the key through ASP.NET Core configuration:

```csharp
string apiKey = configuration["Gemini:ApiKey"]
    ?? throw new InvalidOperationException(
        "Gemini API key is not configured.");
```

**Security notes**

* Never commit your actual API key to source control.
* Do not add API keys to `appsettings.json` or the README.
* User Secrets are intended for local development and are not encrypted. For production deployments, use a secure secret store, such as Azure Key Vault.
* Anyone running the application locally must configure their own Gemini API key.
